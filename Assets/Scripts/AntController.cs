using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{
    public int health;
    public int attack;
    public float moveSpeed = 0.5f;
    public GameObject targetObj;
    public GameObject prevObj;
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
    private void FindNextTarget()
    {
        transform.position = targetObj.transform.position;
        if (targetObj.GetComponent<GridCell>().nextCell == null)
        {
            ChangeAntState(1);
        }
        else
        {
            targetObj = targetObj.GetComponent<GridCell>().nextCell;
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
    public void ChangeAntState(int newState)
    {
        antState = newState;

        switch (antState)
        {
            case 0:
                // targetObj = targetObj.GetComponent<GridCell>().nextCell;
                FindNextTarget();
                break;

            case 1:
                //targetObj = targetObj.GetComponent<GridCell>().previousCell;
                FindPrevTarget();
                break;
        }
    }
    private void FindPrevTarget()
    {
        // transform.position = targetObj.transform.position;
        if (targetObj.GetComponent<GridCell>().previousCell == null)
        {
            ChangeAntState(0);
        }
        else
        {
            targetObj = targetObj.GetComponent<GridCell>().previousCell;
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
}
