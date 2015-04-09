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
	// Read image
	Mat im = imread("blob.jpg", IMREAD_GRAYSCALE);
	// Setup SimpleBlobDetector parameters.
	SimpleBlobDetector::Params params;
	// Change thresholds
	params.minThreshold = 10;
	params.maxThreshold = 200;
	// Filter by Area.
	params.filterByArea = true;
	params.minArea = 1500;
	// Filter by Circularity
	params.filterByCircularity = true;
	params.minCircularity = 0.1;
	// Filter by Convexity
	params.filterByConvexity = true;
	params.minConvexity = 0.87;
	// Filter by Inertia
	params.filterByInertia = true;
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
	// Draw detected blobs as red circles.
	// DrawMatchesFlags::DRAW_RICH_KEYPOINTS flag ensures
	// the size of the circle corresponds to the size of blob
	Mat im_with_keypoints;
	drawKeypoints(im, keypoints, im_with_keypoints, Scalar(0, 0, 255), DrawMatchesFlags::DRAW_RICH_KEYPOINTS);
	// Show blobs
	imshow("keypoints", im_with_keypoints);
	waitKey(0);
	//create GUI windows
	//destroy GUI windows
	destroyAllWindows();
	return EXIT_SUCCESS;
}