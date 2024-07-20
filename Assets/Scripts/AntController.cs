using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{
    public int health;
    public int attack;
    public float moveSpeed = 0.5f;
    public GameObject targetObj;
    // private Vector2 position;
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
                transform.Translate((targetObj.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime);
                // position = Vector2.MoveTowards(position, targetObj.transform.position, moveSpeed * Time.deltaTime).normalized;
                break;
        }
    }

    public void FlipAnt()
    {
        unitSprite.flipY = true;
        antSprite.transform.position = transform.position + new Vector3(0, -.4f, 0);
    }
}
