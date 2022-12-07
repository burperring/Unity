using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SingleShotGun : Gun
{
    [SerializeField] Camera cam;

    Animator anim;

    PhotonView PV;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        PV = GetComponent<PhotonView>();

        wMode = WeaponMode.Normal;
    }

    public override void Joom()
    {
        if (!isRifle)
            return;

        if(Input.GetMouseButtonDown(1))
        {
            switch(wMode)
            {
                case WeaponMode.Normal:
                    SniperMode();
                    break;
                case WeaponMode.Sniper:
                    NormalMode();
                    break;
            }
        }
    }

    void SniperMode()
    {
        // change weapon joom mode
        wMode = WeaponMode.Sniper;

        isJoom = true;

        anim.SetBool("isJoom", isJoom);

        cam.fieldOfView = 20f;
    }

    void NormalMode()
    {
        wMode = WeaponMode.Normal;

        isJoom = false;

        anim.SetBool("isJoom", isJoom);

        cam.fieldOfView = 60f;
    }

    public override void Reload()
    {
        if (currentBullet == maxBullet || isShot || isJoom)
            return;

        isReload = true;

        gunReloadSound.Play();

        anim.SetTrigger("doReload");
        
        currentBullet = maxBullet;

        Invoke(nameof(ReloadOut), reloadSpeed);
    }

    void ReloadOut()
    {
        isReload = false;
    }

    public override void Use()
    {
        if(!isShot && !isReload && !isJoom)
            Shoot();
        else if (!isShot && !isReload && isJoom)
            JoomShot();
    }

    void Shoot()
    {
        if (currentBullet == 0)
            return;

        // play gun sound each other
        gunShotSound.PlayOneShot(gunShotSound.clip);

        isShot = true;

        currentBullet--;

        anim.SetTrigger("doShot");

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            // get player damage
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);

            // if collider not player set bullet hole, bullet effect
            if(hit.collider.gameObject.tag != "Player")
                PV.RPC(nameof(RPC_Shoot), RpcTarget.All, hit.point, hit.normal);
        }

        // gun fire effect start
        StartCoroutine(ShootEffectOn(0.05f));

        Invoke(nameof(ShotEnd), shotSpeed);
    }

    void JoomShot()
    {
        if (currentBullet == 0)
            return;

        // play gun sound each other
        gunShotSound.PlayOneShot(gunShotSound.clip);

        isShot = true;

        currentBullet--;

        anim.SetBool("isJoomShot", true);

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // get player damage
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);

            // if collider not player set bullet hole, bullet effect
            if (hit.collider.gameObject.tag != "Player")
                PV.RPC(nameof(RPC_Shoot), RpcTarget.All, hit.point, hit.normal);
        }

        // gun fire effect start
        StartCoroutine(ShootEffectOn(0.05f));

        Invoke(nameof(ShotEnd), shotSpeed);
    }

    void ShotEnd()
    {
        isShot = false;
        anim.SetBool("isJoomShot", false);
    }

    IEnumerator ShootEffectOn(float duration)
    {
        int num = Random.Range(0, gunFlashPrefab.Length);
        gunFlashPrefab[num].SetActive(true);

        yield return new WaitForSeconds(duration);

        gunFlashPrefab[num].SetActive(false);
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);

        if (colliders.Length != 0)
        {
            // set bullet impact img
            GameObject bulletImpactObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, 
                Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
            
            Destroy(bulletImpactObj, 7f);

            bulletImpactObj.transform.SetParent(colliders[0].transform);

            // set bullet effect(stone break effect)
            GameObject bulletPSObj = Instantiate(bulletParticlePrefab, hitPosition, Quaternion.LookRotation(hitNormal));

            bulletPSObj.GetComponent<ParticleSystem>().Play();

            Destroy(bulletPSObj, 2f);

            bulletPSObj.transform.SetParent(colliders[0].transform);
        }
    }
}
