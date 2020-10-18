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
        enemy.OnHpChanged += (hp) => healthbar.SetHealth(hp);
    }
}
