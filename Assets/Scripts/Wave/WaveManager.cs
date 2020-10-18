using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemyObj;
    public int spawnDist;
    private int mobNum;

    public static WaveManager inst;
    private void Awake()
    {
        inst = this;
    }

    List<IWaveNode> waves
    {
        get
        {
            var waves = new List<IWaveNode>();
            waves.Add(new RandomMountainTopMobWaveNode(30, 1, 1));
            waves.Add(new RandomMobWaveNode(2, 5, 0));
            waves.Add(new RandomMobWaveNode(1, 5, 0));
            //waves.Add();

            return waves;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoWave());
    }

    public void SpawnMob(int enemyCode)
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        Instantiate(enemyObj[enemyCode],
            spawnDist * new Vector3(-Mathf.Sin(angle), 0, -Mathf.Cos(angle)),
            Quaternion.Euler(new Vector3(0, angle * 180 / Mathf.PI, 0)), this.transform);

        mobNum += 1;
    }

    public void SpawnMobLookingPlayer(int enemyCode, Vector3 pos)
    {
        var go = Instantiate(enemyObj[enemyCode], transform);
        go.transform.position = pos;
        go.transform.LookAt(PlayerData.inst.transform);
    }

    int cycle = 1;
    IEnumerator DoWave()
    {
        foreach (var wave in waves)
        {
            yield return wave.Execute(1);
        }
    }
}

public interface IWaveNode
{
    IEnumerator Execute(int cycle);
}


public class RandomMobWaveNode : IWaveNode
{
    float spawnTerm;
    int enemyNum;
    int enemyCode;

    public RandomMobWaveNode(float spawnTerm, int enemyNum, int enemyCode)
    {
        this.spawnTerm = spawnTerm;
        this.enemyNum = enemyNum;
        this.enemyCode = enemyCode;
    }

    public IEnumerator Execute(int cycle)
    {
        for (int i = 0; i < enemyNum; i++)
        {
            WaveManager.inst.SpawnMob(enemyCode);
            yield return new WaitForSeconds(spawnTerm);
        }
    }
}

public class RandomMountainTopMobWaveNode : IWaveNode
{
    float spawnTerm;
    int enemyNum;
    int enemyCode;

    public RandomMountainTopMobWaveNode(float spawnTerm, int enemyNum, int enemyCode)
    {
        this.spawnTerm = spawnTerm;
        this.enemyNum = enemyNum;
        this.enemyCode = enemyCode;
    }

    public IEnumerator Execute(int cycle)
    {
        for (int i = 0; i < enemyNum; i++)
        {
            var tops = TerrainPointManager.inst.mountainTops;
            var index = Random.Range(0, tops.Length - 1);
            WaveManager.inst.SpawnMobLookingPlayer(enemyCode, tops[index].position);
            yield return new WaitForSeconds(spawnTerm);
        }
    }
}

