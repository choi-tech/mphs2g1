using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VRJumpscareTrigger : MonoBehaviour
{
    public GameObject jumpscareObject;
    public float displayDuration = 2f;
    public AudioClip jumpscareSound;
    private AudioSource audioSource;
    private bool hasTriggered = false;


    private Animator jumpscareAnimator;  


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.spatialBlend = 1.0f;


       
        if (jumpscareObject != null)
        {
            jumpscareAnimator = jumpscareObject.GetComponent<Animator>();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(ActivateJumpscare());
        }
    }


    private IEnumerator ActivateJumpscare()
    {
       
        if (jumpscareObject != null)
        {
            jumpscareObject.transform.SetParent(Camera.main.transform);
            jumpscareObject.transform.localPosition = new Vector3(0, 0, 1);
            jumpscareObject.SetActive(true);
        }


       
        if (jumpscareAnimator != null)
        {
            jumpscareAnimator.SetTrigger("ActivateJumpscare");
        }


       
        if (jumpscareSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpscareSound);
        }


       
        yield return new WaitForSeconds(displayDuration);


       
        if (jumpscareObject != null)
        {
            jumpscareObject.SetActive(false);
        }


        Destroy(gameObject);
    }
}