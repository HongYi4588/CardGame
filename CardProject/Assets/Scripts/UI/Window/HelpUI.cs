using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HelpUI : UIBase
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("closeBtn").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            Close();
        });
    }
}
