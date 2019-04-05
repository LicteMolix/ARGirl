using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _UnityFillDance : MonoBehaviour
{
    public Animator unitychanani;
    public GameObject unitychan;
    public GameObject musicDance;
    // Use this for initialization
    void Start()
    {
       
        unityfilldance = this;
        musicDance.SetActive(false);
        unitychanani = unitychan.GetComponent<Animator>();
    }

    public static _UnityFillDance unityfilldance;

    /// <summary>
    /// 跳舞的动画播放函数
    /// </summary>
    public IEnumerator Dance()
    {
        if (unitychanani && _StaticUnityChanstate.loveValue == 100)
        {
            musicDance.SetActive(true);
            unitychanani.SetBool("IsDance", true);
            yield return new WaitForSeconds(1f);
            unitychanani.SetBool("IsReleaseBack", true);//就应该将这个值设置成为一直为真
            unitychanani.SetBool("IsDance", false);
            Hide();
        }
        else
        {
            musicDance.SetActive(false);
            StopCoroutine(Dance());
            yield return new WaitForSeconds(1f);
        }
    }

    public void StartDance()
    {
        StartCoroutine(Dance());
    }

    /// <summary>
    /// 隐藏提示按钮
    /// </summary>
    public void Hide()
    {
        unitychan.GetComponent<_UC_OtherThings>().DanceBTNB();
    }
}
