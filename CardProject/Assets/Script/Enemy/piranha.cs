using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piranha : MonoBehaviour
{
    //获取组件
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    //定义位置
    private Transform player;

    [Header("攻击间隔时间")]
    public float attacktime;
    public float Maxtime;

    void Awake()
    {
        //获取组件，找到游戏中名为player的物体
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        //判断是否进入攻击范围，并且是否到攻击间隔
        if(Mathf.Abs(player.position.x-transform.position.x)<1.8f&&attacktime>Maxtime)
        {
            //是就攻击
            anim.SetTrigger("attack");
            attacktime = 0;
        }

        attacktime += Time.deltaTime;
    }

    public void attack()
    {
        //再次判断是否还在攻击范围
        if (Mathf.Abs(player.position.x - transform.position.x) < 1.8f&& Mathf.Abs(player.position.y - transform.position.y)<1.5f)
        {
            player.gameObject.GetComponent<PlayerControll>().piranhaattack();
        }
    }
    public void dead()
    {
        coll.enabled = false;
        anim.SetBool("die", true);
    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
