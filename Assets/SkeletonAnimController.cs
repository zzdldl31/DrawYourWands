using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimController : MonoBehaviour
{
    private const float AttackDelay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        var enemy = GetComponent<Enemy>();
        var anim = GetComponent<Animator>();
        enemy.OnAttack += () => anim.SetTrigger("StartAttack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
