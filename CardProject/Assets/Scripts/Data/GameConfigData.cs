﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 游戏配置表类 每个对象对应一个txt配置表
/// </summary>
public class GameConfigData 
{
    private List<Dictionary<string, string>> dataDic;//存储配置表中所有数据

    public GameConfigData(string str)
    {
        dataDic = new List<Dictionary<string, string>>();

        //换行切割
        string[] lines = str.Split('\n');
        //第一行是存储数据的类型
        string[] title = lines[0].Trim().Split('\t');//tab切割
        //从第三行(下标2)开始遍历数据 第二行数据是解析说明
        for (int i = 2; i < lines.Length; i++)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string[] temArr = lines[i].Trim().Split('\t');

            for (int j = 0; j < temArr.Length; j++)
            {
                dic.Add(title[j], temArr[j]);
            }

            dataDic.Add(dic);

        }
    }

    public List<Dictionary<string,string>> GetLines()
    {
        return dataDic;
    }

    public Dictionary<string,string> GetOneById(string id)
    {
        for (int i = 0; i < dataDic.Count; i++)
        {
            Dictionary<string, string> dic = dataDic[i];
            if (dic["Id"] == id)
            {
                return dic;
            }
        }
        return null;
    }

    

}
