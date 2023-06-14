using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;

public class LevelUpCardsUI : UIBase
{
    private void Awake()
    {
        GameObject prefab = transform.Find("scroll/bg/grid/CardItem").gameObject;
        Transform parentTf = transform.Find("scroll/bg/grid");
        for (int i = 0; i < RoleManager.Instance.cardList.Count; i++)
        {
            GameObject obj = Instantiate(prefab, parentTf) as GameObject;
            obj.SetActive(true);
            string cardId = RoleManager.Instance.cardList[i];
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            CardItem item = obj.AddComponent<NoramlCard>();
            bool chance = true; 
            item.Init(data);
            obj.transform.Find("bg/levelUpBtn").GetComponent<Button>().onClick.AddListener(delegate ()
            {
                if (RoleManager.Instance.CheckLevelUp(cardId) == true)
                {
                    if (chance == true)
                    {
                        UIManager.Instance.ShowTip("升级成功!", Color.green);
                        chance = false;
                    }
                    else 
                    {
                        UIManager.Instance.ShowTip("已升级过卡牌！", Color.red);
                    }                   
                }
                else
                {
                    UIManager.Instance.ShowTip("已经满级!", Color.green);
                }
            });
        }


        transform.Find("closeBtn").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            Close();

            UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
        });
    }
}
