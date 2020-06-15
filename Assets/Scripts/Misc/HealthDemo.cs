using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDemo : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float startHealth;
    [SerializeField]
    private float damageOverTime;

    [SerializeField]
    Gradient healthColor;
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    Image image;

    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth -= damageOverTime * Time.deltaTime;

        float percentHealth = currentHealth / maxHealth;

        image.color = healthColor.Evaluate(percentHealth);

        healthSlider.value = percentHealth;
    }
}
