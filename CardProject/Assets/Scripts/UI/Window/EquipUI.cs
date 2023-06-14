using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipUI : UIBase
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("addCoinBtn").GetComponent<Button>().onClick.AddListener(addCoinBtn);
        transform.Find("addHpBtn").GetComponent<Button>().onClick.AddListener(addHpBtn);
        transform.Find("levelUpBtn").GetComponent<Button>().onClick.AddListener(onLevelUpBtn);
        transform.Find("close").GetComponent<Button>().onClick.AddListener(onCloseBtn);
    }


    void addCoinBtn()
    {

        int ran = Random.Range(10, 100);
      
        RoleManager.Instance.Money += ran;

        onCloseBtn();

        UIManager.Instance.ShowTip($"»ñµÃ{ran}½ð±Ò", Color.green);

    }

    void addHpBtn()
    {
        FightManager.Instance.CurHp = FightManager.Instance.MaxHp;
        onCloseBtn();
    }

    void onLevelUpBtn()
    {
        UIManager.Instance.ShowUI<LevelUpCardsUI>("LevelUpCardsUI");
        Close();
    }


    void onCloseBtn()
    {
        Close();

        UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
    }
}
