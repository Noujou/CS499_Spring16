using UnityEngine;
using System.Collections;

public class MouseCircle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    Vector3 mouseRay,tangentalRay;
	// Update is called once per frame
	void Update () {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseRay = mp - transform.position;
        tangentalRay = Vector3.Cross(mouseRay, Vector3.forward);
        transform.position += tangentalRay * Time.deltaTime;
	}
    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, mouseRay);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, tangentalRay);
    }
}
