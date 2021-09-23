using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour
{
    public Image[] images;
    public Transform imagesContainer;
    public Color[] oriColor;
    public Color[] targetColor;
    public Button closeBtn, applyBtn;
    bool isTargetColor;

    public bool IsTargetColor { 
        get => isTargetColor;
        set
        { 
            isTargetColor = value;
            if (isTargetColor) ChangeToTargeColor();
            else ChangeToOriColor();
        } }

    private void Awake()
    {
        imagesContainer = transform.GetChild(0);
        applyBtn = transform.GetChild(1).GetComponent<Button>();
        closeBtn = transform.GetChild(2).GetComponent<Button>();

        images = new Image[imagesContainer.childCount];
        oriColor = new Color[imagesContainer.childCount];
        targetColor = new Color[imagesContainer.childCount];

        for (int i = 0; i < imagesContainer.childCount; i++)
        {
            images[i] = imagesContainer.GetChild(i).GetChild(imagesContainer.GetChild(i).transform.childCount - 1).GetComponent<Image>();
            oriColor[i] = images[i].color;
            float h, s, v;
            Color.RGBToHSV(images[i].color, out h, out s, out v);
            h += 0.5f;
            targetColor[i] = Color.HSVToRGB(h>1?h-1:h, s, v);
        }
        closeBtn.onClick.AddListener(() => UIManager.Instance.ClosePlane(PanelStyle.TestPanel));
        closeBtn.GetComponentInChildren<Text>().text = "X";
        applyBtn.onClick.AddListener(()=>IsTargetColor = !isTargetColor);
        applyBtn.GetComponentInChildren<Text>().text = "ChangeColor";
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToTargeColor() {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = targetColor[i];
        }
    }

    public void ChangeToOriColor() {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = oriColor[i];
        }
    }
}
