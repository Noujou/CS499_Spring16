using UnityEngine;
using System.Collections;

public class PrevButton : MonoBehaviour {

    void Start()
    {
        Cardboard.SDK.OnTrigger += On_Click;

    }
    void On_Click()
    {
        Debug.Log("BLAH");
        Ray r = FindObjectOfType<CardboardHead>().Gaze;
        Debug.DrawRay(r.origin, r.direction, Color.blue, 1000000);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit) && hit.collider.name.Equals("PreviousButton"))
        {
            GameObject tempButton = GameObject.Find("PreviousButton");
            GetComponent<Renderer>().material.color = Color.blue;
            PokemonController prevPokemon = FindObjectOfType<PokemonController>();
            int prevPokeCounter = prevPokemon.getCounter();
            prevPokemon.prevPokemon();
            Debug.Log("Counter in prev button is: " + prevPokeCounter);
        }
    }
    void Update()
    {
        Cardboard.SDK.UpdateState();
        Pose3D head = Cardboard.SDK.HeadPose;
        Ray r = FindObjectOfType<CardboardHead>().Gaze;
        Debug.DrawRay(r.origin, r.direction, Color.blue, 1);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit) && hit.collider.name.Equals("PreviousButton"))
        {
            GameObject tempButton = GameObject.Find("PreviousButton");
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            GameObject tempButton = GameObject.Find("PreviousButton");
            GetComponent<Renderer>().material.color = Color.grey;
        }
    }
}
