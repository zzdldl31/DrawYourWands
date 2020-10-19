﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Activate()
    {
        anim.SetTrigger("StartGame");

    }

    public void ResetTime()
    {
        anim.SetTrigger("EndGame");
    }
}
