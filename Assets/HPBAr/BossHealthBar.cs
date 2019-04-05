using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public static BossHealthBar instance;
    public GameObject goHealthBar;
    public Scrollbar forDel;
    private Vector3 positionVec3 = new Vector3();
    public Text HpTxt;
    private int index = 1;//当前血条阶段
    public int Index
    {
        get
        {
            return this.index;
        }

        set
        {
            this.index = value;
        }
    }

    void Start()
    {
        timer = 10;
        this.positionVec3 = this.transform.localPosition;
    }
    void Update()
    {
        this.CastAnimation(this.goHealthBar);
    }
    /// <summary>
    /// 血条当前值显示
    /// </summary>
    /// <param name="curValue">当前血量</param>
    /// <param name="maxValue">最大血量</param>
    /// <param name="isBoss">是否是Bosss</param>
    public void SetHealthValueText(int current, int max, bool isTrubans)
    {
        if (isTrubans)
        {
            float hp = ((float)current / (float)max) * 100;
            HpTxt.text = hp.ToString("f") + "%";
        }
        else
        {
            HpTxt.text = current + "/" + max;
        }
    }

    /// <summary>
    /// 血条状态处理
    /// </summary>
    /// <param name="curValue">当前血量</param>
    /// <param name="maxValue">最大血量</param>
    /// <param name="isBoss">是否是Bosss</param>
    public void UpdateHealthBar(int curValue, int maxValue, bool isTurbans)
    {
        Scrollbar progressBar = null;
        Image _sp = null;
        progressBar = this.goHealthBar.GetComponent<Scrollbar>();
        _sp = this.transform.Find("HealthBar/SlidingArea/forDel").GetComponent<Image>();
        if (curValue >= maxValue)
        {
            curValue = maxValue;
            progressBar.size = 1;
            return;
        }
        if (maxValue <= 0 && isTurbans == false)
        {
            this.gameObject.SetActive(false);
        }
        if (Index == 0) return;
        int valueOfLine = maxValue / Index;
        if (valueOfLine <= 0)
            return;
        int index = curValue / valueOfLine;
        if (curValue % valueOfLine == 0)
            index--;
        float value = (curValue - index * valueOfLine) / (float)valueOfLine;
        if (curValue <= 0)
            value = 0;
        if (null != progressBar)
        {
            progressBar.size = value;
        }
    }

    //血条中间图片滑动处理
    void CastAnimation(GameObject father)
    {
        Scrollbar progressBar = null;
        if (null != father)
        {
            progressBar = father.GetComponent<Scrollbar>();
        }
        if (null == progressBar)
        {
            return;
        }
        if (this.forDel.size <= progressBar.size)
        {
            this.forDel.size = progressBar.size;
        }
        else
        {
            this.forDel.size -= Time.deltaTime * 0.5f;
        }
    }

#region 测试代码
    private float timer;       //距离多长时间刷新列表
    private float useTime = 0;
    int xt = 100;
    void FixedUpdate()
    {
        if (timer > 0 && useTime < Time.time)
        {
            useTime = Time.time + 1;
            timer -= 1;
            xt =xt- 10;
            UpdateHealthBar(xt, 100, false);
            SetHealthValueText(xt, 100, false);
        }
    }
#endregion

}