using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    // Player Status
    [SerializeField]
    private float playerCrawlSpeed = 1.5f;
    [SerializeField]
    private float playerWalkSpeed = 3.0f;
    [SerializeField]
    private float playerRunSpeed = 6.0f;
    [SerializeField]
    private float jumpHeight = 1.5f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 8f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform barrelTransform;
    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private float bulletHitMissDistance = 25f;

    [SerializeField]
    public Transform bulletDecalParent;

    // GetComponent
    private CharacterController controller;
    private PlayerInput playerInput;
    private Transform cameraTransform;
    private Animator anim;

    // Player Setting
    public bool isEquipWeapon;
    public bool isAim;
    
    private float playerSpeed;
    private float rate = 1f;
    private bool groundedPlayer;
    private bool isCrawl;
    private bool isRoot;

    // Weapon
    // Player -> Character1_Reference -> CH_Hips -> CH_Spine -> CH_Chest -> CH_UpperChest -> CH_UpperChest
    // -> CH_Shou_R -> CH_Hand_R -> COL_Hand_R 위치에 무기 세팅
    [SerializeField]
    private GameObject[] weapon;
    [SerializeField]
    private Weapon[] weaponValue;

    private int equipWeaponIndex;
    private int weaponCount = 0;
    private int[] weaponIndex = { 99, 99 };

    // Equip Weapon Value
    private float equipDamage;
    private float equipShotRate;
    private int equipMaxAmmo;
    private int equipCurAmmo1;
    private int equipCurAmmo2;

    // Player Input Controller Setting
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction crawlAction;
    private InputAction runAction;
    private InputAction weapon1Action;
    private InputAction weapon2Action;
    private InputAction handAction;
    private InputAction rootAction;

    private Vector3 playerVelocity;

    private GameObject nearObject = null;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;

        // Game Input Set
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        crawlAction = playerInput.actions["Crawl"];
        runAction = playerInput.actions["Run"];
        weapon1Action = playerInput.actions["Weapon1"];
        weapon2Action = playerInput.actions["Weapon2"];
        handAction = playerInput.actions["Hand"];
        rootAction = playerInput.actions["Root"];

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        rate += Time.deltaTime;

        PlayerMove();
        RotateCamera();
        PlayerDownUp();
        ShootGun();
        Swap();
        Root();
    }

    void PlayerMove()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;

        // Check player state and set playerspeed
        if (Mathf.Abs(input.x) > 0 || Mathf.Abs(input.y) > 0)
        {
            if (shootAction.IsPressed())
                playerSpeed = isAim ? 0f : isEquipWeapon ? playerWalkSpeed : isRoot ? 0f : isCrawl ? playerCrawlSpeed : runAction.IsPressed() ? playerRunSpeed : playerWalkSpeed;
            else
                playerSpeed = isRoot ? 0f : isCrawl ? playerCrawlSpeed : runAction.IsPressed() ? playerRunSpeed : playerWalkSpeed;
        }
        else
            playerSpeed = 0f;

        controller.Move(move * Time.deltaTime * playerSpeed);

        anim.SetFloat("Speed", playerSpeed);
        anim.SetFloat("Horizontal", input.x);
        anim.SetFloat("Vertical", input.y);

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer && !isAim && !shootAction.IsPressed())
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            anim.SetTrigger("doJump");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void RotateCamera()
    {
        if (isAim && !isCrawl)
        {
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y + 40f, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else if(!isAim && !isCrawl && isEquipWeapon && shootAction.IsPressed() && playerSpeed > 0)
        {
            float hori = anim.GetFloat("Horizontal");
            float ver = anim.GetFloat("Vertical");

            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y + (50 + (-30 * hori) + 10 * Mathf.Abs(ver)), 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void PlayerDownUp() // Player crawl
    {
        if(crawlAction.triggered && !isCrawl)
        {
            isCrawl = true;
            anim.SetBool("isDown", true);
        }
        else if (crawlAction.triggered && isCrawl)
        {
            isCrawl = false;
            anim.SetBool("isDown", false);
        }
    }

    private void ShootGun()
    {
        if (!isEquipWeapon)
            return;

        anim.SetBool("isShooting", shootAction.IsPressed());

        // Shoot Bullet
        if (shootAction.IsPressed() && rate > equipShotRate)
        {
            if (equipCurAmmo1 == 0 && equipWeaponIndex == 0)
                return;
            if (equipCurAmmo2 == 0 && equipWeaponIndex == 1)
                return;

            RaycastHit hit;

            GameObject bullet = Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
            BulletController bulletController = bullet.GetComponent<BulletController>();

            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
            {
                bulletController.target = hit.point;
                bulletController.hit = true;
            }
            else
            {
                bulletController.target = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
                bulletController.hit = false;
            }

            if (equipWeaponIndex == 0)
                equipCurAmmo1--;
            else if (equipWeaponIndex == 1)
                equipCurAmmo2--;

            rate = 0;
        }
    }

    void Swap()
    {
        anim.SetBool("isAiming", isAim);

        if (weapon1Action.triggered)
        {
            if (isEquipWeapon)
            {
                anim.SetTrigger("doStrafing");
                weapon[weaponIndex[1]].SetActive(false);
            }
            weapon[weaponIndex[0]].SetActive(true);

            // Set Equip Weapon Value
            equipDamage = weaponValue[weaponIndex[0]].damage;
            equipShotRate = weaponValue[weaponIndex[0]].shotRate;
            equipMaxAmmo = weaponValue[weaponIndex[0]].maxAmmo;

            equipWeaponIndex = 0;

            isEquipWeapon = true;
            anim.SetBool("isStrafing", true);
        }
        else if(weapon2Action.triggered)
        {
            if (isEquipWeapon)
            {
                anim.SetTrigger("doStrafing");
                weapon[weaponIndex[0]].SetActive(false);
            }
            weapon[weaponIndex[1]].SetActive(true);

            // Set Equip Weapon Value
            equipDamage = weaponValue[weaponIndex[1]].damage;
            equipShotRate = weaponValue[weaponIndex[1]].shotRate;
            equipMaxAmmo = weaponValue[weaponIndex[1]].maxAmmo;

            equipWeaponIndex = 1;

            isEquipWeapon = true;
            anim.SetBool("isStrafing", true);
        }
        else if(handAction.triggered)
        {
            isEquipWeapon = false;
            weapon[weaponIndex[equipWeaponIndex]].SetActive(false);
            anim.SetBool("isStrafing", false);
        }
    }

    void Root()
    {
        if (rootAction.triggered)
        {
            isRoot = true;
            anim.SetTrigger("doRoot");

            Invoke("RootOut", 0.8f);

            if (nearObject == null)
                return;
            else if (nearObject.tag == "Weapon")
            {
                Weapon wp = nearObject.GetComponent<Weapon>();

                if (weaponCount == 2)
                {
                    if (weaponIndex[0] == wp.weaponIndex || weaponIndex[1] == wp.weaponIndex)
                        return;

                    weapon[weaponIndex[equipWeaponIndex]].SetActive(false);
                    weaponIndex[equipWeaponIndex] = wp.weaponIndex;
                    weapon[weaponIndex[equipWeaponIndex]].SetActive(true);

                    // Set Equip Weapon Value
                    equipDamage = weaponValue[weaponIndex[equipWeaponIndex]].damage;
                    equipShotRate = weaponValue[weaponIndex[equipWeaponIndex]].shotRate;
                    equipMaxAmmo = weaponValue[weaponIndex[equipWeaponIndex]].maxAmmo;

                    if(equipWeaponIndex == 0)
                        equipCurAmmo1 = weaponValue[weaponIndex[equipWeaponIndex]].maxAmmo;
                    else if(equipWeaponIndex == 1)
                        equipCurAmmo2 = weaponValue[weaponIndex[equipWeaponIndex]].maxAmmo;
                }
                else
                {
                    if (weaponIndex[0] == wp.weaponIndex)
                        return;

                    weaponIndex[weaponCount] = wp.weaponIndex;

                    if(weaponCount == 0)
                        equipCurAmmo1 = weaponValue[weaponIndex[equipWeaponIndex]].maxAmmo;
                    else if(weaponCount == 1)
                        equipCurAmmo2 = weaponValue[weaponIndex[equipWeaponIndex]].maxAmmo;

                    weaponCount++;
                    nearObject.SetActive(false);
                }
            }
        }
    }

    void RootOut()
    {
        isRoot = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;
    }
}
