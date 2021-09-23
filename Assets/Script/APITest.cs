using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using Newtonsoft.Json;
public class APITest : MonoBehaviour
{
    public string homeUrl = @"http://kaoshijxedttest.58v5.cn";//"https://kaoshiapi.jxedt.com/robot";
    public string getUrl = "/robot/getImplementationTools";
    public string postUrl = "/robot/saveImplementationTools";

    // Start is called before the first frame update
    void Start()
    {
        homeUrl = @"http://kaoshijxedttest.58v5.cn";
        TestPost();
        TestGet();

        //可以转换正常的串
        //string a = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(@"110中文");
        //无法转换Json的串
        //a = Newtonsoft.Json.JsonConvert.DeserializeObject<string>("{key:value}");

        UIManager.Instance.OpenPlane(PanelStyle.ButtonListPanel, new Vector3(350, Screen.height * 0.5f + 500, 0));
        ButtonListPanel bl = UIManager.Instance.activePlane[PanelStyle.ButtonListPanel].GetComponent<ButtonListPanel>();
        bl.AddButton("打开颜色面板", () => UIManager.Instance.OpenPlane(PanelStyle.TestPanel, new Vector3(350, Screen.height * 0.5f + 500, 0)));
        bl.AddButton("打开提示面板", () => UIManager.Instance.OpentTipsPanel("测试", "测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试"));
        bl.rectTransform.pivot = new Vector2(0.5f, 0);
        bl.rectTransform.anchoredPosition = new Vector2(Screen.width*0.5f, -Screen.height+10);

        UIManager.Instance.OpenPlane(PanelStyle.CellPanelA, new Vector3(Screen.width-100, Screen.height * 0.5f + 350, 0));
        UIManager.Instance.OpenPlane(PanelStyle.CellPaneB, new Vector3(50, Screen.height * 0.5f + 350, 0));

        //UIManager.Instance.OpenPlane(PanelStyle.TipsPanel,new Vector3(350,Screen.height*0.5f+0,0));

    }

    void TestPost() {
        Dictionary<string, string> parms = new Dictionary<string, string>();
        parms.Add("schoolId", "22144");
        parms.Add("itemValue", "this test info");
        Dictionary<string, string> header = new Dictionary<string, string>();
        //header.Add("Content-Type", "application/json");
        BestHttpRquest.Instance.BestHttpPost<StringResult>(homeUrl + postUrl, parms, TestPostAction, header);
    }

    void TestGet() {
        Dictionary<string, string> parms = new Dictionary<string, string>();
        parms.Add("schoolId", "22144");
        Dictionary<string, string> header = new Dictionary<string, string>();
        //header.Add("Content-Type", "application/json");
        BestHttpRquest.Instance.BestHttpGet<StringResult>(homeUrl + getUrl, parms, TestGetAction, header);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {

        }
    }

    public void TestGetAction(StringResult s, HTTPRequest request, HTTPResponse response) {
        //Debug.LogError(s);
    }

    public void TestPostAction(StringResult s, HTTPRequest request, HTTPResponse response)
    {
        //Debug.LogError(s);
    }
}
