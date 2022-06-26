using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : BasicPanel
{
    public Button applyBtn;
    Text title, content;

    private void Awake()
    {
        title = transform.GetChild(1).GetComponent<Text>();
        content = transform.GetChild(2).GetComponent<Text>();
        applyBtn = transform.GetChild(3).GetComponent<Button>();
        title.text = "title";
        content.text = "content";
        applyBtn.GetComponentInChildren<Text>().text = "确认";
        applyBtn.onClick.AddListener(()=> UIManager.Instance.ClosePlane(PanelStyle.TipsPanel));
    }
    // Start is called before the first frame update
    public void SetTitle(string str) {
        title.text = str;
    }

    public void SetContent(string str) {
        content.text = str;
    }
}
