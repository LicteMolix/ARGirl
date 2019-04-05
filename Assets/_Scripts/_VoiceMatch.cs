/*
 * 作者：licte（陈杰）    版本：1.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用来进行声音匹配的东西
/// </summary>
public class _VoiceMatch : MonoBehaviour {

    public AudioClip[] unityMatchAudio;

    public AudioSource unityChanAudioS;

	// Use this for initialization
	void Start () {
		unityChanAudioS = GetComponent<AudioSource>();
        unityChanAudioS.playOnAwake = false;
        unityChanAudioS.volume = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void VoicePlay_MaiMeng()
    {
        unityChanAudioS.clip = unityMatchAudio[11];
        unityChanAudioS.Play();//这样才能解决音画不同步的问题(我的想法)
    }

    void VoicePlay_XuanFengTi()
    {
        unityChanAudioS.clip = unityMatchAudio[12];
        unityChanAudioS.Play();
    }

    void VoicePlay_ShaJiao()
    {
        unityChanAudioS.clip = unityMatchAudio[9];
        unityChanAudioS.Play();
    }

    void VoicePlay_LaughOut()
    {
        unityChanAudioS.clip = unityMatchAudio[7];
        unityChanAudioS.Play();
    }

    void VoicePlay_Scornfully()
    {
        unityChanAudioS.clip = unityMatchAudio[0];
        unityChanAudioS.Play();
    }

    void VoicePlay_Surprise()
    {
        unityChanAudioS.clip = unityMatchAudio[10];
        unityChanAudioS.Play();
    }

    void VoicePlay_Angry()
    {
        unityChanAudioS.clip = unityMatchAudio[0];
        unityChanAudioS.Play();
    }

    void VoicePlay_Sad()
    {
        unityChanAudioS.clip = unityMatchAudio[13];
        unityChanAudioS.Play();
    }

    void VoicePlay_Smile()
    {
        unityChanAudioS.clip = unityMatchAudio[9];
        unityChanAudioS.Play();
    }
}
