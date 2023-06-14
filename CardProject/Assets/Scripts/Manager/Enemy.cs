using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 敌人的行动枚举
/// </summary>
public enum ActionType
{
    None,
    Defend,//防御
    Attack,//攻击
    Anger,//狂暴
}


/// <summary>
/// 敌人脚本
/// </summary>
public class Enemy : MonoBehaviour
{

    protected Dictionary<string, string> data;//敌人数据表信息

    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionObj;

    //ui相关
    public Transform attackTf;
    public Transform defendTf;
    public Text defendTxt;
    public Text hpTxt;
    public Image hpImg;

    //数据相关
    public int Defend;
    public int DefendPower;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    //组件相关 (小怪身上的组件)
   protected SkinnedMeshRenderer _meshRenderer;
    public Animator ani;

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    protected virtual void Start()
    {
        _meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        ani = transform.GetComponent<Animator>();

        type = ActionType.None;
        hpItemObj = UIManager.Instance.CreateHpItem();
        actionObj = UIManager.Instance.CreateActionIcon();

        attackTf = actionObj.transform.Find("attack");
        defendTf = actionObj.transform.Find("defend");

        defendTxt = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpTxt = hpItemObj.transform.Find("hpTxt").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();

        //设置血条 行动力位置
        Vector3 pos = transform.position;
        pos.y = 0;
        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(pos + Vector3.down * 0.1f);

        if (transform.Find("head"))
            actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position);
        else {
            actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.GetChild(0).Find("RigHead").position);
        }


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

    /// <summary>
    /// 随机一个行动
    /// </summary>
    public virtual void SetRandomAction()
    {
        int ran = Random.Range(1, 3);

        type = (ActionType)ran;

        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(true);
                break;
            case ActionType.Attack:
                attackTf.gameObject.SetActive(true);
                defendTf.gameObject.SetActive(false);
                break;

        }
    }

    /// <summary>
    /// 更新血量
    /// </summary>
    public void UpdateHp()
    {
        hpTxt.text = CurHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurHp / (float)MaxHp;
    }

    /// <summary>
    /// 更新防御信息
    /// </summary>
    public void UpdateDefend()
    {
        defendTxt.text = Defend.ToString();
    }

    /// <summary>
    /// 被攻击卡选中，显示红边
    /// </summary>
    public void OnSelect()
    {
        _meshRenderer.material.SetColor("_OtlColor", Color.red);//这里的"_OtlColor"，对应小怪材质中shader的outline的颜色
    }

    /// <summary>
    /// 未选中
    /// </summary>
    public void OnUnSelect()
    {
        _meshRenderer.material.SetColor("_OtlColor", Color.black);
    }

    /// <summary>
    /// 受伤
    /// </summary>
    public void Hit(int val)
    {
        //先扣护盾
        if (Defend >= val)
        {
            //扣护盾
            Defend -= val;

            //播放受伤
            ani.Play("hit", 0, 0);
        }
        else
        {
            val = val - Defend;
            Defend = 0;
            CurHp -= val;
            if (CurHp <= 0)
            {
                CurHp = 0;
                //播放死亡动画
                ani.Play("die");

                //随机获得一些金币
                int ranMoney = Random.Range(80, 180);
                RoleManager.Instance.Money += ranMoney;
                UIManager.Instance.ShowTip($"获得了{ranMoney}金币", Color.yellow);

                //敌人从列表中移除
                EnemyManager.Instance.DeleteEnemy(this);

                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
            }
            else
            {
                //受伤
                ani.Play("hit", 0, 0);
            }
        }

        //刷新血量等ui
        UpdateDefend();
        UpdateHp();
    }


    /// <summary>
    /// 隐藏怪物头上的行动标志
    /// </summary>
    public virtual void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);
    }

    /// <summary>
    /// 执行敌人行动
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator DoAction()
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

        }

        //等待动画播放完
        yield return new WaitForSeconds(1);
        //播放待机
        ani.Play("idle");
    }

}
