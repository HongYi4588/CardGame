using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ÏûºÄÅÆ¶Ñ½çÃæ
/// </summary>
public class RemoveCardsUI : UIBase
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = transform.Find("scroll/bg/grid/CardItem").gameObject;
        Transform parentTf = transform.Find("scroll/bg/grid");
        for (int i = 0; i < FightCardManager.Instance.removeCardList.Count; i++)
        {
            GameObject obj = Instantiate(prefab, parentTf) as GameObject;
            obj.SetActive(true);
            string cardId = FightCardManager.Instance.removeCardList[i];
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
