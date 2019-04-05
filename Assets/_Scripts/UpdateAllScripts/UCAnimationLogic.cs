using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 该类用于控制UC的动画逻辑，仅仅时逻辑，动画状态机的改变从新写一个
/// </summary>
public class UCAnimationLogic : MonoBehaviour
{
    private UCAnimationsChange instaUAC;
    private Animator UCAni;
    public GameObject musicPlayer;//音乐播放器
    public GameObject RainScenes;
    /// <summary>
    /// 是否能够进行再次播放(时间够没有)
    /// </summary>
    bool isPlay = false;
    float timeCountter = 0;
    int randomState = -1;
    DataHandle dh;
    #region bodyanimationstring_and_Face
    private string IsDie = "IsDie";
    private string IsShaJiao = "IsShaJiao";
    private string IsDance = "IsDance";
    private string IsXuanFengTi = "IsXuanFengTi";
    private string IsMaiMeng = "IsMaiMeng";
    private string IsExit = "IsExit";
    private string FaceAni = "FaceAni";
    private int angry = 1;
    private int scornfully = 2;
    private int sad = 3;
    private int surprise = 4;
    private int laughtOutLoud = 5;
    private int smile = 6;

    #endregion


    void Start()
    {
        dh = new DataHandle();
        if ( (File.Exists(dh.PathReturn())))
        {
            LoadData();
        }
        UCAni = GetComponent<Animator>();//不喜欢这样用，不知道为什么在UCanimationsChange中不能调用
        instaUAC = GetComponent<UCAnimationsChange>();
        StartCoroutine(HealthReduce());
        StartCoroutine(HungryReduce());
    }

    void LoadData()
    {
        //dh = new DataHandle();
        if (Application.platform == RuntimePlatform.Android)
        {
            dh.LoadUCStateAndroid();
        }
        else
        {
            //dh.LoadUCStateWindows();
        }
    }

