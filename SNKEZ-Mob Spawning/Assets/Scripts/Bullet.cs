using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    private Transform player;
    private Vector2 moveDirection;
    public float bulletSpeed = 1.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        moveDirection = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") || collision.gameObject.name.Contains("Wall"))
            DestroyBullet();
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
