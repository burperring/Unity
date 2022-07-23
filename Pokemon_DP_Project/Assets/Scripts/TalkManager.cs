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
        talkData.Add(2000000, new string[] { "용식: 이 문장이 뜬다면 뭔가 잘못된거야..." });
        // ----- Nomal Talk Data -----

        // ----- Quest Talk Data -----
        talkData.Add(10 + 1000000, new string[] { "엄마: 아까 용식이가 너를 찾아왔단다", "무슨 일인지는 모르겠다만 아주 급한 일인가 봐!", "서둘러 용식이에게 가보렴" });

        talkData.Add(11 + 1000000, new string[] { "엄마: 맞다 풀숲에 들어가면 안 된단다! \n야생의 포켓몬이 달려드니까", "네 포켓몬이 있다면 \n괜찮을지 모르겠지만..." });

        talkData.Add(12 + 1000000, new string[] { "엄마: 용식이 집을 못찾았어?", "용식이 집은 위에서 왼쪽 집이야" });

        talkData.Add(12 + 2000000, new string[] { "용식: 뭐야?", "어라, 너 잖아!?!!", "이봐! 호수에 갈 테니까 \n빨리 와!", "알겠어? 늦으면 \n벌금 100만 원이야!" });

        talkData.Add(13 + 2000000, new string[] { "용식: 아! 깜빡한 게 있다!" });
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
