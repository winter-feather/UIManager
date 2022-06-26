using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GridContainerPanel : BasicPanel
{
    public GameObject cellPre;
    public RectTransform titleRT, cellContentRT, cellBGRT;
    public GridLayoutGroup cellglg;
    public Text titleText;
    Vector2 cellOriSize, cellTargetSize, preSize, titleSize, cellContentSize;
    public int cellColCount;
    public Button closeBtn;

    public float width;
    // Start is called before the first frame update
    void Start()
    {
        preSize = (cellPre.transform as RectTransform).sizeDelta;
       
        cellContentSize = Vector2.zero;

        cellglg = cellContentRT.GetComponent<GridLayoutGroup>();
        cellglg.cellSize = preSize;

        width = cellColCount * preSize.x;
        InitTitle("CellContainer");



        cellTargetSize.x = width;
        cellOriSize.y = preSize.y;

       

        for (int i = 0; i < 20; i++)
        {
            AddButton(i.ToString(), null);
        }

       
    }
    void InitTitle(string name) {
        titleText = titleRT.GetComponentInChildren<Text>();
        titleText.text = name;
        titleSize = titleRT.sizeDelta;
        titleSize.x = width;
        titleRT.sizeDelta = titleSize;
    }
    void SoloveTarget()
    {
        //Debug.LogError("CellOri:" + cellOriSize.x);
        //Debug.LogError("Width:" + width);
        //Debug.LogError("PreSize:" + preSize);
        cellTargetSize.y = ( Mathf.Ceil( cellOriSize.x / width)) * preSize.y;
        //Debug.LogError(cellTargetSize);
    }
    public void AddButton(string name, UnityAction action)
    {

        GameObject go = Instantiate<GameObject>(cellPre);
        cellOriSize.x += preSize.x;
        SoloveTarget();
        cellContentRT.sizeDelta = cellTargetSize;
        cellBGRT.sizeDelta = cellTargetSize;

        go.GetComponentInChildren<Text>().text = name;
        go.transform.SetParent(cellContentRT);
        go.transform.localScale = Vector3.one;
        go.GetComponent<Button>().onClick.AddListener(action);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
