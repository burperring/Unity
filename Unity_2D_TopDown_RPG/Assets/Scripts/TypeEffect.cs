using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public GameObject EndCursor;
    public int CharPerSeconds;
    public bool isAnimation;

    Text msgText;
    AudioSource audioSource;
    string targetMsg;
    int msgIndex;
    float interval;


    void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        if(isAnimation)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";
        msgIndex = 0;
        EndCursor.SetActive(false);

        // Start Animation
        interval = 1.0f / CharPerSeconds;
        isAnimation = true;

        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        // Effecting End
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[msgIndex];


        // Sound
        if(targetMsg[msgIndex] != ' ' || targetMsg[msgIndex] != '.')
            audioSource.Play();

        msgIndex++;

        // Recursive
        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        isAnimation = false;
        EndCursor.SetActive(true);
    }
}
