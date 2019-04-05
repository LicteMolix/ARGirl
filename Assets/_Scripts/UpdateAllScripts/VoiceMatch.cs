using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class VoiceMatch : MonoBehaviour
{
    public AudioClip[] unityMatchAudio;

    public AudioSource unityChanAudioS;
    
    void Start()
    {
        unityChanAudioS = GetComponent<AudioSource>();
        unityChanAudioS.playOnAwake = false;
        unityChanAudioS.volume = 1.0f;
    }

    void VoicePlay_Angry()
    {
        unityChanAudioS.clip = unityMatchAudio[0];
        unityChanAudioS.Play();
    }

    void VoicePlay_BeyBey()
    {
        unityChanAudioS.clip = unityMatchAudio[1];
        unityChanAudioS.Play();
    }

    void VoicePlay_BeyBeyHA()
    {
        unityChanAudioS.clip = unityMatchAudio[2];
        unityChanAudioS.Play();
    }

    void VoicePlay_Deyi()
    {
        unityChanAudioS.clip = unityMatchAudio[3];
        unityChanAudioS.Play();
    }

    void VoicePlay_Encourage()
    {
        unityChanAudioS.clip = unityMatchAudio[4];
        unityChanAudioS.Play();
    }

    void VoicePlay_MaiMeng()
    {
        unityChanAudioS.clip = unityMatchAudio[8];
        unityChanAudioS.Play();
    }

    void VoicePlay_XuanFengTi()
    {
        unityChanAudioS.clip = unityMatchAudio[13];
        unityChanAudioS.Play();
    }

    void VoicePlay_ShaJiao()
    {
        unityChanAudioS.clip = unityMatchAudio[11];
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
        unityChanAudioS.clip = unityMatchAudio[12];
        unityChanAudioS.Play();
    }

    void VoicePlay_Sad()
    {
        unityChanAudioS.clip = unityMatchAudio[12];
        unityChanAudioS.Play();
    }

    void VoicePlay_Smile()
    {
        unityChanAudioS.clip = unityMatchAudio[11];
        unityChanAudioS.Play();
    }

    void VoicePlay_HaQian()
    {
        unityChanAudioS.clip = unityMatchAudio[9];//换成打哈欠
        unityChanAudioS.Play();
    }

    void VoicePlay_Hello()
    {
        unityChanAudioS.clip = unityMatchAudio[6];
        unityChanAudioS.Play();
    }


    void ChangLoveVal(float lerpvalue)
    {
        UCState.ChangeUCLoveValue(lerpvalue);
    }

    public void RandomSpeak()
    {
        unityChanAudioS.clip = unityMatchAudio[Random.Range(14,19)];
        unityChanAudioS.Play();
    }
}
