using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardsUI : UIBase
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = transform.Find("scroll/bg/grid/CardItem").gameObject;
        Transform parentTf = transform.Find("scroll/bg/grid");
        for (int i = 0; i < FightCardManager.Instance.cardList.Count; i++)
        {
            GameObject obj = Instantiate(prefab, parentTf) as GameObject;
            obj.SetActive(true);
            string cardId = FightCardManager.Instance.cardList[i];
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            CardItem item = obj.AddComponent<NoramlCard>();
            item.Init(data);
        }


        transform.Find("closeBtn").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            Close();
        });
    }

    
}
