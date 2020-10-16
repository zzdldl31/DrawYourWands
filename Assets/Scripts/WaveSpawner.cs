using DYW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] enemyObj;
    public float spawnperiod;
    public float spawndist;
    float peacetime = 0;
    int mobNum = 0;
    
    void SpawnMob(int enemyNum)
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        Instantiate(enemyObj[enemyNum],
            spawndist * new Vector3(-Mathf.Sin(angle), 0, -Mathf.Cos(angle)), 
            Quaternion.Euler(new Vector3(0, angle*180/Mathf.PI, 0)), this.transform);

        mobNum += 1;
        switch (mobNum)
        {
            case 5:
            case 15:
                spawnperiod -= 1;
                break;
            case 30:
            case 35:
                spawnperiod -= 0.2f;
                break;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameState == 0)
        {
            peacetime = 0;
            return;
        }
        peacetime += Time.deltaTime;

        if (peacetime > spawnperiod)
        {
            peacetime -= spawnperiod;
            SpawnMob(0);// Destroy(this.gameObject);
        }


    }
}
