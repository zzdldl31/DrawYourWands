using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData inst;

    public int hp;
    public int killCount;

    public event Action OnDied;
    public event Action<int> OnDamaged;

    // Start is called before the first frame update
    void Awake()
    {
        inst = this;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        OnDamaged?.Invoke(hp);
        if (hp <= 0)
            OnDied?.Invoke();
    }
}
