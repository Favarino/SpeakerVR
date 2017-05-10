using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemonAi : MonoBehaviour
{
    // TODO HITPOINTS? DIFFERENT MODELS? SOUND EFFECTS? PARTICLE EFFECTS?
    public float raiseTime;
    public GameObject target;
    public float speed;
    private GameObject eventmanager;
    private Rigidbody rb;
    public TrailRenderer trail;
    public float force = 30;
    public GameObject hitEffect;
    public GameObject dieEffect;
    private bool chase;
    private bool taunt = false;
    public bool taunter;
    private Collider col;
    bool visible;
    Vector3 tempTarget;
    //timers
    private float t;
    public float timer;
    public float size;
    public AudioClip[] tauntClips;
    public new AudioSource audio;
    public bool callChase = true;
    // Use this for initialization
    void Start()
    {
        eventmanager = GameObject.FindGameObjectWithTag("event");
        target = GameObject.FindGameObjectWithTag("target");
        rb = GetComponent<Rigidbody>();
        trail.enabled = false;
        tempTarget = Random.onUnitSphere * 2.5f + target.transform.position;
        col = GetComponent<Collider>();
        int r = Random.Range(1, 3);
        if (r == 1) { taunter = true; } 
        else { taunter = false; }
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        //controls how long the demon will raise before coming toward
        timer += Time.deltaTime;
        if (timer < raiseTime)
        {
            Raise();
        }
        else { Travel(); trail.enabled = true; }
        //if (taunt)
        //{
        //    GetComponent<Renderer>().material.color = Color.blue;
        //    Taunt();
        //    Debug.Log("taunting");
        //}
    }
    //make demon raise from person all spooky like
    public void Raise()
    {
        
        transform.Translate(0, 1 * Time.deltaTime, 0);
    }
    //travel straight toward the camera
    void Travel()
    {
        if (callChase)
        {
            chase = true;
            callChase = false;
        }
        if (taunter)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < 5) 
            {
                chase = false;
                taunt = true;
            }
        }
        //if (Vector3.Distance(transform.position, tempTarget) <= 2f)
        //{
        //    PlayTauntClip();
        //    Debug.Log("distance met");
        //}
        if (chase)
        {
            // Wobble();
            col.enabled = true;
            transform.LookAt(target.transform);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        }
        else if (taunt)
        {
            t += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, tempTarget, Time.deltaTime * speed);
            transform.Rotate(Vector3.up, .1f);
            float p = Random.Range(2, 4);
            if (t > p)
            {
                PlayTauntClip();
                int x = Random.Range(1, 3);
                if (x == 1)
                {
                    tempTarget = Random.onUnitSphere * 2.5f + target.transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, tempTarget, Time.deltaTime * speed);
                    t = 0;
                }
                else { taunt = false; chase = true; }
            }
        }
       
        if (Vector3.Distance(transform.position, target.transform.position) < .05f && chase)
        {
            MadeIt();
        }
    }
    void MadeIt()
    {
        eventmanager.GetComponent<EventManager>().PlayHitClip();
        eventmanager.GetComponent<ScoreManager>().ChangeScore(-1);
        gameObject.SetActive(false);
        trail.enabled = false;
        //make stutter or whatever getting hit will be
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "whip")
        {
            chase = false;
            Vector3 dir = col.contacts[0].point - transform.position;
            Instantiate(hitEffect, col.contacts[0].point, Quaternion.Euler(dir));
            dir = -dir.normalized;
            eventmanager.GetComponent<EventManager>().PlaySmackClip();
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.AddForce(dir * force,ForceMode.VelocityChange);
        }
        if (col.collider.tag == "wall")
        {
            Vector3 dir = col.contacts[0].point - transform.position;
            Instantiate(dieEffect, transform.position, Quaternion.Euler(dir));
            gameObject.SetActive(false);
            trail.enabled = false;
        }

    }
    void PlayTauntClip()
    {
        int random = Random.Range(0, tauntClips.Length);
        float ranPitch = Random.Range(1.75f, 1.95f);
        audio.pitch = ranPitch;
        audio.PlayOneShot(tauntClips[random], 1);
        Debug.Log("called");
    }
}
