// Background filtering.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

//opencv
#include <opencv2/core/core.hpp>
#include <opencv2/opencv.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/video/background_segm.hpp>
//C
#include <stdio.h>
//C++
#include <iostream>
#include <sstream>

using namespace cv;
using namespace std;

int _tmain(int argc, _TCHAR* argv[]){

	namedWindow("keypoints");
	namedWindow("Control", CV_WINDOW_AUTOSIZE);
	// Read image
	//Mat in = imread("blob.jpg", IMREAD_GRAYSCALE);
	Mat im = imread("orgafbeelding.jpg", IMREAD_GRAYSCALE);

	//invert(, output);
	//Mat im;
	//bitwise_not(in, im);
	// Setup SimpleBlobDetector parameters.
	int minArea = 30, minCircularity = 30, minConvexity = 30, minInertiaRatio = 30, blobColor = 30, minThreshold = 0, maxThreshold = 255;

	createTrackbar("minArea", "Control", &minArea, 1400);
	createTrackbar("minCircularity", "Control", &minCircularity, 100);
	createTrackbar("minConvexity", "Control", &minConvexity, 100);
	createTrackbar("minInertiaRatio", "Control", &minInertiaRatio, 100);
	createTrackbar("blobColor", "Control", &blobColor, 255);
	createTrackbar("minThreshold", "Control", &minThreshold, 255);
	createTrackbar("maxThreshold", "Control", &maxThreshold, 255);

	while (true)
	{

		SimpleBlobDetector::Params params;
		// Change thresholds
		//params.minThreshold = minThreshold;
		//params.maxThreshold = maxThreshold;

		params.filterByColor = true;
		params.blobColor = blobColor;
		// Filter by Area.
		params.filterByArea = true;
		params.minArea = minArea > 0 ? minArea : 1;
		// Filter by Circularity
		params.filterByCircularity = true;
		params.minCircularity = (float)minCircularity / 100;
		// Filter by Convexity
		params.filterByConvexity = true;
		params.minConvexity = (float)minConvexity / 100;
		// Filter by Inertia
		params.filterByInertia = true;
		params.minInertiaRatio = (float)minInertiaRatio / 100;
		// Storage for blobs
		vector<KeyPoint> keypoints;
		// Set up detector with params
		SimpleBlobDetector detector(params);
		// Detect blobs
		detector.detect(im, keypoints);

		// Draw detected blobs as red circles.
		// DrawMatchesFlags::DRAW_RICH_KEYPOINTS flag ensures
		// the size of the circle corresponds to the size of blob
		Mat im_with_keypoints;
		drawKeypoints(im, keypoints, im_with_keypoints, Scalar(0, 0, 255), DrawMatchesFlags::DRAW_RICH_KEYPOINTS);
		// Show blobs
		imshow("keypoints", im_with_keypoints);

		if (waitKey(30) == 27) //wait for 'esc' key press for 30ms. If 'esc' key is pressed, break loop
		{
			cout << "esc key is pressed by user" << endl;
			break;
		}
	}
	//create GUI windows
	//destroy GUI windows
	destroyAllWindows();
	return EXIT_SUCCESS;
}