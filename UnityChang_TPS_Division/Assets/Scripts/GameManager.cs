using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int enemyCount;

    public GameObject gamePanel;
    public Text playerHealthText;
    public Text playerAmmoText;
    public Text enemyCountText;
    public GameObject startPanel;
    public GameObject pausePanel;
    public GameObject clearPanel;
    public Text clearTitleText;
    public RectTransform playerHPBar;

    public Player player;

    private void Update()
    {
        PlayerState();
        ClearPanel();
    }

    private void PlayerState()
    {
        playerHealthText.text = player.playerHealth.ToString();

        if (player.isEquipWeapon)
        {
            if (player.equipWeaponIndex == 0)
                playerAmmoText.text = player.equipCurAmmo1.ToString();
            else if (player.equipWeaponIndex == 1)
                playerAmmoText.text = player.equipCurAmmo2.ToString();
        }
        else
            playerAmmoText.text = "-";

        enemyCountText.text = enemyCount + " / 31";

        playerHPBar.localScale = new Vector3(player.playerHealth / player.playerMaxHealth, 1, 1);
    }

    private void ClearPanel()
    {
        if(enemyCount == 0)
        {
            gamePanel.SetActive(false);
            clearPanel.SetActive(true);
        }
    }

    public void FailPanel()
    {
        gamePanel.SetActive(false);
        clearPanel.SetActive(true);
        clearTitleText.text = "Fail";
    }

    public void GmaeStart()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        player.isGameStart = true;
        player.PauseOut();
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void SetPausePanel()
    {
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void GameResume()
    {
        player.PauseOut();
        OutPausePanel();
    }

    public void OutPausePanel()
    {
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
