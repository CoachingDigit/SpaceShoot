using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverLimit : MonoBehaviour
{
    private float boundary = 7.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= boundary || transform.position.y <= -boundary)
        {
            Destroy(gameObject);
        }
        
    }
}
