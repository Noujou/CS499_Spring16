using UnityEngine;

public class NextButton : MonoBehaviour {
    // Use this for initialization

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
        if (Physics.Raycast(r, out hit) && hit.collider.name.Equals("NextButton"))
        {
            GameObject tempButton = GameObject.Find("NextButton");
            GetComponent<Renderer>().material.color = Color.blue;
            PokemonController nextPokemon = FindObjectOfType<PokemonController>();
            int nextPokeCounter = nextPokemon.getCounter();
            nextPokemon.nextPokemon();
            Debug.Log("Counter in nextbutton is: "  + nextPokeCounter);
        }
    }
    void Update()
    {
        Cardboard.SDK.UpdateState();
        Pose3D head = Cardboard.SDK.HeadPose;
        Ray r = FindObjectOfType<CardboardHead>().Gaze;
        Debug.DrawRay(r.origin, r.direction, Color.blue, 1);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit) && hit.collider.name.Equals("NextButton"))
        {
            GameObject tempButton = GameObject.Find("NextButton");
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            GameObject tempButton = GameObject.Find("NextButton");
            GetComponent<Renderer>().material.color = Color.grey;
        }
    }
}
