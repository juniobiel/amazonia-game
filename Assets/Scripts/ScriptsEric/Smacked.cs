using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smacked : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);

                //GetComponent<Animation>().Play(); ou
                //gameObject.GetComponent<Animation>().Play();

                Destroy(transform.parent.gameObject, 2.0f);
            }
        }
    }

}
