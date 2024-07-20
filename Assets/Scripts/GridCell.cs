using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    // public Vector2 gridIndex;
    public bool cellFull = false;
    public bool fogOfWar;
    public int cellState = 0;
    public List<GameObject> connectedCells = new List<GameObject>();
    public GameObject objectInCell;
    public GameObject nextCell;
    public GameObject previousCell;
    public void SetPheremoneIndicator(List<Transform> trailList)
    {
        cellState = 2;
        foreach (GameObject go in connectedCells)
        {
            if (go.GetComponent<GridCell>().nextCell != this.gameObject) {
                go.GetComponent<GridCell>().cellState = 1;
            }
        }
        trailList.Add(this.transform);
    }
    public void ErasePheremoneTrail()
    {
        nextCell = null;
        previousCell = null;
    }

}
