using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public Player_Move player;
    public GameObject[] Stages;

    // UI Value
    public Image[] UIHealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestart;

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void NextStage()
    {
        // Change Stage
        if (stageIndex < Stages.Length -1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex + 1);
        }
        else  // Game Clear
        {
            // Player Control Lock
            Time.timeScale = 0;

            // Result UI

            // Restart Button UI
            Text btnText = UIRestart.GetComponentInChildren<Text>();  // GetComponentInChildren : 해당 오브젝트의 자식 오브젝트에 접근해야 할 경우
            btnText.text = "RESTART?";
            UIStage.text = "GAME CLEAR!";
            UIRestart.SetActive(true);
        }

        // Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIHealth[health].color = new Color(1, 1, 1, 0.2f);
            player.PlaySound("DAMAGED");
        }
        else
        {
            // Health Down
            UIHealth[0].color = new Color(1, 1, 1, 0.2f);
            player.PlaySound("DAMAGED");

            // Player Die Effect
            player.PlayerDie();

            // Result UI

            // Retry Button UI
            UIRestart.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player Reposition
            if (health > 1)
            {
                PlayerReposition();
            }

            // Health Down
            HealthDown();
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
