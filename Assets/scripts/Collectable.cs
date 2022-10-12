using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{


    public GameObject explosion;
    GameObject instantiatedObj;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Manager>().itemCollected ++ ;
            collision.gameObject.GetComponent<Manager>().starvetimer += 2f ;
            instantiatedObj = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(instantiatedObj, 1f);
            Destroy(gameObject, 0f);
        }
    }
}
