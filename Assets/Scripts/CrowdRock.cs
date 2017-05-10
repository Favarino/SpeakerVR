using UnityEngine;
using System.Collections;

public class CrowdRock : MonoBehaviour
{
    float timerR=0;
    private bool r;
    private float rotateForce;
    // Use this for initialization
    void Start()
    {
        r = true;
    }

    public void Rock()
    {
        timerR += Time.deltaTime;
        if(timerR>.5)
        {
            r = !r;
            timerR = 0;
        }
        if(r)
        {
            transform.Rotate(0, 0, 1 * Time.deltaTime * 5);
        }
        else
        {
            transform.Rotate(0, 0, -1 * Time.deltaTime * 5);
        }
    }
}
