using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {
    public float curtainTime;
    public GameObject leftCur;
    public GameObject rightCur;
    public List<GameObject> Crowd = new List<GameObject>();
    public float spawnInterval;
    public int spawnThreshold;
    private bool curtainsOpen;
    public GameObject demon; 
    public AudioSource audioMain;
    public AudioSource audioSub;
    public AudioSource audioSmack;
    public AudioClip applause;
    public AudioClip speech;
    public AudioSource speaker1;
    public AudioSource speaker2;
    private bool app = true;
    private bool speek = false;
    private bool gameInSession;
    public AudioClip[] hitClips = new AudioClip[3];
    public AudioClip[] smackClips = new AudioClip[3];
    private float speechLength;
    ScoreManager scoreRef;
    bool control = true;
    bool closeCurtains = false;
    //timers
    private float timer;
    private float t;
    private float p;
    private float n;
    // Use this for initialization
    void Start ()
    {
        Crowd.AddRange(GameObject.FindGameObjectsWithTag("Crowd"));
        curtainsOpen = false;
        speechLength = speech.length;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if ( timer < curtainTime && !curtainsOpen)
        {
            OpenCurtains();
            ApplauseCall();
        }
        if (timer > curtainTime&&control)
        {
            curtainsOpen = true;
            speek = true;
            gameInSession = true;
            control = false;
        }
        if (curtainsOpen== true)
        {
            //stop everything and close the curtains if the speech is done
            t += Time.deltaTime;
            if (t >= speechLength)
            {
                audioMain.Stop();
                gameInSession = false;
                closeCurtains = true;
            }
        }
        if(closeCurtains)
        {
            //wait to make sure no more demons are coming
            n += Time.deltaTime;
            if (n > 3)
            {
                //close curtains for appropriate amount of time
                p += Time.deltaTime;
                if (p < curtainTime)
                {
                    CloseCurtains();
                }
            }
        }
        if (curtainsOpen == true && timer > spawnInterval)
        {
            if (gameInSession)
            {
                SpeechCall();
                SelectCrowdMember();
                timer = 0;
            }
        }
	}
    void OpenCurtains()
    {
        leftCur.GetComponent<CurtainController>().OpenCurtains();
        rightCur.GetComponent<CurtainController>().OpenCurtains();
    }
    void CloseCurtains()
    {
        leftCur.GetComponent<CurtainController>().CloseCurtains();
        rightCur.GetComponent<CurtainController>().CloseCurtains();
    }
    void SelectCrowdMember()
    {
        for (int i = 0; i < Crowd.Count; i++)
        {
            int result;
            result = Random.Range(0, 400);
            if (result > spawnThreshold)
            {
                if (!Crowd[i].GetComponent<CrowdExit>().leave)
                {
                    GameObject obj = ObjectPoolManger.current.GetPooledObject();

                    if (obj == null) return;
                    obj.GetComponent<Rigidbody>().isKinematic = true;
                    obj.GetComponent<Rigidbody>().useGravity = true;
                    obj.GetComponent<DemonAi>().timer = 0;
                    obj.GetComponent<DemonAi>().callChase = true;
                    obj.transform.position = Crowd[i].transform.position;
                    obj.transform.rotation = Quaternion.Euler(0, 180, 0);
                    obj.SetActive(true);
                }
                else { Crowd.RemoveAt(i); SelectCrowdMember(); }
            }
        }
    }
    void ApplauseCall()
    {
        if (app == true)
        {
            audioMain.clip = applause;
            audioMain.Play();
            app = false;
        }
    }
    void SpeechCall()
    {
        if (speek == true)
        {
            audioMain.Stop();
            audioMain.clip = speech;
            audioMain.Play();
            speaker1.PlayDelayed(.1f);
            speaker2.PlayDelayed(.1f);
            speek = false;
        }
    }
    public void PlayHitClip()
    {
        int random = Random.Range(0, hitClips.Length);
        //float ranPitch = Random.Range(1.75f, 1.95f);
        //audioSub.pitch = ranPitch;
        audioSub.PlayOneShot(hitClips[random],1);
    }
    public void PlaySmackClip()
    {
        int random = Random.Range(0, smackClips.Length);
        float ranPitch = Random.Range(.95f, 1.05f);
        audioSmack.pitch = ranPitch;
        audioSmack.PlayOneShot(smackClips[random], 1);
    }

}
