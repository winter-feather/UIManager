using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using WinterFeather;
//---------------------------------------------------------------------------------------------------------------//
//------------------------------------------------StartTips------------------------------------------------------//
//---------------------------------------------------------------------------------------------------------------//


public class UIManager : SingleManager<UIManager>
{
    private Canvas canvas;
    public Canvas canvas3D;
    public Dictionary<int, GameObject> activePlane;
    Dictionary<PanelStyle, GameObject> uiPanelPre;
    public Canvas Canvas
    {
        get
        {
            if (canvas == null)
            {
                InitCanvas();
            }
            return canvas;
        }
    }

    new void Awake()
    {
        base.Awake();
        InitPanelPreGO();
        InitCanvas();
        InitData();
    }
    void InitData() {
        activePlane = new Dictionary<int, GameObject>();
    }
    void InitPanelPreGO() {
        uiPanelPre = new Dictionary<PanelStyle, GameObject>();
        Array values = Enum.GetValues(typeof(PanelStyle));
        
        for (int i = 0; i < values.Length; i++)
        {
            GameObject go = ResourcesManager.Instance.Load<GameObject>("UIPanelPreGO/" + values.GetValue(i));
            if (go)
            {
                uiPanelPre.Add((PanelStyle)Enum.Parse(typeof(PanelStyle),go.name) , go);
            }
            else
            {
                //Debug.LogError("null");
            }
        }
    }
    void InitCanvas()
    {
        if (!canvas) canvas = GameObject.FindObjectOfType<Canvas>();
        canvas.sortingOrder = 1;
        if (!canvas)
        {
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }
    }

    public GameObject OpenPlane<C,P>(PanelStyle panel, Vector3 pos, int id = 0,P parmes = null) where C:BasicPanel where P: PanelParmes
    {
        int panelID = GetPanelID(panel, id);
        if (activePlane.ContainsKey(panelID) && activePlane[panelID] != null)
        {
            activePlane[panelID].SetActive(true);
            C pc = activePlane[panelID].GetComponent<C>();
            pc.Open();
            //pc.SetParmes(parmes);
            pc.SetPos(pos);
            return activePlane[panelID];
        }
        else
        {
            activePlane.Add(panelID, InstantiatePanel(panel, Canvas.transform));
            C pc = activePlane[panelID].GetComponent<C>();
            pc.SetParmes(parmes);
            pc.SetPos(pos);
            return activePlane[panelID];
        }
    }

    public void ClosePlane(PanelStyle panel,int id = 0)
    {
        int panelID = GetPanelID(panel,id);
        if (activePlane.ContainsKey(panelID) && activePlane[panelID] != null)
        {
            activePlane[panelID].SetActive(false);
        }
    }

    public GameObject InstantiatePanel(PanelStyle panel,Transform canvas) {
        return GameObject.Instantiate<GameObject>(uiPanelPre[panel], canvas);
    }

    public int GetPanelID(PanelStyle panelStyle,int id = 0) {
       return (int)panelStyle * 1000 + id % 999;
    }







}

public enum PanelStyle
{
    LoadPanel, SavePanel, DrivinModePanel,

    CameraDesicrible, CameraMoveCtrl, CameraRotaCtrl, CameraList,
    //SystemPanel
    TipsPanel, TestPanel, ButtonListPanel, ContainerPanel,
    CellPanelA,CellPaneB
}

public class ResourcesManager : SingleManager<ResourcesManager>
{
    public T Load<T>(string resourceName) where T:UnityEngine.Object
    {
        return Resources.Load<T> (resourceName);
    }
}

public class UIManager2DCanvas : MonoBehaviour { }
public class UIManager3DCanvas : MonoBehaviour { }



public class LogManager : SingleManager<LogManager> { 

}