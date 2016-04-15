using UnityEngine;

public class Rotate : MonoBehaviour {
    [Range(0,100)]
    public float degPerSec = 10;
    public Vector3 axis = Vector3.up;
    float deg;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        deg += degPerSec*Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(deg, axis);
	}
}
