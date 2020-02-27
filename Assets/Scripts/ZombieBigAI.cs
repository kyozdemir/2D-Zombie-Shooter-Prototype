using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBigAI : MonoBehaviour
{
    public float health = 125f;
    public float speed = 1f;
    public bool amIDead = false;
    public float damage = 40;
    
    Transform Target;
    [SerializeField]
    public GameObject subZombie;
    [SerializeField]
    List<GameObject> lootableObjects;
    GameObject tempZombie1;
    GameObject tempZombie2;
    Transform enemyPool;
    GameObject tempLootableObject;
    float dropChance = 25f;
    

    public void Die(bool amIDead)
    {
        if (amIDead)
        {
            Destroy(gameObject);
            tempZombie1 = Instantiate(subZombie, new Vector2(transform.position.x-1, transform.position.y), transform.rotation,enemyPool);
            tempZombie2 = Instantiate(subZombie, new Vector2(transform.position.x+1, transform.position.y), transform.rotation,enemyPool);
            float chance = Random.Range(0f,100f);
            if (chance < dropChance)
            {
                tempLootableObject = Instantiate(lootableObjects[Random.Range(0, lootableObjects.Count)], transform.position, Quaternion.identity);
            }
            
        }
    }

    

    public void TakeDamage(float damageAmount)
    {
        if (health > 0)
        {
            print(health);
            health -= damageAmount;
        }
        else if (health<= 0)
        {
            health = 0;
            amIDead = true;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player").transform;
        enemyPool = GameObject.Find("Enemies").transform;
        lootableObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
        Die(amIDead);
    }
    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Bullet")
        {
            amIDead = true;
            Destroy(other.gameObject);
        }
        if (other.tag == "ShockBarrier")
        {
            amIDead = true;

        }
    }
}
