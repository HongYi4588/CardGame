using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 开始界面 （要继承UIBase）
/// </summary>
public class LoginUI : UIBase
{
    private void Awake()
    {
        //开始游戏
        Register("bg/startBtn").onClick = onStartGameBtn;

        Register("bg/quitBtn").onClick = onQuitBtn;
    }

    private void onStartGameBtn(GameObject obj,PointerEventData pData)
    {
        //关闭Login界面
        Close();

        ////战斗初始化
        //FightManager.Instance.ChangeType(FightType.Init);

        //显示选关界面
        UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
    }



    private void onQuitBtn(GameObject obj,PointerEventData pData)
    {
        Application.Quit();
    }












    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
