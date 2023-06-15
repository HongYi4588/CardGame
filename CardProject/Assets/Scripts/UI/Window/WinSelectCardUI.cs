using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class WinSelectCardUI : UIBase
{
    private void Start()
    {
        // ��ȡ����Ԥ������
        GameObject prefab = transform.Find("scroll/bg/grid/CardItem").gameObject;
        // ��ȡ������� Transform ���
        Transform parentTf = transform.Find("scroll/bg/grid");
        // ��ȡ���������б�
        List<Dictionary<string, string>> cardList = GameConfigManager.Instance.GetCardLines();

        // ѭ�����ɿ���
        for (int i = 0; i < 3; i++)
        {
            // ���ѡ��һ�ſ���
            int ranIndex = Random.Range(0, cardList.Count);
            // ʵ��������Ԥ�����岢����Ϊ����״̬
            GameObject obj = Instantiate(prefab, parentTf) as GameObject;
            obj.SetActive(true);
            // ��ȡѡ�еĿ��Ƶ�����
            string cardId = cardList[ranIndex]["Id"];
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            // ��ӿ������������ʼ����������
            CardItem item = obj.AddComponent<NoramlCard>();
            item.Init(data);

            // Ϊ���Ƶ�ѡ��ť��ӵ���¼�
            obj.transform.Find("bg/select").GetComponent<Button>().onClick.AddListener(delegate ()
            {
                // ��ѡ�еĿ�����ӵ���ɫ�������еĿ����б�
                RoleManager.Instance.cardList.Add(cardId);

                // �ر�����UI����
                UIManager.Instance.CloseALLUI();
                // ��ʾѡ��ؿ���UI����
                UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
            });
        }

        // �رհ�ť�ĵ���¼�
        transform.Find("closeBtn").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            UIManager.Instance.ShowUI<GameOverUI>("GameOverUI");
            // �ر�����UI����
            //UIManager.Instance.CloseALLUI();
            // ��ʾѡ��ؿ���UI����
            //UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
        });

        // ���ó�ʼ����Ϊ0��ͨ���������ɵ�1
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.35f);
    }
}
