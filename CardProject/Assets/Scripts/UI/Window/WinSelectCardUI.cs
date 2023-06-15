using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class WinSelectCardUI : UIBase
{
    private void Start()
    {
        // 获取卡牌预设物体
        GameObject prefab = transform.Find("scroll/bg/grid/CardItem").gameObject;
        // 获取父物体的 Transform 组件
        Transform parentTf = transform.Find("scroll/bg/grid");
        // 获取卡牌数据列表
        List<Dictionary<string, string>> cardList = GameConfigManager.Instance.GetCardLines();

        // 循环生成卡牌
        for (int i = 0; i < 3; i++)
        {
            // 随机选择一张卡牌
            int ranIndex = Random.Range(0, cardList.Count);
            // 实例化卡牌预设物体并设置为激活状态
            GameObject obj = Instantiate(prefab, parentTf) as GameObject;
            obj.SetActive(true);
            // 获取选中的卡牌的数据
            string cardId = cardList[ranIndex]["Id"];
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            // 添加卡牌组件，并初始化卡牌数据
            CardItem item = obj.AddComponent<NoramlCard>();
            item.Init(data);

            // 为卡牌的选择按钮添加点击事件
            obj.transform.Find("bg/select").GetComponent<Button>().onClick.AddListener(delegate ()
            {
                // 将选中的卡牌添加到角色管理器中的卡牌列表
                RoleManager.Instance.cardList.Add(cardId);

                // 关闭所有UI界面
                UIManager.Instance.CloseALLUI();
                // 显示选择关卡的UI界面
                UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
            });
        }

        // 关闭按钮的点击事件
        transform.Find("closeBtn").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            UIManager.Instance.ShowUI<GameOverUI>("GameOverUI");
            // 关闭所有UI界面
            //UIManager.Instance.CloseALLUI();
            // 显示选择关卡的UI界面
            //UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
        });

        // 设置初始缩放为0，通过动画过渡到1
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.35f);
    }
}
