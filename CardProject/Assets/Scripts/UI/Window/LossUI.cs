using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LossUI : UIBase
{
    private void Start()
    {
        transform.Find("closeBtn").GetComponent<Button>().onClick.AddListener(delegate ()// ����ť�����ʱִ�����´����
        {
            EnemyManager.Instance.DeleteAll();//ɾ�����е���
            UIManager.Instance.CloseALLUI();// �ر�����UI����
            UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");// ��ʾ��Ϊ"SelectLevelUI"��UI����
            SceneManager.LoadScene(0);// ���س�������Ϊ0�ĳ�����ͨ������Ϸ����ʼ������
        });
        transform.Find("aliveBtn").GetComponent<Button>().onClick.AddListener(delegate ()// ����ť�����ʱִ�����´����
        {

            FightManager.Instance.CurHp = FightManager.Instance.MaxHp;
            SceneManager.LoadScene(1);
            AudioManager.Instance.PlayBGM("battle");
            //UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
        });
    }
}
