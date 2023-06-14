using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


//战斗界面
public class FightUI : UIBase
{
    private Text cardCountTxt;//卡牌数量
    private Text noCardCountTxt;//弃牌堆数量
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Text fyTxt;//防御数值
    private List<CardItem> cardItemList;//存储卡牌物体的集合

    private void Awake()
    {
        cardItemList = new List<CardItem>();
        //下面的地址位置，要在预制体里面对应
        cardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        noCardCountTxt = transform.Find("noCard/icon/Text").GetComponent<Text>();
        powerTxt = transform.Find("mana/Text").GetComponent<Text>();
        hpTxt = transform.Find("hp/moneyTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();
        fyTxt = transform.Find("hp/fangyu/Text").GetComponent<Text>();
        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnBtn);
        transform.Find("hasCard").GetComponent<Button>().onClick.AddListener(onShowHasCardsUI);
        transform.Find("noCard").GetComponent<Button>().onClick.AddListener(onShowDisCardsUI);
        transform.Find("disCard").GetComponent<Button>().onClick.AddListener(onShowRemoveCardsUI);
    }

    private void onShowHasCardsUI()
    {
        UIManager.Instance.ShowUI<CardsUI>("CardsUI");
    }

    private void onShowDisCardsUI()
    {
        UIManager.Instance.ShowUI<DisCardsUI>("DisCardsUI");
    }

    /// <summary>
    /// 显示消耗卡
    /// </summary>
    private void onShowRemoveCardsUI()
    {
        UIManager.Instance.ShowUI<RemoveCardsUI>("RemoveCardsUI");
    }


    /// <summary>
    /// 玩家回合结束，切换到敌人回合
    /// </summary>
    private void onChangeTurnBtn()
    {
        //只有玩家回合才能切换
        if (FightManager.Instance.fighUnit is Fight_PlayerTurn)
        {
            FightManager.Instance.ChangeType(FightType.Enemy);
        }

    }

    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardCount();
    }




    /// <summary>
    /// 更新血量显示
    /// </summary>
    public void UpdateHp()
    {
        hpTxt.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHp;
        hpImg.fillAmount = (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp;

    }

    /// <summary>
    /// 更新能量
    /// </summary>
    public void UpdatePower()
    {
        powerTxt.text = FightManager.Instance.CurPowerCount + "/" + FightManager.Instance.MaxPowerCount;

    }

    /// <summary>
    /// 更新防御
    /// </summary>
    public void UpdateDefense()
    {
        fyTxt.text = FightManager.Instance.DefenseCount.ToString();
    }

    /// <summary>
    /// 更新卡堆数量
    /// </summary>
    public void UpdateCardCount()
    {
        cardCountTxt.text = FightCardManager.Instance.cardList.Count.ToString();
    }

    /// <summary>
    /// 更新弃牌数量
    /// </summary>
    public void UpdateUsedCardCount()
    {
        noCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }


    /// <summary>
    /// 创建卡牌物体
    /// </summary>
    public void CreateCardItem(int count)
    {
        if (count > FightCardManager.Instance.cardList.Count)
        {
            count = FightCardManager.Instance.cardList.Count;
        }
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -470);
            //var item = obj.AddComponent<CardItem>();

            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            cardItemList.Add(item);

        }

    }

    /// <summary>
    /// 更新卡牌位置
    /// </summary>
    public void UpdateCardItemPos()
    {
        float offset = 800.0f / cardItemList.Count;
        Vector2 startPos = new Vector2(-cardItemList.Count / 2.0f * offset + offset * 0.5f, -470);
        for (int i = 0; i < cardItemList.Count; i++)
        {
            cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
            startPos.x = startPos.x + offset;
        }

    }

    /// <summary>
    /// 删除卡牌物体
    /// </summary>
    public void RemoveCard(CardItem item)
    {
        AudioManager.Instance.PlayEffect("Cards/cardShove");//移除音效

        item.enabled = false;//禁用卡牌逻辑

       


        //更新使用后的卡牌数量

        transform.Find("disCard/icon/Text").GetComponent<Text>().text = FightCardManager.Instance.removeCardList.Count.ToString();

        noCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();


        //从集合中删除
        cardItemList.Remove(item);

        //刷新卡牌位置
        UpdateCardItemPos();

        //卡牌移到弃牌堆效果
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, -700), 0.25f);

        item.transform.DOScale(0, 0.25f);

        Destroy(item.gameObject, 1);
    }

    /// <summary>
    /// 删除所有卡牌
    /// </summary>
    public void RemoveAllCards()
    {
        for (int i = cardItemList.Count - 1; i >= 0; i--)
        {
            FightCardManager.Instance.usedCardList.Add(cardItemList[i].data["Id"]); //添加到弃牌堆
            RemoveCard(cardItemList[i]);
        }
    }


}
