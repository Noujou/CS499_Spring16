using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100; //starting health variable for the player
    public int currentHealth = 0; //current health variable for the player

    bool isDead; //bool that determines whether the player is alive or dead.
    bool damaged; //bool that checks to see if the player has been damaged or not

	// Use this for initialization
	void Start () {
        currentHealth = startingHealth; //start by setting the current health of the player to the starting health
	}
	
    public void TakeDamage(int damage)
    {
        damaged = true; //since you were damaged, damaged = true

        currentHealth -= damage; //subtract the damage taken from your current health

        if (currentHealth <= 0 && !isDead) //if current health is now less than or equal to zero and you're currently not dead
        {
            Death(); //now you are
        }
    }

    void Death()
    {
        isDead = true; //RIP in pepperoni
    }
}
