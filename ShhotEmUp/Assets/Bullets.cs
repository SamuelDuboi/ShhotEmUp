using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace name
{
    public class Bullets : MonoBehaviour
    {
        Rigidbody2D bulletRgb;
        void Start()
        {
            bulletRgb = GetComponent<Rigidbody2D>();
            bulletRgb.velocity = new Vector2(10, 0);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
            Destroy(gameObject);
        }

    }
}