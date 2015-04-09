#include "stdafx.h"

#include <iostream>
#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"

using namespace cv;
using namespace std;

Mat filterUsingHSV(Mat frame,int iLowH, int iHighH, int iLowS, int iHighS, int iLowV, int iHighV){
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

Mat drawRedFollowLine(Mat frame) {
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
		Mat imgThresholded2 = filterUsingHSV(imgOriginal, iLowH_red, iHighH_red, iLowS_red, iHighS_red, iLowV_red, iHighV_red);
		
		Mat imgResult = imgThresholded + imgThresholded2;

		imshow("Thresholded Image", imgResult); //show the thresholded image

		imgOriginal += drawRedFollowLine(imgResult);
		imshow("Original", imgOriginal); //show the original image

		if (waitKey(30) == 27) //wait for 'esc' key press for 30ms. If 'esc' key is pressed, break loop
		{
			cout << "esc key is pressed by user" << endl;
			break;
		}
	}

	return 0;
}