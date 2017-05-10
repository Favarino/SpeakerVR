using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {
    public List<GameObject> Crowd = new List<GameObject>();
    public int score = 200;
    int prevScore;
    GameObject member;
    public GameObject spotLight;
    // Use this for initialization
    void Start () {
        Crowd.AddRange(GameObject.FindGameObjectsWithTag("Crowd"));
        prevScore = score;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int result;
            result = Random.Range(1, Crowd.Count);
            Crowd[result].GetComponent<CrowdExit>().leave = true;
        }
	if (score < prevScore)
        {
            int result;
            result = Random.Range(1, Crowd.Count);
            Crowd[result].GetComponent<CrowdExit>().leave = true;
            spotLight.GetComponent<SpotLightController>().target = Crowd[result];
            Crowd.RemoveAt(result);
            prevScore = score;
        }
	}
    public void ChangeScore(int amount)
    {
        score += amount;
    }
}
