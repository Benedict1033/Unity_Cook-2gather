using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class NYImageTrackerManager : MonoBehaviour
{
    public XRReferenceImageLibrary targetLib;
    public Transform[] trackerObjs;
    TrackingState[] trackerStates;

    ARTrackedImageManager _foundationTrackerManager;
    Dictionary<string, int> trackerImageNameDict;

    int _imgIndex = 0;
    //public Text DebugText;
    // setting tracked image limit
    Dictionary<int, NYImageTracker> trackedImgs = new Dictionary<int, NYImageTracker>();
    int maxTrackImgCount = 0;
    
    public static string Image_Status;

    public Text Tips_Text;
    public Text Next_Actioon;




    void Start()
    {
        // setup reference
        trackerStates = new TrackingState[targetLib.count];
        trackerImageNameDict = new Dictionary<string, int>();
        for(int i=0; i< trackerObjs.Length; i++)
        {
            trackerImageNameDict.Add(trackerObjs[i].name, i);
            trackerStates[i] = TrackingState.None;
        }


        // init foundation ar image manager
        _foundationTrackerManager = GameObject.FindObjectOfType<ARTrackedImageManager>();
        maxTrackImgCount = _foundationTrackerManager.currentMaxNumberOfMovingImages;

        Debug.Log("Get Manager: " + _foundationTrackerManager.gameObject.name);
        //_foundationTrackerManager = gameObject.AddComponent<ARTrackedImageManager>();
        _foundationTrackerManager.enabled = false;
        _foundationTrackerManager.referenceLibrary = targetLib;
        _foundationTrackerManager.enabled = true;

        Debug.Log("Start AR Manager");
    }


    // Update is called once per frame
    void Update()
    {
        UpdateTrackerStatus();

        //if (debugText != null)
        //{
            LogTrackerStatus();
        //}

        if (Image_Status == "Track 01") {
            Tips_Text.text = "Hit The Meat";
            Tips_Text.gameObject.SetActive(true);
            Next_Actioon.gameObject.SetActive(false);
        }
        else if (Image_Status == "Track 02"){ 
            Tips_Text.text = "Swipe Right or Left To Make Peel Well ";
            Tips_Text.gameObject.SetActive(true);
            Next_Actioon.gameObject.SetActive(false);
        }
        else if (Image_Status == "Track 03")
        {
            Tips_Text.text = "Drag Circle Up and Down To Slice Potato";
            Tips_Text.gameObject.SetActive(true);
            Next_Actioon.gameObject.SetActive(false);
        }
        else if (Image_Status == "Track 04")
        {
            Tips_Text.text = "Drag Up Meat To Make It Cooked";
            Tips_Text.gameObject.SetActive(true);
            Next_Actioon.gameObject.SetActive(false);

        }
        else if (Image_Status == "Track 05")
        {
            Tips_Text.text = "Drag Up Fryer To It Fried";
            Tips_Text.gameObject.SetActive(true);
            Next_Actioon.gameObject.SetActive(false);

        }
    }

    void UpdateTrackerStatus ()
    {
        foreach (ARTrackedImage _arImg in _foundationTrackerManager.trackables)
        {
            _imgIndex = trackerImageNameDict[_arImg.referenceImage.name];

            

            // state has change!
            if (_arImg.trackingState != trackerStates[_imgIndex])
            {
                trackerStates[_imgIndex] = _arImg.trackingState;

                if (_arImg.trackingState == TrackingState.Tracking)
                {
                    if (maxTrackImgCount > 0)
                    {
                        if (trackedImgs.Count < maxTrackImgCount)
                        {
                            trackedImgs.Add(_imgIndex,trackerObjs[_imgIndex].GetComponent<NYImageTracker>());
                            trackerObjs[_imgIndex].GetComponent<NYImageTracker>().OnTrackingFound();
                        }
                    }
                    else
                    {
                        trackerObjs[_imgIndex].GetComponent<NYImageTracker>().OnTrackingFound();
                    }
                }
                else if (_arImg.trackingState == TrackingState.Limited)
                {
                    if(maxTrackImgCount > 0)
                    {
                        trackedImgs.Remove(_imgIndex);
                    }

                    trackerObjs[_imgIndex].GetComponent<NYImageTracker>().OnTrackingLost();
                }
                else if (_arImg.trackingState == TrackingState.None)
                {
                    // error?
                    Debug.LogError("Object [" + _arImg.referenceImage.name + "] become NONE");
                }
            }

            // update data if tracked
            if (_arImg.trackingState == TrackingState.Tracking)
            {
                if(maxTrackImgCount > 0)
                {
                    if (trackedImgs.ContainsKey(_imgIndex))
                        UpdateTargetObjStatus(_arImg, _imgIndex);
                }
                else
                {
                    UpdateTargetObjStatus(_arImg, _imgIndex);
                }
                
            }
        }
    }

    void UpdateTargetObjStatus(ARTrackedImage targetImg, int targetIndex)
    {
        Transform targetObj = trackerObjs[targetIndex];

        //targetObj.position = targetImg.transform.position;
        //targetObj.rotation = targetImg.transform.rotation;
        //targetObj.transform.localScale = new Vector3(targetImg.extents.x, 1.0f, targetImg.extents.y);

        targetObj.GetComponent<NYImageTracker>().UpdateTransform(targetImg.transform.position, targetImg.transform.rotation);
    }

    void LogTrackerStatus ()
    {
 

        string debugMessage = "";
        debugMessage += "trackers: " + _foundationTrackerManager.trackables.count + "\n";

        foreach (ARTrackedImage _arImg in _foundationTrackerManager.trackables)
        {
            debugMessage += _arImg.name + "\n";
            //debugMessage += "[" + _arImg.referenceImage.name + "]: " + _arImg.trackingState.ToString() + "\n";
            //debugMessage += "extends: " + _arImg.extents.x + "  " + _arImg.extents.y + "\n";

            //_arImg.trackingState.ToString() +

            //DebugText.text = _arImg.referenceImage.name;
            Image_Status = _arImg.referenceImage.name.ToString();
        }

        //debugText.text = debugMessage;
    }

}
