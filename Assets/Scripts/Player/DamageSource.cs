using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(damageAmount);  // O operador ? � chamado de operador de navega��o segura. Ele � usado para acessar membros de um objeto de forma segura. Se o objeto for nulo, a chamada do m�todo ser� ignorada.
    }
}
