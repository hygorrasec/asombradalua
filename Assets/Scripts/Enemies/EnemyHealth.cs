using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private float knockbackThrust = 15f;

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start() {
        currentHealth = startingHealth;
    }

    /// <summary>
    /// Aplica dano à saúde do inimigo.
    /// </summary>
    public void TakeDamage(int amount) {
        currentHealth -= amount;
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockbackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    /// <summary>
    /// A função CheckDetectDeathRoutine é chamada após um tempo para verificar se a saúde do inimigo é menor ou igual a zero.
    /// </summary>
    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    /// <summary>
    /// A função DetectDeath é chamada quando a saúde do inimigo é menor ou igual a zero.
    /// </summary>
    public void DetectDeath() {
        if (currentHealth <= 0) {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
