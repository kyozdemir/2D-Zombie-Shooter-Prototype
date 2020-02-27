using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombie
{

    float health { get; set; }
    float speed { get; set; }
    bool amIDead { get; set; }
    float damage { get; set; }
    [SerializeField]
    GameObject Target { get; set; }
    [SerializeField]
    GameObject spawnZombie { get; set; }


    void MakeDamage(float damage);
    void TakeDamage();
    void Die(bool amIDead);
}
