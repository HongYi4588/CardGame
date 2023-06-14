using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 游戏入口脚本
/// </summary>
public class GameAPP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //初始化配置表
        GameConfigManager.Instance.Init();

        //初始化音频管理器
        AudioManager.Instance.Init();

        //初始化用户信息
        RoleManager.Instance.Init();


        //初始化关卡
        LevelManager.Instance.Init();


        //显示loginUI 创建的脚本名字记得跟预制体物体名字一致
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        UIManager.Instance.ShowUI<MainUI>("MainUI");

        //播放bgm
        AudioManager.Instance.PlayBGM("bgm1");
    }

    public static void ResetGame()
    {
        //初始化用户信息
        RoleManager.Instance.Init();


        //初始化关卡
        LevelManager.Instance.Init();

        //播放bgm
        AudioManager.Instance.PlayBGM("bgm1");

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //保存
            RoleManager.Instance.Save();

            UIManager.Instance.ShowUI<QuitGameUI>("QuitGameUI");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
          
        }
    }
}
