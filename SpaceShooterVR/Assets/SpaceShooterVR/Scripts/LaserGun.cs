using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserGun : MonoBehaviour
{

    [SerializeField] private Animator laserAnimator;
    [SerializeField] private AudioClip laserSFX;
    [SerializeField] private Transform raycastOrigin;

    private AudioSource laserAudioSource;

    private RaycastHit hit;

    public GameObject laser;


    private void Awake()
    {
        if(GetComponent<AudioSource>() != null)
        {
            laserAudioSource = GetComponent<AudioSource>();
        }
        
    }


    public void LaserGunFire()
    {
        // animate the gun
        laserAnimator.SetTrigger("Fire");

        // play laser gun SFX
        laserAudioSource.PlayOneShot(laserSFX);

        laser.SetActive(true);

        // raycast
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, 800f))
        {
            if(hit.transform.GetComponent<AsteroidHit>() != null)
            {
                hit.transform.GetComponent<AsteroidHit>().AsteroidDestroyed();
            }
            else if (hit.transform.GetComponent<IRaycastInterface>() != null)
            {
                hit.transform.GetComponent<IRaycastInterface>().HitByRaycast();
            }
        }

        StartCoroutine(LaserSetActiveFalse());

    }

    IEnumerator LaserSetActiveFalse()
    {
        yield return new WaitForSeconds(0.1f);
        laser.SetActive(false);

    }




}
