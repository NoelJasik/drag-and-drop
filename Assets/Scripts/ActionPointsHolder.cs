using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ActionPointsHolder : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int actionPoints = 5;
    [SerializeField]
    private RectTransform actionValueTextHolder;
    [SerializeField]
    private TextMeshProUGUI actionValueText;

    [SerializeField] private Canvas canvas;
    
    private CanvasGroup canvasGroup;
    

    
    
    
    
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
    public void OnPointerClick(PointerEventData eventData)
    {
        actionPoints++;
        Debug.Log("Action Points: " + actionPoints);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(actionPoints <= 0)
            return;
        Debug.Log("Begin Drag");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        actionValueTextHolder.anchoredPosition = new Vector3(0,0,0);
        canvasGroup.blocksRaycasts = true;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if(actionPoints <= 0)
            return;
        Debug.Log("Dragging");
        actionValueTextHolder.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}


