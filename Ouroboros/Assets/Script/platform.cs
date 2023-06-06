using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    //获取组件
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    //物体水平
    [Header("速度")]
    public float speed;
    //怪物的左右点
    [Header("左右点")]
    public Transform left;
    public Transform right;

    public float leftpoint;
    public float rightpoint;
    bool change = true;
    void Awake()
    {
        //获取组件
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
        //怪物掉头以及改变速度方向
        if ((this.transform.position.x < leftpoint || this.transform.position.x > rightpoint) && change)
        {
            speed = speed * -1;
            change = false;
        }
        //防止来回掉头的情况
        if ((this.transform.position.x > leftpoint && this.transform.position.x < rightpoint))
        {
            change = true;
        }
        //保持原有速度
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);

    }
}
