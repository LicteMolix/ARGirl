using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class TwoGOmutual : MonoBehaviour
{
    public Animator UCani;
    public GameObject FoodParent;
    public GameObject IMageFoodTarget;
    public GameObject UCCakeTarget;
    /// <summary>
    /// 雨伞的标记位置
    /// </summary>
    public GameObject UCUmbrellaTarget;
    [Space(12)]
    /// <summary>
    /// 雨伞
    /// </summary>
    public GameObject Umbrella;
    public GameObject RainScene;//下雨的场景，用于判断
    [Space(12)]
    /// <summary>
    /// 蛋糕
    /// </summary>
    public GameObject Cake;

    private float eattime = 0;
    [Space(12)]
    /// <summary>
    /// 电风扇
    /// </summary>
    public GameObject Fanner;

   

    private UCAnimationsChange instUCAC;

    private float umbrella_UC_distance;
    private float umbrella_UC_verticalDis;
    public static bool isHandUmbrella = false;

    private float cake_UC_distance;
    public static bool isEat = false;
    private string Iseat = "IsEatFood";

    private void Awake()
    {
        instUCAC = GetComponent<UCAnimationsChange>();
        UCani = transform.GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(Update_MY());
    }

    IEnumerator Update_MY()
    {
        while (true)
        {
            //该处应该加上判断，不然的话将会变得
            //雨伞部分，空间位置的判断需要好好的注意一下
            umbrella_UC_distance = Vector3.Distance(UCUmbrellaTarget.transform.position, Umbrella.transform.position);
            umbrella_UC_verticalDis = Umbrella.transform.position.y - UCUmbrellaTarget.transform.position.y;
            //print("空间距离" + umbrella_UC_distance);
            //print("垂直距离" + umbrella_UC_verticalDis);
            MutualWithUmbrella(umbrella_UC_distance, umbrella_UC_verticalDis);
            //蛋糕部分
            cake_UC_distance = Vector3.Distance(UCCakeTarget.transform.position, Cake.transform.position);
            MutualWithCake(cake_UC_distance);
            //电风扇部分
            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// 和雨伞的交互-未测试
    /// </summary>
    /// <param name="distance_T"> 空间的距离 </param>
    /// <param name="verticalDis_T"> 垂直方向的距离 </param>
    void MutualWithUmbrella(float distance_T, float verticalDis_T)
    {
        if (RainScene.activeInHierarchy)
        {
            if (distance_T < 1 && verticalDis_T > -0.5f && verticalDis_T < -0.05f)
            {
                isHandUmbrella = true;
                //print("isHandUmbrella-是否在打伞" + isHandUmbrella);
                instUCAC.PlayBodyAni(instUCAC.IsShaJiao);
            }
            else
            {
                isHandUmbrella = false;
                //print("isHandUmbrella-是否在打伞" + isHandUmbrella);
                UCani.SetBool("IsShaJiao", false);
            }
        }
    }

    /// <summary>
    ///  和蛋糕的交互-未测试
    /// </summary>
    /// <param name="distance_T">空间的距离 </param>
    /// <param name="verticalDis_T"> 垂直方向的距离</param>
    /// <param name="horizentalDis_T"> 水平方向的距离</param>
    void MutualWithCake(float distance_T)
    {
        eattime += Time.deltaTime;
       // UCani.SetBool(Iseat, true);
        if (distance_T < 0.7f && distance_T > 0 )
        {
            if (eattime > 10)
            {
                print("开始吃东西");//----可以进来了
                //设置东西为子物体
                //changepos();
                //播放吃东西的动画
                //SetCakeParent();
                StartCoroutine(Eat());
                //循环播放吃东西的动画
                //隐藏东西
                //设置父物体为空
                //修改好感值和饥饿值和健康值
                //返回默认动画
                eattime = 0;
            }
        }
    }

    /// <summary>
    /// 吃东西时改变位置
    /// </summary>
    public void SetCakeParent()
    {
        if (UCCakeTarget)
        {
            Cake.transform.parent = FoodParent.transform;
            Cake.transform.localPosition = new Vector3(-0.069f, 0.059f, -0.018f);
            Cake.transform.localRotation = new Quaternion(0f, 0f, 0, 1);
        }
    }

    /// <summary>
    /// 吃完了修改他的位置，并隐藏，可能会产生再吃识别问题（位置不对）
    /// </summary>
    public void DevidedCakeParent()
    {
        Cake.transform.parent = null;
        Cake.transform.position = new Vector3(-0, 0, -0);
        Cake.transform.rotation = Quaternion.identity;
        UCState.ChangeUCHeathValue(+10);
        UCState.ChangeUCHungryValue(+20);
        UCState.ChangeUCLoveValue(+10);
    }

    public IEnumerator Eat()
    {
        UCani.SetBool(Iseat, true);
        yield return new WaitForSeconds(3f);
        UCani.SetBool(Iseat, false);
    }
}

