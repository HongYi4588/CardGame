using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �������
/// </summary>
public class PowerCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (Input.mousePosition.y <= Screen.height * 0.5f)
        {
            base.OnEndDrag(eventData);
            return;
        }
        if (TryUse() == true)
        {
            //ʹ��Ч��
            int val = int.Parse(data["Arg0"]);

            AudioManager.Instance.PlayEffect("Effect/healspell");//����ֶο����䵽���У���ʧ����

            //�������
            FightManager.Instance.CurPowerCount += val;
            //ˢ�·������ı�
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();

            Vector3 pos = Camera.main.transform.position;
            pos.y = 0;
            PlayEffect(pos);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
