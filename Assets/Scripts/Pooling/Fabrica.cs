using Assets.Scripts.CommonEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabrica : MonoBehaviour
{
    public PrefabPool Solidier1 = new PrefabPool();
    public PrefabPool Solidier2 = new PrefabPool();
    public PrefabPool Solidier3 = new PrefabPool();
    public PrefabPool Barracks = new PrefabPool();
    public PrefabPool PowerPlant = new PrefabPool();
    private GameObject GetGameObject(ObjectType objectType)
    {
        switch(objectType)
        {
            case ObjectType.Solidier1:
                return Solidier1.GetObject();
            case ObjectType.Solidier2:
                return Solidier2.GetObject();
            case ObjectType.Solidier3:
                return Solidier3.GetObject();
                case ObjectType.Barracks: return Barracks.GetObject();
                case ObjectType.PowerPlant: return PowerPlant.GetObject();
        }
        return null;
    }
    public Object GetObject(ObjectType type)
    {
        Object obj = GetGameObject(type).GetComponent<Object>();
        if (obj == null)
            return null;
        return obj;
    }
}
