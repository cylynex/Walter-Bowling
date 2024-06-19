using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float verticalInput;
    float horizontalInput;
    [SerializeField] float speed = 10f;
    Rigidbody rb;
    [SerializeField] GameObject pointer;
    [SerializeField] float timer = 0f;
    [SerializeField] float foulTime = 10f;

    [Header("Restrictions")]
    [SerializeField] float minZ = -19;
    [SerializeField] float maxZ = 2;

    [SerializeField] float energy;
    [SerializeField] float energyMod = 0.05f;
    [SerializeField] bool ballInHand = true;
    [SerializeField] float rotationSpeed = 10f;

    Vector3 startingPosition;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
    }

    private void Update() {
        CheckTimer();
        BallAlignment();
        ChargeEnergy();
        CheckForDud();
    }

    void CheckTimer() {
        if ((!ballInHand) && (timer > 0)) { 
            timer -= Time.deltaTime; 
        }

        if (!ballInHand) {
            if (timer <= 0) {
                ResetPlayer();
            }
        }
    }

    void BallAlignment() {
        if (ballInHand) {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);
        }
    }

    void ChargeEnergy() {
        if (ballInHand) {
            if (Input.GetKey(KeyCode.Space)) {
                energy += energyMod;
            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                LaunchBall();
            }
        }
    }

    void CheckForDud() {
        if ((!ballInHand) && (timer <= 0)) {
            ResetPlayer();
        }
    }

    void LaunchBall() {
        timer = foulTime;
        pointer.SetActive(false);
        ballInHand = false;
        rb.AddForce(Vector3.forward * energy, ForceMode.Impulse);
    }

    void CheckLimits() {
        if (transform.position.z < minZ) {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
        }

        if (transform.position.z > maxZ) {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
        }
    }

    void GetControls() {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        rb.AddForce(Vector3.forward * speed * verticalInput);
        rb.AddForce(Vector3.right * speed * horizontalInput);
    }

    public void ResetPlayer() {
        transform.position = startingPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        ballInHand = true;
    }
}
