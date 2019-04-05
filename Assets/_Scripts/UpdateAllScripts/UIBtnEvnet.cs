using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBtnEvnet : MonoBehaviour
{
    public GameObject musicPlayer;
    public GameObject DanceBtn;
    public GameObject UC;
    private Animator ani;
    private string IsDance = "IsDance";
    private string IsRelease = "IsRelease";
    public bool IsClick = false;
    int i = 0;
    void Start()
    {
        musicPlayer.SetActive(false);
        UC = GameObject.FindWithTag("Pet");
        ani = UC.GetComponent<Animator>();
        //DanceBtn.onClick.AddListener(() => btnHide());
    }

    public void btnHide()
    {
        StartCoroutine(Dance());
        IsClick = true;
    }

    IEnumerator Dance()
    {
        ani.SetBool(IsDance, true);
        musicPlayer.SetActive(true);
        yield return new WaitForSeconds(1f);
    }
}
