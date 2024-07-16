using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRectTranform;
    private Vector3 originalScale;
    private int currentState = 0;
    private Quaternion originalRotation;
    private Vector3 originalPosition;
    int UILayer;
    private GameObject playTarget;
    [SerializeField] public Canvas cardPreviewParent;
    private CardPreview cardPreview;
    
    [SerializeField] private Vector3 movementOffset = new Vector3 (-300, 280, 0);
    [SerializeField] private float selectScale = 1.1f;
    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector3 playPosition;
    [SerializeField] private GameObject glowEffect;
    [SerializeField] private GameObject playArrow;
    [SerializeField] private float lerpFactor = 0.1f;
    [SerializeField] private int cardPlayDivider = 4;
    [SerializeField] private float cardPlayMultiplier = 1f;
    [SerializeField] private bool needUpdateCardPlayPosition = false;
    [SerializeField] private int playPositionYDivider = 2;
    [SerializeField] private float playPositionYMultiplier = 1f;
    [SerializeField] private int playPositionXDivider = 4;
    [SerializeField] private float playPositionXMultiplier = 1f;
    [SerializeField] private bool needUpdatePlayPosition = false;

    void Awake()
    {
        UILayer = LayerMask.NameToLayer("UI");
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            canvasRectTranform = canvas.GetComponent<RectTransform>();
        }

        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;

        updateCardPlayPostion();
        updatePlayPostion();
    }

    void Update()
    {
        if (needUpdateCardPlayPosition)
        {
            updateCardPlayPostion();
        }

        if (needUpdatePlayPosition)
        {
            updatePlayPostion();
        }
        
        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if (IsPointerOverUIElement()) {
                    if (!Input.GetMouseButton(0)) {
                        CheckForPlay();
                        cardPreview.DestroyPreview();
                    }
                }
                if (!Input.GetMouseButton(0)) //Check if mouse button is released
                {
                    TransitionToState0();
                }
                break;
            /*case 3:
                HandlePlayState();
                if (!Input.GetMouseButton(0)) //Check if mouse button is released
                {
                    TransitionToState0();
                }
                break;*/
            case 4:
                HandleAlmostPlayState();  
                break;  
        }
    }

    private void TransitionToState0()
    {
        currentState = 0;
        rectTransform.localScale = originalScale; //Reset Scale
        rectTransform.localRotation = originalRotation; //Reset Rotation
        rectTransform.localPosition = originalPosition; //Reset Position
        glowEffect.SetActive(false); //Disable glow effect
        playArrow.SetActive(false); //Disable playArrow
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;

            currentState = 1;
        } 
    }
    public void OnPointerUp(PointerEventData eventData) {
        if (currentState == 2) {
            Debug.Log("Anything");
            CheckForPlay();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            TransitionToState0();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            cardPreview.PreviewCard(rectTransform.gameObject);
            currentState = 2;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentState == 2)
        {
            /*if (Input.mousePosition.y > cardPlay.y)
            {
                currentState = 3;
                playArrow.SetActive(true);
                rectTransform.localPosition = Vector3.Lerp(rectTransform.position, playPosition, lerpFactor);
            }*/
        }
    }
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    public bool CheckForPlay() {
        return CheckForPlay(GetEventSystemRaycastResults());
    }
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
            {
                if (curRaysastResult.gameObject.GetComponent<PlaySpot>() != null)
                {
                    playTarget = curRaysastResult.gameObject;
                    rectTransform.localPosition = playTarget.GetComponent<RectTransform>().localPosition + movementOffset;
                    return true;
                }
            }
        }
        return false;
    }
    private bool CheckForPlay(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
            {
                if (curRaysastResult.gameObject.GetComponent<PlaySpot>() != null)
                {
                    playTarget = curRaysastResult.gameObject;
                    rectTransform.localPosition = playTarget.GetComponent<RectTransform>().localPosition + movementOffset;
                    //ChangeParent();
                    return true;
                }
            }
        }
        return false;
    }
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

    private void HandleAlmostPlayState() {
        /*List<RaycastResult> eventSystemRaysastResults = GetEventSystemRaycastResults();
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
            {
                if (curRaysastResult.gameObject.GetComponent<PlaySpot>() != null)
                {
                    Debug.Log(curRaysastResult.gameObject);
                    Debug.Log("HEEEELPO");
                    //rectTransform.localPosition = curRaysastResult.gameObject.RectTransform;
                }
            }
        }*/
    }

private void HandleHoverState()
    {
        glowEffect.SetActive(true);
        rectTransform.localScale = originalScale * selectScale;
    }

    private void HandleDragState()
    {
        //Set the card's rotation to zero
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFactor);
    }

    private void HandlePlayState()
    {
        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;

        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }

    private void updateCardPlayPostion()
    {
        if (cardPlayDivider != 0 && canvasRectTranform != null)
        {
            float segment = cardPlayMultiplier / cardPlayDivider;

            cardPlay.y = canvasRectTranform.rect.height * segment;
        }
    }

    private void updatePlayPostion()
    {
        if (canvasRectTranform != null && playPositionYDivider != 0 && playPositionXDivider != 0)
        {
            float segmentX = playPositionXMultiplier / playPositionXDivider;
            float segmentY = playPositionYMultiplier / playPositionYDivider;

            playPosition.x = canvasRectTranform.rect.width * segmentX;
            playPosition.y = canvasRectTranform.rect.height * segmentY;
        }
    }
}
