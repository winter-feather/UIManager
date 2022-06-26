using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonListPanel : BasicPanel
{
    public GameObject buttonPre;// Start is called before the first frame update
    Vector2 size;
    Vector2 preSize;
    private void Awake()
    {
        size = RectTransform.sizeDelta;
        preSize = (buttonPre.transform as RectTransform).sizeDelta;
    }
    public void AddButton(string name, UnityAction action) {
        
        GameObject go  = Instantiate<GameObject>(buttonPre);
        size.x += preSize.x; 
        RectTransform.sizeDelta = size;
        go.GetComponentInChildren<Text>().text = name;
        go.transform.SetParent(transform);
        go.transform.localScale = Vector3.one;
        go.GetComponent<Button>().onClick.AddListener(action);
    }

    public override void SetParmes(PanelParmes parmes)
    {
        ButtListPanelOptions p = parmes as ButtListPanelOptions;
        for (int i = 0; i < p.data.Length; i++)
        {
            AddButton(p.data[i], p.actions[i]);
        }
    }
}

public class ButtListPanelOptions : PanelParmes {
    public string[] data;
    public UnityAction[] actions;
}