using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectMouseOnGameObj : MonoBehaviour
{
    public Camera thisCamera;
    private HomeNest homeNest;
    private bool isSettingPheremones;

    // Start is called before the first frame update
    void Awake()
    {
        isSettingPheremones = true;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public Transform GetGameObjFromClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(thisCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        return hit.collider.gameObject.transform;
    }
}
