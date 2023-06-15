using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class SelectLevelUI : UIBase
{

    void Awake()
    {

        Vector2 pos = new Vector2(-900, 0);
        Vector2 initPos = transform.Find("teamIcon").GetComponent<RectTransform>().anchoredPosition;
        ////获得构建出来的关卡 
        for (int i = 0; i < LevelManager.Instance.Levels.Count; i++)
        {
            LevelData levelData = LevelManager.Instance.Levels[i];

            LevelItem item = createLevelItem(levelData, pos);

            Vector2 dir = (pos - initPos).normalized;

            float dis = Vector2.Distance(pos, initPos) / 4.0f;
            List<GameObject> p_list = new List<GameObject>();
            //创建点
            for (int j = 0; j < 3; j++)
            {
                Vector2 p_pos = initPos + dir * dis * (j + 1);
                GameObject obj = createPoint(p_pos);
                p_list.Add(obj);
            }

            item.InitPoints(p_list);

            if (levelData.IsFinish == true)
            {
                item.SetPointsActive(true);

                transform.Find("player").GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, pos.y + 100);
            }

            initPos = pos;

            pos.x += 400;
        }
    }

    private LevelItem createLevelItem(LevelData levelData, Vector2 pos)
    {
        Dictionary<string, string> data = levelData.data;
        GameObject prefabObj = transform.Find("levelItem").gameObject;
        GameObject obj = MonoBehaviour.Instantiate(prefabObj, transform);
        obj.GetComponent<RectTransform>().anchoredPosition = pos;
        LevelItem item = obj.AddComponent<LevelItem>();
        item.Init(levelData, data);
        obj.SetActive(true);
        int index = LevelManager.Instance.Levels.IndexOf(levelData);
        if (index < 5)
        {
           levelData.IsUnLock = true;
        }

        return item;
    }

    public GameObject createPoint(Vector2 pos)
    {
        GameObject obj = MonoBehaviour.Instantiate(transform.Find("p").gameObject, transform);
        obj.GetComponent<RectTransform>().anchoredPosition = pos;
        return obj;
    }

    private bool isSelect = false;
    public void SelectLevel(LevelItem levelItem)
    {
        if (isSelect == true)
        {
            return;
        }

        if (levelItem.levelData.IsFinish == true)
        {
            //已经通关
            return;
        }
        if (levelItem.levelData.IsUnLock == false)
        {
            Debug.Log("没有解锁");
        }
        else
        {
            levelItem.SetPointsActive(true);
            Vector2 movePos = levelItem.GetComponent<RectTransform>().anchoredPosition;
            movePos.y += 100;
            isSelect = true;

            transform.Find("player").GetComponent<RectTransform>().DOAnchorPos(movePos, 0.5f).onComplete = delegate ()
            {
                UIManager.Instance.CloseALLUI();
                string type = levelItem.levelData.data["Type"];
                if (type == "0")
                {
                    //进入关卡
                    LevelManager.Instance.SelectLevel(levelItem.levelData);
                }
                else if (type == "1")
                {
                    levelItem.levelData.IsFinish = true;
                    LevelManager.Instance.UnNextLevel();
                    UIManager.Instance.ShowUI<EquipUI>("EquipUI");
                }
                else if (type == "2")
                {
                    //商店
                    levelItem.levelData.IsFinish = true;
                    LevelManager.Instance.UnNextLevel();
                    UIManager.Instance.ShowUI<ShopUI>("ShopUI");
                }
            };
        }
    }
}
