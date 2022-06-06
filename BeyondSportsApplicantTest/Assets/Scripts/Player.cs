using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public List<PlayerData> frameList = new List<PlayerData>();
    public bool simStartedLocal = false;
    public int currentFrame = 1;
    public Vector3 vec = new Vector3();

    bool initialPos = false;

    public Slider slider;

    public void Start()
    {
        slider.onValueChanged.AddListener(delegate { SetSliderValueToLocal((int)slider.value); });
    }

    public void Update()
    {
        if (simStartedLocal)
        {
            transform.position = Vector3.MoveTowards(transform.position, vec, (Vector3.Distance(transform.position, vec) / 0.04f) * Time.deltaTime);
        }

        if (GameObject.Find("DataManager").GetComponent<DataManager>().simStarted && !simStartedLocal) //if the sim started but not locally it will start the Movement
        {
            simStartedLocal = true;
            InvokeRepeating("MoveToFramePos", 0f, 0.04f);
        }

        if (!GameObject.Find("DataManager").GetComponent<DataManager>().simStarted) //if the sim stoped it stops the sim locally
        {
            CancelInvoke();
            simStartedLocal = false;
        }

        if (!simStartedLocal && frameList.Count > 0 && !initialPos) //sets object to the first position in the list once the list has a entry
        {
            initialPos = true;
            transform.position = new Vector3(frameList[0].XPosition, 0.5f, frameList[0].ZPosition);
        }
    }

    public void SetFrameToList(string frame)
    {
        string[] splittedFrame = frame.Split(',');
        splittedFrame = splittedFrame.Where(framePart => !string.IsNullOrWhiteSpace(framePart)).ToArray(); //delete blank rows
        PlayerData playerData = new PlayerData()
        {
            Team = Int32.Parse(splittedFrame[0]) / 100,
            TrackingID = Int32.Parse(splittedFrame[1]) / 100,
            PlayerNumber = Int32.Parse(splittedFrame[2]) / 100,
            XPosition = float.Parse(splittedFrame[3]) / 100,
            ZPosition = float.Parse(splittedFrame[4]) / 100,
            PlayerSpeed = float.Parse(splittedFrame[5]),
        };
        frameList.Add(playerData);
    }

    public void MoveToFramePos() 
    {
        if (currentFrame < frameList.Count)
        {
            vec = new Vector3(frameList[currentFrame].XPosition, 0.5f, frameList[currentFrame].ZPosition);
            currentFrame++;
        }
        else 
        {
            CancelInvoke();
            simStartedLocal = false;
        }
    }

    public void SetSliderValueToLocal(int i) 
    {
        currentFrame = i;
        if (!simStartedLocal) 
        {
            transform.position = new Vector3(frameList[currentFrame].XPosition, 0.5f, frameList[currentFrame].ZPosition);
        }
    }
}
