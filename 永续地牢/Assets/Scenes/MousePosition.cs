using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    void Update()
    {
        // 获取鼠标在屏幕上的位置
        Vector3 mouseScreenPos = Input.mousePosition;

        // 输出鼠标位置到控制台
        Debug.Log("Mouse Position: " + mouseScreenPos);
    }
}