using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnControll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject left;
    public GameObject right;
    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        //图片显示
        left.SetActive(true);
        right.SetActive(true);
    }
    //鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        //图片不显示
        left.SetActive(false);
        right.SetActive(false);
    }

}
