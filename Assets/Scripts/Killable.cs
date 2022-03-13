using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public MeshRenderer renderer;
    private Color originalColor;
    private Color flashColor = Color.red;
    private float flashTime = 0.1f;
    public float initialHealth;
    public float actualHealth;

    void Start()
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

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    private void FlashRed()
    {
        renderer.material.color = flashColor;
        Invoke("ResetColor", flashTime);
    }

    private void ResetColor()
    {
        renderer.material.color = originalColor; 
    }
}
