#pragma warning disable 0649
using UnityEngine;
using System.Collections;

using block;
using trout;
public class Stage : MonoBehaviour
{

    //[SerializeField]
    //private TroutManager _troutManager;
    [SerializeField]
    private float stageWidth;           //ステージの幅(x座標)
    [SerializeField]
    private float stageHeight;      //ステージの高さ(y座標)
    [SerializeField]
    private float stageDepth;           //ステージの奥行(z座標)

    // bool[,,] checkBlock;                //ブロックの有無
    public Trout[,,] trout;

    public float getStageWidth { get { return stageWidth; } }
    public float getStageHeight { get { return stageHeight; } }
    public float getStageDepth { get { return stageDepth; } }

    // Use this for initialization
    void Start()
    {

        trout = new Trout[(int)stageWidth, (int)stageHeight, (int)stageDepth];

    }

    // Update is called once per frame
    void Update()
    {
        for (int y = 0; y < stageHeight; y++)
        {
            CheckLine(y);
        }
    }

    private void CheckLine(int y = 0)
    {
        for (int x = 0; x < stageWidth; x++)
        {
            for (int z = 0; z < stageDepth; z++)
            {
                if (!trout[x, y, z].checkBlock) break;

                if (x == stageWidth - 1 && z == stageDepth - 1)
                {
                    Debug.Log(trout[x, y, z].ToString());
                    DeleteLine(y);
                }
            }
        }
    }

    private void DeleteLine(int y)
    {

        for (int x = 0; x < stageWidth; x++)
        {
            for (int z = 0; z < stageDepth; z++)
            {
                Destroy(trout[x, y, z].transform.FindChild("Block(Clone)").gameObject);
                trout[x, y, z].InitTrout();
                if (trout[x, y + 1, z].checkBlock)
                {
                    GameObject upperObj = trout[x, y + 1, z].transform.FindChild("Block(Clone)").gameObject;
                    Vector3 pos = upperObj.transform.position;
                    trout[x, y + 1, z].transform.DetachChildren();
                    pos.y -= 1.0f;
                    upperObj.transform.position = pos;
                }
            }
        }
    }
}
