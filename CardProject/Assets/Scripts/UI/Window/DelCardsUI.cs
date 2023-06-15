using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DelCardsUI : UIBase
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
            item.Init(data);
            obj.transform.Find("bg/levelUpBtn").GetComponent<Button>().onClick.AddListener(delegate ()
            {
                RoleManager.Instance.cardList.Remove(cardId);
                UIManager.Instance.ShowTip("É¾³ý³É¹¦!", Color.green);
                Close();
            });
        }


        transform.Find("closeBtn").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            Close();
        });
    }
}
