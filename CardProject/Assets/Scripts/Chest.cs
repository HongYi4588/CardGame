using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator ani;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        ani = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (isOpen == true)
        {
            return;
        }
        ani.Play("Open Chest");
        isOpen = true;

        Invoke("showWinSelectCardUI", 0.5f);
    }

    private void showWinSelectCardUI()
    {
        UIManager.Instance.ShowUI<WinSelectCardUI>("WinSelectCardUI");
        Destroy(gameObject);
    }
}
