using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 10f;
    [SerializeField] float dönmeHizi = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem rocketJetParticle;
    [SerializeField] ParticleSystem rightThruster;
    [SerializeField] ParticleSystem LeftThruster;

    Rigidbody rocketRigidBody;
    AudioSource audioSource;

    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        ProcessThrust();
        RocketRotation();
    }

    private void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            SopThrusting();
        }
    }
    private void RocketRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }

    private void StartThrusting()
    {
        rocketRigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!rocketJetParticle.isPlaying)
        {
            rocketJetParticle.Play();
        }
    }
    private void SopThrusting()
    {
        audioSource.Stop();
        rocketJetParticle.Stop();
    }


    private void RotateLeft()
    {
        ApplyRotation(+dönmeHizi);
        if (!rightThruster.isPlaying)
        {
            rightThruster.Play();
        }
    }
    private void RotateRight()
    {
        ApplyRotation(-dönmeHizi);
        if (!LeftThruster.isPlaying)
        {
            LeftThruster.Play();
        }
    }
    private void StopRotate()
    {
        rightThruster.Stop();
        LeftThruster.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        Vector3 dönmeOlayı = Vector3.forward * rotationThisFrame * Time.deltaTime;
        rocketRigidBody.freezeRotation = true;
        transform.Rotate(dönmeOlayı);
        rocketRigidBody.freezeRotation = false;
    } 
}
