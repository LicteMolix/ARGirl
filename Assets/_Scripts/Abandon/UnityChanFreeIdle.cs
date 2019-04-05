using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanFreeIdle : MonoBehaviour
{

    public Animator UnityChanAni;//身体动画层
    //public Animator UnityChanFaceAni;//面部动画层

    public float intervalTime = 20.0f;
    public float waitTime = 10.0f;
    public float aniPlayTime = 20.0f;
    public float current_aniPlayTime = 0;
    public float currentTime = 0f;


    private bool isPlayOver = false;
    private bool isCanPlay = false;

    void Start()
    {
        UnityChanAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region animation Test
        if (Input.GetKeyDown(KeyCode.W))
        {
            UnityChanAni.SetBool("Run", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            UnityChanAni.SetBool("Run", false);
        }
        #endregion
        if (Input.touchCount == 0)
        {
            //实现的功能是隔20秒进行一次动画的自由播放
            current_aniPlayTime += Time.deltaTime;
            if (current_aniPlayTime > aniPlayTime)
            {
                isCanPlay = true;
                current_aniPlayTime = 0;
            }
            else
            {
                isCanPlay = false;
            }
            if (isCanPlay)
            {
                //print("开始");
                isCanPlay = false;
                StartCoroutine("ChangeFreeIdle");
            }
            if (!isCanPlay && isPlayOver)
            {
                //print("回滚");
                IdleBack();
                isPlayOver = false;
            }
        }

    }


    IEnumerator ChangeFreeIdle()
    {
        int FreeIdleNumber = Random.Range(1, 3);
        UnityChanAni.SetInteger("FreeIdle", FreeIdleNumber);//播放随机的等待无聊动画
        yield return new WaitForSeconds(5f);//等待5秒
        isPlayOver = true;
        UnityChanAni.SetBool("IsFreeTimeOut", isPlayOver);
    }

    void ChangeFreeIdle1()
    {
        int FreeIdleNumber = Random.Range(1, 3);
        UnityChanAni.SetInteger("FreeIdle", FreeIdleNumber);
        UnityChanAni.SetBool("IsFreeTimeOut", isPlayOver);
    }

    /// <summary>
    /// 将动画还原成为最初的形态
    /// </summary>
    void IdleBack()
    {
        UnityChanAni.SetInteger("FreeIdle", -1);
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(waitTime);
    }
}