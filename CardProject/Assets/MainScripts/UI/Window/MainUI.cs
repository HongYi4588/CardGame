using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : UIBase
{
    private void Awake()
    {
        transform.Find("up/bagBtn").GetComponent<Button>().onClick.AddListener(onBagBtn);
        transform.Find("up/helpBtn").GetComponent<Button>().onClick.AddListener(onHelpBtn);
        onUpdateMoney(RoleManager.Instance.Money);
    }
    private void onBagBtn()
    {
        UIManager.Instance.ShowUI<BagUI>("BagUI");
    }

    private void onHelpBtn()
    {
        UIManager.Instance.ShowUI<HelpUI>("HelpUI");
    }

    public void onUpdateMoney(int val)
    {
        transform.Find("up/coin/txt").GetComponent<Text>().text = val.ToString();
    }
}
