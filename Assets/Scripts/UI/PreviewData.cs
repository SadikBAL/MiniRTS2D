using Assets.Scripts.CommonEnums;
using System.Collections.Generic;
using UnityEngine;

public class PreviewData
{
    public Object Owner { get; set; }
    public string Name { get; set; }
    public string InfoDetail { get; set; }
    public Sprite Image { get; set; }
    public List<ObjectType> Producer { get; set; }
}