using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 获得能量
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
            //使用效果
            int val = int.Parse(data["Arg0"]);

            AudioManager.Instance.PlayEffect("Effect/healspell");//这个字段可以配到表中，他失策了

            //获得能量
            FightManager.Instance.CurPowerCount += val;
            //刷新防御力文本
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
