using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    public abstract void Move(float deltaTime);
    public abstract void Destroy();

    public abstract void Spawn(Transform parent, Vector3 pos);

    public abstract void SetMoveSpeed(int moveSpeed);

    public abstract int GetMoveSpeed();

    public abstract string Type();
}
