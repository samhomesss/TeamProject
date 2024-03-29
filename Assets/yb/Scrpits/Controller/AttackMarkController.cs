using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMarkController : MonoBehaviour
{
    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        transform.LookAt(cameraPos);
    }
}
