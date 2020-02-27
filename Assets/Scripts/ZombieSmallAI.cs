using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSmallAI : MonoBehaviour
{
    public float health = 100f;
    public float speed = 3f;
    public bool amIDead = false;
    public float damage = 25;

    Transform Target;

    [SerializeField]
    List<GameObject> lootableObjects;
    GameObject tempLootableObject;
    float dropChance = 25f;


    public void Die(bool amIDead)
    {
        if (amIDead)
        {
            Destroy(gameObject);
            float chance = Random.Range(0f, 100f);
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
        else if (health <= 0)
        {
            health = 0;
            amIDead = true;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player").transform;
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
