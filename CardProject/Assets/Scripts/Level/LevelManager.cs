using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour//�̳���MonoBehaviour�����ڹ���ؿ��ļ��غ��л�
{
    public static LevelManager Instance;//���ڻ�ȡLevelManager�ĵ���ʵ��

    public List<LevelData> Levels;//���ڴ洢�ؿ�����

    public LevelData currentLevel;//���ڴ洢��ǰѡ�еĹؿ�����

    private int Index = 0;//���ڼ�¼��ǰ�ؿ�������

    private void Awake()//��MonoBehaviour���������е�һ���ص��������ڽű�ʵ��������ʱִ��
    {
        Instance = this;
        Levels = new List<LevelData>();//���ڴ洢�ؿ�����
    }

    public void Init()//���ڳ�ʼ���ؿ�����
    {
        List<Dictionary<string, string>> lines = GameConfigManager.Instance.GetLevelLines();//��ȡ�ؿ����ݵ��б�
        foreach (var item in lines)//�����ؿ����ݵ��б�
        {
            LevelData data = new LevelData();//���ڴ洢�ؿ�����
            data.data = item;//����ǰ�ؿ����ݸ�ֵ
            data.IsFinish = false;
            Levels.Add(data);//�洢�ؿ�����
        }
        Index = 0;
        Levels[0].IsUnLock = true;//������һ��
    }

    public void SelectLevel(LevelData levelData)//����ѡ��ؿ�
    {
        currentLevel = levelData;

        FightManager.Instance.ChangeType(FightType.Init);//��ս����������ΪInit
    }

    public bool UnNextLevel()//��һ��
    {
        Index++;//�л���һ��
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
