using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemyObj;
    public GameObject heart;
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
            waves.Add(new RandomMobWaveNode(3, 5, 0));
            waves.Add(new RandomMountainTopMobWaveNode(20, 1, 1));
            waves.Add(new SpawnHeart());

            waves.Add(new RandomMountainTopMobWaveNode(0, 1, 1));
            waves.Add(new RandomMobWaveNode(3, 5, 0));
            waves.Add(new RandomMobWaveNode(2, 1, 2));
            waves.Add(new RandomMobWaveNode(1, 5, 0));
            waves.Add(new RandomMountainTopMobWaveNode(25, 1, 1));
            waves.Add(new SpawnHeart());

            waves.Add(new RandomMobWaveNode(3, 10, 0));
            waves.Add(new RandomMountainTopMobWaveNode(3, 3, 1));
            waves.Add(new RandomMobWaveNode(2.5f, 6, 0));
            waves.Add(new RandomMobWaveNode(0, 4, 2));
            waves.Add(new RandomMobWaveNode(20, 1, 0));
            waves.Add(new SpawnHeart());

            waves.Add(new RandomMountainTopMobWaveNode(0, 3, 1));
            waves.Add(new RandomMobWaveNode(3, 2, 2));
            waves.Add(new RandomMountainTopMobWaveNode(2, 3, 1));
            waves.Add(new RandomMobWaveNode(0, 1, 0));
            waves.Add(new RandomMobWaveNode(1, 6, 2));
            waves.Add(new RandomMobWaveNode(10, 1, 0));
            waves.Add(new SpawnHeart());

            return waves;
        }
    }

    List<IWaveNode> infWaves
    {
        get
        {
            var waves = new List<IWaveNode>();

            waves.Add(new RandomMobWaveNode(3, 5, 0));
            waves.Add(new RandomMobWaveNode(0, 3, 2));
            waves.Add(new RandomMountainTopMobWaveNode(0, 1, 1));
            waves.Add(new RandomMobWaveNode(2, 5, 0));
            waves.Add(new RandomMobWaveNode(0, 3, 2));
            waves.Add(new RandomMountainTopMobWaveNode(0, 1, 1));
            waves.Add(new RandomMobWaveNode(1, 5, 0));
            waves.Add(new RandomMobWaveNode(0, 3, 2));
            waves.Add(new RandomMountainTopMobWaveNode(10, 1, 1));
            waves.Add(new SpawnHeart());

            return waves;
        }
    }

    Coroutine waveCoroutine;
    public void StartWave()
    {
        waveCoroutine = StartCoroutine(DoWave());
    }

    public void SpawnMob(int enemyCode)
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        Instantiate(enemyCode<0 ? heart : enemyObj[enemyCode],
            spawnDist * new Vector3(-Mathf.Sin(angle), 0, -Mathf.Cos(angle)),
            Quaternion.Euler(new Vector3(0, angle * 180 / Mathf.PI, 0)), this.transform);

        mobNum += 1;
    }

    public void SpawnMobLookingPlayer(int enemyCode, Vector3 pos)
    {
        var go = Instantiate(enemyObj[enemyCode], transform);
        go.transform.position = pos;
        go.transform.LookAt(PlayerData.inst.transform);

        mobNum += 1;
    }

    public void EndWave()
    {
        foreach (Transform obj in transform)
        {
            Destroy(obj.gameObject);
        }

        if(waveCoroutine != null)
            StopCoroutine(waveCoroutine);
    }

    int cycle = 1;
    IEnumerator DoWave()
    {
        foreach (var wave in waves)
        {
            yield return wave.Execute(1);
        }

        while (true)
        {
            foreach(var wave in infWaves)
            {
                yield return wave.Execute(1);
            }
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

public class SpawnHeart : IWaveNode
{
    public IEnumerator Execute(int cycle)
    {
        WaveManager.inst.SpawnMob(-1);
        yield return null;
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