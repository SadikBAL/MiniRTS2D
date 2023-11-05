using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;

public struct ScrollItemData
{
    public Object TargetObject;
}
public class ScrollManager : MonoBehaviour, IRecyclableScrollRectDataSource
{
    public List<GameObject> Items = new List<GameObject>();
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    [SerializeField]
    private int _dataLength;

    //Dummy data List
    private List<ScrollItemData> _contactList = new List<ScrollItemData>();

    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        InitData();
        _recyclableScrollRect.DataSource = this;
    }

    //Initialising _contactList with dummy data 
    private void InitData()
    {
        if (_contactList != null) _contactList.Clear();
        int count = Items.Count;
      for(int i = 0; i < _dataLength; i++) 
      {
            Object o = Items[i % count].GetComponent<Object>();
            if (o)
            {
                _contactList.Add(new ScrollItemData { TargetObject = o });
            }
        }
    }

    #region DATA-SOURCE

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        return _contactList.Count;
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as ScrollItem;
        item.ConfigureCell(_contactList[index], index);
    }

    #endregion
}