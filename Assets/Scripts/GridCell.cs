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
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        } else
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }


    public void SetPheremoneIndicator()
    {
        if (cellState != 2)
        {
            SetCellState(2);
            foreach (GameObject go in connectedCells)
            {
                if (go.GetComponent<GridCell>().cellState == 0)
                {
                    go.GetComponent<GridCell>().SetCellState(1);
                }
            }
        }
    }
    public void RemovePheremoneIndicator()
    {
        foreach (GameObject go in connectedCells)
        {
            if (go.GetComponent<GridCell>().cellState == 1)
            {
                go.GetComponent<GridCell>().SetCellState(0);
            }
        }
    }
    public void ErasePheremoneTrail()
    {
        RemovePheremoneIndicator();
        nextCell = null;
        previousCell = null;
        SetCellState(0);
    }

    public void SetCellState(int cellState)
    {
        this.cellState = cellState;
        switch (cellState)
        {
            case 0:
                spriteRenderer.color = Color.white;
                break;

            case 1:
                spriteRenderer.color = Color.blue;
                break;

            case 2:
                spriteRenderer.color = Color.magenta;
                break;
        }
    }

}
