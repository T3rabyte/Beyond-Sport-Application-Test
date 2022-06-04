using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public List<BallData> frameList = new List<BallData>();
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
        BallData playerData = new BallData()
        {
            XPosition = Int32.Parse(splittedFrame[0]),
            YPosition = Int32.Parse(splittedFrame[1]),
            ZPosition = Int32.Parse(splittedFrame[2]),
            BallSpeed = Int32.Parse(splittedFrame[3]),
        };
        frameList.Add(playerData);
    }

    public void MoveToFramePos()
    {
        Debug.Log("Test");
    }
}
