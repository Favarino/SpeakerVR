using UnityEngine;
using System.Collections;

public class CrowdAi : MonoBehaviour {
    CrowdHop hopRef;
    CrowdRock rockRef;
    private float switchTime =.2f;
    private float t;
    private int result;
    // Use this for initialization
    void Start () {
        hopRef = GetComponent<CrowdHop>();
        rockRef = GetComponent<CrowdRock>();
    }
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        if (t>=switchTime)
        {
            result = Random.Range(1, 3);
            t = 0;
        }
        switch (result)
        {
            case 1:
                {
                    hopRef.Hop();
                    break;
                }
            case 2:
                {
                    rockRef.Rock();
                    break;
                }
        }
    }
   
}
