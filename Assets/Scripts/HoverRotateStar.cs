using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverRotateStar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    RectTransform rectTransform;
    private bool mouseOver = false;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (mouseOver)
        {
            rectTransform.Rotate(0, 0, 0.1f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
