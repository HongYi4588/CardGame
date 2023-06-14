using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 无中生有卡(抽卡效果的卡片)
/// </summary>
public class AddCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (Input.mousePosition.y <= Screen.height * 0.5f)
        {
            base.OnEndDrag(eventData);
            return;
        }
        if (TryUse()==true)
        {
            int val = int.Parse(data["Arg0"]);//抽卡数量

            //是否有卡抽
            if (FightCardManager.Instance.HasCard()==false)
            {

                FightCardManager.Instance.ResetCard();
            }

            UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val);

            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();

            //更新卡牌数
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();

            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));

            PlayEffect(pos);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
        
    }





}
