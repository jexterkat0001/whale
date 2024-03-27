using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whaleSystemScript : MonoBehaviour
{
    private List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    public GameObject ship;
    
    void Start()
    {
        ship = GameObject.FindWithTag("Ship");
    }

    void OnParticleCollision(GameObject other)
    {
        int shipCollisions = ParticlePhysicsExtensions.GetCollisionEvents(GetComponent<ParticleSystem>(), other, collisionEvents);
        for(int i = 0; i < shipCollisions; i++)
        {
            if(collisionEvents[i].colliderComponent.gameObject.layer == 3)
            {
                Debug.Log("ship collision");
            }
        }
    }

    void OnParticleTrigger()
    {
        ParticlePhysicsExtensions.GetTriggerParticles(GetComponent<ParticleSystem>(),ParticleSystemTriggerEventType.Inside,particles);
        for(int i = 0; i < particles.Count; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            particle.startColor = new Color32(255,255,255,255);
            Debug.Log(particle.remainingLifetime);
            if(particle.remainingLifetime > 19f)
            {
                particle.remainingLifetime = 0f;
            }
            particles[i] = particle;
        }
        ParticlePhysicsExtensions.SetTriggerParticles(GetComponent<ParticleSystem>(),ParticleSystemTriggerEventType.Inside,particles);
    }
}
