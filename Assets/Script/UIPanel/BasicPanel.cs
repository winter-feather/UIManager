using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPanel : MonoBehaviour
{
    private RectTransform rectTransform;
    public UIManager manager;

    public RectTransform RectTransform { 
        get {
            if (rectTransform == null) rectTransform = transform as RectTransform;
            return rectTransform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Open() { 
    
    }

    public virtual void Close() { 
    
    
    }

    public void SetPos(Vector2 pos) {
        
        RectTransform.localPosition = pos;
    }

    #region SetPivot
    public enum Pivot { 
        Center,LeftUp,RigntUp,RightDown,LeftDown
    }
    public void SetPivot(Pivot p) {
        switch (p)
        {
            case Pivot.Center:
                RectTransform.pivot = new Vector2(0.5f, 0.5f);
                break;
            case Pivot.LeftUp:
                RectTransform.pivot = Vector2.zero;
                break;
            case Pivot.RigntUp:
                RectTransform.pivot = new Vector2(1, 0);
                break;
            case Pivot.RightDown:
                RectTransform.pivot = Vector2.one;
                break;
            case Pivot.LeftDown:
                RectTransform.pivot = new Vector2(0, 1);
                break;
            default:
                break;
        }
    }
    public void SetCustomPivot(Vector2 pos)
    {
        RectTransform.pivot = pos;
    }
    #endregion


    public virtual void SetParmes(PanelParmes parmes) { 
    
    }
}

public abstract class PanelParmes { 


}