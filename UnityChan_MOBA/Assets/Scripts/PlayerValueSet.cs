using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValueSet : MonoBehaviour
{
    // 0 : Misaki, 1 : UnityChan, 2 : UC WGS, 3 : UC WTD, 4 : Yuko
    public int selectCharNum;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
