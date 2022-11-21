using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMonster : MonsterBase
{
    private RectTransform uiTrans;

    [Header("�ƶ��ٶ�")]
    public int moveSpeed;

    [Header("��ǰ�Ƿ�Ӧ���ƶ�")]
    public bool shouldMove = true;

    private void Awake()
    {
        uiTrans = GetComponent<RectTransform>();
    }

    public override void Destroy()
    {
        shouldMove = false;
        uiTrans.anchoredPosition3D = new Vector3(2000, 0, 0);
        MonsterPool.Instance.Return(this);
    }

    public override void Move(float deltaTime)
    {
        Vector3 oldPos = uiTrans.anchoredPosition3D;
        Vector3 newPos = new Vector3(oldPos.x - moveSpeed * deltaTime, oldPos.y, oldPos.z);
        uiTrans.anchoredPosition3D = newPos;
        if (newPos.x <= GlobalConfig.Instance.GetHitPosX())
        {
            Destroy();
        }
    }

    private void Update()
    {
        if (shouldMove)
        {
            Move(Time.deltaTime);
        }
    }

    public override string Type()
    {
        return "Dummy";
    }

    public override void Spawn(Transform parent, Vector3 pos)
    {
        shouldMove = true;
        transform.SetParent(parent);
        uiTrans.anchoredPosition3D = pos;
    }

    public override void SetMoveSpeed(int moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public override int GetMoveSpeed()
    {
        return moveSpeed;
    }
}
