
using Assets.Scripts.Ability;
using Assets.Scripts.CommonEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Object : MonoBehaviour
{
    public float Healt = 0.0f;
    public float Atack = 0.0f;
    [HideInInspector]
    public float TakingDamage = 0.0f;
    public int Size = 1;
    public SpriteRenderer SpriteRenderer;
    public ObjectType Type;
    public IMoveable MovementComponent;
    public IProducer ProducerComponent;
    public Vector2 Target = Vector2.zero;
    public abstract PreviewData GetPreview();
    public bool IsCollision(Object Target)
    {
        List<Vector2> SourceArea = GetArea();
        List<Vector2> TargetArea = Target.GetArea();
        foreach (var SourceAreaLoc in SourceArea)
        {
            foreach (var TargetAreaLoc in TargetArea)
            {
                if (TargetAreaLoc == SourceAreaLoc) return true;
            }
        }
        return false;
    }
    public bool IsCollision(Vector2 Target)
    {
        List<Vector2> SourceArea = GetArea();
        foreach (var SourceAreaLoc in SourceArea)
        {
            if (Target == SourceAreaLoc) return true;
        }
        return false;
    }
    public List<Vector2> GetArea()
    {
        int x = (int)this.gameObject.transform.position.x;
        int y = (int)this.gameObject.transform.position.y;
        List<Vector2> Area = new List<Vector2>();
        for (int i = 0;i < Size;i++)
        {
            for(int j = 0; j < Size; j++)
            {
                Area.Add(new Vector2(x + (j), y - (i)));
            }
        }
        return Area;
    }
    public void TakeDamage(Object o)
    {
        TakingDamage += o.Atack;
        if ((Healt- TakingDamage) <= 0)
        {
            TakingDamage = 0;
            GameManager.Instance.GridManager.RemoveObject(this);
        }
    }
}
