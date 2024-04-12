using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ActionPointsHolder : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int actionPoints = 5;
    [SerializeField]
    private RectTransform actionValueTextHolder;
    [SerializeField]
    private TextMeshProUGUI actionValueText;

    [SerializeField] private Canvas canvas;
    
    private CanvasGroup canvasGroup;
    
    public static bool IS_DRAGGING = false;
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    private void Start()
    {
        actionValueText.text = actionPoints.ToString();
    }
    
    
    public void ConsumePoints()
    {
        actionPoints = 0;
        actionValueText.text = actionPoints.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(actionPoints <= 0)
            return;
        IS_DRAGGING = true;
        Debug.Log("Begin Drag");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        actionValueTextHolder.anchoredPosition = new Vector3(0,0,0);
        canvasGroup.blocksRaycasts = true;
        IS_DRAGGING = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if(actionPoints <= 0)
            return;
        Debug.Log("Dragging");
        actionValueTextHolder.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}


