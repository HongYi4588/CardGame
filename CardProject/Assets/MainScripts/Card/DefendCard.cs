using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 防御卡(加护盾效果)
/// </summary>
public class DefendCard : CardItem
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

            //播放使用后的声音(每张卡使用的声音可能不一样)
            AudioManager.Instance.PlayEffect("Effect/healspell");//这个字段可以配到表中，他失策了

            //增加防御力
            FightManager.Instance.DefenseCount += val;
            //刷新防御力文本
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();

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
