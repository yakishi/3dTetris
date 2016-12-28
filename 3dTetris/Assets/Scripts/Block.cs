using UnityEngine;
using System.Collections;

namespace block
{
    public class Block : MonoBehaviour
    {

        [SerializeField]
        float velocity;         //入力時の速度
        [SerializeField]
        float fallVelocity;     //落下速度

        float time;
        float fallTime;

        Ray ray;
        LayerMask floor;
        bool hitFloor;
        // Use this for initialization
        void Start()
        {
            time = 0.0f;
            fallTime = 0.7f;

            ray = new Ray(transform.position,
                                    transform.position - transform.up);
            floor = LayerMask.GetMask("Floor");
            hitFloor = false;
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;

            if (hitFloor) return;

            Debug.DrawLine(transform.position,
                           transform.position - transform.up * 0.5f,
                           Color.red);
            FallBlock();
            MoveBlock();
        }

        private void MoveBlock()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))//(MyInput.direction().x > Vector3.zero.x)
            {
                transform.position = new Vector3(transform.position.x + velocity,
                                                 transform.position.y,
                                                 transform.position.z);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))//(MyInput.direction().x < Vector3.zero.x)
            {
                transform.position = new Vector3(transform.position.x - velocity,
                                                 transform.position.y,
                                                 transform.position.z);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))//(MyInput.direction(false).y > Vector3.zero.y)
            {
                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y,
                                                 transform.position.z + velocity);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))//(MyInput.direction(false).y < Vector3.zero.y)
            {
                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y,
                                                 transform.position.z - velocity);
            }
        }

        private void FallBlock()
        {
            if (time <= fallTime) return;
            transform.position = new Vector3(transform.position.x,
                                             transform.position.y - fallVelocity,
                                             transform.position.z);
            time = 0.0f;

            if (isHitFloor())
            {
                Debug.Log("hoge");
                hitFloor = true;

            }
        }

        private bool isHitFloor()
        {
            return Physics.Raycast(ray,0.5f,floor);
        }
    }
}
