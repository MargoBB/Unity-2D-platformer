using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public int Health { get; private set; }

    public UnityEvent<int, int> healthChanged;

    private void Awake()
    {
        Health = MaxHealth;
        if (healthChanged == null)
            healthChanged = new UnityEvent<int, int>();
    }

    public bool Heal(int amount)
    {
        if (Health < MaxHealth)
        {
            Health = Mathf.Min(Health + amount, MaxHealth);
            healthChanged.Invoke(Health, MaxHealth);
            return true;
        }
        return false;
    }

    public void TakeDamage(int amount)
    {
        Health = Mathf.Max(Health - amount, 0);
        healthChanged.Invoke(Health, MaxHealth);
        if (Health == 0)
        {
            // Handle player death
        }
    }
}
