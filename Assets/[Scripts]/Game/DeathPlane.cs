using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private GameObject splashParticleSystem;

    public delegate void OnObjectFallEvent();
    public static event OnObjectFallEvent OnObjectFell;

    /// Functions ///

    private void PlayAudio()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.Play();
    }

    /// Coroutines ///

    private IEnumerator PlayParticle_Coroutine(Vector3 pos)
    {
        GameObject splash = Instantiate(splashParticleSystem, transform, true);
        splash.transform.position = pos;

        yield return new WaitForSeconds(2.0f);

        Destroy(splash);
    }

    /// Collisions ///

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            StartCoroutine(PlayParticle_Coroutine(other.transform.position));

            PlayAudio();

            Destroy(BlockHelperFunctions.GetBlockParent(other.transform).gameObject);
            OnObjectFell?.Invoke();
        }
    }
}
