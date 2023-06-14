using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 失败
/// </summary>
public class Fight_Loss : FightUnit
{

    public override void Init()
    {
        Debug.Log("游戏失败!");
        FightManager.Instance.StopAllCoroutines();
        UIManager.Instance.ShowUI<LossUI>("LossUI");
    }

    public override void OnUpdate()
    {

    }


}
