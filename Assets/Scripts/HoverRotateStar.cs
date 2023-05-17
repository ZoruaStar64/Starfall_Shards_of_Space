using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverRotateStar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    RectTransform rectTransform;
    private bool mouseOver = false;
    // Start is called before the first frame update
    //set the rectTransform Variable to this component's RectTransform.
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    //if hovering over this Object then rotate the rectTransform's z axis by 0.1f every frame.
    void Update()
    {
        if (mouseOver)
        {
            rectTransform.Rotate(0, 0, 0.1f);
        }
    }

    //when hovering over the star set mouseOver to true.
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    //when hovering over the star set mouseOver to false.
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
