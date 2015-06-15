#pragma once

#include <vector>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/opencv.hpp>

/*
*	Struct used for storing keypoints (coordinates) and their corresponding color
*/
struct KeyPointColor{
    cv::KeyPoint keypoint;
    cv::Vec3b color;

    KeyPointColor(cv::KeyPoint keypoint, cv::Vec3b color)
        : keypoint(keypoint)
        , color(color)
    {}
};

/*
*	Defines an upper and lower value
*/
struct Bound {
    int lower;
    int upper;
};

class tracker
{
public:
	/*
	*	Constructor
	*	@param cap Input video capture
	*	@param debug Enables/disables debugging mode
	*/
    tracker(cv::VideoCapture cap, bool debug);
    ~tracker();

	/*
	*	Function to be called when objects have to be tracked
	*/
    std::vector<KeyPointColor> trackObjects();

	/*
	*	Function to be called when objects have to be tracked
	*	@param img_filename input filename
	*/
    std::vector<KeyPointColor> trackObjects(const std::string& img_filename); 

	/*
	*	Function to be called when objects have to be tracked
	*	@param img Input image
	*/
	std::vector<KeyPointColor> trackObjects(const cv::Mat& img);

    void show_debug_window(bool b) { debug = b; }
private:
	
    cv::VideoCapture cap;						//The capture device used for trackign objects
    bool debug;									//Boolean to enable debug mode
    Bound hue, saturation, value;				//HSV: Bounds in between the HSV filter will operate
    Bound area, threshold;
    int minCircularity, minConvexity,			//Blobtracking: Minimum Circularity, minimum Convexity, minimum Inertia, blobcolor
        minInertiaRatio, blobColor; 
    std::vector<KeyPointColor> trackingResult;	//The vector in which the result is stored
	cv::SimpleBlobDetector::Params params;		//Used for storing blob tracking parameters in an openCV way

	/*
	*	Will output an image based on the values set for HSV
	*	@param img Input image
	*	@return the new filtered image
	*/
    cv::Mat filterUsingHSV(const cv::Mat& img);

	/*
	*	Returns a keypoint vector based on the values set for the blobtracking
	*	@param img Input image
	*	@return Vector with blob coordinates
	*/
    std::vector<cv::KeyPoint> trackBlob(const cv::Mat& img);

	/*
	*	Will return keypointcolors based on an inpput image and input keypoints
	*	@param img Input img
	*	@param keypoints Input keypoints
	*	@return vector with keypointcolors
	*/
    std::vector<KeyPointColor> getKeypointColors(const cv::Mat& img, 
                                                     const std::vector<cv::KeyPoint>& keypoints);

	/*
	*	Used for drawing blobs and keypoints on an image
	*	@param Input image (on which the keypoints have to be drawn)
	*	@param keypointcolors vector with keypointcolors
	*/
    void drawPoints(cv::Mat& img, std::vector<KeyPointColor> keypointcolors);
     
};
