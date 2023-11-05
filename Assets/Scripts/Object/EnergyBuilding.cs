using Assets.Scripts.Ability;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBuilding : Object
{
    public EnergyBuilding()
    {
        MovementComponent = new Static(this);
        ProducerComponent = new NoneProducer();
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
