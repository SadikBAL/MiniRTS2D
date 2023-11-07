using Assets.Scripts.CommonEnums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PreviewPopup : MonoBehaviour
{
    public GameObject Popup;
    public Image Image;
    public TMP_Text Title;
    public TMP_Text Info;
    public Button[] ProductButton;
    public Image[] ProductButtonImage;
    public void Show(PreviewData Data)
    {

        Image.sprite = Data.Image;
        Title.text = Data.Name;
        Info.text = Data.InfoDetail;
        Popup.SetActive(true);

        for (int i = 0; i < ProductButton.Length; i++)
        {
            ProductButton[i].onClick.RemoveAllListeners();
            ProductButton[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < Data.Producer.Count; i++)
        {
           if(i < ProductButton.Length && i < ProductButtonImage.Length)
            {
                int a = i;
                ProductButtonImage[i].sprite = GameManager.Instance.GetSprite(Data.Producer[i]);
                ProductButton[i].gameObject.SetActive(true);
                ProductButton[i].onClick.AddListener(delegate { OnClick(Data.Owner, Data.Producer[a]); });
            }
        }
    }
    public void Hide()
    {
        Popup.SetActive(false);
    }
    void OnClick(Object Owner, ObjectType Type)
    {
        Object Solidier = GameManager.Instance.ObjectFabrica.GetObject(Type);
        Solidier.gameObject.transform.position = Owner.Target;
        if (Solidier != null) 
        {
            if (!GameManager.Instance.GridManager.AddObject(Solidier))
            {
                Vector3 Result = Vector3.zero;
                bool a = GameManager.Instance.GridManager.FindNearLocation(Owner, ref Result);
                Solidier.gameObject.transform.position = Result;
                if (!GameManager.Instance.GridManager.AddObject(Solidier))
                {
                    Solidier.GetComponent<PooledPrefab>().Pool.ReturnObject(Solidier.gameObject);
                }
            }
        }
    }

}
