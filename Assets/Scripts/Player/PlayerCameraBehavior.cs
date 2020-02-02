using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerCameraBehavior : MonoBehaviour
{
    [SerializeField]private Transform playerPosition;
    [SerializeField] private Transform cameraPosition;
    void Start()
    {
        cameraPosition = GetComponent<Transform>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        cameraPosition.position = new UnityEngine.Vector3(playerPosition.position.x, cameraPosition.position.y,cameraPosition.position.z);
    }
}
