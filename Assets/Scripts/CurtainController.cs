using UnityEngine;
using System.Collections;

public class CurtainController : MonoBehaviour
{
    public bool left;

    public void OpenCurtains()
    {
        if (left) { transform.Translate(-1 * Time.deltaTime, 0, 0); }
        else if (!left) { transform.Translate(1 * Time.deltaTime, 0, 0); }
    }
    public void CloseCurtains()
    {
        if (left) { transform.Translate(1 * Time.deltaTime, 0, 0); }
        else if (!left) { transform.Translate(-1 * Time.deltaTime, 0, 0); }
    }
}
