using UnityEngine;
using System.Collections;

public class MyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    float spd = 5,anglarSpeed = 36;
    float theta = 0;
	void Update () {
        //Position Change
        theta += anglarSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, theta);
        transform.position += transform.right * spd * Time.deltaTime;

        //draw lines to show global vs local space
        Vector3 globalSpace = new Vector3(1, 2, 0);
        Quaternion rot = Quaternion.AngleAxis(theta, Vector3.forward);
        Vector3 localSpace = rot * globalSpace;
        Debug.DrawRay(transform.position,globalSpace, Color.red);
        Debug.DrawRay(transform.position, localSpace, Color.blue);
    }



}
