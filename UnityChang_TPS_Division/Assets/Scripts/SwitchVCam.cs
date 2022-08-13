using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private int priorityBoostAmount;
    [SerializeField]
    private Canvas thirdPersonCanvas;
    [SerializeField]
    private Canvas aimCanvas;

    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;
    private Player player;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
        player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        virtualCamera.Priority += priorityBoostAmount;

        if (player.isEquipWeapon)
        {
            player.isAim = true;
            aimCanvas.enabled = true;
            thirdPersonCanvas.enabled = false;
        }
        else
        {
            aimCanvas.enabled = false;
            thirdPersonCanvas.enabled = true;
        }
    }

    private void CancelAim()
    {
        virtualCamera.Priority -= priorityBoostAmount;
        aimCanvas.enabled = false;
        thirdPersonCanvas.enabled = true;
        player.isAim = false;
    }
}
