#pragma once

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


class tracker
{
    public:
        tracker(VideoCapture cap);
        ~tracker();
        void trackObjects();
    private:
        VideoCapture cap;
        int iLowH = 0, iHighH = 179, iLowS = 85, iHighS = 255, iLowV = 108, iHighV = 255;
        vector<KeyPointColor> trackingResult;

        Mat filterUsingHSV(Mat&, int iLowH, int iHighH, int iLowS, int iHighS, int iLowV, int iHighV);
        vector<KeyPoint> trackBlob(Mat &);
        vector<KeyPointColor> getKeypointColors(Mat&, vector<KeyPoint>&);
        Mat drawPoints(Mat img, vector<KeyPointColor> keypointcolors);


};
