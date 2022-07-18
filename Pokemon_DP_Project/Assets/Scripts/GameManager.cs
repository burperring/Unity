using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager gameManager;

    public TalkManager talkManager;

    public Text talkText;
    public GameObject canvas;
    public GameObject talkPanel;
    public GameObject scanObject;

    public bool isAction;
    public int talkIndex;
    public int stage;
    public int floor;

    private void Awake()
    {
        if (gameManager == null)
        {
            DontDestroyOnLoad(talkManager);
            DontDestroyOnLoad(canvas);

            gameManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Action(GameObject scanObj)
    {
        isAction = true;      
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id);

        talkPanel.SetActive(isAction);
    }
    
    void Talk(int id)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            talkIndex = 0;
            isAction = false;
            return;
        }

        talkText.text = talkData;

        isAction = true;
        talkIndex++;
    }
}
