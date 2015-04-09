#include "stdafx.h"

#include <iostream>
#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"

using namespace cv;
using namespace std;

int main(int argc, char** argv)
{
	VideoCapture cap(1); //capture the video from webcam
	cap.set(CV_CAP_PROP_SETTINGS, 1);
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

	//BLUE
	int iLowH = 102;
	int iHighH = 120;

	int iLowS = 168;
	int iHighS = 255;

	int iLowV = 103;
	int iHighV = 255;

	//Create trackbars in "Control" window
	/*createTrackbar("LowH", "Control", &iLowH, 179); //Hue (0 - 179)
	createTrackbar("HighH", "Control", &iHighH, 179);

	createTrackbar("LowS", "Control", &iLowS, 255); //Saturation (0 - 255)
	createTrackbar("HighS", "Control", &iHighS, 255);

	createTrackbar("LowV", "Control", &iLowV, 255);//Value (0 - 255)
	createTrackbar("HighV", "Control", &iHighV, 255);*/

	int iLastX = -1;
	int iLastY = -1;

	int iLastX_red = -1;
	int iLastY_red = -1;

	//Capture a temporary image from the camera
	Mat imgTmp;
	cap.read(imgTmp);

	//Create a black image with the size as the camera output
	Mat imgLines = Mat::zeros(imgTmp.size(), CV_8UC3);;


	while (true)
	{
		Mat imgOriginal;

		bool bSuccess = cap.read(imgOriginal); // read a new frame from video



		if (!bSuccess) //if not success, break loop
		{
			cout << "Cannot read a frame from video stream" << endl;
			break;
		}

		Mat imgHSV;

		cvtColor(imgOriginal, imgHSV, COLOR_BGR2HSV); //Convert the captured frame from BGR to HSV

		Mat imgThresholded;
		Mat imgThresholded_red;

		inRange(imgHSV, Scalar(iLowH, iLowS, iLowV), Scalar(iHighH, iHighS, iHighV), imgThresholded); //Threshold the image
		inRange(imgHSV, Scalar(iLowH_red, iLowS_red, iLowV_red), Scalar(iHighH_red, iHighS_red, iHighV_red), imgThresholded_red); //Threshold the image RED

		//morphological opening (removes small objects from the foreground)
		erode(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
		dilate(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));

		//morphological opening (removes small objects from the foreground) RED
		erode(imgThresholded_red, imgThresholded_red, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
		dilate(imgThresholded_red, imgThresholded_red, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));

		//morphological closing (removes small holes from the foreground)
		dilate(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
		erode(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));

		//morphological closing (removes small holes from the foreground) RED
		dilate(imgThresholded_red, imgThresholded_red, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
		erode(imgThresholded_red, imgThresholded_red, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));

		//Calculate the moments of the thresholded image
		Moments oMoments = moments(imgThresholded);

		//Calculate the moments of the thresholded image RED
		Moments oMoments_red = moments(imgThresholded_red);

		double dM01 = oMoments.m01;
		double dM10 = oMoments.m10;
		double dArea = oMoments.m00;

		//RED
		double dM01_red = oMoments_red.m01;
		double dM10_red = oMoments_red.m10;
		double dArea_red = oMoments_red.m00;

		// if the area <= 10000, I consider that the there are no object in the image and it's because of the noise, the area is not zero 
		if (dArea > 10000)
		{
			//calculate the position of the ball
			int posX = dM10 / dArea;
			int posY = dM01 / dArea;

			int posX_red = dM10_red / dArea_red;
			int posY_red = dM01_red / dArea_red;

			if ((iLastX >= 0 && iLastY >= 0 && posX >= 0 && posY >= 0) && (iLastX_red >= 0 && iLastY_red >= 0 && posX_red >= 0 && posY_red >= 0))
			{
				//Draw a red line from the previous point to the current point
				line(imgLines, Point(posX, posY), Point(iLastX, iLastY), Scalar(0, 0, 255), 2);
				//RED
				line(imgLines, Point(posX_red, posY_red), Point(iLastX_red, iLastY_red), Scalar(255, 0, 255), 2);
			}

			iLastX = posX;
			iLastY = posY;

			iLastX_red = posX_red;
			iLastY_red = posY_red;
		}
		Mat result = imgThresholded + imgThresholded_red;
		imshow("Thresholded Image", result); //show the thresholded image

		imgOriginal = imgOriginal + imgLines;
		imshow("Original", imgOriginal); //show the original image

		if (waitKey(30) == 27) //wait for 'esc' key press for 30ms. If 'esc' key is pressed, break loop
		{
			cout << "esc key is pressed by user" << endl;
			break;
		}
	}

	return 0;
}