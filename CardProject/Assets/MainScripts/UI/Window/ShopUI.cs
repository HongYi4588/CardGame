using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// 商店界面
/// </summary>
public class ShopUI : UIBase
{
    // Start is called before the first frame update
    void Start()
    {
        updateMoney();
        transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            Close();
            UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
        });
        transform.Find("delBtn").GetComponent<Button>().onClick.AddListener(onDelBtn);

        GameObject prefab = transform.Find("content/CardItem").gameObject;
        Transform parentTf = transform.Find("content");
        List<Dictionary<string, string>> cardList = GameConfigManager.Instance.GetCardLines();
        for (int i = 0; i < 4; i++)
        {
            int ranIndex = Random.Range(0, cardList.Count);
            GameObject obj = Instantiate(prefab, parentTf) as GameObject;
            obj.SetActive(true);
            string cardId = cardList[ranIndex]["Id"];

            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            CardItem item = obj.AddComponent<NoramlCard>();

            item.Init(data);

            int money = int.Parse(data["Money"]);

            obj.transform.Find("buyBtn/moneyTxt").GetComponent<Text>().text = money.ToString();

            obj.transform.Find("buyBtn").GetComponent<Button>().onClick.AddListener(delegate ()
            {
                if (RoleManager.Instance.Money < money)
                {
                    UIManager.Instance.ShowTip("金币不足!", Color.red);
                }
                else
                {
                    RoleManager.Instance.Money -= money;
                    UIManager.Instance.ShowTip("购买成功!", Color.green);
                    Destroy(obj);
                    RoleManager.Instance.cardList.Add(cardId);
                    updateMoney();
                }
            });
        }
    }

    public void updateMoney()
    {
        transform.Find("money/Text").GetComponent<Text>().text = RoleManager.Instance.Money.ToString();
    }

    //删除卡牌
    void onDelBtn()
    {
        if (RoleManager.Instance.Money < 100)
        {
            UIManager.Instance.ShowTip("金币不足", Color.red);
        }
        else
        {
            RoleManager.Instance.Money -= 100;
            updateMoney();
            UIManager.Instance.ShowUI<DelCardsUI>("DelCardsUI");
        }
    }
}
