using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    CharacterController character;
    LaserHitReceiver laserReceiver;

    //public int hp;
    FloatingCanvas player;
    public float defaultSpeed;
    public float attackRange;
    private float speed;
    public Healthbar healthbar;

    bool isAttacking = false;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("FloatingCanvas").GetComponent<FloatingCanvas>();
        laserReceiver = GetComponent<LaserHitReceiver>();
        character = GetComponent<CharacterController>();
        laserReceiver.OnLaserKeep += (s, e) => Damage(e);
    }

    private void Start()
    {
        speed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthbar.health <= healthbar.minimumHealth)
        {
            Destroy(gameObject);
            player.AddKillCount();
        }

        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
            return;
        }


        if (Vector3.Distance(PlayerData.inst.transform.position, transform.position) < attackRange)
        {
            if (!isAttacking)
            {
                StartCoroutine("Attack");
                isAttacking = true;
            }
        }
        else
            MoveToPlayer();
    }

    public event Action OnAttack;
    IEnumerator Attack()
    {
        OnAttack?.Invoke();
        yield return new WaitForSeconds(0.95f);
        while (true)
        {
            OnAttack?.Invoke();
            player.TakeDamage(5);
            yield return new WaitForSeconds(1.5f);
        }
    }

    public MoveType moveType;

    private void MoveToPlayer()
    {
        if (moveType == MoveType.Walk)
        {
            var dirVec = (PlayerData.inst.transform.position - transform.position).normalized;
            character.Move(dirVec * speed * Time.deltaTime);
        }
        else if (moveType == MoveType.Flying)
        {
            //var dir = (PlayerData.inst.transform.position - transform.position).normalized;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public float delay = 0.5f;
    float delayTimer;
    public void Damage(PointerEventArgs e)
    {
        if(delayTimer <= 0)
        {
            healthbar.TakeDamage(1);
            delayTimer = delay;
        }
    }
}

public enum AttackType
{
    Default,
    SelfDestruct,
}

public enum MoveType
{
    Walk,
    Flying,
}