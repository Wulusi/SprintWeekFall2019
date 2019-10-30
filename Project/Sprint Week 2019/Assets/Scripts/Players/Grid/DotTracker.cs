using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotTracker : MonoBehaviour
{
    public bool isOccupied;

    public GridSystem gridSystem;
    // Start is called before the first frame update
    void Start()
    {
        gridSystem = GridSystem.Instance;
        isOccupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(waitToGetDots());
    }

    private IEnumerator waitToGetDots()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log(this.gameObject.name + "Added to GridSystem");
        gridSystem.gridLocations.Add(this.gameObject.transform);
    }

    private void OnDisable()
    {
        Debug.Log(this.gameObject.name + "Removed from GridSystem");
        gridSystem.gridLocations.Remove(this.gameObject.transform);
    }
}
