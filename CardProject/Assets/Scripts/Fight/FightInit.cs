using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 卡牌战斗初始化
/// </summary>
public class FightInit : FightUnit
{
    public override void Init()
    {
        //初始化战斗数据
        FightManager.Instance.Init();

        //切换bgm
        AudioManager.Instance.PlayBGM("battle");

        //敌人生成
        EnemyManager.Instance.LoadRes(LevelManager.Instance.currentLevel.data["Id"]);

        //初始化战斗卡牌
        FightCardManager.Instance.Init();

        UIManager.Instance.ShowUI<MainUI>("MainUI");
        //显示战斗界面
        UIManager.Instance.ShowUI<FightUI>("FightUI");

        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);


    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }




}
