using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public int weaponIndex;
    [SerializeField]
    public float damage;
    [SerializeField]
    public float shotRate;
    [SerializeField]
    public int maxAmmo;
    [SerializeField]
    public int curAmmo;

    public GameObject bullletShell;
    public Transform bulletEjectionPos;
}
