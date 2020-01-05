using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAni;
    public ParticleSystem particleExplosion;
    public ParticleSystem particleDirt;
    public float jumpForce = 700f;
    public float gravityModifier = 1.5f;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    private bool isOnGround = true;
    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAni = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAni.SetTrigger("Jump_trig");
            particleDirt.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            particleDirt.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            Debug.Log("Game Over");
            playerAni.SetBool("Death_b", true);
            playerAni.SetInteger("DeathType_int", 1);
            particleExplosion.Play();
            particleDirt.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}