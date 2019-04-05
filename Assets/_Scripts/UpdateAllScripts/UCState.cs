using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该类是UC的一些状态的类（包括了好感度和）
/// </summary>
public class UCState : MonoBehaviour{

    private static float maxLoveValue = 100f;
    private static float minLoveValue = 10f;//小于该值宠物将不再与玩家互动

    private static float maxHealthValue = 100f;
    private static float mixHealthValue = 10f;//小于该值宠物死亡

    private static float maxHungerValue = 100f;
    private static float mixHungerValue = 40f;//小于该值开始掉血


    private static float loveValue = 90f;//好感度
    private static float healthValue = 100f;//健康值
    private static float hungryValue = 100f;//饥饿值

    public static float LoveValue
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

    public static float HealthValue
    {
        get
        {
            return healthValue;
        }

        set
        {
            healthValue = value;
        }
    }

    public static float HungryValue
    {
        get
        {
            return hungryValue;
        }

        set
        {
            hungryValue = value;
        }
    }

    /// <summary>
    /// 好感的改变
    /// </summary>
    /// <param name="lerpValue">改变的好感值</param>
    public static void ChangeUCLoveValue(float lerpValue)
    {
        if (lerpValue + LoveValue > maxLoveValue)
        {
            LoveValue = maxLoveValue;
        }
        else if (lerpValue + LoveValue <= minLoveValue)
        {
            LoveValue = minLoveValue;
        }
        else
        {
            LoveValue += lerpValue;
        }
    }

    /// <summary>
    /// 健康值的改变函数
    /// </summary>
    public static void ChangeUCHeathValue(float lerpValue)
    {
        if (lerpValue + HealthValue > maxHealthValue)
        {
            HealthValue = maxHealthValue;
        }
        else if (lerpValue + HealthValue <= 0)
        {
            HealthValue = 0;
        }
        else
        {
            HealthValue += lerpValue;
        }
    }

    /// <summary>
    /// 饥饿值的改变
    /// </summary>
    /// <param name="lerpValue"></param>
    public static void ChangeUCHungryValue(float lerpValue)
    {
        if (lerpValue + HungryValue > maxHungerValue)
        {
            HungryValue = maxHungerValue;
        }
        else if (lerpValue + HungryValue <= 0)
        {
            HungryValue = 0;
        }
        else
        {
            HungryValue += lerpValue;
        }
    }

    /// <summary>
    /// 判断UC当前Love的状态，是处在哪种好感的阶段
    /// </summary>
    /// <returns></returns>
    public static UCLoveStateEnum JudgeUCCurrentLoveState()
    {
        if (LoveValue < 0)
        {
            return UCLoveStateEnum.Level0_Away;
        }
        else if (LoveValue >= 0 && LoveValue < 50)
        {
            return UCLoveStateEnum.Level1_Hate;
        }
        else if (LoveValue >= 50 && LoveValue < 75)
        {
            return UCLoveStateEnum.Level2_Normal;
        }
        else if (LoveValue >= 75 && LoveValue < 90)
        {
            return UCLoveStateEnum.Level3_Good;
        }
        else if(LoveValue >= 90 && LoveValue < 100)
        {
            return UCLoveStateEnum.Level4_Lovely;
        }
        else
        {
            return UCLoveStateEnum.Level5_LovelyDance;
        }
    }

    /// <summary>
    /// 判断UC的健康状态
    /// </summary>
    /// <returns></returns>
    public static UCHealthStateEnum JudgeUCCurrentHealthState()
    {
        if (HealthValue <= 50)
        {
            return UCHealthStateEnum.Sick;
        }
        else if (HealthValue > 50 && HealthValue < 80)
        {
            return UCHealthStateEnum.Normal;
        }
        else
        {
            return UCHealthStateEnum.Health;
        }
    }

    /// <summary>
    /// 判断当前的UC的饥饿状态
    /// </summary>
    /// <returns></returns>
    public static UCHungerStateEnum JudgeUCCurrentHungryState()
    {
        if (HungryValue <= mixHungerValue)
        {
            return UCHungerStateEnum.Hungry;
        }
        else if (HungryValue > mixHungerValue && HungryValue < 80)
        {
            return UCHungerStateEnum.Normal;
        }
        else
        {
            return UCHungerStateEnum.Full;
        }
    }
}
    
