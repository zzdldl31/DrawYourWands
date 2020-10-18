using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    CharacterController character;
    LaserHitReceiver laserReceiver;

    [SerializeField]
    private int hp;
    
    public int Hp
    {
        get => hp;
        set {
            hp = value;
            OnHpChanged?.Invoke(hp);
            if (hp <= 0)
                Kill();
        }
    }

    public float defaultSpeed;
    public float attackRange = 2;
    private float speed;

    public float firstAttackDelay;
    public float secondAttackDelay;
    public int attackPower;

    bool isAttacking = false;

    public static event Action OnAnyEnemyDie;

    public event Action OnAttack;
    public event Action<int> OnHpChanged;
    public event Action<GameObject> OnDie;

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

    // Update is called once per frame
    void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
            return;
        }

        if (Vector3.Distance(PlayerData.inst.transform.position, transform.position) < attackRange)
        {
            if (!isAttacking)
            {
                StartCoroutine(Attack());
                isAttacking = true;
            }
        }
        else
            MoveToPlayer();
    }

    IEnumerator Attack()
    {
        OnAttack?.Invoke();
        yield return new WaitForSeconds(firstAttackDelay);
        while (true)
        {
            OnAttack?.Invoke();
            PlayerData.inst.TakeDamage(attackPower);
            yield return new WaitForSeconds(secondAttackDelay);
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
            var dir = (PlayerData.inst.transform.position - transform.position).normalized;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public float delay = 0.5f;
    float delayTimer;

    public void Kill()
    {
        if (OnDie != null)
            OnDie?.Invoke(gameObject);
        else Destroy(gameObject);
        OnAnyEnemyDie?.Invoke();
    }

    public void Damage(PointerEventArgs e)
    {
        if(delayTimer <= 0)
        {
            Hp--;
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