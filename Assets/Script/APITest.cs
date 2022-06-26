using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using Newtonsoft.Json;
using UnityEngine.Events;

public class APITest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ButtListPanelOptions p = new ButtListPanelOptions();
        p.data = new string[] { "1", "2", "3", "4", "5" };
        p.actions = new UnityAction[5];
        for (int i = 0; i < p.data.Length; i++)
        {
            int id = i;
            p.actions[i] = () =>
            {
                Debug.LogError(id);
            };
        }
     
        UIManager.Instance.OpenPlane<ButtonListPanel,ButtListPanelOptions>(PanelStyle.ButtonListPanel, Vector3.zero, 0, p);
        UIManager.Instance.OpenPlane<TestPanel, ButtListPanelOptions>(PanelStyle.TestPanel, Vector3.zero, 0, null);
        UIManager.Instance.OpenPlane<TipsPanel, ButtListPanelOptions>(PanelStyle.TipsPanel, Vector3.zero, 0, null);

        UIManager.Instance.OpenPlane<ButtonListPanel,ButtListPanelOptions>(PanelStyle.ButtonListPanel, Vector3.zero, 1, p);
    }
    int pivotID = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            UIManager.Instance.ClosePlane(PanelStyle.ButtonListPanel);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UIManager.Instance.activePlane[UIManager.Instance.GetPanelID(PanelStyle.ButtonListPanel)].GetComponent<ButtonListPanel>().SetPivot((BasicPanel.Pivot)(pivotID++ % 5));
            UIManager.Instance.OpenPlane<ButtonListPanel, ButtListPanelOptions>(PanelStyle.ButtonListPanel,Vector2.zero);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            UIManager.Instance.activePlane[UIManager.Instance.GetPanelID(PanelStyle.ButtonListPanel)].GetComponent<ButtonListPanel>().AddButton("New", () => { Debug.LogError("new Button"); });
        }
    }

}
