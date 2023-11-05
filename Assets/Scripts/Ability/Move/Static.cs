using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Static : IMoveable
{
    private Object Owner;
    public Static(Object Owner)
    {
        this.Owner = Owner;
        
    }
    public void Atack(Object Source, Object Target)
    {
        
    }
    public void Move(Object Source, Vector3 Target)
    {
      
        Vector3 MovePosition = Vector2.zero;
        bool Result = GameManager.Instance.GridManager.FindNearLocation(Target, ref MovePosition);
        if (Result)
        {
            Debug.Log("Update Target location to. . ." + MovePosition);
            Source.Target = MovePosition;
        }
        else
        {
             Result = GameManager.Instance.GridManager.FindRandomEmptyLocation(ref MovePosition);
            if (Result)
            {
                Debug.Log("Update Target location to. . ." + MovePosition);
                Source.Target = MovePosition;
            }
            else
            {
                Debug.Log("There is no empty location . . .");
            }
        }
    }
    public void Init()
    {
        Vector3 MovePosition = Vector2.zero;
        bool Result = GameManager.Instance.GridManager.FindNearLocation(Owner, ref MovePosition);
        if (Result)
        {
            Debug.Log("Update Target location to. . ." + MovePosition);
            Owner.Target = MovePosition;
        }
    }
    
}
