using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianBehavior : MonoBehaviour
{
    Rigidbody2D enfantrgb;
    GameManager gameManager;
    public int scoreWhenDestroy;
    [Range(0, 10)]
    public float speed;
    public float cooldown;
    float direction = 10;
    bool isMoving;
    public GameObject rock;
    [Range(1, 10)]
    public int bulletSpeed;
    bool isFire;
    bool isUpDown;
    public int life;
    public float timeBetweenRocks;
    public GameObject death;
    public GameObject spit;
    void Start()
    {
        enfantrgb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (isMoving)
            enfantrgb.velocity = new Vector2(1, 1 * direction)*speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            life--;
            if (collision.gameObject.layer == 11)
                Instantiate(spit, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            if (life <= 0)
            {

                gameManager.ScoreChange(scoreWhenDestroy);
                Instantiate(death, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

        }
        else if (collision.gameObject.name == "Wall")
        {
           
            if (!isUpDown)
                StartCoroutine(UpDown());
            if (!isFire)
                StartCoroutine(Fire());
        }

    }


    IEnumerator UpDown()
    {
        isUpDown = true;
        for (int i = 0; i < 10000; i++)
        {
            yield return new WaitForSeconds(0.5f);
            direction = -direction;
        }
    }

    IEnumerator Fire()
    {
        isFire = true;
        yield return new WaitForSeconds(2);
        isMoving = true;
        for (int i = 0; i < 1000; i++)
        {
            GameObject currentRock = Instantiate(rock, transform.position, Quaternion.identity);
            currentRock.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed;
            yield return new WaitForSeconds(cooldown);
        }
    }
}
