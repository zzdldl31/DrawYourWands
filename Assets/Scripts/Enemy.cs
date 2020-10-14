using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    CharacterController character;
    LaserHitReceiver laserReceiver;
    public int hp;
    public float defaultSpeed;
    private float speed;

    // Start is called before the first frame update
    void Awake()
    {
        laserReceiver = GetComponent<LaserHitReceiver>();
        character = GetComponent<CharacterController>();
        laserReceiver.OnLaserKeep += (s, e) => Damage(e);
    }

    private void Start()
    {
        speed = defaultSpeed;
    }

    void UpdateHealth(int damage)
    {
        hp -= damage;
    }


    // Update is called once per frame
    void Update()
    {
        if(delayTimer > 0)
            delayTimer -= Time.deltaTime;
        MoveToPlayer();
        if (hp <= 0)
            Destroy(gameObject);
    }

    private void MoveToPlayer()
    {
        var dirVec = (PlayerData.inst.transform.position - transform.position).normalized;
        character.Move(dirVec * speed * Time.deltaTime);
    }

    public float delay = 0.5f;
    float delayTimer;
    public void Damage(PointerEventArgs e)
    {
        if(delayTimer <= 0)
        {
            UpdateHealth(1);
            delayTimer = delay;
        }
    }
}
