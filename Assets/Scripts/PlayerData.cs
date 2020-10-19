using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData inst;

    public int maxHp;
    private int hp;
    public int killCount;

    public event Action OnDied;
    public event Action<int> OnDamaged;

    public event Action<int> OnHpUpdated;

    // Start is called before the first frame update
    void Awake()
    {
        inst = this;
    }

    public void ResetForGame()
    {
        hp = maxHp;
        killCount = 0;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        OnDamaged?.Invoke(dmg);
        if (hp <= 0)
            OnDied?.Invoke();
    }

    public void Restore(int value)
    {
        if (hp == maxHp)
            return;

        hp += value;
        if (hp > maxHp)
            hp = maxHp;
        OnHpUpdated?.Invoke(hp);
    }
}
