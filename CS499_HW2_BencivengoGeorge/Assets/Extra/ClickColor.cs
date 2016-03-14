using UnityEngine;
public class ClickColor : MonoBehaviour
{
    //this will show up in the inspector
    public Color left = Color.red;
    void Update()
    {
        //ctrl or mouse1 on computer
        if (Input.GetButtonDown("Fire1"))
        {
            //set up for the ray trace, r in the direction and hit is the out
            RaycastHit hit;
            Ray r = new Ray();
            r.origin = transform.position;
            r.direction = transform.rotation * Vector3.forward;
            //Raycast returns if it hit something
            if (Physics.Raycast(r, out hit))
            {
                //change anything that can render's color.
                //yes this is horible practice. I would make a script(MonoBehaviour) 
                //that I use get component to find and validate my change. 
                hit.collider.gameObject.GetComponent<Renderer>().material.color = left;
            }
            else
            {
                //i guess i should show you print statements
                Debug.Log("Missed any renderers");
                Debug.LogWarning("This will give you a yellow triangle");
                Debug.LogError("Give you red circle that you can \"pause on error\" in the console tab with.");
            }
        }
    }
}
