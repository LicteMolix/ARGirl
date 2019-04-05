/*
 * 作者：licte（陈杰）    版本：1.1.0
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用于数据在游戏运行时保存的类，然后重写一个Ctrl进行数据的本地化的持久储存
/// </summary>
public class _StaticUnityChanstate {
    static float maxLoveValue = 100f;
    static float minLoveValue = 10f;


    public static float loveValue = 70f;//好感度
    public static float healthValue = 100f;//健康值
    public static float hungryValue = 100f;//饥饿值

    /// <summary>
    /// 好感的改变
    /// </summary>
    /// <param name="lerpValue">要改变的好感值</param>
    public static void ChangeLoveValue(float lerpValue)
    {
        if (lerpValue + loveValue > maxLoveValue)
        {
            loveValue = maxLoveValue;
        }
        else if (lerpValue + loveValue <= minLoveValue)
        {
            loveValue = minLoveValue;
        }
        else
        {
            loveValue += lerpValue;
        }
    }

    /// <summary>
    /// 判读当前的状态
    /// </summary>
    /// <returns></returns>
    public static _EnumState JudgeState()
    {
        if (loveValue < 0)
        {
            return _EnumState.SayGoodBey;
        }
        else if (loveValue >= 0 && loveValue < 50)
        {
            return _EnumState.HateYou;
        }
        else if (loveValue >= 50 && loveValue < 80)
        {
            return _EnumState.JustSoSo;
        }
        else if (loveValue >= 80 && loveValue < 90)
        {
            return _EnumState.KnowYou;
        }
        else
        {
            return _EnumState.LoveLy;
        }
    }

    /// <summary>
    /// 健康值的改变函数
    /// </summary>
    public static void ChangeHeathValue(float currhealthValue)
    {
        
        if (healthValue < 2)
        {
            //宠物死亡，退出游戏
        }
        else
        {
            healthValue = healthValue - currhealthValue;
        }
    }

    public static void ChangeHungryValue(float hungryValue)
    {
        if (true)
        {
            //开始减血
        }
        else
        {

        }
    }
}
