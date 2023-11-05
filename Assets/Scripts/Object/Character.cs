using Assets.Scripts.Ability;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Object
{
    public Character() 
    {
        MovementComponent = new Move();
        ProducerComponent = new NoneProducer();
    }
    public override PreviewData GetPreview()
    {
        return new PreviewData
        {
            Image = SpriteRenderer.sprite,
            Name = Type.ToString(),
            InfoDetail = "Healt : " + Healt + "\n" + "Atack : " + Atack + "\n" + "Size : " + (Size * Size),
            Producer = ProducerComponent.GetProducts()
        };
    }
}
