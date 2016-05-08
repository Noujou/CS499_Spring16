using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int shotDamage = 1; //the damage each of your shots does

    public Rigidbody projectile; //the rigidbodry of the projectile you're firing
    public Transform shotPos; //the transform that has the position of the shot
    public AudioSource shootSound;          //Plays a sound whenever a new zombie pokemon is spawned
    public float shotForce = 1000f; //the force at which the projectile is being forced at, basically it's acceleration
    private Ray shootRay; //the ray of the shot, line ray.
    public float timeBetweenAttacks = 0.5f; //the time delay between attacks, so you can't just spam the trigger button and win
    float timer; //and the timer that detects if you can shoot again

    //public Transform objToMove;
    // Use this for initialization
    void Start()
    {
        Cardboard.SDK.OnTrigger += On_Click;
        shootSound = GetComponent<AudioSource>(); //getting the audio source component
    }
    void On_Click()
    {
        //All of the commented out code was used when I was doing raytracing collision instead of projectile based collision
        //int shootableMask = LayerMask.GetMask("Shootable");
        shootRay = GetComponentInChildren<CardboardHead>().Gaze; //gets the current position of the cardboard head (where you're looking)
        //RaycastHit shootHit;
        //if (Physics.Raycast(shootRay, out shootHit, 250, shootableMask))
        //{
        //    Debug.Log("PIKA");
        //    PokemonHealth currentPokeHP = shootHit.collider.GetComponent<PokemonHealth>(); //get the HP of the pokemon that you just hit
        //    currentPokeHP.TakeDamage(shotDamage); //subtract the pokemon's HP from the damage of the shot
        //}
        if (timer >= timeBetweenAttacks) //if enough time has passed since the last shot fired
        {
            Shoot(); //then shoot again
        }
    }
    void Update()
    {
        Cardboard.SDK.UpdateState();
        Pose3D head = Cardboard.SDK.HeadPose;
        shootRay = GetComponentInChildren<CardboardHead>().Gaze;
        timer += Time.deltaTime;
    }

    void Shoot()
    {
        timer = 0f; //reset the timer since you last shot to 0
        Vector3 position = shootRay.direction + (Vector3.up * 2) + Vector3.forward; //get the position in which you're gonna shoot
        Rigidbody shot = Instantiate(projectile, position, shotPos.rotation) as Rigidbody; //create the projectile
        shot.AddForce(shootRay.direction * shotForce); //add acceleration to the projectile
        shootSound.Play(); //play the gun sound
        Destroy(shot.gameObject, 6f); //after 6 seconds, if it hasn't hit anything, then destory it
    }
}
