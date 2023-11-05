using Assets.Scripts.CommonEnums;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private List<ActiveAnimaiton> ActiveAnimationList;

    private bool IsAnimationCompleted;
    void Start()
    {
        ActiveAnimationList = new List<ActiveAnimaiton>();
    }

    void Update()
    {
        foreach (ActiveAnimaiton Animation in ActiveAnimationList)
        {
            IsAnimationCompleted = true;

            Animation.Object.Timer += Time.deltaTime;
            Animation.Object.Percent = Mathf.Clamp01(Animation.Object.Timer / Animation.Object.Duration);
            switch (Animation.Object.EaseType)
            {
                case EaseType.Lerp:
                    if (Animation.Object.AnimationType == AnimationType.Position)
                    {
                        Animation.Object.GameObject.transform.position = Vector3.Lerp(Animation.Object.StartPosition, Animation.Object.EndPosition, Animation.Object.Percent);
                    }
                       
                    else if(Animation.Object.AnimationType == AnimationType.Scale)
                    {
                        Animation.Object.GameObject.transform.localScale = Vector3.Lerp(Animation.Object.StartPosition, Animation.Object.EndPosition, Animation.Object.Percent);
                    }
                    else if(Animation.Object.AnimationType == AnimationType.AStarPath)
                    {
                        float DeltaTime = Animation.Object.Duration / (Animation.Object.Nodes.Count - 1);
                        int NodeIndex = (int)(Animation.Object.Timer / DeltaTime);
                        if (NodeIndex >= (Animation.Object.Nodes.Count - 1) || NodeIndex < 0)
                        {

                        }
                        else
                        {
                            Animation.Object.GameObject.transform.position = Vector3.Lerp(Animation.Object.Nodes[NodeIndex].position, Animation.Object.Nodes[NodeIndex + 1].position, Mathf.Clamp01(Animation.Object.Timer % DeltaTime / DeltaTime));
                        }


                    }

                    break;
            }
            if (Animation.Object.Percent < 1)
                IsAnimationCompleted = false;

            if (IsAnimationCompleted)
            {
                Animation.OnCompleted.Invoke();
                ActiveAnimationList.Remove(Animation);
                break;
            }
        }
    }
    public void Move(SlideObject MoveObject,Action OnCompleted)
    {
        foreach(ActiveAnimaiton a in ActiveAnimationList)
        {
            if(a.Object.GameObject == MoveObject.GameObject)
            {
                ActiveAnimationList.Remove(a);
                break;
            }
        }
        ActiveAnimaiton Anim = new ActiveAnimaiton();
        Anim.Object = MoveObject;
        Anim.OnCompleted = OnCompleted;
        ActiveAnimationList.Add(Anim);
    }
}

public enum AnimationType
{
    Position,
    Scale,
    AStarPath
}
public enum EaseType
{
    Lerp
}
public class SlideObject
{
    public AnimationType AnimationType;
    public GameObject GameObject;
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public float Duration;
    public EaseType EaseType;
    public float Timer = 0.0f;
    public float Percent = 0;
    public List<Node> Nodes;

}
public class ActiveAnimaiton
{
    public SlideObject Object;
    public Action OnCompleted;
}