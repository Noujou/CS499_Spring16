using UnityEngine;
using System.Collections;

public class PokemonHealth : MonoBehaviour {

    public int startingHealth = 5; //variable that holds the starting health of the pokemon
    public int currentHealth; //variable that holds the current health of the pokemon
    public int scoreValue = 1; //the variable that holds their score value (for the score gui text)

    bool isDead;

	// Use this for initialization
	void Start () {
        currentHealth = startingHealth; //set their current health to their starting health
	}
	
    public void TakeDamage(int amount)
    {
        if (isDead) //if you're dead, you can't take anymore damage
            return;

        currentHealth -= amount; //subtractt your current health from the damage of the bullet

        if (currentHealth <= 0)
        {
            Death(); //if the hp is less than or equal to zero, then you're dead.
        }
    }

    void Death()
    {
        isDead = true; //RIP in pepperoni
        ScoreManager.score += scoreValue; //add the pokemon's score value to the overall score of the player
        Destroy(gameObject, 0.5f); //destory the pokemon object
    }
}
