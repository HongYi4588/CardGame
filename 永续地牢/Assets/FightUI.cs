using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//ս������
public class FightUI : UIBase
{
    private Text cardCountTxt;//��������
    private Text noCardCountTxt;//���ƶ�����
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Text fyTxt;//������ֵ
    private List<CardItem> cardItemsList;//�洢��������ļ���

    private void Awake()
    {
        cardItemsList = new List<CardItem>();
        cardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        noCardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        powerTxt = transform.Find("mana/Text").GetComponent<Text>();
        hpTxt = transform.Find("hp/moneyTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();
        fyTxt = transform.Find("hp/fangyu/Text").GetComponent<Text>();
    }

    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardCount();
    }
    //����Ѫ����ʾ
    public void UpdateHp()
    {

    }

    //��������
    public void UpdatePower()
    {

    }

    //���·���
    public void UpdateDefense()
    {

    }

    //���¿�������
    public void UpdateCardCount()
    {

    }

    //�������ƶ�����
    public void UpdateUsedCardCount()
    {

    }

    //������������
    public void CreateCardItem(int count)
    {
        if(count > FightCardManager.Instance.cardList.Count)
        {
            count = FightCardManager.Instance.cardList.Count;

        }
        for(int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resource.Load("UI/CardItem"), transform)as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000.- 700);
            var item = obj.AddComponent<CardItem>();
            string cardId = FightCardManager.Instancce.DrawCard();
            Dictionary<string, string> data = GameconfigManager.Instance.GetCardById(cardId);
            item.Init(data);
            cardItemsList.Add(item);
        }
    }
}
