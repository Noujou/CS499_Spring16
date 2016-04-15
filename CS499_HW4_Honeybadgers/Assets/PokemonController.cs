using UnityEngine;
using System.Collections;

public class PokemonController : MonoBehaviour
{

    GameObject currentPokemon; //the current pokemon or spinning pikachu
    public GameObject[] Pokemon; //the array containing all the pokemon
    int counter; //the counter, keeping track of which pokemon you're on

    // Use this for initialization
    void Start()
    {
        currentPokemon = GameObject.Find("Pikachu"); //sets the spinning pikachu to the current pokemon
        Pokemon = new GameObject[4]; //declares the pokemon array to be a GameObject array of length 4
        counter = 0; //sets the counter to start at zero, the pikachu
        
        InstantiatePokemon();
    }

    /*****************************************
    *Instantiates all the Prefab Pokemon into the Pokemon array, and then takes the current position and initial rotation of the Spinning Pikachu
    *and sets it to all the Prefab Pokemon in the Pokemon array, and then sets them to Invisible (SetActive = false) 
    *****************************************/
    void InstantiatePokemon()
    {
        Pokemon[0] = (GameObject)Instantiate(Resources.Load("Prefabs/Pikachu"));
        Pokemon[0].transform.Translate(currentPokemon.transform.position);
        Pokemon[0].transform.Rotate(new Vector3(0, 180, 0));
        Pokemon[0].SetActive(false);

        Pokemon[1] = (GameObject)Instantiate(Resources.Load("Prefabs/Charmander"));
        Pokemon[1].transform.Translate(currentPokemon.transform.position);
        Pokemon[1].transform.Rotate(new Vector3(0, 180, 0));
        Pokemon[1].SetActive(false);

        Pokemon[2] = (GameObject)Instantiate(Resources.Load("Prefabs/Squirtle"));
        Pokemon[2].transform.Translate(currentPokemon.transform.position);
        Pokemon[2].transform.Rotate(new Vector3(0, 180, 0));
        Pokemon[2].SetActive(false);

        Pokemon[3] = (GameObject)Instantiate(Resources.Load("Prefabs/Bulbasaur"));
        Pokemon[3].transform.Translate(currentPokemon.transform.position);
        Pokemon[3].transform.Rotate(new Vector3(0, 180, 0));
        Pokemon[3].SetActive(false);
    }

    /*
    *Next Pokemon goes in the order of:
    * 0 -> 1 -> 2 -> 3 -> 0
    * when a pokemon gets called, the one prior to it, gets set invisible, the current one gets shown and the counter advances
    */
    public void nextPokemon()
    {
        if (counter >= 3)
        {
            Pokemon[counter-1].SetActive(false);
            Pokemon[counter].SetActive(true);
            counter = 0;
        }
        else
        {
            if (counter == 0)
            {
                Pokemon[3].SetActive(false);
            }
            else
            {
                Pokemon[counter-1].SetActive(false);
            }

            Pokemon[counter].SetActive(true);
            counter++;
        }
    }

    /*
    *Previous Pokemon goes in the order of:
    * 0 -> 3 -> 2 -> 1 -> 0
    * when a pokemon gets called, the one prior to it, gets set invisible, the current one gets shown and the counter advances
    */
    public void prevPokemon()
    {
        if (counter <= 0)
        {
            Pokemon[counter+1].SetActive(false);
            Pokemon[counter].SetActive(true);
            counter = 3;
        }
        else
        {
            if (counter == 3)
            {
                Pokemon[0].SetActive(false);
            }
            else
            {
                Pokemon[counter + 1].SetActive(false);
            }
            Pokemon[counter].SetActive(true);
            counter--;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //nothing ;(
    }

}
