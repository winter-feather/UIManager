using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonListPanel : MonoBehaviour
{
    public GameObject buttonPre;// Start is called before the first frame update
    public RectTransform rectTransform;
    Vector2 size;
    Vector2 preSize;
    private void Awake()
    {
        rectTransform = transform as RectTransform;
        size = rectTransform.sizeDelta;
        preSize = (buttonPre.transform as RectTransform).sizeDelta;
    }

    public void AddButton(string name, UnityAction action) {
        
        GameObject go  = Instantiate<GameObject>(buttonPre);
        size.x += preSize.x; 
        rectTransform.sizeDelta = size;
        go.GetComponentInChildren<Text>().text = name;
        go.transform.SetParent(transform);
        go.transform.localScale = Vector3.one;
        go.GetComponent<Button>().onClick.AddListener(action);
    }
}
