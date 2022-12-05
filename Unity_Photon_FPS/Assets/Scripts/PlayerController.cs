using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable; // if you use photon we have two hashtable(original, photon)

public class PlayerController : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField] Image healthbarImage;

    [SerializeField] TMP_Text ammoText;

    [SerializeField] GameObject hitEffectImage;

    [SerializeField] GameObject UI;

    [SerializeField] GameObject cameraHolder;

    [SerializeField] float mouseNormalSensitivity, mouseJoomSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    [SerializeField] Item[] items;

    // Item Info
    int itemIndex;
    int previousItemIndex = -1;
    
    // Char Move
    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    // Char State
    const float maxHealth = 100f;
    float currentHealth = maxHealth;
    bool isJoom;

    Rigidbody rigid;
    PhotonView PV;
    PlayerManager playerManager;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    void Start()
    {
        if(PV.IsMine)
        {
            EquipItem(0);
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rigid);
            Destroy(UI);
        }
    }

    void Update()
    {
        if (!PV.IsMine)
            return;

        Look();
        Move();
        Jump();
        CheckAmmo();
        Joom();
        Shoot();
        Reload();
        FallDie();
        SwitchWeapon();
    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * (isJoom ? mouseJoomSensitivity : mouseNormalSensitivity));

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * (isJoom ? mouseJoomSensitivity : mouseNormalSensitivity);
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rigid.AddForce(transform.up * jumpForce);
        }
    }

    void CheckAmmo()
    {
        ammoText.text = items[itemIndex].currentBullet + " / " + items[itemIndex].maxBullet;
    }

    void Joom()
    {
        if(Input.GetButtonDown("Joom"))
        {
            isJoom = isJoom ? false : true;

            items[itemIndex].Joom();
        }
    }

    void Reload()
    {
        if(Input.GetButtonDown("Reload"))
        {
            items[itemIndex].Reload();
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            items[itemIndex].Use();
        }
    }

    void FallDie()
    {
        if (transform.position.y < -20f) // Die if you fall out of the world
        {
            Die();
        }
    }

    void SwitchWeapon()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (itemIndex >= items.Length - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(itemIndex + 1);
            }
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (itemIndex <= 0)
            {
                EquipItem(items.Length - 1);
            }
            else
            {
                EquipItem(itemIndex - 1);
            }
        }
    }

    void EquipItem(int _index)
    {
        if (_index == previousItemIndex)
            return;

        itemIndex = _index;

        items[itemIndex].itemGameObject.SetActive(true);

        if(previousItemIndex != -1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
        }

        previousItemIndex = itemIndex;

        if(PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if(changedProps.ContainsKey("itemIndex") && !PV.IsMine && targetPlayer == PV.Owner)
        {
            EquipItem((int)changedProps["itemIndex"]);
        }
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        rigid.MovePosition(rigid.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damage)
    {
        // runs on the shooter's computer
        PV.RPC(nameof(RPC_TakeDamage), PV.Owner, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage, PhotonMessageInfo info)
    {
        currentHealth -= damage;

        healthbarImage.fillAmount = currentHealth / maxHealth;

        StartCoroutine(PlayerHitEffect());

        if (currentHealth <= 0)
        {
            Die();
            PlayerManager.Find(info.Sender).GetKill();
        }
    }

    void Die()
    {
        playerManager.Die();
    }

    IEnumerator PlayerHitEffect()
    {
        hitEffectImage.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        hitEffectImage.SetActive(false);
    }
}
