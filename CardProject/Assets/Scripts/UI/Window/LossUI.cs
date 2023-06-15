using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LossUI : UIBase
{
    private void Start()
    {
        transform.Find("closeBtn").GetComponent<Button>().onClick.AddListener(delegate ()// 当按钮被点击时执行以下代码块
        {
            EnemyManager.Instance.DeleteAll();//删除所有敌人
            UIManager.Instance.CloseALLUI();// 关闭所有UI界面
            UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");// 显示名为"SelectLevelUI"的UI界面
            SceneManager.LoadScene(0);// 加载场景索引为0的场景（通常是游戏的起始场景）
        });
        transform.Find("aliveBtn").GetComponent<Button>().onClick.AddListener(delegate ()// 当按钮被点击时执行以下代码块
        {

            FightManager.Instance.CurHp = FightManager.Instance.MaxHp;
            SceneManager.LoadScene(1);
            AudioManager.Instance.PlayBGM("battle");
            //UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
        });
    }
}
