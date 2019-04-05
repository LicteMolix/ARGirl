using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Dance : MonoBehaviour {
    public Animator UCAni;
    public GameObject DanceMusic;
    private void Start()
    {
        UCAni = GetComponent<Animator>();
        //DanceMusic = GameObject.Find("DanceThing");
        DanceMusic.SetActive(false);
    }
    
    public void Click()
    {
        DanceMusic.SetActive(true);
        StartCoroutine(Dance());
    }

    IEnumerator Dance()
    {
        UCAni.SetBool("IsDance", true);
        yield return new WaitForSeconds(1f);
        UCAni.SetBool("IsDance", false);
        UCAni.SetBool("IsReleaseBack",true);//这个骑士没有必要写
    }
}
