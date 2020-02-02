using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEndControler : MonoBehaviour
{
    public GameObject[] Enemys;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if(Enemys.Length == 0)
        {
            SceneManager.LoadScene("Continua");
        }
    }
}
