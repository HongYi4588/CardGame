using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 胜利
/// </summary>
public class Fight_Win : FightUnit
{
    public override void Init()
    {
        //删除所有卡牌
        UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();

        FightManager.Instance.StopAllCoroutines();

        LevelManager.Instance.currentLevel.IsFinish = true;

        if (LevelManager.Instance.UnNextLevel() == true)
        {
            GameObject obj = Object.Instantiate(Resources.Load("Model/Chest")) as GameObject;
            obj.AddComponent<Chest>();
        }
        else
        {
            UIManager.Instance.ShowUI<GameOverUI>("GameOverUI");
        }

    }

    public override void OnUpdate()
    {

    }


}
