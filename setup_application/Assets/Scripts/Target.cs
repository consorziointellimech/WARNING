using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Renderer ColorRender;
    // Start is called before the first frame update
    void Start()
    {
        ColorRender = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnMouseOver()
    //{
    //    //Debug.Log(ColorRender.material.color);
    //    ColorRender.material.color = Color.red;
    //}
    //private void OnMouseEnter()
    //{
        
    //}

    //private void OnMouseExit()
    //{
    //    ColorRender.material.color = Color.white;
    //}
}
