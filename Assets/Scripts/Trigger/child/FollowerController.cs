using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    public static FollowerController instance;
    public float intervalTime = 0.5f;
    public enum operate
    {
        jump
    }

    public List<Follower> followerList;
    public float tendDistance;
    
    private void Awake()
    {
        if (FollowerController.instance == null)
        {
            FollowerController.instance = this;
        }
        else
        {
            if(FollowerController.instance!= this)
            {
                Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        followerList = new List<Follower>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 添加成功返回编号 添加失败返回-1
    public int AddFollower(Follower f)
    {
        if (!followerList.Contains(f))
        {
            followerList.Add(f);
            return followerList.Count - 1;
        }
        return -1;
    }

    //PlayerKid调用 输送操作给所有Follower
    public void SendOperate(operate o)
    {
        switch (o)
        {
            case operate.jump:
                StartCoroutine("WaitJump");
                break;
            default: break;
        }
    }

    IEnumerator WaitJump()
    {
        foreach (Follower f in followerList)
        {
            yield return new WaitForSeconds(intervalTime);
            f.Jump();
        }
    }
}
