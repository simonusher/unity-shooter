using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootingAudioPlayer))]
public class ShootingController : MonoBehaviour
{
    [SerializeField] private float shootingInterval = 0.3f;
    [SerializeField] private float shootingDistance = 10.0f;
    private bool canShoot;

    private ShootingAudioPlayer audioPlayer;

    private void Start()
    {
        audioPlayer = GetComponent<ShootingAudioPlayer>();
        canShoot = true;
    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (canShoot)
            {
                StartCoroutine("Shoot");
            }
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        audioPlayer.Play();
        HandleHits();
        yield return new WaitForSeconds(shootingInterval);
        canShoot = true;
    }

    private void HandleHits()
    {
        //Debug.DrawRay(transform.parent.position, shootingDistance * transform.parent.forward, Color.red, 3.0f, false);
        if (Physics.Raycast(transform.parent.position, transform.parent.forward, out RaycastHit hit, shootingDistance))
        {
            Messenger<RaycastHit>.Broadcast(GameEvents.HIT, hit);
        }
    }
}
