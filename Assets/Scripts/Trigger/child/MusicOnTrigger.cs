using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnTrigger : MonoBehaviour
{
    public AudioClip musicClip; // 音乐剪辑
    public AudioSource musicSource; // 音乐播放器

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 如果玩家触发了碰撞器
        {
            Debug.Log("播放音乐");
            musicSource.clip = musicClip; // 设置音乐剪辑
            musicSource.loop = true; // 设置为循环播放
            musicSource.Play(); // 开始播放音乐
        }
    }
}
