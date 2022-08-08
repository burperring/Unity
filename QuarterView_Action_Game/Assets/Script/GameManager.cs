using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Camera
    public GameObject menuCam;
    public GameObject gameCam;

    // Game Setting Data
    public Player player;
    public Boss boss;
    public int stage;
    public float playTime;
    public bool isBattle;
    public int enemyCntA;
    public int enemyCntB;
    public int enemyCntC;

    // Game UI Setting
    public GameObject menuPanel;
    public Text maxScoreText;
    public GameObject gamePanel;
    public Text scoreText;              // 좌 상단
    public Text stageText;              // 우 상단
    public Text playTimeText;           //    ː
    public Text playerHPText;           // 좌 하단
    public Text playerAmmoText;         //    ː
    public Text playerCoinText;         //    ː
    public Text enemyAText;             // 우 하단
    public Text enemyBText;             //    ː
    public Text enemyCText;             //    ː
    public Image weapon1Img;            // 중앙 하단
    public Image weapon2Img;            //    ː
    public Image weapon3Img;            //    ː
    public Image weaponGImg;            //    ː
    public RectTransform bossHPGroup;   // 중앙 상단
    public RectTransform bossHPBar;     //    ː

    private void Awake()
    {
        maxScoreText.text = string.Format("{0:n0}", PlayerPrefs.GetInt("MaxScore"));
    }

    public void GameStart()
    {
        menuCam.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);
    }

    void Update()
    {
        if (isBattle)
            playTime += Time.deltaTime;
    }

    void LateUpdate()   // Update()가 끝난 후 호출되는 생명주기
    {
        // Game Setting Value
        scoreText.text = string.Format("{0:n0}", player.score);
        stageText.text = "STAGE " + stage.ToString();

        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int sec = (int)(playTime % 60);
        playTimeText.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", sec);

        // Player Status Text
        playerHPText.text = player.health + " / " + player.maxHealth;
        playerCoinText.text = string.Format("{0:n0}", player.coin);
        if (player.equipWeapon == null)
            playerAmmoText.text = "- / " + player.ammo;
        else if (player.equipWeapon.type == Weapon.Type.Melee)
            playerAmmoText.text = "- / " + player.ammo;
        else
            playerAmmoText.text = player.equipWeapon.curAmmo + " / " + player.ammo;

        // Player Weapon Img
        weapon1Img.color = new Color(1, 1, 1, player.hasWeapons[0] ? 1 : 0);
        weapon2Img.color = new Color(1, 1, 1, player.hasWeapons[1] ? 1 : 0);
        weapon3Img.color = new Color(1, 1, 1, player.hasWeapons[2] ? 1 : 0);
        weaponGImg.color = new Color(1, 1, 1, player.hasGrenade > 0 ? 1 : 0);

        // Enemy Cnt
        enemyAText.text = enemyCntA.ToString();
        enemyBText.text = enemyCntB.ToString();
        enemyCText.text = enemyCntC.ToString();

        // Boss HP
        bossHPBar.localScale = new Vector3((float)boss.curHealth / boss.maxHealth, 1, 1);
    }
}
