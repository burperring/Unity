using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // 1. 변수(위에 선언하는 변수는 전역번수이다.)
    int health = 100;
    int level = 5;
    int mana = 25;
    int exp = 1500;
    float strength = 15.5f;
    string title = "전설의";
    string PlayerName = "player";
    bool isFullLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello Unity");

        // 2. 그룹형 변수
        string[] monsters = { "슬라임", "사막뱀", "악마", "골렘" };
        int[] monsterLevel = new int[4];
        monsterLevel[0] = 1;
        monsterLevel[1] = 5;
        monsterLevel[2] = 20;
        monsterLevel[3] = 40;

        List<string> items = new List<string>();
        items.Add("HP포션 30");
        items.Add("MP포션 30");
        items.RemoveAt(0);  // 0번째 List 값을 삭제하게 된다면 기존에 있던 값들이 앞으로 옮겨지게 된다.


        // 3. 연산자
        exp += 320;
        exp = exp - 10;
        level = exp / 300;
        strength = level * 3.1f;
        int nextLevel = 300 - (exp % 300);

        Debug.Log(title + " " + PlayerName);

        int FullLevel = 99;
        isFullLevel = level == FullLevel;

        bool isEndTutorial = level > 10;
        bool isBadCondition = health <= 50 || mana <= 20;
        bool isGoodCondition = health >= 50 && mana >= 20;

        string condition = isBadCondition ? "나쁨" : "좋음";    // True 일때 "나쁨", False 일때 "좋음" 출력


        // 4. 키워드
        // int, float, double, stirng, bool, new, List
        // 이 값들은 변수 이름이나 값으로 사용할 수 없다.


        // 5. 조건문
        if(isBadCondition && items[0] == "HP포션 30")
        {
            items.RemoveAt(0);
            health += 30;
            Debug.Log(health);
        }
        else if(isBadCondition && items[0] == "MP포션 30")
        {
            items.RemoveAt(0);
            mana += 30;
            Debug.Log(mana);
        }
        else
        {
            Debug.Log("상태가 좋다.");
        }

        switch(monsters[1])
        {
            case "슬라임":
            case "사막뱀":
                Debug.Log("소형 몬스터 출현");
                break;
            case "악마":
                Debug.Log("중형 몬스터 출현");
                break;
            case "골렘":
                Debug.Log("대형 몬스터 출현");
                break;
            default:
                Debug.Log("??? 몬스터 출현");
                break;
        }


        // 6. 반복문
        while (health>0)
        {
            health--;
            if (health > 0)
                Debug.Log("독데미지를 입었습니다." + health);
            else if (health == 0)
                Debug.Log("Player가 사망했습니다.");

            if(health == 10)
            {
                Debug.Log("해독제를 사용합니다.");
                break;
            }
        }

        for (int i = 0; i<10; i++)
        {
            health++;
            Debug.Log("붕대를 감고 있습니다." + health);
        }

        for(int i = 0; i<monsters.Length; i++)
        {
            Debug.Log("이 지역에 있는 몬스터 : " + monsters[i]);
        }

        foreach(string monster in monsters)
        {
            Debug.Log("이 지역에 있는 몬스터 : " + monster);
        }


        // 7. 함수 사용
        health = Heal1(health);
        Heal2();
        for (int i = 0; i < monsters.Length; i++)
        {
            Debug.Log(monsters[i] + "에게 " + Battle(monsterLevel[i]));
        }


        // 8. class(클래스)
        Player player = new Player();
        player.id = 0;
        player.name = "player";
        player.title = "최초의";
        player.weapon = "pistol";
        Debug.Log(player.Talk());
        Debug.Log(player.HasWeapon());

        player.LevelUp();
        Debug.Log(player.level);
        Debug.Log(player.move());
    }


    // 7. 함수
    int Heal1(int currentHealth)
    {
        currentHealth += 10;
        Debug.Log("회복을 받았습니다." + currentHealth);
        return currentHealth;
    }

    void Heal2()
    {
        health += 10;
        Debug.Log("회복을 받았습니다." + health);
    }

    string Battle(int monsterLevel)
    {
        string result;
        if (level > monsterLevel)
            result = "이겼다!!";
        else
            result = "졌다";

        return result;
    }
}
