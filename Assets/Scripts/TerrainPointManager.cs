using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPointManager : MonoBehaviour
{
    public static TerrainPointManager inst;
    private void Awake()
    {
        inst = this;
    }

    public Transform[] mountainTops;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
