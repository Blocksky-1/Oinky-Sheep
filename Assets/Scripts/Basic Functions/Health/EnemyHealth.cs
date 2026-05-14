using UnityEngine;

public class EnemyHealth : Health
{
    protected override void Die()
    {
        Debug.Log("Enemigo derrotado.");
        Destroy(gameObject);
    }
}