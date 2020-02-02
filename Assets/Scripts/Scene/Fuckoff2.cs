using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Fuckoff2 : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    private void Start()
    {
        cameraPosition = GetComponent<Transform>();
        StartCoroutine(TimeToEnd());
    }
    IEnumerator TimeToEnd()
    {
        yield return new WaitForSeconds(5f);
        cameraPosition.position = new Vector3(15, 0, -10);
        yield return new WaitForSeconds(5f);
        cameraPosition.position = new Vector3(30, 0, -10);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Start");
    }
}
