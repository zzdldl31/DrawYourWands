using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    CharacterController character;
    LaserHitReceiver laserReceiver;
    public float hp;
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

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
        if (hp < 0)
            Destroy(gameObject);
    }

    private void MoveToPlayer()
    {
        var dirVec = (PlayerData.inst.transform.position - transform.position).normalized;
        character.Move(dirVec * speed * Time.deltaTime);
    }

    public void Damage(PointerEventArgs e)
    {
        hp -= Time.deltaTime;
    }
}
