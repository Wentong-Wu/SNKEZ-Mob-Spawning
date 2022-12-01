using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[SerializeField] int health;
    //[SerializeField] int attackPower;

    Transform player;

    private float moveSpeed = 1.0f;
    private Vector2 movement;
    Vector3 dir;

    Vector3 randomWayPoint;
    Vector3 currentWayPoint;
    [SerializeField] Vector3[] wayPoints;

    //Time to wait before going into a random position
    float timeToWait = 3.0f;
    float currentTime = 0.0f;

    private float timeBetweenShots;
    private float startTimeBetweenShots = 5.0f;
    private int randomEnemy;
    public GameObject bullets;
    Rigidbody2D rb;
    private ObjectPool pool;
    SpriteRenderer enemyColour;
    // Start is called before the first frame update
    void Start()
    {
        pool = FindObjectOfType<ObjectPool>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeBetweenShots = startTimeBetweenShots;
        currentTime = timeToWait; //Used for random movement
        randomWayPoint = wayPoints[Random.Range(0, wayPoints.Length)];
        currentWayPoint = randomWayPoint;
        randomEnemy = Random.Range(0, 4);
        switch(randomEnemy)
        {
            case 0:
                transform.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 1:
                transform.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case 2:
                transform.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 3:
                transform.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
        }
        enemyColour = transform.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (enemyColour.color == Color.red)
        {
            followPlayerEnemy();
        }
        else if (enemyColour.color == Color.white)
        {
            shooterEnemy();
        }
        else if (enemyColour.color == Color.green)
        {
            wayPointEnemy();
        }
        else if (enemyColour.color == Color.yellow)
        {
            randomMovement();
        }

        //Kill All Enemy
        if (Input.GetKeyDown("x"))
        {
            DestroyEnemy();
        }
    }
    void moveEnemy(Vector2 direction)
    {
        //rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;
    }

    public void shooterEnemy()
    {
        dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;
        if (timeBetweenShots <= 0)
        {   
            Instantiate(bullets, transform.position, Quaternion.identity, gameObject.transform);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
    public void followPlayerEnemy()
    {
        dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;
        movement = dir;
        moveEnemy(movement);
    }
    public void randomMovement()
    {
        if (currentTime < 0.2f)
        {
            dir = (new Vector3(Random.Range(-Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.width)).x, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.width)).x), Random.Range(-Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y))) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90;
            currentTime = timeToWait;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
        movement = dir;
        moveEnemy(movement);
    }
    public void wayPointEnemy()
    {
        dir = currentWayPoint - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;
        movement = dir;
        moveEnemy(movement);
        if(Vector2.Distance(transform.position, currentWayPoint) < 1.0f )
        {
            if (currentWayPoint == randomWayPoint)
                currentWayPoint = wayPoints[Random.Range(0,wayPoints.Length)];
            else
                currentWayPoint = randomWayPoint;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            DestroyEnemy();
            //TakeHealth(attackPower);
        }        
    }
    /*
    void TakeHealth(int amount)
    {
        health -= amount;
        Debug.Log(health);
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }
    */
    void DestroyEnemy()
    {
        if (pool == null)
            Destroy(gameObject);
        else
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            timeBetweenShots = startTimeBetweenShots;
            pool.ReturnObject(gameObject);
        }
    }
    public void Init(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }
}
