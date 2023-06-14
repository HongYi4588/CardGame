using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MySql.Data.MySqlClient;

/// <summary>
/// 用户信息管理器 (拥有的卡牌信息 金币等)
/// </summary>
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();

    public List<string> cardList;//存储拥有的卡牌的id

    private int _money;

    private int id = 1; //对应数据库的表id

    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            UIManager.Instance.GetUI<MainUI>("MainUI")?.onUpdateMoney(_money);
        }
    }

    public void Init()
    {
        Money = 150;
        cardList = new List<string>();

        //四张攻击卡 四张防御卡 一张效果卡
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1005");

        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");

        cardList.Add("1003");
    }


    /// <summary>
    /// 尝试升级
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool CheckLevelUp(string id)
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            if (cardList[i] == id)
            {
                Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardList[i]);
                if (data["levelUp"] == "-1")
                {
                    //不能升级
                    return false;
                }
                cardList[i] = data["levelUp"];
                return true;
            }
        }
        return false;
    }

    public void Save()
    {
        //try
        //{
        //    UserData user = new UserData();
        //    string str = "";
        //    for (int i = 0; i < cardList.Count; i++)
        //    {
        //        str += cardList[i];
        //        if (i != cardList.Count - 1)
        //        {
        //            str += "-";
        //        }
        //    }
        //    user.UpdateData(id.ToString(), Money.ToString(), str);
        //}
        //catch (Exception e)
        //{

        //    Debug.Log(e.Message.ToString());
        //}
    }

    public bool Load()
    {
        //try
        //{
        //    UserData userData = new UserData();
        //    MySqlDataReader reader = userData.GetData(id.ToString());
        //    while (reader.Read())
        //    {
        //        Money = int.Parse(reader["Money"].ToString());
        //        string[] cards = reader["Cards"].ToString().Split("-");
        //        for (int i = 0; i < cards.Length; i++)
        //        {
        //            cardList.Add(cards[i]);
        //        }
        //    }
        //    return true;
        //}
        //catch (Exception e)
        //{
        //    Debug.Log(e.Message.ToString());
        //    return false;
        //}
        return false;
    }
}
