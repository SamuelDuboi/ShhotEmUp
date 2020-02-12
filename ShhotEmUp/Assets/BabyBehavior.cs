using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace name
{
public class BabyBehavior : MonoBehaviour
{
        Rigidbody2D babyrgb;
    void Start()
    {
            babyrgb = GetComponent<Rigidbody2D>();
            babyrgb.velocity = new Vector2(-2, 0);
    }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(gameObject);
        }


    }
}