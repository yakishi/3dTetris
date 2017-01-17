#pragma warning disable 0649
using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

    [SerializeField]
    Gizmo gizmo;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hori = Input.GetAxis("Horizontal") * Time.fixedDeltaTime;
        float vert = Input.GetAxis("Vertical") * Time.fixedDeltaTime;

        gizmo.GetComponent<Rigidbody>().AddTorque(new Vector3(0, hori, 0) * 100f);

        Vector3 left = Vector3.left;//transform.TransformVector(Vector3.left);
        Vector3 horizontalLeft = new Vector3(left.x, 0f, left.z).normalized;
        gizmo.GetComponent<Rigidbody>().AddTorque(Vector3.Cross(left, horizontalLeft) * 4.0f);

        if (isXAxisCorrection())    //"Vertical" → X軸回転
        {
            Vector3 forward = Vector3.forward;//transform.TransformVector(Vector3.forward);
            Vector3 horizontalForward = new Vector3(0f, 0f, forward.z).normalized;
            gizmo.GetComponent<Rigidbody>().AddTorque(Vector3.Cross(forward, horizontalForward) * 2.0f);

            if (isXAxisCorrection(false, "front"))
            {
                //Debug.Log("Front");
                gizmo.GetComponent<Rigidbody>().AddTorque(new Vector3(vert, 0, 0) * 100f);
            }
            else if (isXAxisCorrection(false, "back"))
            {
                //Debug.Log("Back");
                gizmo.GetComponent<Rigidbody>().AddTorque(new Vector3(-vert, 0, 0) * 100f);
            }

        }
        else if (isZAxisCorrection()) //"Vertical" → Z軸回転
        {
            //Debug.Log("Z軸補正");
            Vector3 forward = Vector3.forward;//transform.TransformVector(Vector3.forward);
            Vector3 horizontalForward = new Vector3(forward.x, 0f, 0f).normalized;
            gizmo.GetComponent<Rigidbody>().AddTorque(Vector3.Cross(forward, horizontalForward) * 2.0f);

            if (isZAxisCorrection(false, "left"))
            {
                //Debug.Log("Left");
                gizmo.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, -vert) * 100f);
            }
            else if (isZAxisCorrection(false, "right"))
            {
                //Debug.Log("Right");
                gizmo.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, vert) * 100f);
            }
        }

        ClampRotate();
    }

    public bool isXAxisCorrection(bool both = true,string side = "front")
    {
        if (both == true)
        {
            return ((gizmo.transform.rotation.y <= 0.25f && gizmo.transform.rotation.y >= 0.0f) ||
                                                    (gizmo.transform.rotation.y >= -0.25f && gizmo.transform.position.y <= 0.0f)) ||     //y軸が -45 <= y <= 45
                ((gizmo.transform.rotation.y >= 0.75f && gizmo.transform.rotation.y <= 1.0f) ||
                                                    (gizmo.transform.rotation.y >= -1.0f && gizmo.transform.rotation.y >= -0.75f));      //y軸が -180 <= y <= -135 または
                                                                                                                                         // 135 <= y <= 180
        }
        else if (both == false)
        {
            if (side == "front")
            {
                return ((gizmo.transform.rotation.y <= 0.25f && gizmo.transform.rotation.y >= 0.0f) ||
                                                        (gizmo.transform.rotation.y >= -0.25f && gizmo.transform.position.y <= 0.0f));       //y軸が -45 <= y <= 45
            }
            else if (side == "right")
            {
                return ((gizmo.transform.rotation.y >= 0.75f && gizmo.transform.rotation.y <= 1.0f) ||
                                                        (gizmo.transform.rotation.y >= -1.0f && gizmo.transform.rotation.y >= -0.75f));      //y軸が -180 <= y <= -135 または
                                                                                                                                             // 135 <= y <= 180
            }
        }

        return false;

    }

    public bool isZAxisCorrection(bool both = true, string side = "left")
    {

        if (both == true)
        {
            return (gizmo.transform.rotation.y >= 0.25f && gizmo.transform.rotation.y <= 0.75f) ||                                       //y軸が 45 <= y <= 135
                                    (gizmo.transform.rotation.y >= -0.75f && gizmo.transform.rotation.y <= -0.25f);                      //y軸が -135 <= y <= -45
        }
        else if (both == false)
        {
            if (side == "left")
            {
                return (gizmo.transform.rotation.y >= 0.25f && gizmo.transform.rotation.y <= 0.75f);                                         //y軸が 45 <= y <= 135
            }
            else if (side == "right")
            {
                return (gizmo.transform.rotation.y >= -0.75f && gizmo.transform.rotation.y <= -0.25f);                                       //y軸が -135 <= y <= -45
            }
        }

        return false;
    }

    private void ClampRotate()
    {
        Quaternion tmpRot = gizmo.transform.rotation;

        tmpRot.x = Mathf.Clamp(tmpRot.x, -0.1f, 0.1f);
        tmpRot.z = Mathf.Clamp(tmpRot.z, -0.1f, 0.1f);

        gizmo.transform.rotation = tmpRot;
    }
}
