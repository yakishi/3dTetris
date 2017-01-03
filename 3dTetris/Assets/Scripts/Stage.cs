using UnityEngine;
using System.Collections;

using block;

public class Stage : MonoBehaviour {

    [SerializeField]
    private Floor floor;
    //private GameObject block;

    private float stageWidth;           //ステージの幅(x座標)
    [SerializeField]
    private float stageHeight = 7;      //ステージの高さ(y座標)
    private float stageDepth;           //ステージの奥行(z座標)

    bool[,,] checkBlock;                //ブロックの有無

    public float getStageWidth { get { return stageWidth; } }
    public float getStageHeight { get { return stageHeight; } }
    public float getStageDepth { get { return stageDepth; } }

    // Use this for initialization
    void Start () {
        //block = GameObject.FindWithTag("Block");

        stageWidth = floor.getWQuantity;
        stageDepth = floor.getDQuantity;

        checkBlock = new bool[(int)stageWidth, (int)stageHeight, (int)stageDepth];

        initCheckBlock();


    }
	
	// Update is called once per frame
	void Update () {
	}

    private void initCheckBlock()
    {
        for(int x = 0; x < stageWidth; x++)
        {
            for (int y = 0; y < stageWidth; y++)
            {
                for (int z = 0; z < stageWidth; z++)
                {
                    checkBlock[x, y, z] = false;
                }
            }
        }
    }

    public void setCheckBlock(int widthNumber,int heightNumber,int depthNumber, bool existence)
    {
        checkBlock[widthNumber, heightNumber, depthNumber] = existence;
    }

    
}
