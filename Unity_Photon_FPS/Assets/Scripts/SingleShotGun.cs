using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SingleShotGun : Gun
{
    [SerializeField] Camera cam;

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public override void Reload()
    {
        currentBullet = maxBullet;
    }

    public override void Use()
    {
        if(shotSpeed <= currentTime)
            Shoot();
    }

    void Shoot()
    {
        if (currentBullet == 0)
            return;

        currentBullet--;

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

        currentTime = 0;
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
