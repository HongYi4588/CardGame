using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//战斗界面
public class FightUI : UIBase
{
    private Text cardCountTxt;//卡牌数量
    private Text noCardCountTxt;//弃牌堆数量
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Text fyTxt;//防御数值
    private List<CardItem> cardItemsList;//存储卡牌物体的集合

    private void Awake()
    {
        cardItemsList = new List<CardItem>();
        cardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        noCardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        powerTxt = transform.Find("mana/Text").GetComponent<Text>();
        hpTxt = transform.Find("hp/moneyTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();
        fyTxt = transform.Find("hp/fangyu/Text").GetComponent<Text>();
    }

    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardCount();
    }
    //更新血量显示
    public void UpdateHp()
    {

    }

    //更新能量
    public void UpdatePower()
    {

    }

    //更新防御
    public void UpdateDefense()
    {

    }

    //更新卡队数量
    public void UpdateCardCount()
    {

    }

    //更新弃牌堆数量
    public void UpdateUsedCardCount()
    {

    }

    //创建卡牌物体
    public void CreateCardItem(int count)
    {
        if(count > FightCardManager.Instance.cardList.Count)
        {
            count = FightCardManager.Instance.cardList.Count;

        }
        for(int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resource.Load("UI/CardItem"), transform)as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000.- 700);
            var item = obj.AddComponent<CardItem>();
            string cardId = FightCardManager.Instancce.DrawCard();
            Dictionary<string, string> data = GameconfigManager.Instance.GetCardById(cardId);
            item.Init(data);
            cardItemsList.Add(item);
        }
    }
}
