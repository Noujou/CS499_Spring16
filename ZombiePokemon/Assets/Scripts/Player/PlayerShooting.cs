using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    public int shotDamage = 1;
    //public Transform objToMove;
	// Use this for initialization
	void Start () {
        Cardboard.SDK.OnTrigger += On_Click;
	}
    void On_Click() {
        //Debug.Log("triggered");
        int shootableMask = LayerMask.GetMask("Shootable");
        Ray shootRay = GetComponentInChildren<CardboardHead>().Gaze;
        Debug.DrawRay(shootRay.origin, shootRay.direction, Color.blue, 1);
        RaycastHit shootHit;
        if (Physics.Raycast(shootRay, out shootHit, 250, shootableMask))
        {
            Debug.Log("PIKA");
            PokemonHealth currentPokeHP = shootHit.collider.GetComponent<PokemonHealth>(); //get the HP of the pokemon that you just hit
            currentPokeHP.TakeDamage(shotDamage); //subtract the pokemon's HP from the damage of the shot
            Debug.Log("current pokemon's hp is " + currentPokeHP.currentHealth);
        }
        else
            Debug.Log("Miss");
    }
    void Update()
    {
        Cardboard.SDK.UpdateState();
        Pose3D head = Cardboard.SDK.HeadPose;
        Ray r = GetComponentInChildren<CardboardHead>().Gaze;
        Debug.DrawRay(r.origin, r.direction, Color.blue, 1);

    }
}
