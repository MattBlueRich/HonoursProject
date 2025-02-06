using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectShake))]
public class DarknessObject : MonoBehaviour
{
    public float maxHealth = 100f;
    public float tickSpeed = 2f;
    [ReadOnlyInspector] public float currentHealth;
    public ParticleSystem deathParticleEffect;
    public ParticleSystem damageParticleEffect;

    private ObjectShake objShake;
    private GameObject model;
    private Flashlight flashlight;
    private Collider objCollider;

    bool isLit = false;
    bool shake = false;

    private void Start()
    {
        objShake = GetComponent<ObjectShake>();
        model = transform.GetChild(0).gameObject;
        currentHealth = maxHealth;
        objCollider = GetComponent<Collider>();
    }
    private void Update()
    {
        if (isLit)
        {
            if(currentHealth > 0) 
            {
                currentHealth -= Time.deltaTime * tickSpeed * flashlight.damage;
                shake = true;
            }
            else
            {
                currentHealth = 0;
                isLit = false;

                Destroy(model); // Destroy this object's visuals.
                objCollider.enabled = false; // Remove collider from this parent (disable further collision).

                deathParticleEffect.Play();
                damageParticleEffect.Stop();

                if (shake)
                {
                    shake = false;
                    objShake.Stop();
                }
            }

            objShake.Begin();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flashlight"))
        {
            if(flashlight == null)
            {
                flashlight = other.transform.parent.parent.GetComponent<Flashlight>();
                
            }

            damageParticleEffect.Play();
            isLit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flashlight"))
        {
            isLit = false;
            damageParticleEffect.Stop();
        }
    }

}
