using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<PlayerData> frameList = new List<PlayerData>();

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
}
