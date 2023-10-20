using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float Bounce;
    PlayerMovement characterController;

    private void Awake()
    {
        characterController = GameObject.Find ("Player").GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider mycollision)
    {
        if (mycollision.CompareTag("Player"))
        {
            characterController.ySpeed = 5f;
            characterController.ySpeed = characterController.ySpeed * Bounce;
            
        }

    }
}
