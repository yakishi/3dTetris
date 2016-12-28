using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

    protected int sceneTask;

	// Use this for initialization
	void Start () {
        //配置するプレハブの読み込み
        GameObject prefab = (GameObject)Resources.Load("Objects/Floor/Tile");
        //配置元のオブジェクト設定
        GameObject floorObject = GameObject.FindWithTag("Floor");
        //タイル配置
        for(int i = 0; i < 7; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                Vector3 tile_pos = new Vector3(
                    0 + prefab.transform.localScale.x * i,
                    0,
                    0 + prefab.transform.localScale.z * j);

                if(prefab != null)
                {
                    //プレハブの複製
                    GameObject instant_object = (GameObject)GameObject.Instantiate(prefab, tile_pos, Quaternion.identity);

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
