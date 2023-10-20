using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] float spawn;
    public float threshold;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = new Vector3(spawn, spawn, spawn); 
        }

    }
}
