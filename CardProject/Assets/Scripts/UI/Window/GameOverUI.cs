using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : UIBase
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("returnBtn").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            UIManager.Instance.CloseALLUI();
            EnemyManager.Instance?.DeleteAll();
            GameAPP.ResetGame();
            UIManager.Instance.ShowUI<LoginUI>("LoginUI");
            UIManager.Instance.ShowUI<MainUI>("MainUI");
            
        });
    }
}
