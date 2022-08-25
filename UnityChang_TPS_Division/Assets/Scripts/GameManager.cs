using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gamePanel;
    public Text playerHealthText;
    public Text playerAmmoText;
    public GameObject startPanel;
    public GameObject pausePanel;
    public RectTransform playerHPBar;

    public Player player;

    private void Update()
    {
        PlayerState();
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

        playerHPBar.localScale = new Vector3(player.playerHealth / player.playerMaxHealth, 1, 1);
    }

    public void GmaeStart()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        player.isGameStart = true;
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
}
