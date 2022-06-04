using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<PlayerData> frameList = new List<PlayerData>();
    public bool simStartedLocal = false;

    public void Update()
    {
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
    }

    public void SetFrameToList(string frame)
    {
        string[] splittedFrame = frame.Split(',');
        splittedFrame = splittedFrame.Where(framePart => !string.IsNullOrWhiteSpace(framePart)).ToArray(); //delete blank rows
        PlayerData playerData = new PlayerData()
        {
            Team = Int32.Parse(splittedFrame[0]),
            TrackingID = Int32.Parse(splittedFrame[1]),
            PlayerNumber = Int32.Parse(splittedFrame[2]),
            XPosition = Int32.Parse(splittedFrame[3]),
            YPosition = Int32.Parse(splittedFrame[4]),
            PlayerSpeed = Int32.Parse(splittedFrame[5]),
        };
        frameList.Add(playerData);
    }

    public void MoveToFramePos() 
    {
        Debug.Log("Test");
    }
}
