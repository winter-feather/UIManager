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

//----------------------------------------------新建立Panel步骤--------------------------------------------------//
//--1、创建UI预置体----------------------------------------------------------------------------------------------//
//--2、创建UI对应脚本，在Awake中绑定所有组件，配置组件-----------------------------------------------------------//
//--3、在PanelStyle中加入对应类型
//--4、完成Init方法并配置在UIMnager.InitPanel中------------------------------------------------------------------//
//--5、完成Destory方法并配置在UIMnager.InitPanel中---------------------------------------------------------------//
//---------------------------------------------------------------------------------------------------------------//
//-------------------------------------------InitPlaneStart------------------------------------------------------//
//---------------------------------------------------------------------------------------------------------------//
public class UIManager : SingleManager<UIManager>
{
    private Canvas canvas;
    public Canvas canvas3D;
    public Dictionary<PanelStyle, GameObject> activePlane;

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
        InitCanvas();
        activePlane = new Dictionary<PanelStyle, GameObject>();
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
    public void OpenPlane(PanelStyle panel,Vector3? pos = null)
    {
        if (activePlane.ContainsKey(panel) && activePlane[panel] != null)
        {
            activePlane[panel].SetActive(true);
            if (pos!=null)
            {
                activePlane[panel].transform.position = pos.Value;
            }
        }
        else
        {
            activePlane[panel] = InitPlane(panel,pos);
        }
    }



    //---------------------------------------------------------------------------------------------------------------//
    //-------------------------------------------InitPlaneStart------------------------------------------------------//
    //---------------------------------------------------------------------------------------------------------------//

    public GameObject InitPlane(PanelStyle panel, Vector3? pos = null)
    {
        //Debug.LogError("initPlane:" + panel);
        GameObject go = null;
        switch (panel)
        {
            case PanelStyle.TestPanel:
            case PanelStyle.TipsPanel:
            case PanelStyle.ButtonListPanel:
                go = InitPanel(panel.ToString(), pos);
                break;
            case PanelStyle.LoadPanel:
                break;
            case PanelStyle.SavePanel:
                break;
             
            case PanelStyle.CellPanelA:
                go = InitCellPanelA(pos);
                break;
            case PanelStyle.CellPaneB:
                go = InitCellPaneB(pos);
                break;
        }
        return go;
    }
    public GameObject InitPanel(string name,Vector3? pos = null) {
        GameObject go = ResourcesManager.Instance.Load(name);
        go.name = name;
        go.transform.SetParent(Canvas.transform);
        go.transform.localScale = Vector3.one;
        if (pos!=null)
        {
            go.transform.position = pos.Value;
        }
        else
        {
            go.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f,0);
        }
        return go;
    }

    public GameObject InitCellPanelA(Vector3? pos) {
        GameObject go = InitPanel(PanelStyle.ContainerPanel.ToString(), pos);
        go.GetComponent<GridContainerPanel>().closeBtn.onClick.AddListener(()=>ClosePlane(PanelStyle.CellPanelA));
        return go;
    }

    public GameObject InitCellPaneB(Vector3? pos) {
        GameObject go = InitPanel(PanelStyle.ContainerPanel.ToString(), pos);
        go.GetComponent<GridContainerPanel>().closeBtn.onClick.AddListener(() => ClosePlane(PanelStyle.CellPaneB));
        return go;
    }


    //---------------------------------------------------------------------------------------------------------------//
    //-------------------------------------------InitPlaneEnd--------------------------------------------------------//
    //---------------------------------------------------------------------------------------------------------------//

    //---------------------------------------------------------------------------------------------------------------//
    //-------------------------------------------ClosePlaneStart-----------------------------------------------------//
    //---------------------------------------------------------------------------------------------------------------//
    public void ClosePlane(PanelStyle panel)
    {
        if (activePlane.ContainsKey(panel) && activePlane[panel] != null)
        {
            activePlane[panel].SetActive(false);
            OnClosePanel(panel);
        }
    }
    void OnClosePanel(PanelStyle panel)
    {
        switch (panel)
        {
            case PanelStyle.TestPanel:
                break;
            case PanelStyle.LoadPanel:
                break;
            case PanelStyle.SavePanel:
                break;
            case PanelStyle.DrivinModePanel:
                break;
            case PanelStyle.CameraDesicrible:
                break;
            case PanelStyle.CameraMoveCtrl:
                break;
            case PanelStyle.CameraRotaCtrl:
                break;
            case PanelStyle.CameraList:
                break;
            default:
                break;
        }
    }


    void CloseSavePanel()
    {
        Destroy(activePlane[PanelStyle.SavePanel]);
        activePlane[PanelStyle.SavePanel] = null;
    }
    void CloseLoadPanel()
    {
        Destroy(activePlane[PanelStyle.LoadPanel]);
        activePlane[PanelStyle.LoadPanel] = null;
    }
    void ClosCarModeSetPanel()
    {
        //activePlane[PanelStyle.DrivinModePanel].SetActive(false);
    }
    void CloseTestModePanel() { 
    
    }

    //---------------------------------------------------------------------------------------------------------------//
    //-------------------------------------------ClosePlaneEnd-------------------------------------------------------//
    //---------------------------------------------------------------------------------------------------------------//

    //---------------------------------------------------------------------------------------------------------------//
    //------------------------------------SettingWhithOpenPlaneStart-------------------------------------------------//
    //---------------------------------------------------------------------------------------------------------------//
    public void OpenTipsPanel(string title,string content) {
        UIManager.Instance.OpenPlane(PanelStyle.TipsPanel, new Vector3(350, Screen.height * 0.5f + 0, 0));
        TipsPanel tp = activePlane[PanelStyle.TipsPanel].GetComponentInChildren<TipsPanel>();
        tp.SetTitle(title);
        tp.SetContent(content);
    }

    //---------------------------------------------------------------------------------------------------------------//
    //-------------------------------------SettingWhithOpenPlaneEnd--------------------------------------------------//
    //---------------------------------------------------------------------------------------------------------------//

}

//---------------------------------------------------------------------------------------------------------------//
//-------------------------------------------PlaneStyle&&PlaneClass----------------------------------------------//
//---------------------------------------------------------------------------------------------------------------//

public enum PanelStyle
{
    LoadPanel, SavePanel, DrivinModePanel,

    CameraDesicrible, CameraMoveCtrl, CameraRotaCtrl, CameraList,
    //SystemPanel
    TipsPanel, TestPanel, ButtonListPanel, ContainerPanel,
    CellPanelA,CellPaneB
}

//---------------------------------------------------------------------------------------------------------------//
//-------------------------------------------PlaneStyle&&PlaneClass----------------------------------------------//
//---------------------------------------------------------------------------------------------------------------//




public class ResourcesManager : SingleManager<ResourcesManager>
{
    public GameObject Load(string resourceName)
    {
        return Instantiate<GameObject>(Resources.Load<GameObject>(resourceName));
    }

}

public class LogManager : SingleManager<LogManager> { 

}