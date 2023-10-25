using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ش˽ű�����ѡ������������ڽ�ɫ������ԭ����ʼλ��
public class RefreshItem : MonoBehaviour
{
    public bool isRefreshAfterDeath;
    public ItemBase ib;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<ItemBase>(out ib);
    }

    void OnEnable()
    {
        HealthManager healthManager = FindObjectOfType<HealthManager>();
        if (healthManager != null)
        {
            healthManager.RegisterRefreshItem(this);
        }
    }
    void OnDisable()
    {
        HealthManager healthManager = FindObjectOfType<HealthManager>();
        if (healthManager != null)
        {
            healthManager.UnregisterRefreshItem(this);
        }
    }
}
