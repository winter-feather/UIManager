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
    public Canvas canvas;
    public Dictionary<PanelStyle, GameObject> activePlane;
    GameObject btnNodePre;

    new void Awake()
    {
        base.Awake();
        canvas = GameObject.FindObjectOfType<Canvas>();
        btnNodePre = Resources.Load<GameObject>("ButtonNode");
        activePlane = new Dictionary<PanelStyle, GameObject>();
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
        Debug.LogError("initPlane:" + panel);
        GameObject go = null;
        switch (panel)
        {
            case PanelStyle.TestPanel:
                go = InitTestPanel(pos);
                break;
            case PanelStyle.LoadPanel:
                break;
            case PanelStyle.SavePanel:
                break;
            case PanelStyle.TipsPanel:
                go = InitTipsPanel(pos);
                break;
            case PanelStyle.ButtonListPanel:
                go = InitButtonListPanel(pos);
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
    public GameObject InitCellPanelA(Vector3? pos) {
        GameObject go = ResourcesManager.Instance.Load(PanelStyle.ContainerPanel.ToString());
        go.name = "InitCellPanelA";
        go.transform.SetParent(canvas.transform);
        go.transform.localScale = Vector3.one;
        go.transform.position = pos.Value;
        go.GetComponent<GridContainerPanel>().closeBtn.onClick.AddListener(()=>ClosePlane(PanelStyle.CellPanelA));
        return go;
    }

    public GameObject InitCellPaneB(Vector3? pos) {
        GameObject go = ResourcesManager.Instance.Load(PanelStyle.ContainerPanel.ToString());
        go.name = "InitCellPaneB";
        go.transform.SetParent(canvas.transform);
        go.transform.localScale = Vector3.one;
        go.transform.position = pos.Value;
        go.GetComponent<GridContainerPanel>().closeBtn.onClick.AddListener(() => ClosePlane(PanelStyle.CellPaneB));
        return go;
    }

    public GameObject InitButtonListPanel(Vector3? pos) {
        GameObject go = ResourcesManager.Instance.Load(PanelStyle.ButtonListPanel.ToString());
        go.name = "ButtonListPanel";
        go.transform.SetParent(canvas.transform);
        go.transform.localScale = Vector3.one;
        go.transform.position = pos.Value;
        return go;
    }
    public GameObject InitTestPanel(Vector3? pos)
    {
        GameObject go = ResourcesManager.Instance.Load(PanelStyle.TestPanel.ToString());
        go.name = "TestPanel";
        go.transform.SetParent(canvas.transform);
        go.transform.localScale = Vector3.one;
        go.transform.position = pos.Value;
        return go;
    }

    public GameObject InitTipsPanel(Vector3? pos)
    {
        GameObject go = ResourcesManager.Instance.Load(PanelStyle.TipsPanel.ToString());
        go.name = "TipsPanel";
        //Debug.LogError(go == null);
        go.transform.SetParent(canvas.transform);
        go.transform.localScale = Vector3.one;
        go.transform.position = pos.Value;
        return go;
    }
    public GameObject InitLoadPanel(Vector3? pos)
    {
        return null;
    }
    public GameObject InitSavePanel(Vector3? pos)
    {
        return null;
    }
    public GameObject InitCarModeSetPanel(Vector3? pos)
    {
        GameObject go;
        go = Instantiate<GameObject>(Resources.Load<GameObject>(PanelStyle.DrivinModePanel.ToString()));
        go.transform.SetParent(canvas.transform);
        go.transform.localPosition = new Vector3(380, 525, 0);

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
    public void OpentTipsPanel(params string[] info) {
        UIManager.Instance.OpenPlane(PanelStyle.TipsPanel, new Vector3(350, Screen.height * 0.5f + 0, 0));
        TipsPanel tp = activePlane[PanelStyle.TipsPanel].GetComponentInChildren<TipsPanel>();
        tp.SetTitle(info[0]);
        tp.SetContent(info[1]);
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
        Debug.LogError(resourceName);
        return Instantiate<GameObject>(Resources.Load<GameObject>(resourceName));
    }

}
