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

        LayerMask floor;
        bool hitFloor;
        float rayDistance;
        // Use this for initialization
        void Start()
        {
            time = 0.0f;
            fallTime = 0.7f;

            floor = LayerMask.GetMask("Floor");
            hitFloor = false;
            rayDistance = 2.0f;
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;

            if (hitFloor) return;

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

        }

        void OnCollisionEnter(Collision coll)
        {
            if (hitFloor) return;
            Debug.Log(coll.gameObject.ToString());
            hitFloor = true;
        }
    }
}
