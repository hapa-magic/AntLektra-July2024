using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    // public Vector2 gridIndex;
    public bool cellFull = false;
    public bool fogOfWar;
    public List<GameObject> connectedCells = new List<GameObject>();
    public GameObject objectInCell;
}
