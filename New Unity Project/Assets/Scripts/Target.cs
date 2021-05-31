using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    [SerializeField] private ParticleSystem explosionOne;
    [SerializeField] private ParticleSystem explosionTwo;

    [SerializeField] private Slider healhtBar;
    private bool isDead = false;

    void Start()
    {
        healhtBar.maxValue = health;
        healhtBar.value = health;
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healhtBar.value = health;
        
        if (health <= 0 && !isDead)
        {
            isDead = true;
            explosionOne.Play();
            explosionTwo.Play();
            Destroy(gameObject, 0.3f);
        }
    }
}
