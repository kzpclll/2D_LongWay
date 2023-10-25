using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Transform> prefabBKG;
    public List<Transform> existBKG;

    public List<Transform> prefabCloud;
    public List<Transform> existCloud;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        createDestroyFloor();
        createDestroyCloud();
    }

    void createDestroyFloor()
    {
        Transform lastFloor = existBKG[existBKG.Count - 1];
        if (lastFloor.position.x < transform.position.x + 18)
        {
            Transform prefab = prefabBKG[Random.Range(0, prefabBKG.Count)];
            Transform newFloor = Instantiate(prefab, null);
            newFloor.position = lastFloor.position + new Vector3(18, 0, 0);
            existBKG.Add(newFloor);
        }



        //destroy
        Transform firstFloor = existBKG[0];
        if (firstFloor.position.x < transform.position.x - 18)
        {
            existBKG.RemoveAt(0);
            Destroy(firstFloor.gameObject);
        }
    }

    void createDestroyCloud()
    {
        Transform lastFloor = existCloud[existCloud.Count - 1];
        if (lastFloor.position.x < transform.position.x + 18)
        {
            Transform prefab = prefabCloud[Random.Range(0, prefabCloud.Count)];
            Transform newFloor = Instantiate(prefab, null);
            newFloor.position = lastFloor.position + new Vector3(18, 0, 0);
            existCloud.Add(newFloor);
        }



        //destroy
        Transform firstFloor = existCloud[0];
        if (firstFloor.position.x < transform.position.x - 18)
        {
            existCloud.RemoveAt(0);
            Destroy(firstFloor.gameObject);
        }
    }
}
