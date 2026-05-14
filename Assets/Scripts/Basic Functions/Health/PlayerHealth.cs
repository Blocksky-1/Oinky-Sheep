using UnityEngine;

public class PlayerHealth : Health
{
    protected override void Die()
    {
        Debug.Log("El Jugador ha muerto.");
        Destroy(gameObject);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }
}