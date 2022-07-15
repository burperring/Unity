using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject scanObject;

    public void Action(GameObject Object)
    {
        // Get Current Object
        scanObject = Object;
        ObjData objData = scanObject.GetComponent<ObjData>();
    }

    void Update()
    {
        
    }
}
