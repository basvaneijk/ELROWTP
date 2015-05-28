#include <exception>
#include "tracker.hpp"

tracker::tracker(VideoCapture cap, bool debug)
    : cap(cap)
    , debug(debug)
{
    cap.set(CV_CAP_PROP_SETTINGS, 1);
    cap.set(CV_CAP_PROP_EXPOSURE, -11);
    cap.set(CV_CAP_PROP_CONTRAST, 10);
    cap.set(CV_CAP_PROP_FOCUS, 0);

    hue = { 0, 179 };
    saturation = { 85, 255 };
    value = { 108, 255 };
    threshold = { 0, 12};

    minCircularity = 50;

    params.filterByColor = true;
    params.blobColor = blobColor = 255;
    params.minThreshold = threshold.lower;
    params.maxThreshold = threshold.upper;
    // Filter by Area.
    params.filterByArea = false;
    params.minArea = area.lower = 5;
    params.maxArea = area.upper = 20;
    // Filter by Circularity
    params.filterByCircularity = true;
    params.minCircularity = 0.5;
    // Filter by Convexity
    params.filterByConvexity = false;
    params.minConvexity = 0.80;
    // Filter by Inertia
    params.filterByInertia = true;
    params.minInertiaRatio = 0.01;

    if (debug) {
        //Create trackbars in "Control" window
        namedWindow("Control", CV_WINDOW_AUTOSIZE);

        //TRackbars for HSV
        createTrackbar("LowHue", "Control", &hue.lower, 179); //Hue (0 - 179)
        createTrackbar("HighHue", "Control", &hue.upper, 179);

        createTrackbar("LowSaturation", "Control", &saturation.lower, 255); //Saturation (0 - 255)
        createTrackbar("HighSaturation", "Control", &saturation.upper, 255);

        createTrackbar("LowValue", "Control", &value.lower, 255); //Value (0 - 255)
        createTrackbar("HighValue", "Control", &value.upper, 255);

        //Trackbars for blobs
        createTrackbar("minArea", "Control", &area.lower, 1400);
        createTrackbar("maxArea", "Control", &area.upper, 1400);
        createTrackbar("minCircularity", "Control", &minCircularity, 100);
        createTrackbar("minConvexity", "Control", &minConvexity, 100);
        createTrackbar("minInertiaRatio", "Control", &minInertiaRatio, 100);
        createTrackbar("blobColor", "Control", &blobColor, 255);
        createTrackbar("minThreshold", "Control", &threshold.lower, 255);
        createTrackbar("maxThreshold", "Control", &threshold.upper, 255);
    }
}

using namespace cv;
using namespace std;

Mat tracker::filterUsingHSV(const Mat& frame)
{
    Mat imgHSV;

    cvtColor(frame, imgHSV, COLOR_BGR2HSV); //Convert the captured frame from BGR to HSV

    Mat imgThresholded;

    inRange(imgHSV, Scalar(hue.lower, saturation.lower, value.lower), 
                    Scalar(hue.upper, saturation.upper, value.upper), 
            imgThresholded); //Threshold the image

    //morphological opening (removes small objects from the foreground)
    erode(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
    dilate(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));

    //morphological closing (removes small holes from the foreground)
    dilate(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
    erode(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));

    return imgThresholded;
}

vector<KeyPoint> tracker::trackBlob(const Mat& im)
{
    // Setup SimpleBlobDetector parameters.

    params.blobColor = blobColor;
    // Filter by Area.
    params.minArea = area.lower > 0 ? area.lower : 1;
    params.maxArea = area.upper > 0 ? area.upper : 2;
    // Filter by Circularity
    params.minCircularity = (float)minCircularity / 100;
    // Filter by Convexity
    params.minConvexity = (float)minConvexity / 100;
    // Filter by Inertia
    params.minInertiaRatio = (float)minInertiaRatio / 100;
    // Change thresholds
    params.minThreshold = (float)threshold.lower;
    params.maxThreshold = (float)threshold.upper;

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
    return keypoints;
}

void tracker::drawPoints(Mat& img, vector<KeyPointColor> keypointcolors)
{
    // Draw detected blobs as red circles.
    // DrawMatchesFlags::DRAW_RICH_KEYPOINTS flag ensures
    // the size of the circle corresponds to the size of blob

    for (KeyPointColor kpc : keypointcolors) {
        int x = kpc.keypoint.pt.x;
        int y = kpc.keypoint.pt.y;

        char str[200];
        //std::cout << "X:" << x << " Y:" << y << endl;
        snprintf(str, sizeof(str), "X: %d, Y: %d", x, y);
        Scalar color(kpc.color[0], kpc.color[1], kpc.color[2], 1.0);
        putText(img, str, Point(x + 30, y), FONT_HERSHEY_SIMPLEX, 0.7, color, 2);
    }
}

vector<KeyPointColor> tracker::getKeypointColors(const Mat& img,
    const vector<KeyPoint>& keypoints)
{
    vector<KeyPointColor> keypointcolors;
    if (!keypoints.empty()) {
        for (KeyPoint kp : keypoints) {
            KeyPointColor k;
            k.color = img.at<Vec3b>(kp.pt);
            k.keypoint = kp;
            keypointcolors.push_back(k);
        }
    }
    return keypointcolors;
}

vector<KeyPointColor> tracker::trackObjects()
{
    //Mat imgOriginal;

    //if (!cap.read(imgOriginal)) //if not success, break loop
    //{
    //    throw std::runtime_error("Cannot read a frame from video stream");
    //}
    Mat imgOriginal = imread("Capture.JPG");
    waitKey(100);

    Mat imgThresholded = filterUsingHSV(imgOriginal);

    auto points = trackBlob(imgThresholded);
    auto pointcolors = getKeypointColors(imgThresholded, points);

    if (debug) {
        //drawPoints(imgOriginal, pointcolors); 
        Mat imgDebug;
        drawKeypoints(imgOriginal, points, imgDebug, 
                      Scalar(0, 0, 255), DrawMatchesFlags::DRAW_RICH_KEYPOINTS);
        imshow("Result", imgDebug);
        imshow("HSV", imgThresholded);
        std::ostringstream keypoints_txt;
        keypoints_txt << "Num keypoints: " << points.size();
        displayStatusBar("Controls", keypoints_txt.str());
    }

    return pointcolors;
}

tracker::~tracker()
{
}
