using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrapnelle : MonoBehaviour
{
    public GameObject rock;
    public GameObject direction;


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction.transform.position, 10 * Time.deltaTime);
        if (transform.position.x <= direction.transform.position.x)
            Dstruction();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dstruction();
        }
    }
    void Dstruction()
    {
        for (int i = 0; i < 10; i++)
        {
            float radius = 10;
            float angle = i * Mathf.PI * 2f / radius;
            Vector3 newPos = transform.position + (new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0)) / 20;
            GameObject rockInstantiate = Instantiate(rock, newPos, Quaternion.Euler(0, 0, 0));
            Vector2 distanceSpawns = new Vector2(newPos.x - transform.position.x,
                                                 newPos.y - transform.position.y);
            Rigidbody2D rockRigidbody = rockInstantiate.GetComponent<Rigidbody2D>();
            rockRigidbody.velocity = distanceSpawns * 10;
            Destroy(gameObject);
        }
    }
}
