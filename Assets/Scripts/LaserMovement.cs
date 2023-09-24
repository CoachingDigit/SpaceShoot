using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public bool isPlayer;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
			transform.Translate(Vector2.up * Time.deltaTime * speed);
		}
        else
        {
			transform.Translate(Vector2.down * Time.deltaTime * speed);
		}

    }
}
