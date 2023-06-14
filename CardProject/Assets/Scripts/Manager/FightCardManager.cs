using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 战斗卡牌管理器
/// </summary>
public class FightCardManager 
{
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;//卡堆集合

    public List<string> usedCardList;//弃牌堆

    public List<string> removeCardList;//消耗牌堆

    //初始化
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();
        removeCardList = new List<string>();

        //定义临时集合
        List<string> tempList = new List<string>();
        //将玩家的卡牌存储到临时集合
        tempList.AddRange(RoleManager.Instance.cardList);

        while (tempList.Count>0)
        {
            //随机下标
            int tempIndex = Random.Range(0, tempList.Count);

            //添加到卡堆
            cardList.Add(tempList[tempIndex]);

            //临时集合删除
            tempList.RemoveAt(tempIndex);
        }

        Debug.Log(cardList.Count);
    }


    public void ResetCards()
    {     
        //定义临时集合
        List<string> tempList = new List<string>();

        for (int i = 0; i < cardList.Count; i++)
        {
            tempList.Add(cardList[i]);
        }
        for (int i = 0; i < usedCardList.Count; i++)
        {
            tempList.Add(usedCardList[i]);
        }

        cardList = new List<string>();
        usedCardList = new List<string>();

        while (tempList.Count > 0)
        {
            //随机下标
            int tempIndex = Random.Range(0, tempList.Count);

            //添加到卡堆
            cardList.Add(tempList[tempIndex]);

            //临时集合删除
            tempList.RemoveAt(tempIndex);
        }

    }

    /// <summary>
    /// 卡堆是否有卡
    /// </summary>
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    /// <summary>
    /// 抽卡
    /// </summary>
    public string DrawCard()
    {
        string id = cardList[cardList.Count - 1];
        cardList.RemoveAt(cardList.Count - 1);
        return id;
    }

    public void ResetCard()
    {
        for (int i = 0; i < usedCardList.Count; i++)
        {
            cardList.Add(usedCardList[i]);
        }
        usedCardList.Clear();
    }
}
