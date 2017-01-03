using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

    protected int sceneTask;
    [SerializeField]
    private int blockWQuantity;    //x座標のマス目の数
    private int blockDQuantity;    //z座標のマス目の数

    public int getWQuantity { get { return blockWQuantity; } }
    public int getDQuantity { get { return blockDQuantity; } }

    

    // Use this for initialization
    void Start () {

        blockDQuantity = blockWQuantity;
        

        //配置するプレハブの読み込み
        GameObject prefab = (GameObject)Resources.Load("Objects/Floor/Tile");
        //配置元のオブジェクト設定
        GameObject floorObject = GameObject.FindWithTag("Floor");
        //タイル配置
        for(int i = 0; i < blockWQuantity; i++)
        {
            for(int j = 0; j < blockDQuantity; j++)
            {
                Vector3 tile_pos = new Vector3(
                    0 + prefab.transform.localScale.x * i,
                    0,
                    0 + prefab.transform.localScale.z * j);

                
                if(prefab != null)
                {
                    //プレハブの複製
                    GameObject instant_object = (GameObject)GameObject.Instantiate(prefab, tile_pos, Quaternion.identity);

                    if (i == 0 && j == 0)
                    {
                        instant_object.tag = "First Tile";
                    }
                    else if(i == blockWQuantity - 1 && j == blockDQuantity - 1)
                    {
                        instant_object.tag = "Last Tile";
                    }

                    //生成元の下に複製したプレハブをくっつける
                    instant_object.transform.parent = floorObject.transform;
                }
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
