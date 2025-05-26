using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta classe controla a espada do jogador
public class Sword : MonoBehaviour
{
    // Prefab da animação de ataque da espada
    [SerializeField] private GameObject slashAnimPrefab;
    // Ponto de spawn da animação de ataque da espada
    [SerializeField] private Transform slashAnimSpawnPoint;
    // Collider da espada
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float swordAttackCooldown = 0.5f;

    private PlayerControls playerControls; // Referência para os controles do jogador
    private Animator myAnimator; // Referência para o Animator da espada
    private PlayerController playerController; // Referência para o controlador do jogador
    private ActiveWeapon activeWeapon; // Referência para a arma ativa do jogador
    private bool attackButtonDown, isAttacking = false; // Variáveis para controlar o ataque do jogador
    private GameObject slashAnim; // Referência para a animação de ataque da espada

    private void Awake() {
        // Inicializa as referências
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        // Habilita os controles do jogador
        playerControls.Enable();
    }

    void Start() {
        // Configura o evento de ataque do jogador
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update() {
        // Faz a espada seguir o mouse com um offset
        MouseFollowWithOffset();
        Attack();
    }

    private void StartAttacking()
    {
        // Ativa a variável de ataque
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        // Desativa a variável de ataque
        attackButtonDown = false;
    }

    private void Attack() {
        if (attackButtonDown && !isAttacking)
        {
            // Ativa a variável de ataque
            isAttacking = true;
            // Ativa a animação de ataque
            myAnimator.SetTrigger("Attack");
            // Ativa o collider da espada
            weaponCollider.gameObject.SetActive(true);
            // Cria a animação de ataque da espada
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            // Configura a rotação da animação de ataque
            slashAnim.transform.parent = this.transform.parent;
            // Configura a escala da animação de ataque
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        // Aguarda o tempo de cooldown do ataque
        yield return new WaitForSeconds(swordAttackCooldown);
        // Desativa a variável de ataque
        isAttacking = false;
    }

    public void DoneAttackingAnimEvent()
    {
        // Desativa o collider da espada
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent() {
        // Configura a rotação da animação de ataque para cima
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        // Inverte a animação se o jogador estiver virado para a esquerda
        if (playerController.FacingLeft) { 
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent() {
        // Configura a rotação da animação de ataque para baixo
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        // Inverte a animação se o jogador estiver virado para a esquerda
        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset() {
        // Faz a arma ativa seguir o mouse com um offset
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        // Rotaciona a arma ativa de acordo com a posição do mouse
        if (mousePos.x < playerScreenPoint.x) {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, angle);
        } else {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
