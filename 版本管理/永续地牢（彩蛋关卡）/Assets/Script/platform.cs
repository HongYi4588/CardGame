using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    //??????
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    //??????
    [Header("???")]
    public float speed;
    //??????????
    [Header("?????")]
    public Transform left;
    public Transform right;

    public float leftpoint;
    public float rightpoint;
    bool change = true;
    void Awake()
    {
        //??????
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
        //??????????????????
        if ((this.transform.position.x < leftpoint || this.transform.position.x > rightpoint) && change)
        {
            speed = speed * -1;
            change = false;
        }
        //??????????????
        if ((this.transform.position.x > leftpoint && this.transform.position.x < rightpoint))
        {
            change = true;
        }
        //??????????
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);

    }
}
