using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _Load_to_Game : MonoBehaviour
{

    AsyncOperation async;

    public Text loadPercent;
    public Slider loadProcess;
    [Space(10)]
    int currentPercent = 0;

    void Start()
    {
        loadPercent.text = currentPercent.ToString() + "%";
        StartCoroutine(LoadSceneAsyc());
    }


    void Update()
    {
        loadPercent.text = (async.progress * 100 + 1).ToString() + "%";
        loadProcess.value = async.progress  + 1;
    }

    IEnumerator LoadSceneAsyc()
    {
        yield return async = SceneManager.LoadSceneAsync("02UpdateMaingame 1");
    }
}
