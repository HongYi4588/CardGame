using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public List<LevelData> Levels;

    public LevelData currentLevel;

    private int Index = 0;

    private void Awake()
    {
        Instance = this;
        Levels = new List<LevelData>();
    }

    public void Init()
    {
        List<Dictionary<string, string>> lines = GameConfigManager.Instance.GetLevelLines();
        foreach (var item in lines)
        {
            LevelData data = new LevelData();
            data.data = item;
            data.IsFinish = false;
            Levels.Add(data);
        }
        Index = 0;
        Levels[0].IsUnLock = true;
    }

    public void SelectLevel(LevelData levelData)
    {
        currentLevel = levelData;

        FightManager.Instance.ChangeType(FightType.Init);
    }

    public bool UnNextLevel()
    {
        Index++;
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
