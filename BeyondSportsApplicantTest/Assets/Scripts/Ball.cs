using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public List<BallData> frameList = new List<BallData>();

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
        Debug.Log(frameList.Count);
    }
}
