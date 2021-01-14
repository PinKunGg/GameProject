﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float h,v;
    private Rigidbody2D rb;
    [SerializeField]private float speed = 7f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }
    void Update()
    {
        if(GM.GMinstanse.GetisPC == true)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(h * speed,v * speed);
        }
        else
        {
            /*
            if(RaycastBuildZone.RCBZinstanse.GetBuildMode == false)
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
                rb.velocity = new Vector2(h * speed,v * speed);
            }
            */
        }
    }
}
