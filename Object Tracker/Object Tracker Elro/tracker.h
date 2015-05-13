#pragma once

#include <iostream>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/opencv.hpp>

struct KeyPointColor{
	KeyPoint keypoint;
	Vec3b color;
};

using namespace cv;
using namespace std;

class tracker
{
public:
	tracker(VideoCapture cap);
	~tracker();

private:
	VideoCapture cap;
	int iLowH, iHighH, iLowS, iHighS, iLowV, iHighV;
	vector<KeyPointColor> trackingResult;

	Mat filterUsingHSV(Mat&, int iLowH, int iHighH, int iLowS, int iHighS, int iLowV, int iHighV);
	vector<KeyPoint> trackBlob(Mat &);	
	vector<KeyPointColor> getKeypointColors(Mat&, vector<KeyPoint>&);
	Mat drawPoints(Mat img, vector<KeyPointColor> keypointcolors);

	void trackObjects();



	
};

