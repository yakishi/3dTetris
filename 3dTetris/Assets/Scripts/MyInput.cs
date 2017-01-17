#pragma warning disable 0649
using UnityEngine;
using System.Collections;

public class MyInput : MonoBehaviour {
    float hori, vert;

    void FixedUpdate()
    {
        hori = Input.GetAxis("Horizontal") * Time.fixedDeltaTime;
        vert = Input.GetAxis("Vertical") * Time.fixedDeltaTime;
    }

    public float getHori() { return hori; }
    public float getVert() { return vert; }


    public static Vector3 direction(bool onlyHorizontal = true)
    {
        Vector3 ret = Vector3.zero;
        if (Input.GetAxis("Horizontal") >= 0.5f)
        {
            ret += Vector3.right;
        }
        else if(Input.GetAxis("Horizontal") <= -0.5f)
        {
            ret += Vector3.left;
        }

        if (onlyHorizontal)
        {
            return ret;
        }

        if(Input.GetAxis("Vertical") >= 0.5f)
        {
            ret += Vector3.up;
        }
        else if(Input.GetAxis("Vertical") <= -0.5f)
        {
            ret += Vector3.down;
        }

        ret.Normalize();
        return ret;
    }
}
