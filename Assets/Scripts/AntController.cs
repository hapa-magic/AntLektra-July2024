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
    // Start is called before the first frame update
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
}
