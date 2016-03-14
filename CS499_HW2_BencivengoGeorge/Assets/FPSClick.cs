using UnityEngine;
using System.Collections;

public class FPSClick : MonoBehaviour {
    public Transform button;
    public Transform door;
    public bool doorOpen = false;
    void checkLocation(Vector3 pos) {
        //get the magnitude of (the position of the mouse cursor - the position of the button), if that's lower or equal to 0.325f, then move the door 
        //I used 0.376f because it seems to be the highest magnitude possible while still be hovering over the button. I know in general magic numbers are bad, but for the purposes of the this assignment and the simplistic nature, it felt ok.
        if ((pos - button.position).magnitude <= 0.376f)
        {
            if (!doorOpen)
            {
                door.position = door.position + (Vector3.up * 6);
                doorOpen = true;
            }
            else
            {
                door.position = door.position + (Vector3.down * 6);
                doorOpen = false;
            }
        }
    }
    Vector3 mousePos, cameraPos;
    void Update () {
        if (Input.GetMouseButtonDown(1)) {
            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        Camera c = GetComponent<Camera>();
        if (Input.GetButtonDown("Fire1")) {
            Ray r = c.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit))
                mousePos = hit.point;
            r.direction = transform.rotation * Vector3.forward;
            if (Physics.Raycast(r, out hit))
                cameraPos = hit.point;
            checkLocation(cameraPos);
        }
	}
    void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(cameraPos, 1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere( mousePos, .5f);
    }
}
