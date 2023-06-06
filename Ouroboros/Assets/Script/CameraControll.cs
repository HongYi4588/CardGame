using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    //定义位置
    private Transform player;
    void Awake()
    {
        //获取组件，找到游戏中名为player的物体
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        this.transform.position=new Vector3(player.position.x,player.position.y, this.transform.position.z); 
    }
}
