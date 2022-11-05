using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Item
{
    public abstract override void Use();
    public abstract override void Reload();

    // Impact img, particle
    public GameObject bulletImpactPrefab;
    public GameObject bulletParticlePrefab;

    // Gun state
    public float maxBullet;
    public float currentBullet;
    public float shotSpeed;
    public float currentTime;

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
    }
}
