using UnityEngine;
using System.Collections;

namespace block
{
    public class BlockFactory : MonoBehaviour
    {
        public Block blockPrefab;
        [SerializeField]
        private Stage stage;

        private float tmpPosX;
        private float tmpPosZ;

        Vector3 insPos;

        Block[] block;
        int activeNum;

        // Use this for initialization
        void Start()
        {

            tmpPosX = Mathf.Floor(stage.getStageWidth / 2);
            tmpPosZ = Mathf.Floor(stage.getStageDepth / 2);
            insPos = new Vector3(tmpPosX, stage.getStageHeight - 0.5f, tmpPosZ);

            block = new Block[100];
            activeNum = 0;

            
            block[0] = (Block)Instantiate(blockPrefab, insPos, Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {

            if (block[activeNum].onBlock == false)
            { 
                block[activeNum + 1] = (Block)Instantiate(blockPrefab, insPos, Quaternion.identity);
                activeNum += 1;
            }
          
        }

    }
}
