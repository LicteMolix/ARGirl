using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UICtrl:MonoBehaviour
{
    public Slider LoveSilder;
    public Slider HealthSlider;
    public Slider HungerSldier;
    
    public Text currentLoveState;
    public Text currentTouchPartEnum;
    public Text lovevalue;
    public GameObject danceButton;
    /// <summary>
    /// 前几帧是不是点击过
    /// </summary>
    private bool isClickLastFrames = false;

    private void Start()
    {
        StartCoroutine(UIUpdate());   
    }

    IEnumerator UIUpdate()
    {
        while(true)
        {
            LoveSilder.value = UCState.LoveValue / 100;
            HungerSldier.value = UCState.HungryValue / 100;
            HealthSlider.value = UCState.HealthValue / 100;

            currentTouchPartEnum.text = UCCurrentOpreation.currentTouchPartEnum.ToString();
            currentLoveState.text = UCCurrentOpreation.currentLoveState.ToString();
            lovevalue.text = UCState.LoveValue.ToString();
            CtrlDanceBtn();
            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// 控制控制跳舞的按钮显示和隐藏
    /// </summary>
    void CtrlDanceBtn()
    {
        //获得按键是否点击
        isClickLastFrames = danceButton.GetComponent<UIBtnEvnet>().IsClick;
        //如果处于跳舞的状态且没有点击过按钮就显示出来
        if (UCCurrentOpreation.currentLoveState == UCLoveStateEnum.Level5_LovelyDance && !isClickLastFrames)
        {
            ShowGO(danceButton);
        }
        //如果没有到达那个状态或者
        if (UCCurrentOpreation.currentLoveState != UCLoveStateEnum.Level5_LovelyDance )
        {
            HideGO(danceButton);
            danceButton.GetComponent<UIBtnEvnet>().IsClick = false;
        }
        if (UCCurrentOpreation.currentLoveState == UCLoveStateEnum.Level5_LovelyDance && isClickLastFrames)
        {
            HideGO(danceButton);
        }
    }

    void HideGO(GameObject go)
    {
        go.SetActive(false);
    }
    void ShowGO(GameObject go)
    {
        go.SetActive(true);
    }
}
