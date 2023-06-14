using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// �˳����ν���
/// </summary>
public class QuitGameUI : UIBase
{
    private void Awake()
    {
        transform.Find("bg/okBtn").GetComponent<Button>().onClick.AddListener(onOkBtn);
        transform.Find("bg/noBtn").GetComponent<Button>().onClick.AddListener(onNoBtn);
    }

    //�˳�
    void onOkBtn()
    {
        Application.Quit();
    }

    void onNoBtn()
    {
        Close();
    }
}
