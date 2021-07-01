using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this is behaviour of laser and is called everytime laser object is used

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.up * _speed *Time.deltaTime);
        if (transform.position.y >= 5.62f)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject); //specifies that the object attached to this script is destroyed
        }
    }
}




