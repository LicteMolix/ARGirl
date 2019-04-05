using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanState
{
    //private static UnityChanState singleState;

    private float unityLoveValue = 50f;//陌生人设置为50

    private float unityChanHungryValue = 80f;//饥饿值

    public float UnityLoveValue
    {
        get
        {
            return unityLoveValue;
        }

        set
        {
            unityLoveValue = value;
        }
    }

    public float UnityChanHungryValue
    {
        get
        {
            return unityChanHungryValue;
        }

        set
        {
            unityChanHungryValue = value;
        }
    }
}
