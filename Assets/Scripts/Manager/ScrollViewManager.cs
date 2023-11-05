using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewManager : MonoBehaviour
{
    public GameObject ContentGameObject;
    public List<GameObject> ObjectList;
    public GameObject ScrollItem;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in ObjectList) 
        {
           ScrollViewItem Item =  Instantiate(ScrollItem).GetComponent<ScrollViewItem>();
            if(Item)
            {
                Item.UpdateData(item);
                Item.gameObject.transform.SetParent(ContentGameObject.transform);
                Item.gameObject.transform.localScale = Vector3.one;
            }
        }
    }


}
