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
        // ----- Nomal Talk Data -----
        // Bike
        talkData.Add(1000, new string[] { "�ڻ���� ���� �����ض�...", "�����Ŵ� �ǳ����� Ż �� ����!!" });

        // TV
        talkData.Add(2000, new string[] { "Ŀ�ٶ� TV��", "�����ִ°� ����..." });

        // Wii
        talkData.Add(3000, new string[] { "������ ���ӱ��̴�." });

        // Plant
        talkData.Add(4000, new string[] { "�Ĺ��̴�", "�Ʒ��� ��ü Ȯ���� ���� ��縦 �־���..." });

        // PlayerMom
        talkData.Add(1000000, new string[] { "����: �������� ���� ���Ա���", "������ ������ ����." });

        // DragonSick
        talkData.Add(2000000, new string[] { "���: �� ������ ��ٸ� ���� �߸��Ȱž�..." });
        // ----- Nomal Talk Data -----

        // ----- Quest Talk Data -----
        talkData.Add(10 + 1000000, new string[] { "����: �Ʊ� ����̰� �ʸ� ã�ƿԴܴ�", "���� �������� �𸣰ڴٸ� ���� ���� ���ΰ� ��!", "���ѷ� ����̿��� ������" });

        talkData.Add(11 + 1000000, new string[] { "����: �´� Ǯ���� ���� �� �ȴܴ�! \n�߻��� ���ϸ��� �޷���ϱ�", "�� ���ϸ��� �ִٸ� \n�������� �𸣰�����..." });

        talkData.Add(12 + 1000000, new string[] { "����: ����� ���� ��ã�Ҿ�?", "����� ���� ������ ���� ���̾�" });

        talkData.Add(12 + 2000000, new string[] { "���: ����?", "���, �� �ݾ�!?!!", "�̺�! ȣ���� �� �״ϱ� \n���� ��!", "�˰ھ�? ������ \n���� 100�� ���̾�!" });

        talkData.Add(13 + 2000000, new string[] { "���: ��! ������ �� �ִ�!" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if(!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 1000, talkIndex);     // Get First Talk
            else
                return GetTalk(id - id % 10, talkIndex);       // Get First Quest Talk
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
