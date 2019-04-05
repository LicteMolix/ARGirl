/*
 * 作者：licte（陈杰）    版本：1.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum _EnumBodypart
{
    NULL,//没有点击的时候，或者点击到其他地方的时候
    Hands,
    Head,
    Arms,
    Legs,
    Breast,
    Ass
}

public class _UnityChanAniCtrl : MonoBehaviour
{
    public Animator UnityChanAni;

    private string faceAni = "FaceAni";//状态机中的parameters
    private int faceAniNumber = -1;//当前的状态的数字值

    private string touchPart;

    public float number;//面部状态机的数据

    public _EnumState currentState;

    [SerializeField]
    public _EnumBodypart currentBodyPart;
    public Text showstate;
    public Text showTouch;
    //其他类的东西，用以在主游戏更新中进行物体的交互
    _UC_OtherThings gameObjectCtrl;

    //构造单例，方便其他访问
    public static _UnityChanAniCtrl ucLovectrl;

    public int isUnderUMBonce = 0;

    private void Awake()
    {
        ucLovectrl = this;
    }

    void Start() { 
        UnityChanAni = GetComponent<Animator>();
        gameObjectCtrl = GetComponent<_UC_OtherThings>();
    }

    void Update()
    {
        //number = unityChanCurrentState.LoveValue;
        number = _StaticUnityChanstate.loveValue;
        currentState = _StaticUnityChanstate.JudgeState();
        //currentState = unityChanCurrentState.JudgeState();//获取当前的状态
        touchPart = ReturnStrTouched();//获取点击的目标的位置,string

        currentBodyPart = SetPartByTouch(touchPart);//获得当前的目标的点击部位的装换,封装起来
        showTouch.text = touchPart.ToString();
        showstate.text = currentState.ToString();
        PlayAniByTouch(currentBodyPart, currentState);

        UCstate_Particle(currentState);
    }

    /// <summary>
    /// 通过判断当前的状态进行对应好感的状态的设置
    /// </summary>
    /// <param name="current"></param>
    public void SetAniByState(_EnumState current)
    {

        switch (current)
        {
            case _EnumState.SayGoodBey://退出游戏前做点准备
                SetFaceAnimation(1);
                gameObjectCtrl.LoveParticleB();
                //并显示游戏结束
                break;
            case _EnumState.HateYou://播放厌恶的表情，默认状态
                //设置表情，现将表情恢复成默认表情，然后将表情切换到厌恶
                SetFaceAnimation(1);
                gameObjectCtrl.LoveParticleB();
                break;
            case _EnumState.JustSoSo://播放一般的表情默认状态
                //设置表情,将表情恢复成默认表情
                SetFaceAnimation(0);
                gameObjectCtrl.LoveParticleB();
                break;
            case _EnumState.KnowYou://播放微笑的表情,默认状态
                //设置表情,将表情恢复成默认表情,然后将表情设置为微笑状态
                SetFaceAnimation(6);
                gameObjectCtrl.LoveParticleB();
                break;
            case _EnumState.LoveLy://播放开心的表情，默认状态,相应的粒子特效，并解锁极乐净土的播放按钮
                //设置表情,将表情恢复成默认表情，播放开心的表情
                gameObjectCtrl.LoveParticleS();
                SetFaceAnimation(5);
                break;
        }
    }

    /// <summary>
    /// 退出时不同的动画的改变
    /// </summary>
    /// <param name="current_state">当前状态</param>
    public void ExitGame(_EnumState current_state)
    {
        //不同状态下，玩家退出游戏看到的动画是不同的
    }

    /// <summary>
    /// 用以操控其他的物体,和UC有关的各种特效
    /// </summary>
    /// <param name=""></param>
    public void UCstate_Particle(_EnumState current)
    {
        //Debug.LogError(current);
        switch (current)
        {
            case _EnumState.SayGoodBey:
                gameObjectCtrl.LoveParticleB();
                break;
            case _EnumState.HateYou:
                gameObjectCtrl.LoveParticleB();
                break;
            case _EnumState.JustSoSo:
                if (!gameObjectCtrl.RainScene.activeInHierarchy)
                {
                    gameObjectCtrl.RainDayShow();
                    gameObjectCtrl.GetYouUMB();
                }
                gameObjectCtrl.LoveParticleB();
                gameObjectCtrl.RainDayOpreation(UnityChanAni);
                break;
            case _EnumState.KnowYou:
                gameObjectCtrl.LoveParticleB();
                break;
            case _EnumState.LoveLy:
                if (!gameObjectCtrl.LoveParticle.activeInHierarchy)
                {
                    gameObjectCtrl.LoveParticleS();
                }

                //跳舞只能跳一次
                //if (!gameObjectCtrl.buttonDance.activeInHierarchy && _StaticUnityChanstate.loveValue == 100 && !gameObjectCtrl.isClick)
                //{
                //    gameObjectCtrl.DanceBTNShow();
                //    gameObjectCtrl.isClick = true;
                //}
                //可以重复多次
                if (!gameObjectCtrl.buttonDance.activeInHierarchy && _StaticUnityChanstate.loveValue == 100)
                {
                    gameObjectCtrl.DanceBTNShow();
                    gameObjectCtrl.isClick = true;
                }
                if (_StaticUnityChanstate.loveValue != 100)
                {
                    _UnityFillDance.unityfilldance.Hide();
                }
                if (_StaticUnityChanstate.loveValue == 100)
                {

                }
                break;
            default:
                OtherthingDefaultStateChange();
                break;
        }
    }

    #region 关于和其他东西进行互动的函数块
    /// <summary>
    /// 设置他们的默认状态
    /// </summary>
    void OtherthingDefaultStateChange()
    {
        gameObjectCtrl.isClick = false;
        gameObjectCtrl.LoveParticleB();
        gameObjectCtrl.RainDayHide();
        gameObjectCtrl.DanceBTNB();
    }



    #endregion

    /// <summary>
    /// 通过判断当前状态进行确定什么部位播放什么动画
    /// </summary>
    /// <param name="current_bodypart">当前的身体部位</param>
    /// <param name="current_State">当前的好感状态</param>
    public void PlayAniByTouch(_EnumBodypart current_bodypart, _EnumState current_State)//----------------------
    {
        switch (current_bodypart)
        {
            //在不是很坏的情况下面进行增加好感
            case _EnumBodypart.Hands:
            case _EnumBodypart.Head:
                TouchGood(current_State);
                break;
            #region 摸头和手状态的改变
            /*if (current_State == _EnumState.LoveLy)
             {
                 StartCoroutine(SetUnityChanAni("IsMaiMeng"));
                 //unityChanCurrentState.Change_LoveValue(10);-----------------------------------------------------数据的修改有问题
                 //添加UI交互，好感度条 +10
                 StopCoroutine("SetUnityChanAni");
             }
             else if (current_State == _EnumState.KnowYou)
             {
                 StartCoroutine(SetUnityChanAni("IsShaJiao"));
                 //unityChanCurrentState.Change_LoveValue(5);-----------------------------------------------------数据的修改有问题
                 //添加UI交互，好感度条 +5
                 StopCoroutine("SetUnityChanAni");
             }
             else if(current_State == _EnumState.JustSoSo)
             {
                 StartCoroutine(SetFaceAnimation_Touch(6));
                 //unityChanCurrentState.Change_LoveValue(2);-----------------------------------------------------数据的修改有问题
                 StopCoroutine("SetFaceAnimation_Touch");
             }
             else//其他的想要提高好感度的需要喂东西才能够进行提高了
             {
                 StartCoroutine(SetUnityChanAni("IsXuanFengTi"));
                 //unityChanCurrentState.Change_LoveValue(0);-----------------------------------------------------数据的修改有问题
                 //添加UI交互，好感度条 +5
                 StopCoroutine("SetUnityChanAni");
             }
             //else
             //{
             //    StartCoroutine(SetUnityChanAni("", true));
             //    //添加UI交互，好感度条
             //    StopCoroutine("SetUnityChanAni");
             //}break;*/
            #endregion

            case _EnumBodypart.Arms:
                TouchSoso_Arms(current_State);

                break;
            case _EnumBodypart.Legs:

                #region 摸手臂和腿状态的改变
                /*if (current_State == _EnumState.LoveLy || current_State == _EnumState.KnowYou)
                {
                    StartCoroutine(SetUnityChanAni("IsShaJiao"));
                    //添加UI交互，好感度条 +5

                    StopCoroutine("SetUnityChanAni");
                }
                else if (current_State == _EnumState.JustSoSo)
                {
                    StartCoroutine(SetFaceAnimation_Touch(4));
                    StopCoroutine("SetFaceAnimation_Touch");
                }
                else
                {
                    StartCoroutine(SetUnityChanAni("IsXuanFengTi"));
                    //添加UI交互，好感度条 -20
                    StopCoroutine("SetUnityChanAni");
                }*/
                #endregion
                TouchSoso_Legs(current_State);
                break;

            case _EnumBodypart.Ass:
            case _EnumBodypart.Breast:
                #region 点击到不好的地方的状态
                /*
                if (currentState == _EnumState.LoveLy)
                {
                    StartCoroutine(SetFaceAnimation_Touch(2));
                    StopCoroutine("SetFaceAnimation_Touch");
                    //好感度的改变
                }
                else
                {
                    StartCoroutine(SetUnityChanAni("IsXuanFengTi"));
                    StartCoroutine(SetFaceAnimation_Touch(1));
                    StopCoroutine("SetFaceAnimation_Touch");
                    //添加UI交互，好感度条 +5
                    StopCoroutine("SetUnityChanAni");
                }*/
                #endregion
                TouchBad(current_State);
                break;
            default:
                //SetAniByState(current_State);
                break;
        }
    }

    #region 两个识别物体之间的的互动
    /// <summary>
    /// 通过判断游戏对象的当前的状态进行效果的显示
    /// </summary>
    /// <param name="currentState"></param>
    void InitialCloud(_EnumState currentState)
    {
        if (currentState == _EnumState.JustSoSo)
        {
            //设置RainScenes;
            //应为是在下雨，可以通过使用雨伞效果进行好感的提升，如果淋雨的时间太长的话，健康值和心情还有好感都会降低
            //(健康和心情系统在代码重构之后进行加入)

        }
    }

    #endregion

    #region 动画的改变
    /// <summary>
    /// 点击到好的地方
    /// </summary>
    /// <param name="current">当前的状态</param>
    void TouchGood(_EnumState current)
    {
        switch (current)
        {
            case _EnumState.LoveLy:
                StartCoroutine(SetUnityChanAni("IsMaiMeng"));
                //添加UI交互，好感度条 +10
                StopCoroutine("SetUnityChanAni");
                break;
            case _EnumState.KnowYou:
                //SetFaceAnimation(5);
                StartCoroutine(SetFaceAnimation_Touch(5));
                ////添加UI交互，好感度条 +10
                StopCoroutine("SetFaceAnimation_Touch");
                break;

            case _EnumState.JustSoSo:
                StartCoroutine(SetUnityChanAni("IsShaJiao"));
                //添加UI交互，好感度条 +10
                StopCoroutine("SetUnityChanAni");
                break;

            case _EnumState.HateYou:
            case _EnumState.SayGoodBey:
                StartCoroutine(SetFaceAnimation_Touch(2));
                StopCoroutine("SetFaceAnimation_Touch");
                break;
        }
    }

    /// <summary>
    /// 一般的点击
    /// </summary>
    /// <param name="current">当前的状态</param>
    void TouchSoso_Arms(_EnumState current)
    {
        switch (current)
        {
            case _EnumState.LoveLy:
            case _EnumState.KnowYou:
                //SetFaceAnimation(4);
                StartCoroutine(SetFaceAnimation_Touch(4));
                StopCoroutine("SetFaceAnimation_Touch");
                break;

            case _EnumState.JustSoSo:
                //SetFaceAnimation(3);
                StartCoroutine(SetFaceAnimation_Touch(3));
                StopCoroutine("SetFaceAnimation_Touch");
                break;

            case _EnumState.HateYou:
            case _EnumState.SayGoodBey:
                // SetFaceAnimation(2);
                StartCoroutine(SetFaceAnimation_Touch(2));
                StopCoroutine("SetFaceAnimation_Touch");
                break;
        }
    }

    void TouchSoso_Legs(_EnumState current)
    {
        switch (current)
        {
            case _EnumState.LoveLy:
            case _EnumState.KnowYou:
                // SetFaceAnimation(3);
                StartCoroutine(SetFaceAnimation_Touch(3));
                StopCoroutine("SetFaceAnimation_Touch");
                break;

            case _EnumState.JustSoSo:
                //SetFaceAnimation(2);
                StartCoroutine(SetFaceAnimation_Touch(2));
                StopCoroutine("SetFaceAnimation_Touch");
                break;

            case _EnumState.HateYou:
            case _EnumState.SayGoodBey:
                StartCoroutine(SetUnityChanAni("IsXuanFengTi"));
                StopCoroutine("SetUnityChanAni");
                break;
        }
    }

    /// <summary>
    /// 点击到不好的地方
    /// </summary>
    /// <param name="current">当前的状态</param>
    void TouchBad(_EnumState current)
    {
        switch (current)
        {
            case _EnumState.LoveLy:
            case _EnumState.KnowYou:
                StartCoroutine(SetFaceAnimation_Touch(1));
                //添加UI交互，好感度条 +10
                StopCoroutine("SetFaceAnimation_Touch");
                // SetFaceAnimation(1);
                break;

            case _EnumState.JustSoSo:
            case _EnumState.HateYou:
                StartCoroutine(SetUnityChanAni("IsXuanFengTi"));
                StopCoroutine("SetUnityChanAni");
                break;

            case _EnumState.SayGoodBey:
                //使用shader进行角色的消融的展示
                //-------------------------------------------------------------------------------------------
                //未写
                break;
        }
    }
    #endregion

    #region 好感的处理
    void changeLove(int number)//通过使用帧事件进行引用，确保好感在某一帧修改对应的数值
    {
        _StaticUnityChanstate.ChangeLoveValue(number);
    }
    #endregion

    #region 数据的装换以及状态动画的改变与封装
    /// <summary>
    /// 通过点击位置的tag进行部位的转换，用来判断当前的状态，参考：currentBodyPart = SetPartByTouch(tag);
    /// </summary>
    /// <param name="touchPartName"> 点击位置的tag </param>
    /// <returns></returns>
    public _EnumBodypart SetPartByTouch(string touchPartName)
    {
        switch (touchPartName)
        {
            case ConstPart.Part_Head: return _EnumBodypart.Head;
            case ConstPart.Part_Hands: return _EnumBodypart.Hands;

            case ConstPart.Part_Arm: return _EnumBodypart.Arms;
            case ConstPart.Part_Legs: return _EnumBodypart.Legs;

            case ConstPart.Part_Ass: return _EnumBodypart.Ass;
            case ConstPart.Part_Breast: return _EnumBodypart.Breast;

            default: return _EnumBodypart.NULL;
        }
    }

    /// <summary>
    /// 设置面部表情,状态（用当前的好感值进行的判断）改变的表情设置
    /// </summary>
    /// <param name="FaceAniNumber">状态机中的转换参数</param>
    public void SetFaceAnimation(int FaceAniNumber)
    {
        //设置状态，从默认的动画切换到Bey动画
        UnityChanAni.SetInteger(faceAni, 0);
        //等待一秒后yield return new WaitForSeconds(1f);
        UnityChanAni.SetInteger(faceAni, FaceAniNumber);
    }

    /// <summary>
    /// 设置面部表情,点击时的动画转换
    /// </summary>
    /// <param name="FaceAniNumber">状态机中的转换参数</param>
    IEnumerator SetFaceAnimation_Touch(int FaceAniNumber)
    {
        UnityChanAni.SetInteger(faceAni, 0);
        yield return new WaitForSeconds(0.5f);
        UnityChanAni.SetInteger(faceAni, FaceAniNumber);
        yield return new WaitForSeconds(5f);
        UnityChanAni.SetInteger(faceAni, 0);
    }

    /// <summary>
    /// 设置播放动画之后立刻将动画状态还原，并且只播放一次动画
    /// </summary>
    /// <param name="name">状态机中的parameters的名称</param>
    /// <param name="state">设置状态</param>
    /// <returns></returns>
    IEnumerator SetUnityChanAni(string name)
    {
        UnityChanAni.SetBool(name, true);
        yield return new WaitForSeconds(0.4f);
        UnityChanAni.SetBool(name, false);
        UnityChanAni.SetBool("IsReleaseBack", true);
    }
    #endregion

    #region 位置处理
    /// <summary>
    /// 返回当前点击的部位的tag
    /// </summary>
    /// <returns></returns>
    public string ReturnStrTouched()
    {
        RaycastHit rayhitinfo;
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayhitinfo, Mathf.Infinity))
            {
                Debug.Log(rayhitinfo.collider.tag);
                return rayhitinfo.collider.tag;
            }
            else
            {
                return " ";
            }
        }
        else
        {
            return " ";
        }

    }
    #endregion

}
