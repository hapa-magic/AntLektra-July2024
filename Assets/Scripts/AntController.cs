using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{
    public int health;
    public int attack;
    public float moveSpeed = 0.5f;
    public GameObject targetObj;
    private Vector2 moveVector;
    public int antState = 0;
    private Transform antSprite;
    public SpriteRenderer unitSprite;
    // Start is called before the first frame update
    private void Awake()
    {
        antSprite = transform.Find("Sprite");
        unitSprite = antSprite.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        // position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (antState) {
            case 0:
                if (unitSprite.flipX == false && transform.position.x > targetObj.transform.position.x)
                {
                    FindNextTarget();
                } else if (unitSprite.flipX == true && transform.position.x < targetObj.transform.position.x)
                {
                    FindNextTarget();
                }
                transform.Translate(moveVector * moveSpeed * Time.deltaTime);
                break;

            case 1:
                if (unitSprite.flipX == false && transform.position.x > targetObj.transform.position.x)
                {
                    FindPrevTarget();
                }
                else if (unitSprite.flipX == true && transform.position.x < targetObj.transform.position.x)
                {
                    FindPrevTarget();
                }
                transform.Translate(moveVector * moveSpeed * Time.deltaTime);
                break;

            case 2: 
                if (targetObj != null)
                {
                    moveVector = new Vector2(targetObj.transform.position.x - transform.position.x, targetObj.transform.position.y - transform.position.y).normalized;
                    antState = 0;
                }
                break;
        }
    }

    public void FlipAnt()
    {
        unitSprite.flipY = true;
        antSprite.transform.position = transform.position + new Vector3(0, -.3f, 0);
    }
    private void FindNextTarget() {
        GridCell thisCell = targetObj.GetComponent<GridCell>();
        transform.position = targetObj.transform.position;
        if (thisCell.nextCell == null)
        {
            if (thisCell.previousCell == null) {
                ChangeAntState(3);
            } else {
                ChangeAntState(1);
            }
        }
        else {
            targetObj = thisCell.nextCell;
            moveVector = new Vector2(targetObj.transform.position.x - transform.position.x, targetObj.transform.position.y - transform.position.y).normalized;
            if (moveVector.x > 0)
            {
                unitSprite.flipX = false;
            }
            else
            {
                unitSprite.flipX = true;
            }
        }
    }
    public void ChangeAntState(int newState) {
        antState = newState;

        switch (antState) {
            case 0:
                // targetObj = targetObj.GetComponent<GridCell>().nextCell;
                FindNextTarget();
                break;

            case 1:
                //targetObj = targetObj.GetComponent<GridCell>().previousCell;
                FindPrevTarget();
                break;

            case 3: // case if ant is confused 
                StartCoroutine(WaitForPheremones());
                break;
        }
    }
    private void FindPrevTarget() {
        GridCell thisCell = targetObj.GetComponent<GridCell>();
        if (thisCell.previousCell == null) {
            ChangeAntState(0);
        }
        else {
            targetObj = thisCell.previousCell;
            moveVector = new Vector2(targetObj.transform.position.x - transform.position.x, targetObj.transform.position.y - transform.position.y).normalized;
            if (moveVector.x > 0)
            {
                unitSprite.flipX = false;
            }
            else
            {
                unitSprite.flipX = true;
            }
        }
    }
    private IEnumerator WaitForPheremones() {
        GridCell thisCell = targetObj.GetComponent<GridCell>();
        while (thisCell.nextCell == null && thisCell.previousCell == null) {
            // Ant confusion animation
            yield return new WaitForSeconds(3);
        }
        if (thisCell.nextCell != null) {
            ChangeAntState(0);
            FindNextTarget();
        } else if (thisCell.previousCell != null) {
            ChangeAntState(1);
            FindPrevTarget();
        }
    }
}
