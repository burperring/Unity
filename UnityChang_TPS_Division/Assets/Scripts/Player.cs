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

    // GetComponent
    private CharacterController controller;
    private PlayerInput playerInput;
    private Transform cameraTransform;
    private Animator anim;

    // Player Setting
    public bool isEquipWeapon;
    public bool isAim;
    
    private float playerSpeed;
    private bool groundedPlayer;
    private bool isCrawl;

    // Weapon
    // Player -> Character1_Reference -> CH_Hips -> CH_Spine -> CH_Chest -> CH_UpperChest -> CH_UpperChest
    // -> CH_Shou_R -> CH_Hand_R -> COL_Hand_R 위치에 무기 세팅

    // Player Input Controller Setting
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction crawlAction;
    private InputAction runAction;
    private InputAction weapon1Action;
    private InputAction weapon2Action;
    private InputAction handAction;

    private Vector3 playerVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        crawlAction = playerInput.actions["Crawl"];
        runAction = playerInput.actions["Run"];
        weapon1Action = playerInput.actions["Weapon1"];
        weapon2Action = playerInput.actions["Weapon2"];
        handAction = playerInput.actions["Hand"];
    }

    void Update()
    {
        PlayerMove();
        RotateCamera();
        PlayerDownUp();
        Swap();
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

        if (Mathf.Abs(input.x) > 0 || Mathf.Abs(input.y) > 0)
            playerSpeed = isCrawl ? playerCrawlSpeed : runAction.IsPressed() ? playerRunSpeed : playerWalkSpeed;
        else
            playerSpeed = 0f;

        controller.Move(move * Time.deltaTime * playerSpeed);

        anim.SetFloat("Speed", playerSpeed);
        anim.SetFloat("Horizontal", input.x);
        anim.SetFloat("Vertical", input.y);

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            anim.SetTrigger("doJump");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void RotateCamera()
    {
        Debug.Log(isAim);

        if (isAim && !isCrawl)
        {
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y + 40f, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void PlayerDownUp()
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

    void Swap()
    {
        anim.SetBool("isAiming", isAim);

        if (weapon1Action.triggered || weapon2Action.triggered)
        {
            if (isEquipWeapon)
                anim.SetTrigger("doStrafing");

            isEquipWeapon = true;
            anim.SetBool("isStrafing", true);
        }
        else if(handAction.triggered)
        {
            isEquipWeapon = false;
            anim.SetBool("isStrafing", false);
        }
    }
}
