/*
    Positiontracker & server for ELRO Wants To Play
    Copyright (C) 2015 Niek Arends
    Copyright (C) 2015 Olaf van der Kruk
    Copyright (C) 2015 Simon Voordouw

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

#include <exception>
#include <cstdio>
#include <vector>
#include "tracker.hpp"

using namespace cv;

tracker::tracker(VideoCapture cap, bool debug)
    : cap(cap)
    , debug(debug)
{
    cap.set(CV_CAP_PROP_SETTINGS, 1);			//Opens the context window associated with the capture device
    cap.set(CV_CAP_PROP_EXPOSURE, -11);			//Sets the exposure
    cap.set(CV_CAP_PROP_CONTRAST, 10);			//Sets the contrast
    cap.set(CV_CAP_PROP_FOCUS, 0);				//Disables autofocus

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
    params.filterByArea = true;
    params.minArea = area.lower = 5;
    params.maxArea = area.upper = 8000;
    // Filter by Circularity
    params.filterByCircularity = true;
    params.minCircularity = 0.5;
    // Filter by Convexity
    params.filterByConvexity = false;
    params.minConvexity = 0.80;
    // Filter by Inertia
    params.filterByInertia = false;
    params.minInertiaRatio = 0.01;

	//If debug mode is enabled a bunch of trackbars will be created
    if (debug) {
       
        namedWindow("Control", CV_WINDOW_AUTOSIZE);

        //Trackbars for HSV
        createTrackbar("LowHue", "Control", &hue.lower, 179); //Hue (0 - 179)
        createTrackbar("HighHue", "Control", &hue.upper, 179);

        createTrackbar("LowSaturation", "Control", &saturation.lower, 255); //Saturation (0 - 255)
        createTrackbar("HighSaturation", "Control", &saturation.upper, 255);

        createTrackbar("LowValue", "Control", &value.lower, 255); //Value (0 - 255)
        createTrackbar("HighValue", "Control", &value.upper, 255);

        //Trackbars for blobs
        createTrackbar("minArea", "Control", &area.lower, 10000);
        createTrackbar("maxArea", "Control", &area.upper, 10000);
        createTrackbar("minCircularity", "Control", &minCircularity, 100);
        createTrackbar("minConvexity", "Control", &minConvexity, 100);
        createTrackbar("minInertiaRatio", "Control", &minInertiaRatio, 100);
        createTrackbar("blobColor", "Control", &blobColor, 255);
        createTrackbar("minThreshold", "Control", &threshold.lower, 255);
        createTrackbar("maxThreshold", "Control", &threshold.upper, 255);
    }
}


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

std::vector<KeyPoint> tracker::trackBlob(const Mat& im)
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
    std::vector<KeyPoint> keypoints;
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

void tracker::drawPoints(Mat& img, std::vector<KeyPointColor> keypointcolors)
{
    // Draw detected blobs as red circles.
    // DrawMatchesFlags::DRAW_RICH_KEYPOINTS flag ensures
    // the size of the circle corresponds to the size of blob

    for (KeyPointColor kpc : keypointcolors) {
        int x = kpc.keypoint.pt.x;
        int y = kpc.keypoint.pt.y;

        char str[200];
        //std::cout << "X:" << x << " Y:" << y << endl;
        std::sprintf(str, "X: %d, Y: %d", x, y);
        Scalar color(kpc.color[0], kpc.color[1], kpc.color[2], 1.0);
        putText(img, str, Point(x + 30, y), FONT_HERSHEY_SIMPLEX, 0.7, color, 2);
    }
}

std::vector<KeyPointColor> tracker::getKeypointColors(const Mat& img,
    const std::vector<KeyPoint>& keypoints)
{
    std::vector<KeyPointColor> keypointcolors;
    for (KeyPoint kp : keypoints) {
        keypointcolors.emplace_back(kp, img.at<Vec3b>(kp.pt));
    }
    return keypointcolors;
}

std::vector<KeyPointColor> tracker::trackObjects()
{    
    Mat imgCam;

    if (!cap.read(imgCam))
    {
        throw std::runtime_error("Cannot read a frame from video stream");
    }
    waitKey(100);

    return trackObjects(imgCam);
}

std::vector<KeyPointColor> tracker::trackObjects(const std::string& img_filename)
{
    Mat img = imread(img_filename.c_str());
    waitKey(100);
    return trackObjects(img);
}

std::vector<KeyPointColor> normalizeKeyPoints(const std::vector<KeyPointColor>& keypoints, int width, int height)
{
    std::vector<KeyPointColor> norm_keypoints(keypoints);
    for (auto & kp : norm_keypoints) {
        kp.keypoint.pt.x - width;
        kp.keypoint.pt.y - height;
    }
    return norm_keypoints;
}

std::vector<KeyPointColor> tracker::trackObjects(const Mat& imgOriginal)
{
    Mat imgThresholded = filterUsingHSV(imgOriginal);

    std::vector<KeyPoint> points = trackBlob(imgThresholded);
    std::vector<KeyPointColor> pointcolors = getKeypointColors(imgOriginal , points);

    if (debug) {
        Mat imgDebug;
        drawKeypoints(imgOriginal, points, imgDebug, 
                      Scalar(0, 0, 255), DrawMatchesFlags::DRAW_RICH_KEYPOINTS);
        drawPoints(imgDebug, pointcolors); 
        imshow("Result", imgDebug);
        imshow("HSV", imgThresholded);
    }

    return pointcolors;
}

tracker::~tracker()
{
}
