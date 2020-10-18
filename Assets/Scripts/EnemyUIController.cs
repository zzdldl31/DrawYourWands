using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyUIController : MonoBehaviour
{
    public Healthbar healthbar;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        healthbar.maximumHealth = enemy.Hp;
        healthbar.health = enemy.Hp;
        enemy.OnHpChanged += (hp) => healthbar.SetHealth(hp);
    }
}
