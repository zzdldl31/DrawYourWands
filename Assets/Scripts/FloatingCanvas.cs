using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingCanvas : MonoBehaviour
{
    public Transform cam;
    public Healthbar healthbar;
    public Text scoreboard;

    int killCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //playerHP = healthbar.maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.position + cam.forward * 1f;
        transform.LookAt(cam);
    }


    public void TakeDamage(int dmg)
    {
        healthbar.TakeDamage(dmg);
    }

    public void AddKillCount()
    {
        scoreboard.text = "Kills: " + (++killCount).ToString();
    }



}
