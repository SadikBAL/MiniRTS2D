using Assets.Scripts.Ability;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Object
{
    public Building() 
    {
        MovementComponent = new Static(this);
        ProducerComponent = new SolidierProducer();
    }
    public override PreviewData GetPreview()
    {
        return new PreviewData
        {
            Owner = this,
            Image = SpriteRenderer.sprite,
            Name = Type.ToString(),
            InfoDetail = "Healt : " + (Healt - TakingDamage) + "\n" + "Size : " + (Size * Size),
            Producer = ProducerComponent.GetProducts()
        };
    }
}
