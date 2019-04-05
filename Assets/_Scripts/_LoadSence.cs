/*
 * 作者：licte（陈杰）    版本：1.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _LoadSence : MonoBehaviour {
    
    public void SceneChange()
    {
        SceneManager.LoadScene("02LoadingScenes");
    }
}
