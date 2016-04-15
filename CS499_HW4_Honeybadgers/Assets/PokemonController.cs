using UnityEngine;
using System.Collections;

public class PokemonController : MonoBehaviour
{

    GameObject currentPokemon;
    public GameObject[] Pokemon;
    protected int counter;
    int waitTimeCounter;

    // Use this for initialization
    void Start()
    {
        currentPokemon = GameObject.Find("Pikachu");
        Pokemon = new GameObject[4];
        counter = 0;
        
        InstantiatePokemon();
    }

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

    public int getCounter()
    {
        return counter;
    }

    public void nextPokemon()
    {
        Debug.Log("counter is: " + counter);

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

    public void prevPokemon()
    {
        Debug.Log("counter is: " + counter);

        if (counter <= 0)
        {
            Pokemon[counter].SetActive(false);
            counter = 3;
            Pokemon[counter].SetActive(true);
        }
        else
        {
            if (counter == 1)
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
        //if (waitTimeCounter > Time.deltaTime * 10000)
        //{

        //    if (counter >= 4)
        //    {
        //        Pokemon[counter - 1].SetActive(false);
        //        counter = 0;
        //        Pokemon[counter].SetActive(true);
        //    }
        //    else
        //    {
        //        if (counter == 0)
        //        {
        //            Pokemon[3].SetActive(false);
        //        }
        //        else
        //        {
        //            Pokemon[counter - 1].SetActive(false);
        //        }

        //        Pokemon[counter].SetActive(true);
        //        counter++;
        //    }
        //    waitTimeCounter = 0;
        //}
        //else
        //    waitTimeCounter++;

        //Debug.Log("waitTime is: " + Time.deltaTime * 10000);
    }

}
