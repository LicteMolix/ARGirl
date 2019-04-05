/*
 * 作者：licte（陈杰）    版本：1.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ExitGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
