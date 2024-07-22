using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HapaMagic;
using UnityEngine.EventSystems;

public class HomeNest : MonoBehaviour
{
    [SerializeField] public GameObject _antPrefab;
    [SerializeField] public GameObject _honeyAntPrefab;
    [SerializeField] public GameObject _beetleAntPrefab;
    [SerializeField] public GameObject _mantisAntPrefab;
    [SerializeField] public GameObject _robotAntPrefab;
    [SerializeField] GameObject spawnParent;
    public GameObject gameManager;
    private DetectMouseOnGameObj detectMouse;
    public float spawnWaitTime;
    GameObject pheremoneStart;
    GridCell thisCell;
    public List<Transform> cells = new List<Transform>();
    public List<GameObject> spawningCards = new List<GameObject>(); 
    private bool upsideDown = false;
    public int pheremoneState = 1;

    // Start is called before the first frame update
    void Start()
    {
        thisCell = GetComponent<GridCell>();
        detectMouse = gameManager.GetComponent<DetectMouseOnGameObj>();
        StartNewTrail();
    }
    private void Update()
    {
        if (pheremoneState == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckForPheremoneTrailAdd();
            } else if (Input.GetMouseButtonDown(1))
            {
                EndPheremoneTrailing();
            }
        }
    }


    // Update is called once per frame

    public IEnumerator SpawnAnt(GameObject ant, int power, PlaySpot playSpot) {
        for (int i = 0; i < power; ++i) {
            GameObject newAnt = Instantiate(ant, transform.position, Quaternion.identity, spawnParent.transform);
            if (!upsideDown)
            {
                upsideDown = true;
            } else {
                newAnt.GetComponent<AntController>().FlipAnt();
                upsideDown = false;
            }
            AntController antControl = newAnt.GetComponent<AntController>();
            antControl.targetObj = cells[1].gameObject;
            antControl.antState = 2;
            yield return new WaitForSeconds(spawnWaitTime);
        }
        playSpot.Discard();
    }
    
    public void StartNewTrail()
    {
        RemoveTrail();
        pheremoneState = 1;
        GetComponent<GridCell>().SetPheremoneIndicator();
        cells.Add(this.transform);
    }
    private void CheckForPheremoneTrailAdd()
    {
        GridCell nextCell = detectMouse.GetGameObjFromClick().GetComponent<GridCell>();
        if (nextCell != null)
        {
            if (nextCell.cellState == 1) // add cell to list regularly
            {
                cells[cells.Count - 1].GetComponent<GridCell>().nextCell = nextCell.gameObject;
                nextCell.previousCell = cells[cells.Count - 1].gameObject;
                cells[cells.Count - 1].GetComponent<GridCell>().RemovePheremoneIndicator();
                cells.Add(nextCell.transform);
                nextCell.SetPheremoneIndicator();
            } else if (nextCell.cellState == 2 && nextCell.connectedCells.Contains(cells[cells.Count - 1].gameObject)) // if it's in the list of cells
            {
                if (cells[cells.Count - 1] == nextCell.transform || cells[cells.Count - 2] == nextCell.transform) // if it's the last couple cells
                {
                    EndPheremoneTrailing();
                } else
                {
                    nextCell.previousCell = cells[cells.Count - 1].gameObject;
                    cells[cells.Count - 1].GetComponent<GridCell>().nextCell = nextCell.gameObject;
                    cells.Add(nextCell.transform);
                    EndPheremoneTrailing();
                }
            }
        }
    }
    public void RemoveTrail()
    {
        foreach (Transform go in cells)
        {
            go.GetComponent<GridCell>().ErasePheremoneTrail();
        }
        cells.Clear();
    }
    public void EndPheremoneTrailing()
    {
        foreach (Transform go in cells)
        {
            go.GetComponent<GridCell>().RemovePheremoneIndicator();
        }
        Debug.Log("Ending pheremone state");
        pheremoneState = 0;
    }
    public void PlayInstant(Effect.EffectAbility effectAbility, PlaySpot playSpot)
    {
        switch (effectAbility)
        {
            case Effect.EffectAbility.None:
                break;

            case Effect.EffectAbility.HoneyAnt:
                gameManager.GetComponent<GameManager>().eggIncome += 1;
                break;

            case Effect.EffectAbility.Draw:
                // draw function here
                gameManager.GetComponent<GameManager>().Draw();
                break;

            // case Effect.EffectAbility.Discard:
                

                //case Effect.EffectAbility.Cycle:

      
        }
        playSpot.Discard();
    }
}
