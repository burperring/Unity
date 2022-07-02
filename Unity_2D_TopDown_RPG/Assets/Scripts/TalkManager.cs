using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        // ----- Normal Talk -----
        // Npc A Talk
        talkData.Add(1000, new string[] { "�ȳ�?:2", 
                                          "�� ���� ó�� �Ա���?:0",
                                          "�ѹ� �ѷ������� ��.:0" });

        // Npc B Talk
        talkData.Add(2000, new string[] { "����.:0", 
                                          "�� ȣ�� ���� �Ƹ�����?:2", 
                                          "��� �� ȣ������ ������ ����� �������ִٰ� ��.:3" });

        // Box Talk
        talkData.Add(100, new string[] { "����� �������ڴ�..." });

        // Desk Talk
        talkData.Add(200, new string[] { "������ ����� ������ �ִ� å���̴�." });
        // ----- Normal Talk -----


        // ----- Quest Talk -----
        // Qeust 10
        talkData.Add(10 + 1000, new string[] { "� ��.:0", 
                                               "�� ������ ���� ������ �ִٴ���...:1", 
                                               "������ ȣ�� �ʿ� �絵�� �˷��ٰž�.:2", });
        talkData.Add(11 + 1000, new string[] { "���� �� ������?:1",
                                               "�絵�� ������ ȣ���� �����ž�.:0",});
        talkData.Add(11 + 2000, new string[] { "����...:1",
                                               "�� ȣ���� ������ ������ �°ž�?:0",
                                               "�׷� �� �� �ϳ� ���ָ� �����ٵ�...:1",
                                               "�� �� ��ó�� ������ ���� �� �ֿ������� ��.:2", });

        // Qeust 20
        talkData.Add(20 + 1000, new string[] { "�絵�� ����?:1",
                                               "���� �긮�� �ٴϸ� ������.:3",
                                               "���߿� �絵���� �Ѹ��� �ؾ߰ھ�.:3", });
        talkData.Add(20 + 2000, new string[] { "ã���� �� �� ������ ��.:1", });
        talkData.Add(20 + 300, new string[] { "��ó���� ������ ã�Ҵ�.", });
        talkData.Add(21 + 2000, new string[] { "ã���༭ ����.:2", });
        // ----- Quest Talk -----


        // Portrait Data 0: Normal, 1:Speak, 2:Happy, 3:Angry
        // Npc A Portrait
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);

        // Npc B Portrait
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if(!talkData.ContainsKey(id))  // ContainsKey() : Dictionary�� Key�� �����ϴ��� �˻�
        {
            
            if (!talkData.ContainsKey(id - id % 10))
                // ����Ʈ �� ó�� ��縶�� ���� �� �⺻ ��縦 ������ �´�.
                return GetTalk(id - id % 100, talkIndex);
            else  
                // �ش� ����Ʈ ���� �� ���� ��簡 ���� �� ����Ʈ �� ó�� ��縦 ������ �´�.
                return GetTalk(id - id % 10, talkIndex);
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
