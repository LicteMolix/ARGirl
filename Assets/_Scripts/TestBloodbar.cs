/*
 * 作者：licte（陈杰）    版本：1.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestBloodbar : MonoBehaviour {
    //public UISlider progressBar;//原来的那个NGUI的滑动条
    public Slider LoveProgressBar;//显示好感的slider条
	
	// Update is called once per frame
	void Update () {
        //progressBar.sliderValue = _StaticUnityChanstate.loveValue/100;//这个是NGUI的那个效果
        LoveProgressBar.value = _StaticUnityChanstate.loveValue / 100;

    }
}
