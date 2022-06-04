using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<GameObject> movableObjects = new List<GameObject>();

    public string[] splittedFrame;

    public bool simStarted = false;

    void Start()
    {
        string path = Application.dataPath + "/Data/Applicant-test.dat";
        if (File.Exists(path))
        {
            string[] frames = File.ReadAllLines(path, Encoding.UTF8);
            FrameToModel(frames);
        }
    }

    public void StartSimulation() 
    {
        simStarted = true;
    }

    public void StopSimulation()
    {
        simStarted = false;
    }

    public void FrameToModel(string[] frames)
    {
        foreach (string frame in frames)
        {
            splittedFrame = frame.Split(':', ';');
            splittedFrame = splittedFrame.Where(framePart => !string.IsNullOrWhiteSpace(framePart)).ToArray(); //delete blank rows
            SendFrameToPlayer(splittedFrame);
        }
    }

    public void SendFrameToPlayer(string[] frame)
    {
        for (int i = 1; i < frame.Length; i++)
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
