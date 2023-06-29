using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroMove : MonoBehaviour
{
    public float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0, z);
        transform.Translate(movement * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(new Vector3(0, 1, 0) * (speed / 2) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(new Vector3(0, -1, 0) * (speed / 2) * Time.deltaTime);
        }



    }
}
