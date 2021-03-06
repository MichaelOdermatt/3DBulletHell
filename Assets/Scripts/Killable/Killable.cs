using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Killable : MonoBehaviour
{
    public MeshRenderer renderer;
    private Color originalColor;
    private Color flashColor = Color.red;
    private float flashTime = 0.1f;
    public float initialHealth;
    private float actualHealth;

    void Awake()
    {
        actualHealth = initialHealth;
        renderer = GetComponent<MeshRenderer>();
        originalColor = renderer.material.color;
    }

    public void TakeDamage(float amount)
    {
        actualHealth -= amount;
        FlashRed();

        if (actualHealth <= 0)
        {
            Die();
        }
    }

    public abstract void Die();

    private void FlashRed()
    {
        renderer.material.color = flashColor;
        Invoke("ResetColor", flashTime);
    }

    private void ResetColor()
    {
        renderer.material.color = originalColor; 
    }

    public void ResetValues()
    {
        actualHealth = initialHealth;
    }
}
