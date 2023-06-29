using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 turn;
    public float sensitivity = .5f;
    [SerializeField] private float speed = 1;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            turn.x += Input.GetAxis("Mouse X") * sensitivity;
            turn.y += Input.GetAxis("Mouse Y") * sensitivity;
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        }

        //Vector3 inputDir = new Vector3(0, 0, 0);

        //if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
        //if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        //if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        //if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

        //Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        //float moveSpeed = 50f;
        //transform.position += moveDir * moveSpeed * Time.deltaTime;


        //float rotateDir = 0f;
        if (Input.GetKey(KeyCode.E)) transform.localPosition +=(new Vector3(0, 1, 0) * (speed) * Time.deltaTime);
        //rotateDir = +1f;

        if (Input.GetKey(KeyCode.Q)) transform.localPosition+=(new Vector3(0, -1, 0) * (speed) * Time.deltaTime);
        //rotateDir = -1f;

        transform.localPosition += (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime);

        //float rotateSpeed = 300f;
        //transform.eulerAngles += new Vector3(0,rotateDir * rotateSpeed * Time.deltaTime, 0);



    }
}
