using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void Move(Object Source, Vector3 Target);
    void Atack(Object Source, Object Target);
    void Init();
}
