#pragma once

#include <vector>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/opencv.hpp>

struct KeyPointColor{
    cv::KeyPoint keypoint;
    cv::Vec3b color;

    KeyPointColor(cv::KeyPoint keypoint, cv::Vec3b color)
        : keypoint(keypoint)
        , color(color)
    {}
};

struct Bound {
    int lower;
    int upper;
};

class tracker
{
public:
    tracker(cv::VideoCapture cap, bool debug);
    ~tracker();
    std::vector<KeyPointColor> trackObjects();
    std::vector<KeyPointColor> trackObjects(const std::string& img_filename); 
    void show_debug_window(bool b) { debug = b; }
private:
    cv::VideoCapture cap;
    bool debug;
    Bound hue, saturation, value;
    Bound area, threshold;
    int minCircularity, minConvexity, 
        minInertiaRatio, blobColor; 

    std::vector<KeyPointColor> trackingResult;

    cv::Mat filterUsingHSV(const cv::Mat&);
    std::vector<cv::KeyPoint> trackBlob(const cv::Mat&);
    std::vector<KeyPointColor> getKeypointColors(const cv::Mat&, 
                                                     const std::vector<cv::KeyPoint>&);
    void drawPoints(cv::Mat& img, std::vector<KeyPointColor> keypointcolors);
    std::vector<KeyPointColor> trackObjects(const cv::Mat& img);

    cv::SimpleBlobDetector::Params params;
};
