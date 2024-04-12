using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ActionPointsConsumer : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private TextMeshProUGUI actionValueText;
    public int requiredActionPoints = 10;
    public bool isCompleted = false;
    
    
    
        
    Image image;
    [SerializeField] Sprite unselectedSprite;
    [SerializeField] Sprite selectedSprite;

    
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Start()
    {
        actionValueText.text = requiredActionPoints.ToString();
    }

    public void ConsumeActionPoints(int _actionPoints)
    {
        requiredActionPoints -= _actionPoints;
        if (requiredActionPoints <= 0)
        {
            requiredActionPoints = 0;
            isCompleted = true;
        }
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        if (eventData.pointerDrag != null)
        {
            ActionPointsHolder actionPointsHolder = eventData.pointerDrag.GetComponent<ActionPointsHolder>();
            if (actionPointsHolder != null)
            {
                ConsumeActionPoints(actionPointsHolder.actionPoints);
                actionPointsHolder.ConsumePoints();
                actionValueText.text = requiredActionPoints.ToString();
                if (isCompleted)
                {
                    Debug.Log("Completed");
                }
            }
        }
    }
}

