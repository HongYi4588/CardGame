using UnityEngine;

public class mouse : MonoBehaviour
{
    private bool isDragging = false;  // ����Ƿ������϶�����
    private Vector3 offset;  // �����λ�����������ĵ��ƫ����

    void Update()
    {
        // ��ȡ�������Ļ�ϵ�λ��
        Vector3 mouseScreenPos = Input.mousePosition;

        // ����Ļλ��ת��Ϊ��������
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        if (Input.GetMouseButtonDown(0))  // ������������ʱ
        {
            // ���߼�⣬�ж�����Ƿ�����������
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // ���������λ�����������ĵ��ƫ����
                offset = transform.position - mouseWorldPos;
                isDragging = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))  // ���������ͷ�ʱ
        {
            isDragging = false;
        }

        if (isDragging)
        {
            // ��������λ��Ϊ���λ�ü���ƫ����
            transform.position = mouseWorldPos + offset;
        }
    }
}

