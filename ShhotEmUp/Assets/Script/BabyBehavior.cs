using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BabyBehavior : MonoBehaviour
{
    Rigidbody2D babyrgb;
    GameManager gameManager;
    public int scoreWhenDestroy;
    void Start()
    {
        babyrgb = GetComponent<Rigidbody2D>();
        babyrgb.velocity = new Vector2(-2, 0);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.ScoreChange(scoreWhenDestroy);
        Destroy(gameObject);
    }


}
