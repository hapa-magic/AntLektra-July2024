using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeNest : MonoBehaviour
{
    [SerializeField] GameObject _antPrefab;
    [SerializeField] GameObject _beetleAntPrefab;
    [SerializeField] GameObject _mantisAntPrefab;
    [SerializeField] GameObject _robotAntPrefab;
    [SerializeField] GameObject spawnParent;
    GameObject pheremoneStart;
    GridCell thisCell;



    // Start is called before the first frame update
    void Start()
    {
        thisCell = GetComponent<GridCell>();
        pheremoneStart = thisCell.connectedCells[0];
    }

    // Update is called once per frame
    void Update()
    {
        // if ()
    }
    public void SpawnAnt(GameObject ant) {
        GameObject newAnt = Instantiate(ant, transform.position, Quaternion.identity, spawnParent.transform);
        AntController antControl = newAnt.GetComponent<AntController>();
        antControl.targetObj = pheremoneStart;
    }
}
