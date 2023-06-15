using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour//继承自MonoBehaviour，用于管理关卡的加载和切换
{
    public static LevelManager Instance;//用于获取LevelManager的单例实例

    public List<LevelData> Levels;//用于存储关卡数据

    public LevelData currentLevel;//用于存储当前选中的关卡数据

    private int Index = 0;//用于记录当前关卡的索引

    private void Awake()//是MonoBehaviour生命周期中的一个回调方法，在脚本实例被加载时执行
    {
        Instance = this;
        Levels = new List<LevelData>();//用于存储关卡数据
    }

    public void Init()//用于初始化关卡数据
    {
        List<Dictionary<string, string>> lines = GameConfigManager.Instance.GetLevelLines();//获取关卡数据的列表
        foreach (var item in lines)//遍历关卡数据的列表
        {
            LevelData data = new LevelData();//用于存储关卡数据
            data.data = item;//将当前关卡数据赋值
            data.IsFinish = false;
            Levels.Add(data);//存储关卡数据
        }
        Index = 0;
        Levels[0].IsUnLock = true;//解锁第一关
    }

    public void SelectLevel(LevelData levelData)//用于选择关卡
    {
        currentLevel = levelData;

        FightManager.Instance.ChangeType(FightType.Init);//将战斗类型设置为Init
    }

    public bool UnNextLevel()//下一关
    {
        Index++;//切换下一关
        if (Index > Levels.Count - 1)
        {
            return false;
        }
        else
        {
            Levels[Index].IsUnLock = true;
            return true;
        }
    }
}
