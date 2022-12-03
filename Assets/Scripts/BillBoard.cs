using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    void LateUpdate()
    {
        //transform.LookAt(Camera.main.transform.position, -Vector3.up);
        transform.LookAt(transform.position - Camera.main.transform.up);
    }
}
