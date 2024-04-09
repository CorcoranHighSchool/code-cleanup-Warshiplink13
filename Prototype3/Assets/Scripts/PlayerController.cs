using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The player's Rigidbody
    private Rigidbody playerRb;
    //Jump force
    private float jumpForce = 15.0f;
    //Gravity Modifier
    [Serializefield] private float gravityModifier;
    //Are we on the ground?
    [Serializefield] private bool isOnGround { get; private set;};
    //Is the Game Over
    [Serializefield] private bool gameOver { get; private set;};

    //Player Animator
    private Animator playerAnim;

    //ParticleSystem explosion
    [Serializefield] private ParticleSystem explositionParticle;
    //ParticleSystem dirt
    [Serializefield] private ParticleSystem dirtParticle;

    //Jump sound
    [Serializefield] private AudioClip jumpSound;
    //Crash sound
    [Serializefield] private AudioClip crashSound;
    //Player Audio
    [Serializefield] private AudioSource playerAudio;
    private const string gameover = "Game Over!";
    private const string death_b = "Death_b";
    private const string deathtype_int = "DeathType_int";
    private const string obstacle = "Obstacle";
    private const string ground = "Ground";
    private const string jump_trig = "Jump_trig";

    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody
        playerRb = GetComponent<Rigidbody>();
        //Connect the Animator
        playerAnim = GetComponent<Animator>();
        //Player Audio
        //playerAudio.GetComponent<AudioSource>();
        //update the gravity
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //If we press space, jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //trigger the jump animation
            playerAnim.SetTrigger(jump_trig);
            isOnGround = false;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticle.Stop();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ground))
        {
            dirtParticle.Play();
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag(obstacle))
        {
            explositionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);

            gameOver = true;
            Debug.Log(gameover);
            playerAnim.SetBool(death_b, true);
            playerAnim.SetInteger(deathtype_int, 1);
        }
    }
}
