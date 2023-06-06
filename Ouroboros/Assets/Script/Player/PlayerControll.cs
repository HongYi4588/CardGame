using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    //��ȡ���
    [Header("���")]
    public Collider2D boxcoll;
    public Collider2D circoll;
    private Rigidbody2D Rb;
    private Animator anim;

    //����ˮƽ����ֱ�ٶ�
    [Header("�ٶ�")]
    public float speed;
    public float jumpspeed;
    //��Ծ����
    int extrajump = 2;

    [Header("��Ϸͼ��")]
    public LayerMask ground;
    //�������
    public Slider lifelider;
    public float life = 100;
    //�Ƿ�����ƶ�
    bool ismove = true;
    //�������
    public GameObject win;
    public GameObject lost;
    void Awake()
    {
        //��ȡ���
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
        //ʵʱ��ʾ
        lifelider.value = life / 100.0f;
        if(ismove)
        { move(); }
        swithanim();
    }
    void move()
    {
        //horizontalmove��ȡ�����Ƿ�����facedirection�������ﳯ��
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        if (facedirection != 0)
        {
            //�ı����ﳯ��
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        //������һ��ˮƽ���ٶ�
        if (horizontalmove != 0)
        {
            Rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, Rb.velocity.y);
            //ִ���ܶ�����
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        //��k��Ծ�һ�����Ծ��������һ����Ծ���������л�������Ծ������һ
        if (Input.GetKeyDown(KeyCode.K) && extrajump > 0)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, jumpspeed * Time.fixedDeltaTime);
            //ִ����Ծ����
            anim.SetBool("jump", true);
            anim.SetBool("idle", false);
            extrajump--;
        }

    }
    void swithanim()
    {
        //��Ծ�ٶ�С��0�л����䶯��
        if (Rb.velocity.y < 0)
        {
            anim.SetBool("idle", false);
            anim.SetBool("jump", false);
            anim.SetBool("fall", true);
        }
        //�Ӵ���groundͼ����л���idle״̬
        if (circoll.IsTouchingLayers(ground) && anim.GetBool("fall"))
        {
            anim.SetBool("fall", false);
            anim.SetBool("idle", true);
            extrajump = 2;
        }
    }
    //�������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�Ƿ�������ǩΪenemy������
        if (collision.gameObject.tag == "enemy"|| collision.gameObject.tag == "piranha")
        {
            //�������fall״̬���������Ϸ��Ͳ�����
            if (anim.GetBool("fall")&&(this.transform.position.y-0.6f> collision.gameObject.transform.position.y))
            {
                //�ж��ĸ����͹�����ô���
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
            //��Ȼ�Ļ������˲����ڹ������һ�������һ�κ���
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
        //ʤ��
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
        //��������
        for(int i=0;i<10;i++)
        {
            life -= 1;
        }
        //�л�����
        anim.SetBool("hurt", false);
        ismove = true;
    }
    public void deadli()
    {
        Debug.Log("111");
        //������������ֱ������
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
