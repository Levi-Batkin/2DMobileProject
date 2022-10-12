using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform topLeft, bottomRight;
    public GameObject collectable;
    GameObject spawnitem;
    float timer = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            SpawnObject();
            timer = 1.5f;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        
    }
    void SpawnObject()
    {
        var position = new Vector2(Random.Range(topLeft.position.x, bottomRight.position.x), Random.Range(topLeft.position.y, bottomRight.position.y));
        
        spawnitem = Instantiate(collectable, position, Quaternion.identity);
        Destroy(spawnitem, 5.0f);
    }
}
