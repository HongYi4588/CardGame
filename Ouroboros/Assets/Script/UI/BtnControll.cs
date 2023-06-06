using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnControll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject left;
    public GameObject right;
    //������
    public void OnPointerEnter(PointerEventData eventData)
    {
        //ͼƬ��ʾ
        left.SetActive(true);
        right.SetActive(true);
    }
    //����뿪
    public void OnPointerExit(PointerEventData eventData)
    {
        //ͼƬ����ʾ
        left.SetActive(false);
        right.SetActive(false);
    }

}
