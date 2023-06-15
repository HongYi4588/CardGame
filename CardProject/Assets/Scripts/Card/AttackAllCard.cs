using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �������е���
/// </summary>
public class AttackAllCard : CardItem
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

            //�����Ч
            AudioManager.Instance.PlayEffect("Effect/sword");


            for (int i = EnemyManager.Instance.enemyList.Count - 1; i >= 0; i--)
            {
                EnemyManager.Instance.enemyList[i].Hit(val);

                PlayEffect(EnemyManager.Instance.enemyList[i].transform.position);
            }
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}