    void Update()
    {
        //DataLoad();此处添加数据的读取
        IsExitgame();//需要解开注释，数据的保存
        WaitAni();
        GameLogicMain(UCCurrentOpreation.currentLoveState, UCCurrentOpreation.currentHealthState,
            UCCurrentOpreation.currentHungerState, UCCurrentOpreation.currentTouchPartEnum);
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    void IsExitgame()
    {
        if (UCState.HealthValue < 5 || UCState.HungryValue < 5)
        {
            StartCoroutine(ExitAnimation());
            StopAllCoroutines();
            Application.Quit();
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            StartCoroutine(ExitAnimation());
            StopAllCoroutines();
            Application.Quit();
        }
        else
        {

        }
    }

    #region 游戏逻辑
    /// <summary>
    /// 判断执行时的游戏逻辑
    /// </summary>
    /// <param name="UCLoveState"> 实时的爱心状态 </param>
    /// <param name="UCHealthState"> 实时的健康状态 </param>
    /// <param name="UCHungryState"> 实时的饥饿状态 </param>
    /// <param name="touchPart"> 实时点击的位置 </param>
    void GameLogicMain(UCLoveStateEnum UCLoveState, UCHealthStateEnum UCHealthState,
        UCHungerStateEnum UCHungryState, Opreation_TouchPartEnum touchPart)
    {
        switch (UCLoveState)
        {
            case UCLoveStateEnum.Level1_Hate:
                instaUAC.LoveParticle.SetActive(false);
                musicPlayer.SetActive(false);
                TouchPartLogic_Level1_Hate(touchPart);
                break;
            case UCLoveStateEnum.Level2_Normal:
                instaUAC.LoveParticle.SetActive(false);
                musicPlayer.SetActive(false);
                TouchPartLogic_Level2_Normal(touchPart);
                break;

            case UCLoveStateEnum.Level3_Good:
                instaUAC.LoveParticle.SetActive(false);
                musicPlayer.SetActive(false);
                TouchPartLogic_Level3_Good(touchPart);
                break;

            case UCLoveStateEnum.Level4_Lovely:
                instaUAC.LoveParticle.SetActive(true);//在这个时候的表示出爱心粒子特效
                musicPlayer.SetActive(false);
                TouchPartLogic_Level4_Lovely(touchPart);
                break;
            case UCLoveStateEnum.Level5_LovelyDance://空，因为UICtrl已经帮忙做了事情了
                instaUAC.LoveParticle.SetActive(false);
                TouchPartLogic_Level5_LovelyDance(touchPart);
                break;
        }
    }
    #region 五个不同好感状态下不同的动画反应

    /// <summary>
    /// 点击了按钮之后在进行播放跳舞
    /// </summary>
    void TouchPartLogic_Level5_LovelyDance(Opreation_TouchPartEnum touchPart)
    {
        switch (touchPart)
        {
            case Opreation_TouchPartEnum._Ass:
            case Opreation_TouchPartEnum._Breast://生气降低好感度
                UCAni.SetBool(IsDance, false);//点击其他部位将会退出跳舞状态
                StartCoroutine(instaUAC.MatchFaceMechineParamater(angry));
                break;

            case Opreation_TouchPartEnum._Arms:
            case Opreation_TouchPartEnum._Legs://播放惊讶反应，并减少好感度
                UCAni.SetBool(IsDance, false);//点击其他部位将会退出跳舞状态
                StartCoroutine(instaUAC.MatchFaceMechineParamater(surprise));
                break;
            case Opreation_TouchPartEnum._Hands:
            case Opreation_TouchPartEnum._Head:
                UCAni.SetBool(IsDance, false);//点击其他部位将会退出跳舞状态
                break;
        }
    }
    /// <summary>
    /// 好感Lovely
    /// </summary>
    /// <param name="touchpart"></param>
    void TouchPartLogic_Level4_Lovely(Opreation_TouchPartEnum touchpart)
    {
        UCAni.SetBool(IsDance, false);//退出跳舞状态
        switch (touchpart)
        {
            case Opreation_TouchPartEnum._Ass:
            case Opreation_TouchPartEnum._Breast://生气降低好感度
                // instaUAC.PlayFaceAni(FaceAni,angry);//----------------------------------------------
                StartCoroutine(instaUAC.MatchFaceMechineParamater(angry));
                break;

            case Opreation_TouchPartEnum._Arms:
            case Opreation_TouchPartEnum._Legs://播放惊讶反应，并减少好感度
                //instaUAC.PlayFaceAni(FaceAni, surprise);//----------------------------------------
                StartCoroutine(instaUAC.MatchFaceMechineParamater(surprise));
                break;

            case Opreation_TouchPartEnum._Hands:
            case Opreation_TouchPartEnum._Head://开始卖萌，增加好感
                instaUAC.PlayBodyAni(IsMaiMeng);
                break;
        }
    }
    /// <summary>
    /// 好感good
    /// </summary>
    /// <param name="touchpart"></param>
    void TouchPartLogic_Level3_Good(Opreation_TouchPartEnum touchpart)
    {
        UCAni.SetBool(IsDance, false);//退出跳舞状态
        switch (touchpart)
        {
            case Opreation_TouchPartEnum._Ass:
            case Opreation_TouchPartEnum._Breast://旋风踢
                instaUAC.PlayBodyAni(IsXuanFengTi);
                break;

            case Opreation_TouchPartEnum._Arms:
            case Opreation_TouchPartEnum._Legs:
                //生气，并降低好感度（降低好感度全部要从动画事件中去修改）
                //instaUAC.PlayFaceAni(FaceAni, angry);//------------------------------------------
                StartCoroutine(instaUAC.MatchFaceMechineParamater(angry));
                break;

            case Opreation_TouchPartEnum._Hands:
            case Opreation_TouchPartEnum._Head:
                instaUAC.PlayBodyAni(IsShaJiao);
                //发出笑声，增加好感
                break;
        }
    }
    /// <summary>
    /// 好感normal
    /// </summary>
    /// <param name="touchpart"></param>
    void TouchPartLogic_Level2_Normal(Opreation_TouchPartEnum touchpart)
    {
        UCAni.SetBool(IsDance, false);//退出跳舞状态
        switch (touchpart)
        {
            case Opreation_TouchPartEnum._Ass:
            case Opreation_TouchPartEnum._Breast:
                instaUAC.PlayBodyAni(IsXuanFengTi);
                break;

            case Opreation_TouchPartEnum._Arms:
            case Opreation_TouchPartEnum._Legs:
                //instaUAC.PlayFaceAni(FaceAni, angry);//--------------------------------------
                StartCoroutine(instaUAC.MatchFaceMechineParamater(angry));
                break;

            case Opreation_TouchPartEnum._Hands:
            case Opreation_TouchPartEnum._Head:
                //instaUAC.PlayFaceAni(FaceAni, smile);//---------------------------------------
                StartCoroutine(instaUAC.MatchFaceMechineParamater(smile));
                break;
        }
    }

    void TouchPartLogic_Level1_Hate(Opreation_TouchPartEnum touchpart)
    {
        UCAni.SetBool(IsDance, false);//退出跳舞状态
        switch (touchpart)
        {
            case Opreation_TouchPartEnum._Ass:
            case Opreation_TouchPartEnum._Breast:
            case Opreation_TouchPartEnum._Arms:
            case Opreation_TouchPartEnum._Legs:
                instaUAC.PlayBodyAni(IsXuanFengTi);
                break;
            case Opreation_TouchPartEnum._Hands:
            case Opreation_TouchPartEnum._Head:
                StartCoroutine(instaUAC.MatchFaceMechineParamater(scornfully));
                //instaUAC.PlayFaceAni(FaceAni, scornfully);//-------------------------------------
                break;
        }
    }

    #endregion

    /// <summary>
    /// 在好感不差的条件下退出游戏说再见
    /// </summary>
    /// <returns></returns>
    IEnumerator ExitAnimation()
    {
        switch (UCCurrentOpreation.currentLoveState)
        {
            case UCLoveStateEnum.Level2_Normal:
            case UCLoveStateEnum.Level3_Good:
            case UCLoveStateEnum.Level4_Lovely:
            case UCLoveStateEnum.Level5_LovelyDance:
                UCAni.SetTrigger(IsExit);
                yield return new WaitForSeconds(2f);
                break;
        }
    }
    /// <summary>
    /// 生成无聊的动画
    /// </summary>
    /// <returns></returns>
    void WaitAni()
    {
        switch (UCCurrentOpreation.currentLoveState)
        {
            case UCLoveStateEnum.Level2_Normal:
            case UCLoveStateEnum.Level3_Good:
            case UCLoveStateEnum.Level4_Lovely:
                //可能会导致游戏动画的紊乱
                if (true) //如果当前播放的动画是默认的动画,不是的话时间归零-----------------先暂时不改
                {
                    if (timeCountter <= 15)
                    {
                        StartCoroutine(SetFreeIdleBack());
                        StopCoroutine(SetFreeIdleBack());
                        timeCountter += Time.deltaTime;
                    }
                    else
                    {
                        StartCoroutine(SetFreeIdle());
                        StopCoroutine(SetFreeIdle());
                        timeCountter = 0;
                    }
                }
                break;
        }
    }

    /// <summary>
    /// 设置随机的静止动画
    /// </summary>
    /// <returns></returns>
    IEnumerator SetFreeIdle()
    {
        UCAni.SetBool("IsReleaseBack", true);
        yield return new WaitForSeconds(1f);
        UCAni.SetInteger("RandomIdle", Random.Range(1, 3));
    }

    IEnumerator SetFreeIdleBack()
    {
        yield return new WaitForSeconds(1f);
        UCAni.SetInteger("RandomIdle", -1);
    }

    /// <summary>
    /// 淋雨的时候健康值减少
    /// </summary>
    /// <returns></returns>
    IEnumerator HealthReduce()
    {
        while (true)
        {
            if (!TwoGOmutual.isHandUmbrella && RainScenes.activeInHierarchy)
            {
                UCState.ChangeUCHeathValue(-2);
            }
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator HungryReduce()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            UCState.ChangeUCHungryValue(-5);
        }
    }

    #endregion
}
