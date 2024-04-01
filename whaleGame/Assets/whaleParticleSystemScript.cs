using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whaleParticleSystemScript : MonoBehaviour
{
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    public GameObject ship;
    public ParticleSystem whalePing;
    public menuScript menuScript;

    float xOffset;
    float yOffset;
    
    void Start()
    {
        ship = GameObject.FindWithTag("Ship");
        whalePing = GameObject.FindWithTag("whalePing").GetComponent<ParticleSystem>();
        menuScript = GameObject.FindWithTag("gameUI").GetComponent<menuScript>();

        xOffset = transform.parent.position.x;
        yOffset = transform.parent.position.y;
    }

    void OnParticleCollision(GameObject other)
    {
        int shipCollisions = ParticlePhysicsExtensions.GetCollisionEvents(GetComponent<ParticleSystem>(), other, collisionEvents);
        for(int i = 0; i < shipCollisions; i++)
        {
            if(collisionEvents[i].colliderComponent.gameObject.layer == 3)
            {
                menuScript.whalesHit++;
            }   
        }
    }

    void OnParticleTrigger()
    {
        ParticlePhysicsExtensions.GetTriggerParticles(GetComponent<ParticleSystem>(),ParticleSystemTriggerEventType.Enter,particles);
        for(int i = 0; i < particles.Count; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            if(particle.remainingLifetime < 19f && particle.startColor.a != 255)
            {
                particle.startColor = new Color32(255,255,255,255);
                particle.remainingLifetime = 40f;

                whalePing.transform.position = new Vector3(particle.position.x+xOffset, particle.position.y+yOffset, particle.position.z - 1);
                var whalePingMain = whalePing.main;
                whalePingMain.startSpeed = particle.velocity.magnitude;
                float angle = ((Vector2.SignedAngle(particle.velocity, Vector2.right) * -1) + 360) % 360;
                whalePing.transform.rotation = Quaternion.Euler(0f,0f,angle);
                whalePing.Emit(1);
            }
            particles[i] = particle;
        }
        ParticlePhysicsExtensions.SetTriggerParticles(GetComponent<ParticleSystem>(),ParticleSystemTriggerEventType.Enter,particles);
    
        ParticlePhysicsExtensions.GetTriggerParticles(GetComponent<ParticleSystem>(),ParticleSystemTriggerEventType.Inside,particles);
        for(int i = 0; i < particles.Count; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            if(particle.remainingLifetime > 19f)
            {
                particle.remainingLifetime = 0f;
            }
            particles[i] = particle;
        }
        ParticlePhysicsExtensions.SetTriggerParticles(GetComponent<ParticleSystem>(),ParticleSystemTriggerEventType.Inside,particles);
    
    }
}
