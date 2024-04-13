using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class spotterSystemScript : MonoBehaviour
{
    public GameObject ship;
    public shipScript shipScript;
    public ParticleSystem spotterSystem;
    public whaleSpotterScript whaleSpotterScript;

    private ParticleSystem.Particle[] particle = new ParticleSystem.Particle[1];

    public Transform spotterSlider;
    [SerializeField] private float maxSpotterCooldown;
    private float currentSpotterCooldown = 0f;

    // Update is called once per frame
    void Update()
    {
        if(shipScript.canUseSpotter)
        {
            currentSpotterCooldown -= Time.deltaTime;
            if(currentSpotterCooldown < 0f)
            {
                currentSpotterCooldown = 0f;
                spotterSlider.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Press E to launch";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y, -0.75f);
                    transform.rotation = Quaternion.Euler(-((ship.transform.eulerAngles.z + 360) % 360), 90f, 0f);
                    var spotterSystemMain = spotterSystem.main;
                    spotterSystemMain.startSpeed = ship.GetComponent<Rigidbody2D>().velocity.magnitude + 5f;
                    spotterSystem.Emit(1);

                    currentSpotterCooldown = maxSpotterCooldown;
                }
            }
            else
            {
                spotterSlider.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Cooldown: {Mathf.Round(currentSpotterCooldown * 100) / 100}s";
            }
            spotterSlider.GetComponent<Slider>().value = currentSpotterCooldown/maxSpotterCooldown;

            bool particleExists = (spotterSystem.GetParticles(particle, 1) == 1);
            if (particleExists)
            {
                if (particle[0].remainingLifetime < 0.05f)
                {
                    whaleSpotterScript.spawnAt(particle[0].position);
                    spotterSystem.SetParticles(new ParticleSystem.Particle[0]);
                }
            }
        }
    }
}
