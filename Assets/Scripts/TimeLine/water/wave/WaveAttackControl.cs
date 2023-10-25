using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttackControl : MonoBehaviour
{
    public GameObject self;
    private PlayerControllerBow pcb;
    private PlayerController99 pc99;
    [Header("是否黑幕效果")]
    public bool isEffectWave = false;
    [Header("是否心电效果")]
    public bool isHeartWave = false;
    [Header("声音的顺序")]
    public int soundCount = 0;
    // 1=papersound 2=bottlesound 3=wavesound
    private bool isAttacked = false;
    
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            self.GetComponent<WaveControl>().WaveAttack();

            player.TryGetComponent<PlayerControllerBow>(out pcb);
            if (pcb && pcb.enabled && !isAttacked)
            {
                isAttacked = true;
                AudioManager.PlayWaveObjSound(soundCount);
                Debug.Log("WaveAttackControl.cs get PlayerControllerbow.cs...");
                if (isEffectWave)
                {
                    pcb.WaveHit(true);
                }
                else
                {
                    pcb.WaveHit();
                }
            }
            else
            {
                player.TryGetComponent<PlayerController99>(out pc99);
                if (pc99 && !isAttacked)
                {
                    isAttacked = true;
                    AudioManager.PlayWaveObjSound(soundCount);
                    Debug.Log("WaveAttackControl.cs get PlayerController99.cs...");
                    if (player.GetComponent<PlayerController99>().isOpenUmbrella == false && player.GetComponent<PlayerController99>().isDead == false)
                    {
                        player.GetComponent<PlayerController99>().getHit();
                    }
                    else if (player.GetComponent<PlayerController99>().isOpenUmbrella == true && player.GetComponent<PlayerController99>().isDead == false)
                    {
                        player.GetComponent<PlayerController99>().BlockWave(isEffectWave, isHeartWave);
                    }
                }
                else
                {
                    Debug.Log("WaveAttackControl.cs get nothing...");
                }
            }
        }
    }
}
