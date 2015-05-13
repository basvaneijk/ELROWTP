#include "stdafx.h"

#include <iostream>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/opencv.hpp>

using namespace cv;
using namespace std;

struct KeyPointColor{
	KeyPoint keypoint;
	Vec3b color;
};

Mat filterUsingHSV(Mat &frame, int iLowH, int iHighH, int iLowS, int iHighS, int iLowV, int iHighV){
	Mat imgHSV;

	cvtColor(frame, imgHSV, COLOR_BGR2HSV); //Convert the captured frame from BGR to HSV

	Mat imgThresholded;

	inRange(imgHSV, Scalar(iLowH, iLowS, iLowV), Scalar(iHighH, iHighS, iHighV), imgThresholded); //Threshold the image

	//morphological opening (removes small objects from the foreground)
	erode(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
	dilate(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));

	//morphological closing (removes small holes from the foreground)
	dilate(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
	erode(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));


	return imgThresholded;
}

Mat drawRedFollowLine(Mat &frame) {
	int iLastX = -1;
	int iLastY = -1;
	//Calculate the moments of the thresholded image
	Moments oMoments = moments(frame);
	Mat imgLines = Mat::zeros(frame.size(), CV_8UC3);;

	double dM01 = oMoments.m01;
	double dM10 = oMoments.m10;
	double dArea = oMoments.m00;

	// if the area <= 10000, I consider that the there are no object in the image and it's because of the noise, the area is not zero 
	if (dArea > 10000)
	{
		//calculate the position of the ball
		int posX = dM10 / dArea;
		int posY = dM01 / dArea;

		if (iLastX >= 0 && iLastY >= 0 && posX >= 0 && posY >= 0)
		{
			//Draw a red line from the previous point to the current point
			line(imgLines, Point(posX, posY), Point(iLastX, iLastY), Scalar(0, 0, 255), 2);
		}

		iLastX = posX;
		iLastY = posY;
	}
	return imgLines;
}
vector<KeyPoint> blobTrack(Mat &im){
	// Setup SimpleBlobDetector parameters.
	SimpleBlobDetector::Params params;
	// Change thresholds
	//params.minThreshold = 254;
	//params.maxThreshold = 255;
	//filter by color
	params.filterByColor = true;
	params.blobColor = 255;
	// Filter by Area.
	params.filterByArea = true;
	params.minArea = 5;
	// Filter by Circularity
	params.filterByCircularity = true;
	params.minCircularity = 0.5;
	// Filter by Convexity
	params.filterByConvexity = true;
	params.minConvexity = 0.80;
	// Filter by Inertia
	params.filterByInertia = false;
	params.minInertiaRatio = 0.01;
	// Storage for blobs
	vector<KeyPoint> keypoints;
#if CV_MAJOR_VERSION < 3 // If you are using OpenCV 2
	// Set up detector with params
	SimpleBlobDetector detector(params);
	// Detect blobs
	detector.detect(im, keypoints);
#else
	// Set up detector with params
	Ptr<SimpleBlobDetector> detector = SimpleBlobDetector::create(params);
	// Detect blobs
	detector->detect(im, keypoints);
#endif

	//
	return keypoints;
}
Mat drawPoints(Mat img, vector<KeyPointColor> keypointcolors){
	// Draw detected blobs as red circles.
	// DrawMatchesFlags::DRAW_RICH_KEYPOINTS flag ensures
	// the size of the circle corresponds to the size of blob

	if (!keypointcolors.empty()){
		for each(KeyPointColor kpc in keypointcolors){
			int x = kpc.keypoint.pt.x;
			int y = kpc.keypoint.pt.y;

			char str[200];
			//std::cout << "X:" << x << " Y:" << y << endl;
			sprintf(str, "X: %d, Y: %d", x, y);
			Scalar color = kpc.color;
			putText(img, str, Point(x + 30, y), FONT_HERSHEY_SIMPLEX, 0.7, color, 2);
		}
	}
	return img;
}
vector<KeyPointColor> getKeypointColors(Mat & img, vector<KeyPoint> & keypoints){
	vector<KeyPointColor> keypointcolors;
	if (!keypoints.empty()){
		for each(KeyPoint kp in keypoints){
			KeyPointColor k;
			k.color = img.at<Vec3b>(kp.pt);
			k.keypoint = kp;
			keypointcolors.push_back(k);
		}
	}
	return keypointcolors;
}
int main(int argc, char** argv)
{
	VideoCapture cap(0); //capture the video from webcam
	cap.set(CV_CAP_PROP_SETTINGS, 1);
	cap.set(CV_CAP_PROP_EXPOSURE, -11);
	cap.set(CV_CAP_PROP_CONTRAST, 10);
	cap.set(CV_CAP_PROP_FOCUS, 0);
	//cap.set(CV_CAP_PROP_FRAME_HEIGHT, 720); //Does not seem to work yet..?
	//cap.set(CV_CAP_PROP_FRAME_WIDTH, 1280);
	

	if (!cap.isOpened())  // if not success, exit program
	{
		cout << "Cannot open the web cam" << endl;
		return -1;
	}

	namedWindow("Control", CV_WINDOW_AUTOSIZE); //create a window called "Control"

	//RED
	int iLowH_red = 170;
	int iHighH_red = 179;

	int iLowS_red = 150;
	int iHighS_red = 255;

	int iLowV_red = 60;
	int iHighV_red = 255;

	//ALL LIGHTS WITH HIGH SATURATION
	int iLowH = 0;
	int iHighH = 179;

	int iLowS = 85;
	int iHighS = 255;

	int iLowV = 108;
	int iHighV = 255;

	//Create trackbars in "Control" window
	createTrackbar("LowH", "Control", &iLowH, 179); //Hue (0 - 179)
	createTrackbar("HighH", "Control", &iHighH, 179);

	createTrackbar("LowS", "Control", &iLowS, 255); //Saturation (0 - 255)
	createTrackbar("HighS", "Control", &iHighS, 255);

	createTrackbar("LowV", "Control", &iLowV, 255);//Value (0 - 255)
	createTrackbar("HighV", "Control", &iHighV, 255);
	//Capture a temporary image from the camera
	Mat imgTmp;
	cap.read(imgTmp);

	while (true)
	{
		Mat imgOriginal;

		bool bSuccess = cap.read(imgOriginal); // read a new frame from video

		if (!bSuccess) //if not success, break loop
		{
			cout << "Cannot read a frame from video stream" << endl;
			break;
		}

		Mat imgThresholded = filterUsingHSV(imgOriginal, iLowH, iHighH, iLowS, iHighS, iLowV, iHighV);
		vector<KeyPoint> points = blobTrack(imgThresholded);
		vector<KeyPointColor> pointsWithColor = getKeypointColors(imgOriginal, points);
		Mat result = drawPoints(imgOriginal, pointsWithColor);
		imshow("Thresholded Image", result); //show the thresholded image



		if (waitKey(30) == 27) //wait for 'esc' key press for 30ms. If 'esc' key is pressed, break loop
		{
			cout << "esc key is pressed by user" << endl;
			break;
		}
	}

	return 0;
}

