using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayCast : MonoBehaviour
{
    public TMP_Text testo;
    private float m_Size;
    private RaycastHit Oggetto;
    public List<GameObject> listaEsclusioniVerdi;
    public List<GameObject> listaEsclusioniBlue;


    // Start is called before the first frame update
    void Start()
    {
    }


    
    // Update is called once per frame
    void Update()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.GetComponent<MeshRenderer>())
            {
                go.GetComponent<MeshRenderer>().material.color = Color.white;

                if (listaEsclusioniVerdi.Contains(go))
                {
                    go.GetComponent<MeshRenderer>().material.color = Color.green;
                }

                if (listaEsclusioniBlue.Contains(go))
                {
                    go.GetComponent<MeshRenderer>().material.color = Color.blue;
                }

            }
        }

        RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (hits.Length > 0)
        {
            m_Size = hits[0].transform.GetComponent<Collider>().bounds.size.x * hits[0].transform.GetComponent<Collider>().bounds.size.y * hits[0].transform.GetComponent<Collider>().bounds.size.z; ;
            foreach (RaycastHit hit in hits)
            {
                var addVolume = hit.transform.GetComponent<Collider>().bounds.size.x * hit.transform.GetComponent<Collider>().bounds.size.y * hit.transform.GetComponent<Collider>().bounds.size.z;
                if (addVolume <= m_Size)
                {
                    Oggetto = hit;
                    m_Size = addVolume;
                }
                //hit.transform.GetComponent<Renderer>().material.color = Color.white;
            }
            Oggetto.transform.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        if (Input.GetMouseButtonDown(0))
        {
            listaEsclusioniVerdi.Add(Oggetto.transform.gameObject);
            testo.text = Oggetto.transform.gameObject.name.ToString();
            DB obj = GameObject.Find("DB connection").GetComponent<DB>();
            obj.AddCadName(Oggetto.transform.gameObject);
            GameObject.Find("Main Camera").GetComponent<Movimento>().enabled = false;
            this.enabled = false;
        }
    }
}
