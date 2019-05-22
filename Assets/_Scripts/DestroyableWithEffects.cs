using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(AudioSource), typeof(MeshRenderer))]
public class DestroyableWithEffects : MonoBehaviour
{
    private bool isAlive;
    private ParticleSystem effectParticleSystem;
    private AudioSource audioSource;
    private Renderer myRenderer;
    void Start()
    {
        effectParticleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        myRenderer = GetComponent<Renderer>();
        isAlive = true;
    }
    
    public void PlayEffectThenDestroy()
    {
        if (isAlive)
        {
            isAlive = false;
            myRenderer.enabled = false;
            effectParticleSystem.Play();
            audioSource.Play();
            StartCoroutine("DestroyAfterEffect");
        }
    }

    private IEnumerator DestroyAfterEffect()
    {
        yield return new WaitForSeconds(effectParticleSystem.main.duration);
        int myId = GetComponent<IdComponent>().id;
        Messenger<int>.Broadcast(GameEvents.TARGET_DESTROYED, myId);
        Destroy(this.gameObject);
    }
}
