#pragma warning disable 0649
using UnityEngine;
using System;
using System.Collections;

namespace trout
{
    public class TroutManager : MonoBehaviour
    {

        [SerializeField]
        private Stage stage;
        [SerializeField]
        private Floor floor;
        private GameObject troutField;
        [SerializeField]
        private GameObject troutPrefab;

        public int blockWTrouts { get; set; }   //X方向のマスの数
        public int blockHTrouts { get; set; }   //Y方向のマスの数
        public int blockDTrouts { get; set; }   //Z方向のマスの数

        // Use this for initialization
        void Start()
        {
            blockWTrouts = (int)stage.getStageWidth;
            blockHTrouts = (int)stage.getStageHeight;
            blockDTrouts = (int)stage.getStageDepth;

            troutField = GameObject.FindWithTag("Field");

            //ForTrout((int x, int y, int z) =>
            //{
            //    CreateTrout(x,y,z);
            //});
            for (int y = 0; y < blockHTrouts; y++)
            {
                for (int x = 0; x < blockWTrouts; x++)
                {
                    for (int z = 0; z < blockDTrouts; z++)
                    {
                        CreateTrout(x,y,z);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ForTrout(Action<int,int,int> act)
        {
            for (int y = 0; y < blockHTrouts; y++)
            {
                for (int x = 0; x < blockWTrouts; x++)
                {
                    for (int z = 0; z < blockDTrouts; z++)
                    {
                        act(x,y,z);
                    }
                }
            }
        }
        private void CreateTrout(int x, int y, int z)
        {

            Vector3 trout_pos = new Vector3(
           0 + troutPrefab.transform.localScale.x * x,
           0.5f + troutPrefab.transform.localScale.y * y,
           0 + troutPrefab.transform.localScale.z * z);


            if (troutPrefab != null)
            {
                //プレハブの複製
                GameObject instant_object = (GameObject)GameObject.Instantiate(troutPrefab, trout_pos, Quaternion.identity);


                //生成元の下に複製したプレハブをくっつける
                instant_object.transform.parent = troutField.transform;

                if (stage.trout[x, y, z] == null) stage.trout[x, y, z].GetComponent<Trout>(); ;

                stage.trout[x, y, z].SetPos = trout_pos;
            }

        }

    }
}
