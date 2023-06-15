using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piranha : MonoBehaviour
{
    //��ȡ���
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    //����λ��
    private Transform player;

    [Header("�������ʱ��")]
    public float attacktime;
    public float Maxtime;

    void Awake()
    {
        //��ȡ������ҵ���Ϸ����Ϊplayer������
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        //�ж��Ƿ���빥����Χ�������Ƿ񵽹������
        if(Mathf.Abs(player.position.x-transform.position.x)<1.8f&&attacktime>Maxtime)
        {
            //�Ǿ͹���
            anim.SetTrigger("attack");
            attacktime = 0;
        }

        attacktime += Time.deltaTime;
    }

    public void attack()
    {
        //�ٴ��ж��Ƿ��ڹ�����Χ
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
