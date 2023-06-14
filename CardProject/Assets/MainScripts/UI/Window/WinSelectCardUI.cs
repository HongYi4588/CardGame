using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class WinSelectCardUI : UIBase
{
    private void Start()
    {
        GameObject prefab = transform.Find("scroll/bg/grid/CardItem").gameObject;
        Transform parentTf = transform.Find("scroll/bg/grid");
        List<Dictionary<string, string>> cardList = GameConfigManager.Instance.GetCardLines();
        for (int i = 0; i < 3; i++)
        {
            int ranIndex = Random.Range(0, cardList.Count);
            GameObject obj = Instantiate(prefab, parentTf) as GameObject;
            obj.SetActive(true);
            string cardId = cardList[ranIndex]["Id"];
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            CardItem item = obj.AddComponent<NoramlCard>();
            item.Init(data);

            obj.transform.Find("bg/select").GetComponent<Button>().onClick.AddListener(delegate ()
            {
                RoleManager.Instance.cardList.Add(cardId);

                UIManager.Instance.CloseALLUI();
                UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
            });
        }


        transform.Find("closeBtn").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            UIManager.Instance.CloseALLUI();
            UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
        });

        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.35f);
    }
}
