using UnityEngine;

public class mouse : MonoBehaviour
{
    private bool isDragging = false;  // 标记是否正在拖动物体
    private Vector3 offset;  // 鼠标点击位置与物体中心点的偏移量

    void Update()
    {
        // 获取鼠标在屏幕上的位置
        Vector3 mouseScreenPos = Input.mousePosition;

        // 将屏幕位置转换为世界坐标
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        if (Input.GetMouseButtonDown(0))  // 当鼠标左键按下时
        {
            // 射线检测，判断鼠标是否点击到了物体
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // 计算鼠标点击位置与物体中心点的偏移量
                offset = transform.position - mouseWorldPos;
                isDragging = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))  // 当鼠标左键释放时
        {
            isDragging = false;
        }

        if (isDragging)
        {
            // 更新物体位置为鼠标位置加上偏移量
            transform.position = mouseWorldPos + offset;
        }
    }
}

