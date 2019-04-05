using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

class AppendRandomRain : MonoBehaviour
{
    public GameObject RainScene;
    /// <summary>
    /// 下雨的时间
    /// </summary>
    private float RainRandomTimeC;
    /// <summary>
    /// 等待多久下雨
    /// </summary>
    private float WaitRainTime;
    /// <summary>
    /// 计时器
    /// </summary>
    private float TimeCount = 0;
    private float TimeRain = 0;
    private float TimeFix = 0;
    private void Awake()
    {
        RainScene.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(Update_Rain());
    }


    IEnumerator Update_Rain()
    {
        while (true)
        {
            yield return new WaitForSeconds(6);
            RandomRainScene();
            yield return new WaitForSeconds(15);
            //yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// 在特定的好感度下面可能会生成下雨的场景
    /// </summary>
    void RandomRainScene()
    {
        //在这些状态下，都可能生成下雨的场景
        switch (UCCurrentOpreation.currentLoveState)
        {
            case UCLoveStateEnum.Level1_Hate:
            case UCLoveStateEnum.Level2_Normal:
            case UCLoveStateEnum.Level3_Good:
                if (isRain())
                {
                    StartCoroutine(RainLastTime());
                }
                break;
            case UCLoveStateEnum.Level4_Lovely:
            case UCLoveStateEnum.Level5_LovelyDance:
                RainScene.SetActive(false);
                break;
        }
    }
    /// <summary>
    /// 是否应该下雨
    /// </summary>
    /// <returns></returns>
    public bool isRain()
    {
        int number1 = Random.Range(0, 9);
        int number2 = Random.Range(2, 6);
        return number1 > number2;
    }
    

    /// <summary>
    /// 下雨持续时间
    /// </summary>
    /// <returns></returns>
    IEnumerator RainLastTime()
    {
        RainScene.SetActive(true);
        float randlast = Random.Range(10, 30);
        yield return new WaitForSeconds(randlast);
        print("隐藏");
        RainScene.SetActive(false);//下雨完进行隐藏
    }
}
