using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{    
    private GameObject dustEffect;    
    [SerializeField] GameObject dustEffectPrefab;
    [SerializeField] Rigidbody rb;  
    [SerializeField] Transform target;

    [SerializeField] float time;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 0f;
    [SerializeField] bool isFlying, isJumping = false;
    [SerializeField] private SwipeDetector swipe;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (GameObject.Find("SwipeDetector") != null)
            SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Manager.singleton.gameState == State.Win)
            return;

        rb.isKinematic = false;
        rb.AddForce(Vector3.up * jumpForce);
        if (other.transform.tag == "Ground")
        {
            Vector3 position = new Vector3(transform.position.x, 0.25f, transform.position.z);
            dustEffect = Instantiate(dustEffectPrefab, position, Quaternion.Euler(-90, 0, 0));
            dustEffect.GetComponent<ParticleSystem>().Play();
            Destroy(dustEffect, 1f);
        }
    }
    private void FixedUpdate()
    {
        if (Manager.singleton.gameState != State.Play)
            return;

        if (isFlying)
            IsBallFlying();

        if (isJumping)
            BallJump();
    }
    public void BallJump()
    {
        rb.isKinematic = false;
        rb.AddForce((Vector3.up+Vector3.forward) * 0.5f, ForceMode.VelocityChange);
        isJumping = false;
    }
    public void IsBallFlying()
    {
        time += Time.deltaTime;
        float duration = 0.5f;
        float t01 = time / duration;

        Vector3 A = transform.position;
        Vector3 B = target.position;
        Vector3 pos = Vector3.Lerp(A, B, t01);
        Vector3 arc = Vector3.up * 0.5f * Mathf.Sin(t01 * Mathf.PI);
        transform.position = pos + arc;

        if (t01 >= 1)
        {
            rb.isKinematic = false;
            isFlying = false;
        }
    }
    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        if (Manager.singleton.gameState != State.Play)
            return;

        if (data.Direction == SwipeDirection.Left)
        {
            transform.position -= Vector3.left * speed * Time.deltaTime;
        }
        if (data.Direction == SwipeDirection.Right)
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }
        if (data.Direction == SwipeDirection.Up)
        {
            if (swipe.endTime > 1f)
            {
                transform.position -= Vector3.forward * speed * Time.deltaTime;
            }
            else
            {
                if (Vector3.Distance(target.position, transform.position) < 5f)
                {
                    isFlying = true;
                    isJumping = false;
                }
                else
                {
                    isFlying = false;
                    isJumping = true;
                }
            }
        }
        if (data.Direction == SwipeDirection.Down)
        {
            transform.position -= Vector3.back * speed * Time.deltaTime;
        }
    }
}
