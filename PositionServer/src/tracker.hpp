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

struct Bound {
    int lower;
    int upper;
};

class tracker
{
public:
    tracker(VideoCapture cap, bool debug);
    ~tracker();
    vector<KeyPointColor> trackObjects();
    vector<KeyPointColor> trackObjects(const std::string& img_filename); 
    void show_debug_window(bool b) { debug = b; }
private:
    VideoCapture cap;
    bool debug;
    Bound hue, saturation, value;
    Bound area, threshold;
    int minCircularity, minConvexity, 
        minInertiaRatio, blobColor; 

    vector<KeyPointColor> trackingResult;

    Mat filterUsingHSV(const Mat&);
    vector<KeyPoint> trackBlob(const Mat&);
    vector<KeyPointColor> getKeypointColors(const Mat&, const vector<KeyPoint>&);
    void drawPoints(Mat& img, vector<KeyPointColor> keypointcolors);
    vector<KeyPointColor> trackObjects(const Mat& img);

    SimpleBlobDetector::Params params;

};
