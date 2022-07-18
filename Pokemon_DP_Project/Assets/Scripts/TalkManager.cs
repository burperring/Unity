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
        talkData.Add(100, new string[] { "Ŀ�ٶ� TV��.", "�����ִ°� ����..." });

        // Wii
        talkData.Add(200, new string[] { "������ ���ӱ��̴�." });

        // Plant
        talkData.Add(300, new string[] { "�Ĺ��̴�.", "�Ʒ��� ��ü Ȯ���� ���� ��縦 �־���..." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
