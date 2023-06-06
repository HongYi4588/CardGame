using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    //��ȡ���
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    //����ˮƽ
    [Header("�ٶ�")]
    public float speed;
    //��������ҵ�
    [Header("���ҵ�")]
    public Transform left;
    public Transform right;

    public float leftpoint;
    public float rightpoint;
    bool change = true;
    void Awake()
    {
        //��ȡ���
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        leftpoint = left.position.x;
        rightpoint = right.position.x;
        Destroy(left.gameObject);
        Destroy(right.gameObject);
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        move();
    }
    void move()
    {
        //�����ͷ�Լ��ı��ٶȷ���
        if ((this.transform.position.x < leftpoint || this.transform.position.x > rightpoint) && change)
        {
            speed = speed * -1;
            change = false;
        }
        //��ֹ���ص�ͷ�����
        if ((this.transform.position.x > leftpoint && this.transform.position.x < rightpoint))
        {
            change = true;
        }
        //����ԭ���ٶ�
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);

    }
}
