using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ActionPointsConsumer : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI actionValueText;
    public int requiredActionPoints = 10;
    public bool isCompleted = false;
    public bool isHovered = false;
    
    
    Image image;
    [SerializeField] Sprite unselectedSprite;
    [SerializeField] Sprite selectedSprite;
    [SerializeField] Sprite completedSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] GameObject checkMark;
    [SerializeField] GameObject additionEffect;

    private Animator anim;
    static string POP_TRIGGER = "Pop";
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        image = GetComponent<Image>();
    }
    private void Start()
    {
        actionValueText.text = requiredActionPoints.ToString();
    }

    public void Update()
    {
        if(isCompleted || isHovered)
            return;
        if (ActionPointsHolder.IS_DRAGGING)
        {
            image.sprite = selectedSprite;
        }
        else
        {
            image.sprite = unselectedSprite;
        }
    }

    public void ConsumeActionPoints(int _actionPoints)
    {
        requiredActionPoints -= _actionPoints;
        if (requiredActionPoints <= 0)
        {
            requiredActionPoints = 0;
            isCompleted = true;
            checkMark.SetActive(true);
            actionValueText.gameObject.SetActive(false);
            image.sprite = completedSprite;
        }
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        if(isCompleted)
            return;
        Debug.Log("Dropped");
        if (eventData.pointerDrag != null)
        {
            ActionPointsHolder actionPointsHolder = eventData.pointerDrag.GetComponent<ActionPointsHolder>();
            if (actionPointsHolder != null)
            {
                anim.SetTrigger(ActionPointsConsumer.POP_TRIGGER);
                Instantiate(additionEffect, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                ConsumeActionPoints(actionPointsHolder.actionPoints);
                actionPointsHolder.ConsumePoints();
                actionValueText.text = requiredActionPoints.ToString();
                isHovered = false;

                if (isCompleted)
                {
                    Debug.Log("Completed");
                }
            }
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
        if(isCompleted || !ActionPointsHolder.IS_DRAGGING)
            return;
        image.sprite = hoverSprite;
        isHovered = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
        if(isCompleted || !ActionPointsHolder.IS_DRAGGING)
            return;
        image.sprite = unselectedSprite;
        isHovered = false;
    }
}

