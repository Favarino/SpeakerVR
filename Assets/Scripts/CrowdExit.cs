using UnityEngine;
using System.Collections;

public class CrowdExit : MonoBehaviour {

    private GameObject exit;
    private float speed = 5;
    public bool leave = false;
    // Use this for initialization
    void Start()
    {
        exit = GameObject.Find("Exit");
    }
    void Update()
    {
        if(leave)
        {
            Exit();
        }
    }

    void Exit()
    {
        transform.position=Vector3.MoveTowards(transform.position, exit.transform.position, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position,exit.transform.position)<.5f)
        {
            gameObject.SetActive(false);
        }
    }
}