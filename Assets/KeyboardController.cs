using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class KeyboardController : MonoBehaviour
{
    [SerializeField] private float speed = 15.0f;
    [SerializeField] private float gravity = -9.8f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;
        
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        controller.Move(movement);
    }
}
