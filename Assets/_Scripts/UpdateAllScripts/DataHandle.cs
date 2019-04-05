using System.Collections;
using UnityEngine;
using System.Xml.Linq;
using System;

class DataHandle : MonoBehaviour 
{
    private string Path = Application.streamingAssetsPath + @"\data.xml";
    /// <summary>
    /// 使用XML进行数据的保存
    /// 默认在退出游戏的时候进行保存（宠物没有死亡的状态）
    /// </summary>
    public void SaveUCState()
    {
        float healthValue = UCState.HealthValue;
        float loveValue = UCState.LoveValue;
        float hungryValue = UCState.HungryValue;
        XDocument XmlD = new XDocument();
            XElement UCstate = new XElement("UnityChan");
                XElement healthvalue = new XElement("healthvalue", healthValue);
                XElement hungryvalue = new XElement("hungryvalue", hungryValue);
                XElement lovevalue = new XElement("lovevalue", loveValue);
            UCstate.Add(healthvalue);
            UCstate.Add(hungryvalue);
            UCstate.Add(lovevalue);
        XmlD.Add(UCstate);
        XmlD.Save(Path);
    }

    public string PathReturn()
    {
        return Path;
    }

    public void LoadUCStateWindows()
    {
        //从XMl中读取了东西之后
        //将值赋值给静态的数据储存类中即可
        XDocument xdo = new XDocument();
        xdo = XDocument.Load(Path);
        XElement root = xdo.Element("UnityChan");
            XElement health = root.Element("healthvalue");
            XElement love = root.Element("lovevalue");
            XElement hungry = root.Element("hungryvalue");
        UCState.HealthValue = (float)Convert.ToDouble(health.Value);
        UCState.LoveValue = (float)Convert.ToDouble(love.Value);
        UCState.HungryValue = (float)Convert.ToDouble(hungry.Value);

        //print(health.Value);
    }

    public void LoadUCStateAndroid()
    {
        StartCoroutine(LoadDataAndroid());
    }

    public IEnumerator LoadDataAndroid()
    {
        float health, love, hungry;
        WWW www = new WWW(Path);
        print(www.text);
        while (!www.isDone)
        {
            yield return www;
        }
        XDocument xdoc = XDocument.Load(new System.IO.MemoryStream(www.bytes));

        print(xdoc);
        XElement root = xdoc.Element("UnityChan");
        XElement HealthNode = root.Element("healthvalue");
        XElement HungryNode = root.Element("hungryvalue");
        XElement LoveNode = root.Element("lovevalue");

        //XmlNode root = xdo.SelectSingleNode("UnityChan");
        //XmlNode HealthNode = root.SelectSingleNode("healthvalue");
        //XmlNode HungryNode = root.SelectSingleNode("hungryvalue");
        //XmlNode LoveNode = root.SelectSingleNode("lovevalue");

        health = (float)Convert.ToDouble(HealthNode.Value);
        //print(HealthNode.Value);
        love = (float)Convert.ToDouble(LoveNode.Value);
        //print(LoveNode.Value);
        hungry = (float)Convert.ToDouble(HungryNode.Value);
        //print(HungryNode.Value);
    }

    IEnumerator Load()
    {
        WWW www = new WWW(Path);
        yield return www;
    }
}

