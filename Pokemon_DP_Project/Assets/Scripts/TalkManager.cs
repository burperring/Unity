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
        talkData.Add(1000, new string[] { "�ڻ���� ���� �����ض�...", "�����Ŵ� �ǳ����� Ż �� ����!!" });

        // TV
        talkData.Add(2000, new string[] { "Ŀ�ٶ� TV��.", "�����ִ°� ����..." });

        // Wii
        talkData.Add(3000, new string[] { "������ ���ӱ��̴�." });

        // Plant
        talkData.Add(4000, new string[] { "�Ĺ��̴�.", "�Ʒ��� ��ü Ȯ���� ���� ��縦 �־���..." });

        // PlayerMom
        talkData.Add(1000000, new string[] { "�������� ���� ���Ա���.", "������ ������ ����." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
