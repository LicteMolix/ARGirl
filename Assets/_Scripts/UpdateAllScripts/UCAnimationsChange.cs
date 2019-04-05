using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCAnimationsChange : MonoBehaviour
{
    /// <summary>
    /// 动画的返回态
    /// </summary>
    private string IsReleaseBack = "IsReleaseBack";
    public string IsDie = "IsDie";
    public string IsShaJiao = "IsShaJiao";
    public string IsDance = "IsDance";
    public string IsXuanFengTi = "IsXuanFengTi";
    public string IsMaiMeng = "IsMaiMeng";
    public string IsEatFood = "IsEatFood";
    public string IsExit = "IsExit";
    public int angry = 1;
    public int scornfully = 2;
    public int sad = 3;
    public int surprise = 4;
    public int laughtOutLoud = 5;
    public int smile = 6;
    [Space(10)]
    public GameObject LoveParticle;//爱心的效果
    
    #region bodyanimationstring_and_FaceTi";
    /// <summary>
    /// 面部表情的动画的状态机参数
    /// </summary>
    public static string FaceAni = "FaceAniStateNum";
    #endregion

    /// <summary>
    /// UC的动画组件
    /// </summary>
    private Animator thisUCani;

    void Start()
    {
        thisUCani = GetComponent<Animator>();
    }

    
    /// <summary>
    /// 播放身体的动画
    /// </summary>
    /// <param name="paramter">  </param>
    public void PlayBodyAni(string paramter)
    {
        StartCoroutine(MatchBodyMechineParamater(paramter));
        StopCoroutine("MatchBodyMechineParamater");
    }

    /// <summary>
    /// 和身体的动画状态机相匹配的协程
    /// </summary>
    /// <param name="paramter"></param>
    /// <returns></returns>
    public IEnumerator MatchBodyMechineParamater(string paramter)
    {
        thisUCani.SetBool(IsReleaseBack, true);
        //进入动画
        thisUCani.SetBool(paramter, true);
        //等待一秒
        yield return new WaitForSeconds(0.2f);
        //退出动画状态，让其不再重复播放
        thisUCani.SetBool(paramter, false);
    }

    /// <summary>
    /// 播放面部的表情的动画
    /// </summary>
    /// <param name="paramter"></param>
    public void PlayFaceAni(int paramterNum)
    {
        StartCoroutine(MatchFaceMechineParamater(paramterNum));
        StopCoroutine("MatchFaceMechineParamater");
    }

    public IEnumerator MatchFaceMechineParamater(int paramternumber)
    {
        //设置状态的动画
        thisUCani.SetInteger(FaceAni, 0);
        thisUCani.SetInteger(FaceAni, paramternumber);
        yield return new WaitForSeconds(2f);
        //设置成为默认的动画
        thisUCani.SetInteger(FaceAni, 0);
    }

    
}
