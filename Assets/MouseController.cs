using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 9.0f;

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
    }
}
