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

    public enum WeaponMode
    {
        Normal,
        Sniper
    }
    public WeaponMode wMode;
}
