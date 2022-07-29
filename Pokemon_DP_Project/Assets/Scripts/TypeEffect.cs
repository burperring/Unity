using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int charPerSeconds;
    public bool isAnimation;
    public GameObject EndCursor;
    public Text msgText;
    public AudioSource audioSource;

    string targetMsg;
    int index;
    float interval;

    public void SetMsg(string msg)
    {
        if (isAnimation)
        {
            CancelInvoke();
            msgText.text = targetMsg;
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
        index = 0;
        EndCursor.SetActive(false);

        // Sound
        audioSource.Play();

        isAnimation = true;

        interval = 1.0f / charPerSeconds;
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        // End Effect
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];
        index++;

        // Recursive
        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        isAnimation = false;
        EndCursor.SetActive(true);
    }
}
