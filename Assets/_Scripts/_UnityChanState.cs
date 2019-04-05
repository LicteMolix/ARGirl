using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _UnityChanState {
    float maxLoveValue = 100f;

    private float loveValue = 70f;

    public float LoveValue
    {
        get
        {
            return loveValue;
        }

        set
        {
            loveValue = value;
        }
    }

    public _UnityChanState()
    {

    }

    public bool ChangeLoveValue(float lerpValue)
    {
        if (lerpValue + LoveValue > maxLoveValue)
        {
            return false;
        }
        else
        {
            LoveValue += lerpValue; 
            return true;
        }
    }

    public void Change_LoveValue(float lerpValue)
    {
        LoveValue += lerpValue;
    }

    public _EnumState JudgeState()
    {
        if (LoveValue < 0) {
            return _EnumState.SayGoodBey;
        }
        else if (LoveValue >= 0 && LoveValue < 50)
        {
            return _EnumState.HateYou;
        }
        else if(LoveValue >= 50 && LoveValue < 80) {
            return _EnumState.JustSoSo;
        }
        else if (LoveValue >= 80 && LoveValue < 90) {
            return _EnumState.KnowYou;
        }
        else {
            return _EnumState.LoveLy;
        }
    }
}
