using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBlogica
{
    private int numero;
    private string info;
    private List<GameObject> oggetti;
    private string color;

    public int Numero { get => numero; set => numero = value; }
    public string Info { get => info; set => info = value; }
    public string Color { get => color; set => color = value; }
    public List<GameObject> Oggetti { get => oggetti; set => oggetti = value; }

    public DBlogica()
    {
        numero = 0;
        info = "";
        color = "white";
        oggetti = new List<GameObject>();
    }
}
