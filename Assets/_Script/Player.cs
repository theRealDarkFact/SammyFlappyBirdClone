using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }                                                                 //Make the static public player reference
    PlayerControls playerControls;                                                                              //Reference to the player input system that handles input
    
    private Rigidbody2D rigidBody;                                                                              //Rigidbody that handles physics and collision. Reference.
    private Animator playerAnim;                                                                                //Players animations.
    private int TurdCounter = 0;                                                                                //count how many poops are in play
    private float invCounter = 0;                                                                               //the counter for invulnerability
    private BoxCollider2D playerCollider;                                                                       //the players collider reference so it can be turned off/on
    private int invDelay = 5;                                                                                   //Max delay for invulnerability
    private bool ControlStatus = false;                                                                         //Whether a control status has occured.
    private SpriteRenderer playerSprite;                                                                        //The players sprite reference.

    [SerializeField] private AudioSource FlapSource, PoopSource, RespawnSource;
    [SerializeField] private ParticleSystem PSFeathers;
    [SerializeField] float flapForce = 10f;                                                                     //How much force to apply to the player lesser force means lighter lift when player flaps.
    [SerializeField] float tiltSmooth = 5f;                                                                     //Smoothing to apply to the players rotation during a flap
    [SerializeField] Quaternion downRotation;                                                                   //Handles how far down the rotation goes.
    [SerializeField] Quaternion forwardRotation;                                                                //Handles the forward rotation
    [SerializeField] private Transform Pooper;                                                                  //A empty game object where the bird poop is spawned.
    [SerializeField] private GameObject BigPoop;                                                                //The object itself
    [SerializeField] private Vector2 RespawnLocation;                                                           //Respawn location of the player

    private void Awake()                                                                                        //Happens when the object first loads and it initializes.
    {
        if (Instance == null) Instance = this; else Destroy(this);                                              //Singleton set up
        playerControls = new PlayerControls();                                                                  //Create our new Player Controls
        rigidBody = GetComponent<Rigidbody2D>();                                                                //Set rigidbody variable
        playerAnim = GetComponent<Animator>();                                                                  //Set the Animator variable
        playerCollider = GetComponent<BoxCollider2D>();                                                         //Set the player collider reference
        playerSprite = GetComponent<SpriteRenderer>();                                                          //Set the player sprite reference.

        //Set up for controls handling
        playerControls.Player.Flap.performed += _ => Flap();                                                    //Let's set a listener for the flap control event.
        playerControls.Player.Poop.performed += _ => Poop();                                                    //Listen for the event to make the bird drop its bombs
        playerControls.Player.Pause.performed += _ => PauseGame();                                              //Listen for the pause button event.
    }

    private void Update()                                                                                       //Updates every tick of the game engine
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);    //Handles Bird Rotation
        transform.position = new Vector2(0f, Mathf.Clamp(transform.position.y, 0.22f, 3f));                     //Let's clamp the player position to the play field
        if (invCounter > 0) InvCountDown();                                                                     //Handles invulnerability timer
    }

    private void InvCountDown()                                                                                 //Invulnerability counter
    {
        invCounter -= Time.deltaTime;                                                                           //Start counting
        if (invCounter <= 0)                                                                                    //if we reach zero invulnerable time has ended
        {   
            invCounter = 0;                                                                                     //reset counter for next use
            playerCollider.enabled = true;                                                                      //set the player collider to on.
        }
    }

    public void PauseGame()                                                                                     //Pauses the game
    {
        GameManager.Instance.togglePause();                                                                     //Player requested the pause call that method.
    }

    public void Flap()                                                                                          //Handles player flap
    {
        if (!ControlStatus) return;                                                                             //Nothing happening let's just get out of this method.
        playerAnim.SetTrigger("flap");                                                                          //Triggers the flap animation
        FlapSource.Play();                                                                                      //Plays the flap sound
        transform.rotation = forwardRotation;                                                                   //Sets the amount of rotation for the player
        rigidBody.velocity = Vector3.zero;                                                                      //sets the rigidbody velocity to a zero sum for x,y and z via the Vector3.zero
        rigidBody.AddForce(Vector2.up * flapForce, ForceMode2D.Force);                                          //This adds upward velocity to the player to give the feel the flap pushed upwards
    }

    public void ClearDropCounter()                                                                              //Clears the drop counter
    {
        TurdCounter -= 1;                                                                                       //Subtracts 1 from poop drop counter
        if (TurdCounter < 0) TurdCounter = 0;                                                                   //if we are less than 0 then set the counter to zero.
    }                                                                                                           //This is a fail safe against potention negative numbers.
    public void Poop()                                                                                          //Handles when the player uses the poop action.
    {
        if (!ControlStatus) return;                                                                             //If control status is false return no need to go on.
        if (TurdCounter >= 3) return;                                                                           //If our counter is higher than or equal to 3 also return preventing the player from dropping too many poops.
        TurdCounter += 1;                                                                                       //Increment our poop counter
        GameObject obj = Instantiate(BigPoop, Pooper.position, Quaternion.identity);                            //Instantiate/Create the object at the location.
        Destroy(obj, 2);                                                                                        //Destroy the object in 2 seconds. This prevents unecessary objects leaving the screen staying active.
        PoopSource.Play();                                                                                      //Play a sound effect for the action.
    }

    public void HurtPlayer()                                                                                    //If the player is hurt/killed.
    {
        DisableControls();                                                                                      //Let's disable controls
        playerAnim.SetTrigger("Death");                                                                         //Play the players death animation
        StartCoroutine(RespawnPlayer());                                                                        //Start the coroutine that handles the respawn.
    }

    public void HitBird()                                                                                       //Bird is hit (Originally a health system was in place but this is used for alternate death state)
    {
        DisableControls();                                                                                      //Disable controls
        playerSprite.enabled = false;                                                                           //Disable the sprite
        PSFeathers.Play();                                                                                      //Play the feathers being blow everywhere
        StartCoroutine(RespawnPlayer());                                                                        //Start the respawn counter (is used by both Hurt and Hit methods
    }

    private void OnEnable()                                                                                     //When the object is enabled
    {
        playerControls.Enable();                                                                                //Enable player controls, this is required for the player to have working controls.
    }

    private void OnDisable()                                                                                    //When this object is disabled
    {
        playerControls.Disable();                                                                               //Player controls are turned off.
    }

    public void DisableControls()                                                                               //Small method to disable controls for the player
    {
        rigidBody.velocity = Vector2.zero;                                                                      //Zero any rigid body velocities
        rigidBody.gravityScale = 0;                                                                             //Turn off gravity for a bit
        playerControls.Disable();                                                                               //Disable player controls.
        ControlStatus = false;                                                                                  //Disable the control status.
    }

    public void EnableControls()                                                                                //Enables controls
    {
        rigidBody.gravityScale = 0.5f;                                                                          //reset rigidbody to normal state.
        playerControls.Enable();                                                                                //enable controls.
        ControlStatus = true;                                                                                   //Control state now reactivated.
    }

    private IEnumerator RespawnPlayer()                                                                         //Handles player respawn
    {
        playerCollider.enabled = false;                                                                         //Turn off player collider
        invCounter = invDelay;                                                                                  //Set our invulnerability to start counting
        StartCoroutine(RemovePlayerSprite());                                                                   //Start the remove player coroutine
        yield return new WaitForSeconds(4);                                                                     //Wait for 4 real time seconds.
        transform.position = RespawnLocation;                                                                   //Move the player to the respawn location.
        if (!playerSprite.enabled) playerSprite.enabled = true;                                                 //if the player sprite is not enabled enable it.
        playerAnim.SetTrigger("Reset");                                                                         //Trigger the reset animation of the player
        EnableControls();                                                                                       //Turn controls back on.
        GameManager.Instance.TakeLife();                                                                        //Subtract a life (Lives and score handled by game manager)
        RespawnSource.Play();                                                                                   //Play respawn sound.
    }

    private IEnumerator RemovePlayerSprite()                                                                    //Removes the player sprite
    {
        yield return new WaitForSeconds(1);                                                                     //Wait 1 real time second
        playerSprite.enabled = false;                                                                           //turn off the sprite
        PSFeathers.Play();                                                                                      //Play the feather particle system
    }
}
