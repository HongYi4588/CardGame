using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    void Update()
    {
        // ��ȡ�������Ļ�ϵ�λ��
        Vector3 mouseScreenPos = Input.mousePosition;

        // ������λ�õ�����̨
        Debug.Log("Mouse Position: " + mouseScreenPos);
    }
}