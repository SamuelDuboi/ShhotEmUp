using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace name
{
public class CameraBehavior : MonoBehaviour
{
        Rigidbody2D camrgb;   
    void Start()
    {
            camrgb = GetComponent<Rigidbody2D>();
            
    }

    void FixedUpdate()
	{
            camrgb.velocity = new Vector2(1, 0);
        }


    void Update()
    {
        
    }
}
}