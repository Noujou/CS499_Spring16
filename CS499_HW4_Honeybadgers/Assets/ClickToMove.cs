using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {
    public Transform objToMove;
	// Use this for initialization
	void Start () {
        Cardboard.SDK.OnTrigger += On_Click;
	}
    void On_Click() {
        Debug.Log("triggered");
        Ray r = GetComponentInChildren<CardboardHead>().Gaze;
        Debug.DrawRay(r.origin, r.direction, Color.blue, 1);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit))
        {
            objToMove.position = hit.point;
        }
        else
            objToMove.position += Vector3.up;
    }
    void Update()
    {
        Cardboard.SDK.UpdateState();
        Ray r = GetComponentInChildren<CardboardHead>().Gaze;
        Debug.DrawRay(r.origin, r.direction, Color.blue, 1);

    }
}
