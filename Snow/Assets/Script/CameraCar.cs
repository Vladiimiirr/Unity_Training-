using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCar : MonoBehaviour
{
    [SerializeField] GameObject car;
    
    void Update()
    {
        
       transform.position = car.transform.position + new Vector3(0, 0, -10);
    }
}
