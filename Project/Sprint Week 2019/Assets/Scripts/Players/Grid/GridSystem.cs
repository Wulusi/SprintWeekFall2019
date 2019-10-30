using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{

    public float gridHeight, gridWidth;
   
    public GameObject gridDot;

    public List<Transform> gridLocations = new List<Transform>();

    public static GridSystem Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        BuildGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuildGrid()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                GameObject spawnedDot = Instantiate(gridDot);
                spawnedDot.transform.position = new Vector3(i + 0.5f , j + 0.5f);
                gridLocations.Add(spawnedDot.transform);
            }
        }
    }
}
