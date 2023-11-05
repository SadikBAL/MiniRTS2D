using Assets.Scripts.CommonEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Move : IMoveable
{
    public event Action OnMoveEvent;
    public event Action OnAtackEvent;
    private Object SourceRef;
    private Object TargetRef;
    public Move()
    {
        OnMoveEvent += OnMoveComplated;
        OnAtackEvent += OnAtackComplated;
    }

    private void OnAtackComplated()
    {
        TargetRef.TakeDamage(SourceRef);
    }

    private void OnMoveComplated()
    {
        
    }

    void IMoveable.Move(Object Source, Vector3 Target)
    {
        
        List<Node> Path = GameManager.Instance.GridManager.FindPath(Source, Target);
        GameManager.Instance.AnimationManager.Move(new SlideObject
        {
            AnimationType = AnimationType.AStarPath,
            EaseType = EaseType.Lerp,
            StartPosition = Source.gameObject.transform.position,
            EndPosition = Target,
            Duration = 1,
            GameObject = Source.gameObject,
            Nodes = Path
        }, OnMoveEvent);
    }

    public void Atack(Object Source, Object Target)
    {
        SourceRef = Source; TargetRef = Target;
        Vector3 MovePosition = Vector2.zero;
        bool Result = GameManager.Instance.GridManager.FindNearLocation(Target, ref MovePosition);
        if (Result)
        {
            List<Node> lasd = GameManager.Instance.GridManager.FindPath(Source, MovePosition);
            GameManager.Instance.AnimationManager.Move(new SlideObject
            {
                AnimationType = AnimationType.AStarPath,
                EaseType = EaseType.Lerp,
                StartPosition = Source.gameObject.transform.position,
                EndPosition = MovePosition,
                Duration = 1,
                GameObject = Source.gameObject,
                Nodes = lasd
            }, OnAtackEvent);
        }

    }

    public void Init()
    {
        
    }
}
