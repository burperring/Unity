using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // 1. ����(���� �����ϴ� ������ ���������̴�.)
    int health = 100;
    int level = 5;
    int mana = 25;
    int exp = 1500;
    float strength = 15.5f;
    string title = "������";
    string PlayerName = "player";
    bool isFullLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello Unity");

        // 2. �׷��� ����
        string[] monsters = { "������", "�縷��", "�Ǹ�", "��" };
        int[] monsterLevel = new int[4];
        monsterLevel[0] = 1;
        monsterLevel[1] = 5;
        monsterLevel[2] = 20;
        monsterLevel[3] = 40;

        List<string> items = new List<string>();
        items.Add("HP���� 30");
        items.Add("MP���� 30");
        items.RemoveAt(0);  // 0��° List ���� �����ϰ� �ȴٸ� ������ �ִ� ������ ������ �Ű����� �ȴ�.


        // 3. ������
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

        string condition = isBadCondition ? "����" : "����";    // True �϶� "����", False �϶� "����" ���


        // 4. Ű����
        // int, float, double, stirng, bool, new, List
        // �� ������ ���� �̸��̳� ������ ����� �� ����.


        // 5. ���ǹ�
        if(isBadCondition && items[0] == "HP���� 30")
        {
            items.RemoveAt(0);
            health += 30;
            Debug.Log(health);
        }
        else if(isBadCondition && items[0] == "MP���� 30")
        {
            items.RemoveAt(0);
            mana += 30;
            Debug.Log(mana);
        }
        else
        {
            Debug.Log("���°� ����.");
        }

        switch(monsters[1])
        {
            case "������":
            case "�縷��":
                Debug.Log("���� ���� ����");
                break;
            case "�Ǹ�":
                Debug.Log("���� ���� ����");
                break;
            case "��":
                Debug.Log("���� ���� ����");
                break;
            default:
                Debug.Log("??? ���� ����");
                break;
        }


        // 6. �ݺ���
        while (health>0)
        {
            health--;
            if (health > 0)
                Debug.Log("���������� �Ծ����ϴ�." + health);
            else if (health == 0)
                Debug.Log("Player�� ����߽��ϴ�.");

            if(health == 10)
            {
                Debug.Log("�ص����� ����մϴ�.");
                break;
            }
        }

        for (int i = 0; i<10; i++)
        {
            health++;
            Debug.Log("�ش븦 ���� �ֽ��ϴ�." + health);
        }

        for(int i = 0; i<monsters.Length; i++)
        {
            Debug.Log("�� ������ �ִ� ���� : " + monsters[i]);
        }

        foreach(string monster in monsters)
        {
            Debug.Log("�� ������ �ִ� ���� : " + monster);
        }


        // 7. �Լ� ���
        health = Heal1(health);
        Heal2();
        for (int i = 0; i < monsters.Length; i++)
        {
            Debug.Log(monsters[i] + "���� " + Battle(monsterLevel[i]));
        }


        // 8. class(Ŭ����)
        Player player = new Player();
        player.id = 0;
        player.name = "player";
        player.title = "������";
        player.weapon = "pistol";
        Debug.Log(player.Talk());
        Debug.Log(player.HasWeapon());

        player.LevelUp();
        Debug.Log(player.level);
        Debug.Log(player.move());
    }


    // 7. �Լ�
    int Heal1(int currentHealth)
    {
        currentHealth += 10;
        Debug.Log("ȸ���� �޾ҽ��ϴ�." + currentHealth);
        return currentHealth;
    }

    void Heal2()
    {
        health += 10;
        Debug.Log("ȸ���� �޾ҽ��ϴ�." + health);
    }

    string Battle(int monsterLevel)
    {
        string result;
        if (level > monsterLevel)
            result = "�̰��!!";
        else
            result = "����";

        return result;
    }
}
