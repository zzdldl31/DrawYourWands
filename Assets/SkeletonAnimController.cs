using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimController : MonoBehaviour
{
    private const float AttackDelay = 0.5f;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        var enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();

        enemy.OnAttack += () => anim.SetTrigger("StartAttack");
        enemy.OnDie += (go) => StartCoroutine(OnDie(go));
    }

    private IEnumerator OnDie(GameObject go)
    {
        anim.SetTrigger("Die");
        yield return null;
        var clipInfos = anim.GetCurrentAnimatorClipInfo(0);
        var currentClipInfo = clipInfos[0];
        yield return new WaitForSeconds(currentClipInfo.clip.length);
        Destroy(go);
    }
}
