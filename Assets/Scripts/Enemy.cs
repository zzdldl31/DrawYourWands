using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    LaserHitReceiver laserReceiver;
    public float hp;

    // Start is called before the first frame update
    void Awake()
    {
        laserReceiver = GetComponent<LaserHitReceiver>();
        laserReceiver.OnLaserKeep += (s, e) => Damage(e);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 0)
            Destroy(gameObject);
    }

    public void Damage(PointerEventArgs e)
    {
        hp -= Time.deltaTime;
    }
}
