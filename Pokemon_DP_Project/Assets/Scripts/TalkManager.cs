using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        // TV
        talkData.Add(100, new string[] { "커다란 TV다.", "꺼져있는거 같다..." });

        // Wii
        talkData.Add(200, new string[] { "오래된 게임기이다." });

        // Plant
        talkData.Add(300, new string[] { "식물이다.", "아래쪽 물체 확인을 위해 대사를 넣었다..." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
