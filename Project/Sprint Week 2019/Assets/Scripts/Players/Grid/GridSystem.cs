using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{

    public float gridHeight, gridWidth;
   
    public GameObject gridDot;
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
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                GameObject spawnedDot = Instantiate(gridDot);
                spawnedDot.transform.position = new Vector3(i * gridWidth * 0.5f, j * gridHeight * 0.5f);
            }
        }
    }
}
