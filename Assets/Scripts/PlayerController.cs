using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float health = 100f;
    [SerializeField]
    public bool amIDead = false;
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    GameObject bullet;
    GameObject tempbullet;
    [SerializeField]
    GameObject nozzle;
    [SerializeField]
    GameObject bulletPool;

    List<GameObject> enemyList;
    Transform enemypool;
    public float bulletForce;
    float fireTime = 0f;
    float fireRate = 0.25f;
    float damageTakeTime = 0;
    float damageTakeInterval = 1;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = new Vector2(0f,0f);
        enemyList = new List<GameObject>();
        enemypool = GameObject.Find("Enemies").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetEnemies();
        Move();
        Aim();
        Fire();
        Die(amIDead);
        ThrowBomb();
        
        
    }
    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * speed;
        float verticalInput = Input.GetAxis("Vertical") * speed;
        if (horizontalInput != 0)
        {
            print("input varr");
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalInput, GetComponent<Rigidbody2D>().velocity.y);
        }
        if (verticalInput != 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, verticalInput);
        }
        if (verticalInput == 0 && horizontalInput == 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
    }
    void Fire()
    {
        if (Input.GetMouseButton(0) && (Time.time - fireTime) > fireRate)
        {
            fireTime = Time.time;
            tempbullet = Instantiate(bullet, nozzle.transform.position, nozzle.transform.rotation, bulletPool.transform);
            tempbullet.GetComponent<Rigidbody2D>().AddForce(nozzle.transform.up*bulletForce);
            Destroy(tempbullet, 5f);
        }
        
    }
    void Aim()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle-90);
    }
    void TakeDamage(float damageAmount)
    {
        if (health > 0)
        {
            print(health);
            health -= damageAmount;
        }
        else if (health <= 0)
        {
            health = 0;
            amIDead = true;
        }

    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if ((Time.time - damageTakeTime) > damageTakeInterval)
            {
                damageTakeTime = Time.time;
                TakeDamage(25);
                print(health);
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shock")
        {
            StartCoroutine(ShockBarrier());
            Destroy(other.gameObject);
        }
        else if (other.tag == "Health")
        {
            if (health == 100f)
            {
                Destroy(other.gameObject);
                return;
            }
            else
            {
                print(health);
                health += 25;
                print(health);
                Destroy(other.gameObject);
            }
            
        }
    }
    void Die(bool amIDead)
    {
        if (amIDead)
        {
            Destroy(gameObject);
        }
    }
    void ThrowBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.GetComponent<ShakeBehaviour>().TriggerShake();
           var gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

            for (var i = 0; i < gameObjects.Length; i++)
            {
                Destroy(gameObjects[i]);
            }
        }
        
    }
    void GetEnemies()
    {
        for (int i = 0; i < enemypool.childCount; i++)
        {
            enemyList.Add(enemypool.GetChild(0).gameObject);
        }
    }
    IEnumerator ShockBarrier() {
        print("şok kalkanı açık");
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        transform.GetChild(0).gameObject.SetActive(false);
    }


}
