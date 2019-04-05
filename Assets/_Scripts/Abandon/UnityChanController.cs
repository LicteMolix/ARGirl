using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum LoveState
{
    GoodBey,
    Awful,
    JustSoSo,
    KnowYou,
    LoveLy
}

public enum PartOfBodyEnum
{
    NULL,//没有点击的时候，或者点击到其他地方的时候
    Hands,
    Head,
    Arms,
    Legs,
    Breast,
    Ass
}

public class UnityChanController : MonoBehaviour
{
    #region 数据定义
    //public Camera mainCamera;
    public Animator UnityChanAni;//身体动画层
    public Animator UnityChanFaceAni;//面部动画层

    public PartOfBodyEnum currentState;

    private bool isTrue = false;
    //用于判断的条件
    private bool isExit = false;
    private bool isFreeTimeOut = false;
    private bool isExcessively = false;


    private int perDay_RaiseFrequency = 20;//能够提升好感的最高次数，每天


    //初期增加好感的触碰地方 
    private bool isTouch_Hands = false;   // +5
    private bool isTouch_Face = false;    // +10
    private bool isTouch_Head = false;    // +15
    //后期可以解锁为增加好感的地方
    private bool isTouch_Arm = false;     // -10  +1
    private bool isTouch_Leg = false;     // -20  +1
    //绝对不能触碰的地方
    private bool isTouch_Breast = false;  // -30
    private bool isTouch_Ass = false;     // -50

    //判断好感的增减
    private bool isRiseLove = false;
    private bool isReduceLove = false;
    private LoveState currentLoveState;//-------------此处要增加一个好感的状态判断，是不是达到了比较熟的地步，是不是可以解锁东西了


    //时间计数部分
    private float touchMaxTime = 5f;
    public float interval = 40f;//动画播放的时间
    public float waitAniStartTime = 1f;//点击后过了多少时间之后锁定切换状态
    #endregion

    void Start()
    {
        UnityChanAni = GetComponent<Animator>();
        UnityChanFaceAni = GetComponent<Animator>();
    }

    void Update()
    {
        #region 表情测试
        //if (Input.GetKeyDown(KeyCode.Q)){
        //    UnityChanAni.SetBool("IsPos",true);
        //}
        //if (Input.GetKeyUp(KeyCode.Q)){
        //    UnityChanAni.SetBool("IsPos", false);
        //}
        #endregion

        RaycastHit rayhitinfo;
        #region 判断点击的位置，并修改动画转换的条件
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayhitinfo, Mathf.Infinity))
            {

                Debug.Log(rayhitinfo.collider.tag);//---------------------------------------------

                switch (rayhitinfo.collider.tag)
                {
                    #region 增加好感的部分-----------------添加功能的时候需要进行改动
                    case ConstPart.Part_Hands:
                    case ConstPart.Part_Head://播放对应的动画，改变对应的好感度，刷新UI
                        isRiseLove = true;

                        UnityChanAni.SetBool("IsHappy", true);
                        StartCoroutine("ChangeHappyStateBack");

                        //如果使用的整体的话，此处添加一个锁定状态的函数（将动画的转换条件修改回来）
                        break;
                    #endregion

                    #region 前期么不能增加好感部分,后期可以增加好感
                    case ConstPart.Part_Arm://dosomething
                    case ConstPart.Part_Legs:

                        break;
                    #endregion

                    #region 永远都不能增加好感部分
                    case ConstPart.Part_Ass:
                    case ConstPart.Part_Breast:
                        //if(Input.touches[1].deltaTime >= touchMaxTime){
                        UnityChanAni.SetBool("IsTouchBanPart", true);
                        StartCoroutine("ChangeHeatStateBack");
                        //}
                        //播放讨厌动画的那部分场景
                        break;
                        #endregion
                }
            }
        }
        #endregion

        #region 动画状态的转变

        #endregion
    }
    
    /// <summary>
    /// 从不讨厌的状态变回来
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeNotHeatStateBack()
    {
        yield return new WaitForSeconds(waitAniStartTime);
        UnityChanAni.SetBool("IsFreeTimeOut", true);
        UnityChanAni.SetBool("IsHappy", false);
    }
    /// <summary>
    /// 从开心的状态变回来
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeHappyStateBack()
    {
        yield return new WaitForSeconds(waitAniStartTime);
        UnityChanAni.SetBool("IsFreeTimeOut", true);
        UnityChanAni.SetBool("IsHappy", false);
    }
    /// <summary>
    /// 从讨厌的状态变回来
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeHeatStateBack()
    {
        yield return new WaitForSeconds(waitAniStartTime);
        UnityChanAni.SetBool("IsFreeTimeOut", true);
        UnityChanAni.SetBool("IsTouchBanPart", false);//这里应该添加是不是应该将状态修改的判断条件
    }
}
