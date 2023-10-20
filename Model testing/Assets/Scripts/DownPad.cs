using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPad : MonoBehaviour
{
    public float Speed;
    PlayerMovement Dash;

    private void Awake()
    {
        Dash = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider mycollision)
    {
        if (mycollision.CompareTag("Player"))
        {
            Dash.maximumSpeed = 15f;
            Dash.maximumSpeed = Dash.maximumSpeed * Speed;

        }

    }
}
