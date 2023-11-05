using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewItem : MonoBehaviour, IPointerClickHandler
{
    public Image Image;
    private Object TargetObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.BuildItem(TargetObject);
    }

    public void UpdateData(GameObject TargetObject)
    {
        Object Obj = TargetObject.GetComponent<Object>();
        if(Obj)
        {
            Image.sprite = Obj.SpriteRenderer.sprite;
            this.TargetObject = Obj;
        }

    }
}
