using PolyAndCode.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollItem : MonoBehaviour, ICell
{
    //UI
    public Image image;

    //Model
    private ScrollItemData data;
    private int _cellIndex;

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(ScrollItemData data, int cellIndex)
    {
        _cellIndex = cellIndex;
        this.data = data;

        image.sprite = data.TargetObject.SpriteRenderer.sprite;
    }


    private void ButtonListener()
    {
        GameManager.Instance.BuildItem(data.TargetObject);
    }
}
