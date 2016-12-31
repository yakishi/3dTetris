using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {

    [SerializeField]
    private Floor floor;

    private float stageWidth;       //ステージの幅(x座標)
    [SerializeField]
    private float stageHeight = 7;      //ステージの高さ(y座標)
    private float stageDepth;       //ステージの奥行(z座標)

    public float getStageWidth { get { return stageWidth; } }
    public float getStageHeight { get { return stageHeight; } }
    public float getStageDepth { get { return stageDepth; } }

    // Use this for initialization
    void Start () {
        stageWidth = floor.getWQuantity;
        stageDepth = floor.getDQuantity;

        //stageWidth = floor.getWQuantity;
        //stageDepth = floor.getDQuantity;
        //stageHeight = stageWidth * 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
