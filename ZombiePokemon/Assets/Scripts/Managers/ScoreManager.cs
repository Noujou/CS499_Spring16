using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public static int score;        // The player's score.
    Text text;                      // Reference to the Text component.

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>(); //get the reference to the GUI text component

        score = 0; //initialize score to 0
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Score: " + score; //update the text gui component with the current score
	}
}
