using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 战斗枚举
/// </summary>
public enum FightType
{
    None,
    Init,
    Player,//玩家回合
    Enemy,//敌人回合
    Win,
    Loss
}



/// <summary>
/// 战斗管理器
/// </summary>
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public FightUnit fighUnit;//战斗单元

    public int MaxHp=30;//最大血量
    public int CurHp=30;//当前血量

    public int MaxPowerCount;//最大能量（卡牌使用会消耗能量）
    public int CurPowerCount;//当前能量
    public int DefenseCount;//防御值

    //初始化
    public void Init()
    {     
        MaxPowerCount = 3;
        CurPowerCount = 3;
        DefenseCount = 0;
        
        
    }


    private void Awake()
    {
        Instance = this;
        MaxHp = 30;
        CurHp = 30;
    }


    /// <summary>
    /// 切换战斗类型
    /// </summary>
    public void ChangeType(FightType type)
    {
        switch (type)
        {
            case FightType.None:
                break;
            case FightType.Init:
                fighUnit = new FightInit();
                break;
            case FightType.Player:
                fighUnit = new Fight_PlayerTurn();
                break;
            case FightType.Enemy:
                fighUnit = new Fight_EnemyTurn();
                break;
            case FightType.Win:
                fighUnit = new Fight_Win();
                break;
            case FightType.Loss:
                fighUnit = new Fight_Loss();
                break;
        }
        fighUnit.Init();//初始化

    }


    /// <summary>
    /// 玩家受伤逻辑
    /// </summary>
    public void GetPlayerHit(int hit)
    {
        //加护盾
        if (DefenseCount >= hit)
        {
            DefenseCount -= hit;
        }
        else
        {
            hit = hit - DefenseCount;
            DefenseCount = 0;
            CurHp -= hit;
            if (CurHp <= 0)
            {
                CurHp = 0;

                //切换到游戏失败状态
                ChangeType(FightType.Loss);
            }
        }

        //更新界面
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
    }



    private void Update()
    {
        if (fighUnit != null)
        {
            fighUnit.OnUpdate();
        }
    }



}
