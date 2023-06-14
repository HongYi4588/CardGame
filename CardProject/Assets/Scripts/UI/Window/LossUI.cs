using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LossUI : UIBase
{
    private void Start()
    {
        transform.Find("closeBtn").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            EnemyManager.Instance.DeleteAll();
            UIManager.Instance.CloseALLUI();
            UIManager.Instance.ShowUI<SelectLevelUI>("SelectLevelUI");
            SceneManager.LoadScene(0);
        });
    }
}
