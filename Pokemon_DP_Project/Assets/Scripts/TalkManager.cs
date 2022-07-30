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
        talkData.Add(1000, new string[] { "박사님의 말을 생각해라...", "자전거는 실내에서 탈 수 없어!!" });

        // TV
        talkData.Add(2000, new string[] { "커다란 TV다", "꺼져있는거 같다..." });

        // Wii
        talkData.Add(3000, new string[] { "오래된 게임기이다." });

        // Plant
        talkData.Add(4000, new string[] { "식물이다", "아래쪽 물체 확인을 위해 대사를 넣었다..." });

        // PlayerMom
        talkData.Add(1000000, new string[] { "엄마: 오랜만에 집에 들어왔구나", "집에서 쉬었다 가렴." });

        // DragonSick
        talkData.Add(2000000, new string[] { "용식: 어!! 우리집에는 무슨 일이야?", "난 오랜만에 집에 쉬러왔지..", "너도 오랜만에 집에서 쉬는건 어때?" });

        // DragonSickMom
        talkData.Add(3000000, new string[] { "용식엄마: 용식이 찾으러 왔나?" });
        // ----- Nomal Talk Data -----

        // ----- Quest Talk Data -----
        // Quest_10_Start
        talkData.Add(10 + 1000000, new string[] { "엄마: 아까 용식이가 너를 찾아왔단다", "무슨 일인지는 모르겠다만 아주 급한 일인가 봐!", "서둘러 용식이에게 가보렴" });
        talkData.Add(11 + 1000000, new string[] { "엄마: 맞다 풀숲에 들어가면 안 된단다! \n야생의 포켓몬이 달려드니까", "네 포켓몬이 있다면 \n괜찮을지 모르겠지만..." });
        talkData.Add(12 + 1000000, new string[] { "엄마: 용식이 집을 못찾았어?", "용식이 집은 위에서 왼쪽 집이야" });
        talkData.Add(12 + 2000000, new string[] { "콰당!!!!", "용식: 뭐야-?", "어라, 너 잖아!?!!", "이봐! 호수에 갈 테니까 \n빨리 와!", "알겠어? 늦으면 \n벌금 100만 원이야!" });
        talkData.Add(13 + 2000000, new string[] { "용식: 아! 깜빡한 게 있다!" });
        // Quest_10_Done

        // Quest_20_Start
        talkData.Add(20 + 3000000, new string[] { "용식엄마: 아 용식이 찾아왔구나?", "그 애 방금 나갔었는데 \n바로 돌아왔지 뭐니", "정말 가만히 있지를 않는구나 \n누굴 닮아서 저럴까?" });
        talkData.Add(20 + 2000000, new string[] { "용식: ...가방이랑 \n모험노트도 들고 가볼까", "오!! 빨리 호수에 가자", "도로에서 기다릴 테니까 \n늦으면 벌금 1000만 원이야!" });
        // Quest_20_Done

        // Quest_30_Start
        talkData.Add(20 + 2000000, new string[] { "용식: " });
        // Quest_30_Done
        // ----- Quest Talk Data -----
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
