using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace name
{
    public class PlayerBehavior : MonoBehaviour
    {
        Rigidbody2D rgb;
        [Range(0,5)]
        public float speed;
        public int life;
        bool cantMove;
        bool cantShoot;
        Transform splitPoint;
        Animator animator;
        int attackNumber;

        public GameObject[] attacks = new GameObject[1];
        void Start()
        {
            splitPoint = transform.GetChild(0).transform;
            rgb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            if (!cantMove)
                rgb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (!cantShoot)
                    StartCoroutine(PiouPiou());
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                attackNumber++;
                if (attackNumber == 5)
                    attackNumber = 0;

            }


        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Fire")
            {
                StartCoroutine(damage());
                rgb.velocity = new Vector2(10, 0);
            }
        }
        IEnumerator damage()
        {
            cantMove = true;
            life--;
            animator.SetTrigger("Hurt");
            yield return new WaitForSeconds(0.2f);
            cantMove = false;
        }


        IEnumerator PiouPiou()
        {

            cantShoot = true;
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Split");
            if (attackNumber >= 3)
            {

                Instantiate(attacks[2], splitPoint.position, Quaternion.identity);
                Instantiate(attacks[attackNumber], splitPoint.position, Quaternion.identity);
            }
            else
                Instantiate(attacks[attackNumber], splitPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);

            cantShoot = false;
        }

    }

}