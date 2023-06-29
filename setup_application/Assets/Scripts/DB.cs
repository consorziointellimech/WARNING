using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using System;
using System.Text.Json;
using System.Data.SqlClient;
using System.Data;

public class DB : MonoBehaviour
{
    public List<DBlogica> listaOperazioni = new List<DBlogica>();

    private int procNumber = 0; 

    public GameObject NewButton;
    public GameObject RepeatButton;
    public GameObject NewCadButton;
    public GameObject NewStepButton;
    public GameObject InputField;
    public GameObject noteField;
    public GameObject addCadButton;
    public GameObject saveStepButton;
    public GameObject SaveProcedureButton;
    public GameObject SetZeroButton;
    public GameObject SaveZeroButton;
    public GameObject AvantiButton;
    public GameObject IndietroButton;
    public GameObject ToggleExpert;
    public GameObject SaveProcedureDB;
    public TMP_InputField nomeFile;
    public TMP_InputField note;
    public TMP_Text testoOperazioneShow;
    public TMP_Text noteOperazione;
    public TMP_Text NomeProcedura;


    private List<GameObject> listaCad = new List<GameObject>();

    private GameObject myPrefab;
    private GameObject currentObj;
    private int cordinate_x = 0;
    private int cordinate_y = 0;
    private int cordinate_z = 0;
    // Start is called before the first frame update
    void Start() 
    {
    }
    

    // Update is called once per frame
    void Update()
    {
    }

    public void NewProcedure()
    {
        NewButton.SetActive(false);
        RepeatButton.SetActive(false);
        NewCadButton.SetActive(true);
        InputField.SetActive(true);
    }

    public void ImportCad() 
    {
        Console.WriteLine(nomeFile.text.ToString());
        if (nomeFile.text.ToString() != "")
        {
            myPrefab = (GameObject)Resources.Load("Prefabs/" + nomeFile.text.ToString(), typeof(GameObject));
            currentObj = Instantiate(myPrefab, new Vector3(cordinate_x, cordinate_y, cordinate_z), Quaternion.identity);
            currentObj.transform.Rotate(270f, 0, 0f);
            Instantiate((GameObject)Resources.Load("Prefabs/Zero", typeof(GameObject)), new Vector3(-1.035f, 1.701f, -0.606f), Quaternion.identity);
            


            foreach (Transform child in currentObj.transform)
            {
                child.gameObject.GetComponent<MeshRenderer>().material = Instantiate(Resources.Load("Material/White") as Material);
                child.gameObject.AddComponent<BoxCollider>();
                //child.gameObject.AddComponent<Target>();
            }

            NewCadButton.SetActive(false);
            InputField.SetActive(false);
            NewStepButton.SetActive(true);
            SetZeroButton.SetActive(true); 
        }
    }

    public void NewStep() 
    { 
        SetZeroButton.SetActive(false);
        SaveProcedureButton.SetActive(false);
        ToggleExpert.SetActive(false);
        NewStepButton.SetActive(false);
        noteField.SetActive(false);
        SetZeroButton.SetActive(false);
        if (GameObject.Find("Zero(Clone)"))
        {
            GameObject.Find("Zero(Clone)").SetActive(false);
        }
        GameObject.Find("Main Camera").GetComponent<Movimento>().enabled = true;
        GameObject.Find("DB connection").GetComponent<RayCast>().enabled = true;
    } 

    public void AddCadName(GameObject cad)
    {
        listaCad.Add(cad);
        cad.transform.GetComponent<MeshRenderer>().material.color = Color.green;
        addCadButton.SetActive(true);
        saveStepButton.SetActive(true);
        noteField.SetActive(true);
    }

