using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gamePanel;
    public Text playerHealthText;
    public Text playerAmmoText;
    public RectTransform playerHPBar;

    protected Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

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
}
