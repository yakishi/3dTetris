using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour {

    [SerializeField]
    Stage stage;

    public float gizmoSize = 0.3f;
    public Color gizmoColor = Color.yellow;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }

    void Update()
    {
        this.transform.position = new Vector3(stage.getStageWidth / 2, stage.getStageHeight / 2, stage.getStageDepth / 2);

    }
}
