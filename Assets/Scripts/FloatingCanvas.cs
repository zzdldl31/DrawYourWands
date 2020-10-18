using DYW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingCanvas : MonoBehaviour
{
    public Transform cam;
    public Healthbar healthbar;
    public Text scoreboard, textCenter;

    public int killCount = 0;

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
        GameManager.Instance.Pulse(0.01f, 150, 75);

        if (healthbar.health <= 0)
        {
            healthbar.healthPerSecond = 0;
            GameManager.Instance.ChangeGameState(0);
        }

    }

    public void AddKillCount()
    {
        scoreboard.text = "Kills: " + (++killCount).ToString();
    }


    public void SetTextCenter(string str)
    {
        textCenter.text = str;
    }
    public void MoveScoreBoard(int ypos)
    {
        Vector3 curpos = scoreboard.transform.localPosition;
        curpos.y = ypos;
        scoreboard.transform.localPosition = curpos;
    }

}
