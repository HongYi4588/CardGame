using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraControll : MonoBehaviour
{
    //����λ��
    private Transform player;
    void Awake()
    {
        //��ȡ������ҵ���Ϸ����Ϊplayer������
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {this.transform.position=new Vector3(player.position.x, player.position.y+1, this.transform.position.z);}
}
