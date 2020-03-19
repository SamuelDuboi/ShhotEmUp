using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BabyBehavior : MonoBehaviour
{
    public GameObject rock;
    public int numberOfRocks;
    Rigidbody2D babyrgb;
    GameManager gameManager;
    [Range(1, 10)]
    public float speed;
    public int scoreWhenDestroy;
    public int life;
    public GameObject death;
    void Start()
    {
        babyrgb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            life--;
            if (collision.gameObject.layer == 11)
                Destroy(collision.gameObject);
            if (life <= 0)
            {
                gameManager.ScoreChange(scoreWhenDestroy);

                for (int i = 0; i < numberOfRocks; i++)
                {
                    float radius = numberOfRocks;
                    float angle = i * Mathf.PI * 2f / radius;
                    Vector3 newPos = transform.position + (new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0)) / 20;
                    GameObject rockInstantiate = Instantiate(rock, newPos, Quaternion.Euler(0, 0, 0));
                    Vector2 distanceSpawns = new Vector2(newPos.x - transform.position.x,
                                                         newPos.y - transform.position.y);
                    Rigidbody2D rockRigidbody = rockInstantiate.GetComponent<Rigidbody2D>();
                    rockRigidbody.velocity = distanceSpawns * 10;
                    Instantiate(death, transform.position, Quaternion.identity);
                    
                }
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.name == "Wall")
            babyrgb.velocity = new Vector2(-2, 0) * speed;
    }



}