    public void SalvaStep() 
    {
        DBlogica operazione = new DBlogica();
        foreach (GameObject ogg in listaCad)
        {
            operazione.Oggetti.Add(ogg);
            ogg.transform.GetComponent<MeshRenderer>().material.color = Color.blue;
            GameObject.Find("DB connection").gameObject.GetComponent<RayCast>().listaEsclusioniBlue.Add(ogg);
        }
        operazione.Info = note.text.ToString();
        operazione.Numero = procNumber;
        listaOperazioni.Add(operazione);
        //foreach (DBlogica op in listaOperazioni)
        //{
        //    Debug.Log(op.Numero);
        //}

        procNumber += 1;
        GameObject.Find("DB connection").gameObject.GetComponent<RayCast>().listaEsclusioniVerdi.Clear();
        note.text = "";
        listaCad = new List<GameObject>();

        addCadButton.SetActive(false);
        saveStepButton.SetActive(false);
        noteField.SetActive(false);
        NewStepButton.SetActive(true);
        SaveProcedureButton.SetActive(true);
        ToggleExpert.SetActive(true);
    }

    public void AggiungiCad()
    {
        ToggleExpert.SetActive(false);
        SaveProcedureButton.SetActive(false);
        addCadButton.SetActive(false);
        saveStepButton.SetActive(false);
        noteField.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<Movimento>().enabled = true;
        GameObject.Find("DB connection").GetComponent<RayCast>().enabled = true;
    }

    public void SaveProcedure()
    {
        NewStepButton.SetActive(false);
        SaveProcedureButton.SetActive(false);
        ToggleExpert.SetActive(false);
        NewButton.SetActive(true);
        RepeatButton.SetActive(true);
        SaveProcedureDB.SetActive(true);
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.GetComponent<MeshRenderer>())
            {
                go.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        string json = "";
        foreach (var op in listaOperazioni)
        {
            json += JsonUtility.ToJson(op);
        }
        Debug.Log(json);

        GameObject.Find("DB connection").gameObject.GetComponent<RayCast>().listaEsclusioniVerdi.Clear();
        GameObject.Find("DB connection").gameObject.GetComponent<RayCast>().listaEsclusioniBlue.Clear();
        currentObj.SetActive(false);

    }
     
    public void SetZero()
    {
        SaveZeroButton.SetActive(true);
        SetZeroButton.SetActive(false);
        NewStepButton.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<FollowTarget>().target = GameObject.Find("Zero(Clone)").transform;
        GameObject.Find("Main Camera").GetComponent<FollowTarget>().enabled = true;
        GameObject.Find("Main Camera").transform.Rotate(15, 0, 0);
        GameObject.Find("Zero(Clone)").GetComponent<ZeroMove>().enabled = true;
    }

    public void SaveZero()
    {
        SaveZeroButton.SetActive(false);
        SetZeroButton.SetActive(true);
        NewStepButton.SetActive(true);
        GameObject.Find("Main Camera").GetComponent<FollowTarget>().enabled = false;
        GameObject.Find("Zero(Clone)").GetComponent<ZeroMove>().enabled = false;
    }

