using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Ball : MonoBehaviour
{
    // public으로 인자를 주게 될 경우 Unity에서 바로 값을 변경할 수 있다.
    public float jumpPower;
    public int itemCount;
    public GameManagerLogic manager;
    bool isJump;
    Rigidbody rigid;
    AudioSource eatAudio;

    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        eatAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isJump = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // 오브젝트를 Tag를 통해 묶어서 사용한다.
        if(other.tag == "Item")
        {
            itemCount++;
            eatAudio.Play();
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
        }
        else if(other.tag == "Finish")
        {
            // Find 계열 함수는 부하를 초래할 수 있으므로 피하는 것을 권장
            if(manager.totalItemCount == itemCount)
            {
                // Game Clear!
                if (manager.stage == 3)
                    SceneManager.LoadScene("Example1_0");
                SceneManager.LoadScene("Example1_" + (manager.stage + 1).ToString());
            }
            else
            {
                // Restart..
                SceneManager.LoadScene("Example1_" + manager.stage.ToString());
            }
        }
    }
}
