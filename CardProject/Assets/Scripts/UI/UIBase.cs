using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 界面基类
/// </summary>
public class UIBase : MonoBehaviour
{


    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public UIEventTrigger Register(string name)
    {
        Transform tf = transform.Find(name);
        return UIEventTrigger.Get(tf.gameObject);
    }




    /// <summary>
    /// 显示
    /// </summary>
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }


    /// <summary>
    /// 关闭界面(销毁)
    /// </summary>
    public virtual void Close()
    {
        UIManager.Instance.CloseUI(gameObject.name);
    }







    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
