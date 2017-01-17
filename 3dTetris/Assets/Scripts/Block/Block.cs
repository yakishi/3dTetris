#pragma warning disable 0649
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

        private GameObject mainCamera;

        float time;
        float fallTime;

        private bool hitFloor;          //地面やブロックにヒットしたフラグ
        public bool stopBlock;         //地面に触れた後数カウント後操作を不可能にするフラグ

        private Renderer firstTile;
        private Renderer lastTile;


        
        // Use this for initialization
        void Start()
        {
            mainCamera =GameObject.Find("Main Camera");

            time = 0.0f;
            fallTime = 0.7f;
            hitFloor = false;
            stopBlock = false;

            firstTile = GameObject.FindGameObjectWithTag("First Tile").GetComponent<Renderer>();
            lastTile = GameObject.FindGameObjectWithTag("Last Tile").GetComponent<Renderer>();

        }

        // Update is called once per frame
        void Update()
        {

            time += Time.deltaTime;

            if (hitFloor)
                StopMove();

            FallBlock();
            if (mainCamera.GetComponent<MainCamera>().isXAxisCorrection()) XAxisMoveBlock();
            else if (mainCamera.GetComponent<MainCamera>().isZAxisCorrection()) ZAxisMoveBlock();
            

        }

        private void XAxisMoveBlock()
        {
            if (stopBlock) return;

            float velocityDirection = 1.0f;

            if (mainCamera.GetComponent<MainCamera>().isXAxisCorrection(false, "front")) velocity *= velocityDirection;
            else if (mainCamera.GetComponent<MainCamera>().isXAxisCorrection(false, "back")) velocity *= -velocityDirection;

            if (Input.GetKeyDown(KeyCode.RightArrow))//(MyInput.direction().x > Vector3.zero.x)
            {
                transform.position = new Vector3(transform.position.x + velocity,
                                                 transform.position.y,
                                                 transform.position.z);
                if (hitFloor) time = 0.0f;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))//(MyInput.direction().x < Vector3.zero.x)
            {
                transform.position = new Vector3(transform.position.x - velocity,
                                                 transform.position.y,
                                                 transform.position.z);
                if (hitFloor) time = 0.0f;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))//(MyInput.direction(false).y > Vector3.zero.y)
            {
                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y,
                                                 transform.position.z + velocity);
                if (hitFloor) time = 0.0f;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))//(MyInput.direction(false).y < Vector3.zero.y)
            {
                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y,
                                                 transform.position.z - velocity);
                if (hitFloor) time = 0.0f;
            }

            Clamp();
        }

        private void ZAxisMoveBlock()
        {
            if (stopBlock) return;

            float velocityDirection = 1.0f;

            if (mainCamera.GetComponent<MainCamera>().isZAxisCorrection(false,"left")) velocity *= velocityDirection;
            else if (mainCamera.GetComponent<MainCamera>().isZAxisCorrection(false, "right")) velocity *= -velocityDirection;

            if (Input.GetKeyDown(KeyCode.RightArrow))//(MyInput.direction().x > Vector3.zero.x)
            {
                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y,
                                                 transform.position.z + velocity);
                if (hitFloor) time = 0.0f;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))//(MyInput.direction().x < Vector3.zero.x)
            {
                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y,
                                                 transform.position.z - velocity);
                if (hitFloor) time = 0.0f;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))//(MyInput.direction(false).y > Vector3.zero.y)
            {
                transform.position = new Vector3(transform.position.x + velocity,
                                                 transform.position.y,
                                                 transform.position.z);
                if (hitFloor) time = 0.0f;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))//(MyInput.direction(false).y < Vector3.zero.y)
            {
                transform.position = new Vector3(transform.position.x - velocity,
                                                 transform.position.y,
                                                 transform.position.z);
                if (hitFloor) time = 0.0f;
            }

            Clamp();
        }


        private void FallBlock()
        {
            if (hitFloor) return;
            if (time <= fallTime) return;
            transform.position = new Vector3(transform.position.x,
                                             transform.position.y - fallVelocity,
                                             transform.position.z);

            time = 0.0f;
        }

        private void StopMove()
        {
            if (time <= fallTime * 1.5f) return;
            stopBlock = true;
        }

        void OnCollisionEnter(Collision coll)
        {
            if (hitFloor) return;
            //Debug.Log(coll.gameObject.ToString());
            hitFloor = true;
        }

        public void Clamp()    //ブロックの移動範囲の制限
        {
            Vector3 tmpPos = transform.position;

            Vector3 min = firstTile.transform.position;
            Vector3 max = lastTile.transform.position;


            tmpPos.x = Mathf.Clamp(tmpPos.x, min.x, max.x);
            tmpPos.z = Mathf.Clamp(tmpPos.z, min.z, max.z);

            transform.position = tmpPos;
        }

    }
}
