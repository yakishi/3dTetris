﻿using UnityEngine;
using System.Collections;

namespace block
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private Stage stage;    
        [SerializeField]
        float velocity;         //入力時の速度
        [SerializeField]
        float fallVelocity;     //落下速度

        float time;
        float fallTime;

        bool hitFloor;          //地面やブロックにヒットしたフラグ
        bool stopBlock;         //地面に触れた後数カウント後操作を不可能にするフラグ
        int hitCount;           //地面やブロックに当たっている時間
        public bool onBlock;           //ブロックがあるかどうか

        private Renderer firstTile;
        private Renderer lastTile;


        
        // Use this for initialization
        void Start()
        {
            time = 0.0f;
            fallTime = 0.7f;
            hitFloor = false;
            hitCount = 0;
            onBlock = true;

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
            MoveBlock();

        }

        private void MoveBlock()
        {
            if (stopBlock) return;

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
            onBlock = false;
        }

        void OnCollisionEnter(Collision coll)
        {
            if (hitFloor) return;
            Debug.Log(coll.gameObject.ToString());
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
