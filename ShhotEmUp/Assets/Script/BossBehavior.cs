using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public GameObject shrapnelle;
    public GameObject bullet;
    public float timerMax = 5;
    float timer;
    public int life;
    Rigidbody2D enfantrgb;
    GameManager gameManager;
    public int scoreWhenDestroy;
    bool canMove;
    bool movingBack;
    public GameObject[] target = new GameObject[2];
    public GameObject canonEdge;
    public GameObject rock;
    public GameObject[] canons = new GameObject[2];
    Animator bossAnimator;
    public GameObject death;
    // Start is called before the first frame update
    void Start()
    {

        enfantrgb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        bossAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timerMax)
        {
            int random = Random.Range(1, 3);
            if (random == 1)
            {
                StartCoroutine(Pattern1());
                timer = 0;
            }
            else
            {
                StartCoroutine(Pattern2());
                timer = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (canMove && !movingBack)
            enfantrgb.velocity = Vector2.right;
        else if (canMove && movingBack)
            enfantrgb.velocity = Vector2.right * 2;
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
                Instantiate(death, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

        }
        else if (collision.gameObject.name == "Wall")
        {
            Debug.Log(canMove);
            StartCoroutine(Moving());
            movingBack = false;
        }

    }


    IEnumerator Moving()
    {
        yield return new WaitForSeconds(1.5f);
        canMove = true;
        yield return new WaitForSeconds(Random.Range(7, 12));
        enfantrgb.velocity = Vector2.zero;
        canMove = false;
        yield return new WaitForSeconds(4f);
        movingBack = true;
        canMove = true;
    }

    IEnumerator Pattern1()
    {
        bossAnimator.SetTrigger("IsShooting");
        yield return new WaitForSeconds(0.3f);
        GameObject currentShrapnelle = Instantiate(shrapnelle, canonEdge.transform);
        int firstRandom = Random.Range(0, target.Length - 1);
        currentShrapnelle.GetComponent<Shrapnelle>().direction = target[firstRandom];
        yield return new WaitForSeconds(0.5f);
        GameObject newshrapnelle = Instantiate(shrapnelle, canonEdge.transform);
        int random = (Random.Range(0, target.Length - 1));
        while (random == firstRandom)
            random = (Random.Range(0, target.Length - 1));
        newshrapnelle.GetComponent<Shrapnelle>().direction = target[random];
    }

    IEnumerator Pattern2()
    {
        for (int x= 0; x<4; x++)
        {
            for (int z =0; z<2; z++)
            {
                for (int i = 0; i < 20; i++)
                {
                    float radius = 20;
                    float angle = i * Mathf.PI / radius*2;
                    Vector3 newPos =canons[z].transform.position + (new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0)) / 20;
                    GameObject rockInstantiate = Instantiate(rock, newPos, Quaternion.Euler(0, 0, 0));
                    Vector2 distanceSpawns = new Vector2(newPos.x - canons[z].transform.position.x,
                                                         newPos.y - canons[z].transform.position.y);
                    Rigidbody2D rockRigidbody = rockInstantiate.GetComponent<Rigidbody2D>();
                    rockRigidbody.velocity = distanceSpawns * 10;
                    if (rockRigidbody.velocity.x > 2)
                        Destroy(rockInstantiate);
                }
            }
            
            yield return new WaitForSeconds(0.3f);
        }
        
    }
}
