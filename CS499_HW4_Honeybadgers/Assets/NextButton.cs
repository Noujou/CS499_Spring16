using UnityEngine;

public class NextButton : MonoBehaviour {
    // Use this for initialization

    void Start()
    {
        Cardboard.SDK.OnTrigger += On_Click;
        
    }
    void On_Click()
    {
        Ray r = FindObjectOfType<CardboardHead>().Gaze;
        Debug.DrawRay(r.origin, r.direction, Color.blue, 1000000);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit) && hit.collider.name.Equals("NextButton")) //if the raycast hit anything AND it's next button
        {
            GetComponent<Renderer>().material.color = Color.blue; //then turn it blue
            PokemonController nextPokemon = FindObjectOfType<PokemonController>(); 
            nextPokemon.nextPokemon(); //and switch to the next pokemon
        }
    }
    void Update()
    {
        Cardboard.SDK.UpdateState();
        Ray r = FindObjectOfType<CardboardHead>().Gaze;
        Debug.DrawRay(r.origin, r.direction, Color.blue, 1);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit) && hit.collider.name.Equals("NextButton")) //if the raycast hit anything AND it's next button (this is the hover over part)
        {
            GetComponent<Renderer>().material.color = Color.yellow; //then change it's color to yellow
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.grey; //otherwise, there are no important intersections, change it's color to grey
        }
    }
}
