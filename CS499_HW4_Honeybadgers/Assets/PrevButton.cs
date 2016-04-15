using UnityEngine;
using System.Collections;

public class PrevButton : MonoBehaviour {

    void Start()
    {
        Cardboard.SDK.OnTrigger += On_Click;

    }
    void On_Click()
    {
        Ray r = FindObjectOfType<CardboardHead>().Gaze;
        Debug.DrawRay(r.origin, r.direction, Color.blue, 1000000);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit) && hit.collider.name.Equals("PreviousButton")) //if the raycast hit anything AND it's previous button
        {
            GetComponent<Renderer>().material.color = Color.blue; //then change it's color to blue
            PokemonController prevPokemon = FindObjectOfType<PokemonController>();
            prevPokemon.prevPokemon(); //and switch to the previous pokemon
        }
    }
    void Update()
    {
        Cardboard.SDK.UpdateState();
        Ray r = FindObjectOfType<CardboardHead>().Gaze;
        Debug.DrawRay(r.origin, r.direction, Color.blue, 1);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit) && hit.collider.name.Equals("PreviousButton")) //if the raycast hit anything AND it's the previous button (this is for hovering over)
        {
            GetComponent<Renderer>().material.color = Color.yellow; //then switch it's color to yellow
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.grey; //otherwise, there are no important intersections
        }
    }
}
