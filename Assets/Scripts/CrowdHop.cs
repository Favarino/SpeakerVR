using UnityEngine;
using System.Collections;

public class CrowdHop : MonoBehaviour
{
    float timer;
    private bool u;

    public void Hop()
    {
        timer += Time.deltaTime;
        if (timer > .1)
        {
            u = !u;
            timer = 0;
        }
        if (u)
        {
            transform.Translate(0,1*Time.deltaTime*.5f,0);
        }
        else
        {
            transform.Translate(0, -1*Time.deltaTime * .5f, 0);
        }
    }
}
