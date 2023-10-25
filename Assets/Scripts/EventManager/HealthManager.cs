using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    List<RefreshItem> refreshObjs = new List<RefreshItem>();
    List<MoveTable> moveTables = new List<MoveTable>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void RegisterRefreshItem(RefreshItem item)
    {
        if (!refreshObjs.Contains(item))
        {
            refreshObjs.Add(item);
        }
    }

    public void UnregisterRefreshItem(RefreshItem item)
    {
        refreshObjs.Remove(item);
    }

    public void RegisterMoveTable(MoveTable table)
    {
        if (!moveTables.Contains(table))
        {
            moveTables.Add(table);
        }
    }

    public void UnregisterMoveTable(MoveTable table)
    {
        moveTables.Remove(table);
    }
    public void PlayerDead()
    {
        foreach (MoveTable table in moveTables)
        {
            if (table.isFresh)
            {
                table.ResetItem();
            }
        }

        foreach (RefreshItem item in refreshObjs)
        {
            if (item.isRefreshAfterDeath)
            {
                if (item.ib)
                {
                    item.ib.Recover2Awake();
                }
            }
        }
    }

}
