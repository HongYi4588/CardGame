using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class lifecontroll : MonoBehaviour
{

public GameObject player;
public Sprite[] lifeimage;
private int currentlife;
public Image lifeage;
    void Update()
    {
        currentlife = player.GetComponent<PlayerControll>().life;
        if (currentlife > -1)
        {
            lifeage.sprite = lifeimage[currentlife];}
    }

}
