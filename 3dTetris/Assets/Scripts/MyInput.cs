using UnityEngine;
using System.Collections;

public class MyInput : MonoBehaviour {

	public static Vector3 direction(bool onlyHorizontal = true)
    {
        Vector3 ret = Vector3.zero;
        if (Input.GetAxis("Horizontal") >= 1.0f)
        {
            ret += Vector3.right;
        }
        else if(Input.GetAxis("Horizontal") <= -1.0f)
        {
            ret += Vector3.left;
        }

        if (onlyHorizontal)
        {
            return ret;
        }

        if(Input.GetAxis("Vertical") >= 1.0f)
        {
            ret += Vector3.up;
        }
        else if(Input.GetAxis("Vertical") <= -1.0f)
        {
            ret += Vector3.down;
        }

        ret.Normalize();
        return ret;
    }
}
