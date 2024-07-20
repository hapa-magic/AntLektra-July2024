using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeNest : MonoBehaviour
{
    [SerializeField] public GameObject _antPrefab;
    [SerializeField] public GameObject _honeyAntPrefab;
    [SerializeField] public GameObject _beetleAntPrefab;
    [SerializeField] public GameObject _mantisAntPrefab;
    [SerializeField] public GameObject _robotAntPrefab;
    [SerializeField] GameObject spawnParent;
    public float spawnWaitTime;
    GameObject pheremoneStart;
    GridCell thisCell;
    public List<Transform> cells = new List<Transform>();



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
    public IEnumerator SpawnAnt(GameObject ant, int power, PlaySpot playSpot) {
        for (int i = 0; i < power; ++i) {
            GameObject newAnt = Instantiate(ant, transform.position, Quaternion.identity, spawnParent.transform);
            AntController antControl = newAnt.GetComponent<AntController>();
            antControl.targetObj = pheremoneStart;
            yield return new WaitForSeconds(spawnWaitTime);
        }
        playSpot.Discard();
    }
}
