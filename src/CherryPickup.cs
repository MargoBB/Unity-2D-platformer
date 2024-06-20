using System.Collections;
using UnityEngine;

public class CherryPickup : MonoBehaviour
{
    public int damagePerInstance = 5;
    public int numberOfInstances = 5;
    public float intervalBetweenInstances = 1f;
    public float damageDuration = 5f; // Duration in seconds to apply damage
    public Vector2 knockback = Vector2.zero;
    public Color damageColor = Color.red;

    AudioSource pickupSource;
    bool hasAppliedDamage = false;
    bool isPickedUp = false;
    Collider2D coll;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable && !hasAppliedDamage && !isPickedUp)
        {
            StartCoroutine(ApplyDamageOverTime(damageable));

            if (pickupSource)
                AudioSource.PlayClipAtPoint(pickupSource.clip, transform.position, pickupSource.volume);

            hasAppliedDamage = true; // Set flag to prevent multiple applications
            isPickedUp = true;

            // Disable collider and renderer
            coll.enabled = false;
            spriteRenderer.enabled = false;
        }
    }

    private IEnumerator ApplyDamageOverTime(Damageable damageable)
    {
        Color originalColor = damageable.GetComponent<SpriteRenderer>().color;

        // Change color to damageColor
        damageable.GetComponent<SpriteRenderer>().color = damageColor;

        // Apply damage multiple times with intervals
        float timer = 0f;
        while (timer < damageDuration)
        {
            damageable.Hit(damagePerInstance, knockback);
            yield return new WaitForSeconds(intervalBetweenInstances);
            timer += intervalBetweenInstances;
        }

        // Restore original color
        damageable.GetComponent<SpriteRenderer>().color = originalColor;
    }
}
