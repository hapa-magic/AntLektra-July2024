using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectMouseOnGameObj : MonoBehaviour
{
    public Camera thisCamera;
    bool weHitSomething = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public Transform GetPheremoneClick()
    {
        Vector3 mousePos = Input.mousePosition;

        Ray ray = thisCamera.ScreenPointToRay(mousePos);

        RaycastHit hit;

        weHitSomething = Physics.Raycast(ray, out hit);

        if (weHitSomething)
        {
            return hit.transform;
        } else { return null; }
    }
}
