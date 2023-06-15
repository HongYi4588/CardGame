using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
/// <summary>
/// boss脚本
/// </summary>
public class Boss : Enemy
{
    public Transform fireTf;

    bool isFire = false;

    protected override void Start()
    {
        _meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        ani = transform.GetComponent<Animator>();

        type = ActionType.None;
        hpItemObj = UIManager.Instance.CreateHpItem();
        actionObj = UIManager.Instance.CreateActionIcon();

        attackTf = actionObj.transform.Find("attack");
        defendTf = actionObj.transform.Find("defend");
        fireTf = actionObj.transform.Find("fire");
        defendTxt = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpTxt = hpItemObj.transform.Find("hpTxt").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();

        //设置血条 行动力位置
        Vector3 pos = transform.position;
        pos.y = 0;
        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(pos + Vector3.down * 0.1f);
        actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position);



        //初始化数值
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);
        DefendPower = int.Parse(data["Defend"]);

        attackTf.transform.Find("txt").GetComponent<Text>().text = Attack.ToString();

        UpdateHp();
        UpdateDefend();
        SetRandomAction();

       
    }

    public override void SetRandomAction()
    {
        int ran = Random.Range(1, 3);

        type = (ActionType)ran;

        if (CurHp <= MaxHp / 2 && isFire == false)
        {
            isFire = true;
            type = ActionType.Anger;
        }

        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(true);
                fireTf.gameObject.SetActive(false);
                break;
            case ActionType.Attack:
                attackTf.gameObject.SetActive(true);
                defendTf.gameObject.SetActive(false);
                fireTf.gameObject.SetActive(false);
                break;
            case ActionType.Anger:
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(false);
                fireTf.gameObject.SetActive(true);
                break;
        }
    }

    public override void HideAction()
    {
        base.HideAction();
        fireTf.gameObject.SetActive(false);
    }

    public override IEnumerator DoAction()
    {
        HideAction();

        //播放对应的动画(可以配置到excel表 这里都默认播放攻击)
        ani.Play("attack");
        //等待某一时间的后执行对应的行为(也可以配置到excel表)
        yield return new WaitForSeconds(0.5f);//这里写死了

        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:

                //加防御
                Defend += DefendPower;
                UpdateDefend();
                //可以播放对应的特效
                break;
            case ActionType.Attack:

                //玩家扣血
                FightManager.Instance.GetPlayerHit(Attack);

                //摄像机可以抖一抖
                Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
                break;
            case ActionType.Anger:
                //加防御
                Defend += 5;
                //加攻击力
                Attack += 5;

                attackTf.transform.Find("txt").GetComponent<Text>().text = Attack.ToString();

                UpdateDefend();
                break;
        }

        //等待动画播放完
        yield return new WaitForSeconds(1);
        //播放待机
        ani.Play("idle");
    }
}
