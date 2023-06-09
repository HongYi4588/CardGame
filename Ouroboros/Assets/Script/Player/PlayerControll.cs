using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    //获取组件
    [Header("组件")]
    public Collider2D boxcoll;
    public Collider2D circoll;
    private Rigidbody2D Rb;
    private Animator anim;

    //物体水平和竖直速度
    [Header("速度")]
    public float speed;
    public float jumpspeed;
    //跳跃次数
    int extrajump = 2;

    [Header("游戏图层")]
    public LayerMask ground;
    //玩家生命
    public Slider lifelider;
    public float life = 100;
    //是否可以移动
    bool ismove = true;
    //结算界面
    public GameObject win;
    public GameObject lost;
    void Awake()
    {
        //获取组件
        Rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        if (life< 0)
        {
            lost1();
        }
        else
        { Time.timeScale = 1; }
        //实时显示
        lifelider.value = life / 100.0f;
        if(ismove)
        { move(); }
        swithanim();
    }
    void move()
    {
        //horizontalmove获取键盘是否输入facedirection决定人物朝向
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        if (facedirection != 0)
        {
            //改变人物朝向
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        //给人物一个水平初速度
        if (horizontalmove != 0)
        {
            Rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, Rb.velocity.y);
            //执行跑动动画
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        //按k跳跃且还有跳跃次数，给一个跳跃的力并且切换动画跳跃次数减一
        if (Input.GetKeyDown(KeyCode.K) && extrajump > 0)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, jumpspeed * Time.fixedDeltaTime);
            //执行跳跃动画
            anim.SetBool("jump", true);
            anim.SetBool("idle", false);
            extrajump--;
        }

    }
    void swithanim()
    {
        //跳跃速度小于0切换下落动画
        if (Rb.velocity.y < 0)
        {
            anim.SetBool("idle", false);
            anim.SetBool("jump", false);
            anim.SetBool("fall", true);
        }
        //接触到ground图层就切换回idle状态
        if (circoll.IsTouchingLayers(ground) && anim.GetBool("fall"))
        {
            anim.SetBool("fall", false);
            anim.SetBool("idle", true);
            extrajump = 2;
        }
    }
    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //是否碰到标签为enemy的物体
        if (collision.gameObject.tag == "enemy"|| collision.gameObject.tag == "piranha")
        {
            //如果处于fall状态并且在它上方就踩死它
            if (anim.GetBool("fall")&&(this.transform.position.y-0.6f> collision.gameObject.transform.position.y))
            {
                //判断哪个类型怪物调用代码
                if(collision.gameObject.tag == "enemy")
                { collision.gameObject.GetComponent<bee>().dead(); }
                else
                { collision.gameObject.GetComponent<piranha>().dead(); }
                Rb.velocity = new Vector2(Rb.velocity.x, 10);
                anim.SetBool("jump", true);
                anim.SetBool("idle", false);
                anim.SetBool("run", false);
                extrajump = 1;
            }
            //不然的话就受伤并且在怪物的那一侧就往那一次后退
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                ismove = false;
                anim.SetBool("hurt",true);
                Rb.velocity = new Vector2(-5,5);

            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                ismove = false;
                anim.SetBool("hurt", true);
                Rb.velocity = new Vector2(5,5);

            }
        }
        if (collision.gameObject.tag == "deadline")
        {
            lost.active = true;
            Time.timeScale = 0;
        }
        //胜利
        if (collision.gameObject.tag == "win")
        {
            win.active = true;
        }

    }

    public void piranhaattack()
    {
        ismove = false;
        anim.SetBool("hurt", true);
        Rb.velocity = new Vector2(-5, 5);
    }

    public void Hurt()
    {
        //减少生命
        for(int i=0;i<10;i++)
        {
            life -= 1;
        }
        //切换动画
        anim.SetBool("hurt", false);
        ismove = true;
    }
    public void deadli()
    {
        Debug.Log("111");
        //慢慢减少生命直到死亡
        while(life==0)
        {
            life--;
        }
    }
    void lost1()
    {
        lost.active = true;
        Time.timeScale = 0;
    }
}
