using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    public Animator anim;

    public GameObject[] itemObject;
    public Transform[] itemPos;
    public Text talkText;
    public int[] itemPrice;
    public string[] talkData;

    Player enterPlayer;

    public void Enter(Player player)
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    public void Exit()
    {
        anim.SetTrigger("doHello");
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }

    public void Buy(int index)
    {
        int price = itemPrice[index];

        if(price > enterPlayer.coin)
        {
            StopCoroutine(bankrupTalk());
            StartCoroutine(bankrupTalk());
            return;
        }

        StopCoroutine(buyTalk());
        StartCoroutine(buyTalk());
        enterPlayer.coin -= price;
        Vector3 ranVec = Vector3.right * Random.Range(-3, 3) + Vector3.forward * Random.Range(-3, 3);
        Instantiate(itemObject[index], itemPos[index].position + ranVec, itemPos[index].rotation);
    }

    IEnumerator bankrupTalk()
    {
        talkText.text = talkData[1];
        yield return new WaitForSeconds(2f);

        talkText.text = talkData[0];
    }

    IEnumerator buyTalk()
    {
        talkText.text = talkData[2];
        yield return new WaitForSeconds(2f);

        talkText.text = talkData[0];
    }
}
