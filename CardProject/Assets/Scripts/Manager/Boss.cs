using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
/// <summary>
/// boss�ű�
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

        //����Ѫ�� �ж���λ��
        Vector3 pos = transform.position;
        pos.y = 0;
        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(pos + Vector3.down * 0.1f);
        actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position);



        //��ʼ����ֵ
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

        //���Ŷ�Ӧ�Ķ���(�������õ�excel�� ���ﶼĬ�ϲ��Ź���)
        ani.Play("attack");
        //�ȴ�ĳһʱ��ĺ�ִ�ж�Ӧ����Ϊ(Ҳ�������õ�excel��)
        yield return new WaitForSeconds(0.5f);//����д����

        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:

                //�ӷ���
                Defend += DefendPower;
                UpdateDefend();
                //���Բ��Ŷ�Ӧ����Ч
                break;
            case ActionType.Attack:

                //��ҿ�Ѫ
                FightManager.Instance.GetPlayerHit(Attack);

                //��������Զ�һ��
                Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
                break;
            case ActionType.Anger:
                //�ӷ���
                Defend += 5;
                //�ӹ�����
                Attack += 5;

                attackTf.transform.Find("txt").GetComponent<Text>().text = Attack.ToString();

                UpdateDefend();
                break;
        }

        //�ȴ�����������
        yield return new WaitForSeconds(1);
        //���Ŵ���
        ani.Play("idle");
    }
}
