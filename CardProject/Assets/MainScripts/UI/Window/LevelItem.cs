using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//¹Ø¿¨Ñ¡Ïî
public class LevelItem : MonoBehaviour, IPointerClickHandler
{
    public LevelData levelData;
    public Dictionary<string, string> data;
    public List<GameObject> points;
    void Start()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>(this.data["Icon"]);

        if (levelData.IsUnLock == false)
        {
            GetComponent<Image>().color = Color.black;
        }
    }

    public void Init(LevelData l_data, Dictionary<string, string> data)
    {
        this.levelData = l_data;
        this.data = data;
    }

    public void InitPoints(List<GameObject> points)
    {
        this.points = new List<GameObject>();
        this.points.AddRange(points);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Instance.GetUI<SelectLevelUI>("SelectLevelUI").SelectLevel(this);
    }

    public void SetPointsActive(bool isActive)
    {
        for (int i = 0; i < this.points.Count; i++)
        {
            this.points[i].SetActive(isActive);
        }
    }
}
