#include <exception>
#include "tracker.hpp"

tracker::tracker(VideoCapture cap)
{
    this->cap = cap;
}

using namespace cv;
using namespace std;

Mat tracker::filterUsingHSV(Mat &frame, int iLowH, int iHighH, int iLowS, int iHighS, int iLowV, int iHighV){
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

vector<KeyPoint> tracker::trackBlob(Mat &im){
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

Mat tracker::drawPoints(Mat img, vector<KeyPointColor> keypointcolors){
    // Draw detected blobs as red circles.
    // DrawMatchesFlags::DRAW_RICH_KEYPOINTS flag ensures
    // the size of the circle corresponds to the size of blob

    if (!keypointcolors.empty()){
        for (KeyPointColor kpc : keypointcolors){
            int x = kpc.keypoint.pt.x;
            int y = kpc.keypoint.pt.y;

            char str[200];
            //std::cout << "X:" << x << " Y:" << y << endl;
            sprintf(str, "X: %d, Y: %d", x, y);
            Scalar color (kpc.color[0], kpc.color[1], kpc.color[2], 1.0);
            putText(img, str, Point(x + 30, y), FONT_HERSHEY_SIMPLEX, 0.7, color, 2);
        }
    }
    return img;
}

vector<KeyPointColor> tracker::getKeypointColors(Mat & img, const vector<KeyPoint> & keypoints){
    vector<KeyPointColor> keypointcolors;
    if (!keypoints.empty()){
        for (KeyPoint kp : keypoints){
            KeyPointColor k;
            k.color = img.at<Vec3b>(kp.pt);
            k.keypoint = kp;
            keypointcolors.push_back(k);
        }
    }
    return keypointcolors;
}

vector<KeyPointColor> tracker::trackObjects(){
    Mat imgOriginal;

    if (!cap.read(imgOriginal)) //if not success, break loop
    {
        throw std::runtime_error("Cannot read a frame from video stream");
    }

    Mat imgThresholded = filterUsingHSV(imgOriginal, iLowH, iHighH, iLowS, iHighS, iLowV, iHighV);
    auto points = trackBlob(imgThresholded);
    auto pointcolors = getKeypointColors(imgThresholded, points);
    
    if (debug) {
        imshow("Tracker", drawPoints(imgOriginal, pointcolors));
    }

    return pointcolors;
}

tracker::~tracker()
{
}
