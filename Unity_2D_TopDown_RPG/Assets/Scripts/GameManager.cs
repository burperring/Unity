using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text TalkText;
    public GameObject scanObject;
    public GameObject talkPanel;
    public bool isActionPanel;

    public void Action(GameObject Object)
    {
        if(isActionPanel)   // Exit Action
        {
            isActionPanel = false;
        }
        else   // Enter Action
        {
            isActionPanel = true;
            scanObject = Object;
            TalkText.text = "�̰��� " + scanObject.name + "�̴�.";
        }

        talkPanel.SetActive(isActionPanel);
    }
}
