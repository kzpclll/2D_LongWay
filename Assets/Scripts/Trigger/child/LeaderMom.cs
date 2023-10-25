using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderMom : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Jump2()
    {
        animator.Play("jump2");
    }

    public void Move2()
    {
        animator.Play("move2");
    }
    public void Idle()
    {
        animator.Play("mother");
    }
}
