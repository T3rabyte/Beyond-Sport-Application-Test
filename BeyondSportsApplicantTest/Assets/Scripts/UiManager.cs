using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public DataManager dataManager;

    public GameObject legendPanel;
    public TMP_Text legendOpener;

    public Slider slider;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (legendPanel.activeSelf)
            {
                legendPanel.gameObject.SetActive(false);
                legendOpener.gameObject.SetActive(true);
            }
            else 
            {
                legendPanel.gameObject.SetActive(true);
                legendOpener.gameObject.SetActive(false);
            }
        }
    }
    public void StartSim()
    {
        dataManager.simStarted = true;
    }

    public void PauseSim()
    {
        dataManager.simStarted = false;
    }

    public void RestartSim() 
    {
        dataManager.simStarted=false;
        foreach (GameObject movableObj in dataManager.movableObjects) 
        {
            if(movableObj.name == "Ball") 
            {
                movableObj.transform.position = new Vector3(movableObj.GetComponent<Ball>().frameList[0].XPosition, movableObj.GetComponent<Ball>().frameList[0].YPosition, movableObj.GetComponent<Ball>().frameList[0].ZPosition);
                movableObj.GetComponent<Ball>().currentFrame = 1;
            }
            else 
            {
                movableObj.transform.position = new Vector3(movableObj.GetComponent<Player>().frameList[0].XPosition, 0.5f, movableObj.GetComponent<Player>().frameList[0].ZPosition);
                movableObj.GetComponent<Player>().currentFrame = 1;
            }
        }
        slider.value = 1;
    }
}
