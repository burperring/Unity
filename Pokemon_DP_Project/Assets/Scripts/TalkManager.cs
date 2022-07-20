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
        // Bike
        talkData.Add(1000, new string[] { "박사님의 말을 생각해라...", "자전거는 실내에서 탈 수 없어!!" });

        // TV
        talkData.Add(2000, new string[] { "커다란 TV다.", "꺼져있는거 같다..." });

        // Wii
        talkData.Add(3000, new string[] { "오래된 게임기이다." });

        // Plant
        talkData.Add(4000, new string[] { "식물이다.", "아래쪽 물체 확인을 위해 대사를 넣었다..." });

        // PlayerMom
        talkData.Add(1000000, new string[] { "오랜만에 집에 들어왔구나.", "집에서 쉬었다 가렴." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
