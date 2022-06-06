using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public List<BallData> frameList = new List<BallData>();
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
            slider.value = currentFrame;
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
            transform.position = new Vector3(frameList[0].XPosition, frameList[0].YPosition, frameList[0].ZPosition);
        }
    }
    public void SetFrameToList(string frame)
    {
        string[] splittedFrame = frame.Split(',');
        splittedFrame = splittedFrame.Where(framePart => !string.IsNullOrWhiteSpace(framePart)).ToArray(); //delete blank rows
        BallData playerData = new BallData()
        {
            XPosition = float.Parse(splittedFrame[0]) / 100,
            ZPosition = float.Parse(splittedFrame[1]) / 100,
            YPosition = float.Parse(splittedFrame[2]) / 100,
            BallSpeed = float.Parse(splittedFrame[3]),
        };
        frameList.Add(playerData);
    }

    public void MoveToFramePos()
    {
        if (currentFrame < frameList.Count)
        {
            vec = new Vector3(frameList[currentFrame].XPosition, frameList[0].YPosition, frameList[currentFrame].ZPosition);
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
            transform.position = new Vector3(frameList[currentFrame].XPosition, frameList[0].YPosition, frameList[currentFrame].ZPosition);
        }
    }
}
