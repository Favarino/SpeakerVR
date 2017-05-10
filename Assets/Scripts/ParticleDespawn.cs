using UnityEngine;
using System.Collections;

public class ParticleDespawn : MonoBehaviour {
    private ParticleSystem pS;
    private float time;

    // Use this for initialization
    void Start()
    {
        pS = GetComponent<ParticleSystem>();
        time = pS.startLifetime;
    }
    // Update is called once per frame
    void Update () {
        time -= Time.deltaTime;
        if (time < 0)
        {
            Destroy(gameObject);
        }
	}
}
