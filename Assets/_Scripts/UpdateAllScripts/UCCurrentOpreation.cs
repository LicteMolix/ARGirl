using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该类用于记录时时操作的状态
/// </summary>
public class UCCurrentOpreation : MonoBehaviour
{
    [HideInInspector]
    public static UCCurrentOpreation instanceUCCO;

    public GameObject UC;//UnityChan
    public Animator UCani;//动画组件

    /// <summary>
    /// UC当前的好感度
    /// </summary>
    public static UCLoveStateEnum currentLoveState;
    /// <summary>
    /// UC当前的饥饿值
    /// </summary>
    public static UCHungerStateEnum currentHungerState;
    /// <summary>
    /// UC当前的健康值
    /// </summary>
    public static UCHealthStateEnum currentHealthState;
    /// <summary>
    /// 当前点击的地方
    /// </summary>
    public static string currentTouchPart;
    /// <summary>
    /// 当前点击的地方的枚举
    /// </summary>
    public static Opreation_TouchPartEnum currentTouchPartEnum;

    void Awake()
    {
        instanceUCCO = new UCCurrentOpreation();
    }

    void Start()
    {
        if (UC != null)
        {
            UCani = UC.GetComponent<Animator>();
        }
        else
        {
            UCani = GetComponent<Animator>();
        }
        StartCoroutine(UpdateMY());
    }
   
    IEnumerator UpdateMY()
    {
        while (true)
        {
            currentLoveState = UCState.JudgeUCCurrentLoveState();
            currentHungerState = UCState.JudgeUCCurrentHungryState();
            currentHealthState = UCState.JudgeUCCurrentHealthState();
            currentTouchPart = CurrentTouchPart();
            currentTouchPartEnum = touchStringToEnum(currentTouchPart);
            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// 当前触碰的地方
    /// </summary>
    /// <returns></returns>
    private string CurrentTouchPart()
    {
        RaycastHit rayhitinfo;
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayhitinfo, Mathf.Infinity))
            {
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

    /// <summary>
    /// 将点击的地方转化成为Opreation_TouchPartEnum
    /// </summary>
    /// <param name="currentTouchPart"></param>
    /// <returns></returns>
    private Opreation_TouchPartEnum touchStringToEnum(string currentTouchPart)
    {
        if (currentTouchPart == Opreation_TouchConst.Part_Arm)
        {
            return Opreation_TouchPartEnum._Arms;
        }
        if (currentTouchPart == Opreation_TouchConst.Part_Ass)
        {
            return Opreation_TouchPartEnum._Ass;
        }
        if (currentTouchPart == Opreation_TouchConst.Part_Breast)
        {
            return Opreation_TouchPartEnum._Breast;
        }
        if (currentTouchPart == Opreation_TouchConst.Part_Hands)
        {
            return Opreation_TouchPartEnum._Hands;
        }
        if (currentTouchPart == Opreation_TouchConst.Part_Head)
        {
            return Opreation_TouchPartEnum._Head;
        }
        if (currentTouchPart == Opreation_TouchConst.Part_Legs)
        {
            return Opreation_TouchPartEnum._Legs;
        }
        else
        {
            return Opreation_TouchPartEnum._NULL;
        }
    }
}