    public void RiapriProcedure()
    {
        SaveProcedureDB.SetActive(false);
        NewButton.SetActive(false);
        RepeatButton.SetActive(false);
        IndietroButton.SetActive(true);
        AvantiButton.SetActive(true);
        testoOperazioneShow.text = "0";

        GameObject.Find("Main Camera").GetComponent<Movimento>().enabled = true;
        currentObj.SetActive(true);
        foreach (GameObject ope in listaOperazioni[0].Oggetti)
        {
            ope.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        noteOperazione.text = "Description:\n" + listaOperazioni[0].Info;
        NomeProcedura.text = "ECB900-01548";

        Debug.Log(listaOperazioni.Count);
    }

    public void AvantiShow()
    {
        int operazione = int.Parse(testoOperazioneShow.text.ToString());
        operazione += 1;

        if (listaOperazioni.Count > operazione)
        {
            testoOperazioneShow.text = operazione.ToString();
            foreach (GameObject ope in listaOperazioni[operazione].Oggetti)
            {
                ope.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            foreach (GameObject ope in listaOperazioni[operazione-1].Oggetti)
            {
                ope.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            noteOperazione.text = "Description:\n"+listaOperazioni[operazione].Info;
        }
        else
        {
            NewButton.SetActive(true);
            RepeatButton.SetActive(true);
            SaveProcedureDB.SetActive(true);
            IndietroButton.SetActive(false);
            AvantiButton.SetActive(false);
            GameObject.Find("Main Camera").GetComponent<Movimento>().enabled = false;
            currentObj.SetActive(false);
            noteOperazione.text = "";
            NomeProcedura.text = "";
            testoOperazioneShow.text = "";
        }
    }

    public void IndietroShow()
    {
        int operazione = int.Parse(testoOperazioneShow.text.ToString());
        if (operazione != 0)
        {
            foreach (GameObject ope in listaOperazioni[operazione].Oggetti)
            {
                ope.GetComponent<MeshRenderer>().material.color = Color.white;
            }
            operazione -= 1;
            testoOperazioneShow.text = operazione.ToString();
            
            foreach (GameObject ope in listaOperazioni[operazione].Oggetti)
            {
                ope.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            
            noteOperazione.text = "Description:\n" + listaOperazioni[operazione].Info;
        }
    }
    private bool SalvaProceduraDB()
    {
        string connectionString = "Data Source=192.168.3.215,49779;User ID=UnityUsr;Password=VrUnity;Database=UnityTesting";
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();


        command.Connection = conn;
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = "INSERT INTO Procedures ( Name,CadName,Steps,Expert,Date) VALUES(@NAME,@CADNAME,@STEPS,@EXPERT,@DATE)";
        command.Parameters.Clear();
        command.Parameters.Add("@NAME", SqlDbType.NVarChar).Value = "test1";
        command.Parameters.Add("@CADNAME", SqlDbType.NVarChar).Value = currentObj.name.ToString();
        command.Parameters.Add("@STEPS", SqlDbType.Int).Value = listaOperazioni.Count;
        command.Parameters.Add("@EXPERT", SqlDbType.Bit).Value = ToggleExpert.GetComponent<Toggle>().isOn;
        command.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DateTime.Now;
        conn.Open();
        int n = command.ExecuteNonQuery();
        conn.Close();
        if (n == 1)
        {
            Debug.Log("true");
            return true;
        }
        else
        {
            return false;
            Debug.Log("false");
        }
    }

    private bool salvaStepDB(DBlogica operazione)
    {
        string listaCad = "";
        foreach (var cad in operazione.Oggetti)
        {
            listaCad += cad.name + ";;";
        }

        string connectionString = "Data Source=192.168.3.215,49779;User ID=UnityUsr;Password=VrUnity;Database=UnityTesting";
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();


        command.Connection = conn;
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = "INSERT INTO Steps ( ProcedureID,StepNumber,Cads,Color,Note) VALUES(@PROCEDUREID,@STEPNUMBER,@CADS,@COLOR,@NOTE)";
        command.Parameters.Clear();
        command.Parameters.Add("@PROCEDUREID", SqlDbType.Int).Value = "2";
        command.Parameters.Add("@STEPNUMBER", SqlDbType.Int).Value = operazione.Numero;
        command.Parameters.Add("@CADS", SqlDbType.NVarChar).Value = listaCad;
        command.Parameters.Add("@COLOR", SqlDbType.NChar).Value = operazione.Color;
        command.Parameters.Add("@NOTE", SqlDbType.NVarChar).Value = operazione.Info;
        conn.Open();
        int n = command.ExecuteNonQuery();
        conn.Close();
        if (n == 1)
        {
            Debug.Log("true");
        }
        else
        {
            Debug.Log("false");
        }
        return true;
    }

    public void SaveProcDB()
    {
        if (SalvaProceduraDB())
        {
            foreach (var ope in listaOperazioni)
            {
                if (salvaStepDB(ope))
                {
                    Debug.Log("true");
                }
                else
                {
                    Debug.Log("false");
                }
            }
        }
        else
        {
            Debug.Log("false");
        }
    }
}
