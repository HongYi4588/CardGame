using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql;
using MySql.Data.MySqlClient;
using System.Data;

public class UserData
{
    public MySqlDataReader GetData(string Id)
    {
        string sqlStr = "select Money,Cards from role where Id=@Id";
        MySqlParameter[] parameters = new MySqlParameter[]
        {
                new MySqlParameter("@Id",Id),
        };
        return MySqlHelper.ExecuteReader(CommandType.Text, sqlStr, parameters);
    }

    public void UpdateData(string id, string money, string cards)
    {
        string sqlStr = "update role set Money =@money.Cards=@cards where Id =@id";
        MySqlParameter[] parameters = new MySqlParameter[]
        {
                new MySqlParameter("@id",id),
                new MySqlParameter("@money",money),
                new MySqlParameter("@cards",cards)
        };
        MySqlHelper.ExecuteNonQuery(CommandType.Text, sqlStr, parameters);
    }
}
