using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterWalker : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float speed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        controller.Move(new Vector3(horizontal, 0, vertical) * speed);
    }
}
