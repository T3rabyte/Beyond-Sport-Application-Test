using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public Slider slider;

    public List<GameObject> movableObjects;

    string[] splittedFrame;

    public bool simStarted = false;

    private void Update()
    {
        if (slider.value >= slider.maxValue) 
        {
            simStarted = false;
        }
    }

    void Start()
    {
        string path = Application.dataPath + "/Data/Applicant-test.dat"; 
        if (File.Exists(path))
        {
            string[] frames = File.ReadAllLines(path, Encoding.UTF8);
            FrameToModel(frames);
        }
    }

    public void FrameToModel(string[] frames)
    {
        foreach (string frame in frames)
        {
            splittedFrame = frame.Split(':', ';'); //splits the frame, player and bale values from each other
            splittedFrame = splittedFrame.Where(framePart => !string.IsNullOrWhiteSpace(framePart)).ToArray(); //delete blank rows
            SendFrameToPlayer(splittedFrame);
        }
        slider.maxValue = frames.Length -1;
    }

    public void SendFrameToPlayer(string[] frame)
    {
        for (int i = 1; i < frame.Length; i++) //sends the splitted string to the corresponding object
        {
            if (i - 1 < frame.Length - 2)
            {
                movableObjects[i - 1].GetComponent<Player>().SetFrameToList(frame[i]);
            }
            else
            {
                movableObjects[i - 1].GetComponent<Ball>().SetFrameToList(frame[i]);
            }
        }
    }
}
