using UnityEngine;
using System.Collections;

using block;
namespace trout
{
    public class Trout : MonoBehaviour
    {    //マス目クラス

        public bool checkBlock;
        //private Block block;
        private Vector3 pos;

        public Vector3 SetPos { set { pos = value; } }
        public Vector3 GetPos { get { return pos; } }

        // Use this for initialization
        void Start()
        {
            InitTrout();
            pos = Vector3.zero;

        }
        public void InitTrout()
        {
            checkBlock = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerStay(Collider coll)
        {
            
            if (checkBlock == true) return;
            if (coll.gameObject.tag != "Block") return;
            if(coll.gameObject.GetComponent<Block>().stopBlock == true)
            {
                checkBlock = true;
                coll.transform.SetParent(this.transform);
            }
        }

        //public void setBlock(Block bc)
        //{
        //    block = bc;
        //}
    }
}
