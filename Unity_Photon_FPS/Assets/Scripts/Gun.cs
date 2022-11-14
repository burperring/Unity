using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Item
{
    public abstract override void Joom();
    public abstract override void Reload();
    public abstract override void Use();

    // Impact img, particle, audio
    public GameObject bulletImpactPrefab;
    public GameObject bulletParticlePrefab;
    public GameObject[] gunFlashPrefab;
    public AudioSource gunShotSound;
    public AudioSource gunReloadSound;

    // Gun state
    public float maxBullet;
    public float currentBullet;
    public float reloadSpeed;
    public float shotSpeed;
    public bool isRifle;
    public bool isReload;
    public bool isShot;

    public enum WeaponMode
    {
        Normal,
        Sniper
    }
    public WeaponMode wMode;
}
