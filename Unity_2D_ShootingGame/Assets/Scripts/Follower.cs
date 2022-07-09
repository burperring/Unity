using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float maxShotDelay;  // 최대
    public float curShotDelay;  // 현재
    public ObjectManager objectManager;

    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;

    void Awake()
    {
        // FIFO(First In First Out)
        parentPos = new Queue<Vector3>();
    }

    void Update()
    {
        // Encapsulation(캡슐화)
        Watch();
        Follow();
        Fire();
        Reload();
    }

    void Watch()
    {
        // Input Pos
        if(!parentPos.Contains(parent.position))     // Contains(value) : value를 이미 가지고 있는가?
            parentPos.Enqueue(parent.position);

        // Output Pos
        if (parentPos.Count > followDelay)
            followPos = parentPos.Dequeue();
        else if (parentPos.Count < followDelay)
            followPos = parent.position;
    }

    void Follow()
    {
        transform.position = followPos;
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;

        // maxDelay까지 기다리지 않았다면 Fire를 실행하지 않는다.
        if (curShotDelay < maxShotDelay)
            return;

        GameObject bullet1 = objectManager.MakeObj("BulletFollower");
        bullet1.transform.position = transform.position;
        
        Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
        rigid1.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        curShotDelay = 0;
    }

    void Reload()
    {
        // 실시간 Delay 추가
        curShotDelay += Time.deltaTime;
    }
}
