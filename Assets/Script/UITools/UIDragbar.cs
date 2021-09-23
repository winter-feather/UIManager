using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDragbar : MonoBehaviour,IDragHandler,IPointerDownHandler
{
    public GameObject dragGO;
    Vector3 dragStartGOPOS, dragStartMousePos;
    public void OnDrag(PointerEventData eventData)
    {
        dragGO.transform.localPosition = dragStartGOPOS + Input.mousePosition - dragStartMousePos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStartGOPOS = dragGO.transform.localPosition;
        dragStartMousePos = Input.mousePosition;
        dragGO.transform.SetAsLastSibling();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
