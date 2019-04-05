using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _CtrlBtnHide : MonoBehaviour {
    public GameObject danceBtn;
    public void HideBtn()
    {
        if (danceBtn.activeInHierarchy)
        {
            danceBtn.SetActive(false);
        }
    }
}
