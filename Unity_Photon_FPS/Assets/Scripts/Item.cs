using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemInfo itemInfo;
    public GameObject itemGameObject;

    // Gun state
    public float maxBullet;
    public float currentBullet;
    public float reloadSpeed;
    public float shotSpeed;
    public bool isRifle;
    public bool isReload;
    public bool isShot;
    public bool isJoom;

    public abstract void Joom();
    public abstract void Reload();
    public abstract void Use();
}
