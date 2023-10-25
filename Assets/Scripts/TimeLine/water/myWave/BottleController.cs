using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    public GameObject self;
    private PlayerControllerBow pcb;
    private PlayerController99 pc99;
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            self.GetComponent<WaveControl>().WaveAttack();

            player.TryGetComponent<PlayerControllerBow>(out pcb);
            if (pcb && pcb.enabled)
            {
                Debug.Log("WaveAttackControl.cs get PlayerControllerbow.cs...");
                pcb.WaveHit();
            }
            else
            {
                player.TryGetComponent<PlayerController99>(out pc99);
                if (pc99)
                {
                    Debug.Log("WaveAttackControl.cs get PlayerController99.cs...");
                    if (player.GetComponent<PlayerController99>().isOpenUmbrella == false && player.GetComponent<PlayerController99>().isDead == false)
                    {
                        player.GetComponent<PlayerController99>().getHit();
                    }
                    else if (player.GetComponent<PlayerController99>().isOpenUmbrella == true && player.GetComponent<PlayerController99>().isDead == false)
                    {
                        player.GetComponent<PlayerController99>().BlockWave(false,false);
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
