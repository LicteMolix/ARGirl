/*
 * 作者：licte（陈杰）    版本：1.1
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _UC_OtherThings : MonoBehaviour
{
    //心情不好进行的好感交互--下雨状态
    public GameObject Umbrella;//获得雨伞
    public GameObject UmbrellaPosOfUC;//雨伞在模型上面应该存在的位置
    public GameObject RainScene;//雨天的效果的获取

    //心情好的时候进行的粒子效果的显示的粒子特效
    //public ParticleSystem LoveParticle;
    public GameObject LoveParticle;
    //是否进行雨伞交互需要的参数
    private float minDis = 0.5f;
    private float maxDis = 2.0f;
    [Space(10)]
    public bool IsU_UC = false;//雨伞是否能和UnityChan进行互动
    public bool isClick = false;

    //解锁舞蹈按钮
    public GameObject buttonDance;

    //吃东西
    public GameObject Food;
    private bool isNearUCMouth = false;

    public static _UC_OtherThings UCOtherThings;

    // Use this for initialization
    void Awake()
    {
        UCOtherThings = this;
        buttonDance.SetActive(false);
        LoveParticle.SetActive(false);
        //LoveParticle.Pause();
        Umbrella.SetActive(false);
        RainScene.SetActive(false);
    }


    #region 下雨的交互
    /// <summary>
    /// 将雨天的效果实现
    /// (下一个版本将雨天变成由时间控制的随机场景，然后以影响好感度以及健康值；而不是现在这种用好感驱动的场景的实现)
    /// </summary>
    public void RainDayShow()
    {
        RainScene.SetActive(true);
    }


    /// <summary>
    /// 隐藏下雨天场景
    /// </summary>
    public void RainDayHide()
    {
        RainScene.SetActive(false);
    }
    /// <summary>
    /// 雨伞和人物的互动
    /// </summary>
    /// <param name="UCAni">UC的动画组件</param>
    public void RainDayOpreation(Animator UCAni)
    {
        if (Umbrella.activeInHierarchy)
        {
            float distance = Vector3.Distance(Umbrella.transform.position, UmbrellaPosOfUC.transform.position);
            //如果和角色之间产生了可以发生关系的条件，那么将进行一定交互的操作（雨伞高于头部且距离适中时）
            IsU_UC = (distance > minDis && distance < maxDis) ? true : false;
            if (IsU_UC && Umbrella.transform.position.y > UmbrellaPosOfUC.transform.position.y - Vector3.up.y / 4
                && Umbrella.transform.position.y < (UmbrellaPosOfUC.transform.position.y + Vector3.up.y))
            {
                //Debug.LogError(distance);
                StartCoroutine(UnderUMBHappy(UCAni));
                StopCoroutine(UnderUMBHappy(UCAni));
            }
            else
            {
                UCAni.SetBool("IsReleseBack", true);
            }
        }
    }

    /// <summary>
    /// 下雨的时候伞就可以识别了
    /// </summary>
    public void GetYouUMB()
    {
        Umbrella.SetActive(true);
    }

    /// <summary>
    /// 在伞下面，切换其状态
    /// </summary>
    /// <param name="UCAni"></param>
    /// <returns></returns>
    IEnumerator UnderUMBHappy(Animator UCAni)
    {
        yield return new WaitForSeconds(2f);
        UCAni.SetBool("IsShaJiao", true);
        yield return new WaitForSeconds(1f);
        UCAni.SetBool("IsShaJiao", false);
    }
    #endregion

    #region 爱心效果
    /// <summary>
    /// 修改特效的存在
    /// </summary>
    public void LoveParticleB()
    {
        LoveParticle.SetActive(false);
        //LoveParticle.Stop();
    }
    /// <summary>
    /// 显示Love特效
    /// </summary>
    public void LoveParticleS()
    {
        LoveParticle.SetActive(true);
        //LoveParticle.Simulate(5f);
    }
    #endregion

    #region 舞蹈效果配合_Dance.cs使用
    /// <summary>
    ///显示按钮
    /// </summary>
    public void DanceBTNShow()
    {
        buttonDance.SetActive(true);
    }
    /// <summary>
    /// 隐藏按钮
    /// </summary>
    public void DanceBTNB()
    {
        buttonDance.SetActive(false);
    }
    #endregion

    #region 喂食效果
    //正在开发中
    #endregion
}
