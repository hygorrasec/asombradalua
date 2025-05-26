using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Esta classe controla o jogador
public class PlayerController : Singleton<PlayerController>
{
    // Propriedade para verificar se o jogador está virado para a esquerda
    public bool FacingLeft { get { return facingLeft; } }

    // Velocidade de movimento do jogador (serializada para poder ser modificada no Editor Unity)
    [SerializeField] private float moveSpeed = 4f;

    // Velocidade do dash do jogador (serializada para poder ser modificada no Editor Unity)
    [SerializeField] private float dashSpeed = 4f;

    // Referência para o TrailRenderer do jogador (serializada para poder ser modificada no Editor Unity)
    [SerializeField] private TrailRenderer myTrailRenderer;

    private PlayerControls playerControls; // Referência para os controles do jogador
    private Vector2 movement; // Vetor de movimento do jogador
    private Rigidbody2D rb; // Referência para o Rigidbody2D do jogador
    private Animator myAnimator; // Referência para o Animator do jogador
    private SpriteRenderer mySpriteRender; // Referência para o SpriteRenderer do jogador
    private float startingMoveSpeed; // Variável para armazenar a velocidade inicial do jogador
    private bool facingLeft = false; // Variável para armazenar se o jogador está virado para a esquerda
    private bool isDashing = false; // Variável para armazenar se o jogador está realizando um dash

    /// <summary>
    /// A função Awake é chamada quando o script é inicializado.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// A função Start é chamada antes da primeira atualização do frame.
    /// </summary>
    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash(); // Adiciona a função Dash ao evento de dash
        startingMoveSpeed = moveSpeed; // Armazena a velocidade inicial do jogador
    }

    /// <summary>
    /// A função OnEnable é chamada quando o script é habilitado.
    /// </summary>
    private void OnEnable() {
        // Habilita os controles do jogador
        playerControls.Enable();
    }

    /// <summary>
    /// A função Update é chamada a cada frame e é responsável por capturar a entrada do jogador.
    /// </summary>
    private void Update() {
        // Captura a entrada do jogador
        PlayerInput();
    }

    /// <summary>
    /// A função FixedUpdate é chamada a cada frame, mas é sincronizada com a física do jogo.
    /// </summary>
    private void FixedUpdate() {
        // Ajusta a direção do jogador e o movimenta
        AdjustPlayerFacingDirection();
        Move();
    }

    /// <summary>
    /// A função PlayerInput é responsável por capturar a entrada do jogador.
    /// </summary>
    private void PlayerInput() {
        // Captura o movimento do jogador
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        //Debug.Log("Movimento: " + movement);

        // Define os parâmetros de animação do jogador
        myAnimator.SetFloat("player_move_x", movement.x);
        myAnimator.SetFloat("player_move_y", movement.y);
    }

    /// <summary>
    /// A função Move é responsável por movimentar o jogador.
    /// </summary>
    private void Move() {
        // Movimenta o jogador
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// A função AdjustPlayerFacingDirection é responsável por ajustar a direção do jogador de acordo com a posição do mouse.
    /// </summary>
    private void AdjustPlayerFacingDirection() {
        // Obtém a posição do mouse e a posição do jogador na tela
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // Verifica se o mouse está à esquerda do jogador e ajusta a direção do Sprite
        if (mousePos.x < playerScreenPoint.x) {
            mySpriteRender.flipX = true;
            facingLeft = true;
        } else {
            mySpriteRender.flipX = false;
            facingLeft = false;
        }
    }

    /// <summary>
    /// A função Dash é responsável por realizar o dash do jogador.
    /// </summary>
    private void Dash()
    {
        // Verifica se o jogador já está realizando um dash
        if (isDashing) return;

        moveSpeed *= dashSpeed; // Ajusta a velocidade do jogador para a velocidade do dash
        myTrailRenderer.emitting = true; // Habilita o TrailRenderer
        StartCoroutine(EndDashRoutine()); // Inicia a rotina de cooldown do dash
    }

    /// <summary>
    /// A função EndDashRoutine é responsável por controlar o cooldown do dash.
    /// Está sendo utilizado IEnumerator para que a função possa ser chamada de forma assíncrona.
    /// Assincronia é a capacidade de executar tarefas em paralelo com o restante do código.
    /// </summary>
    /// <returns></returns>
    private IEnumerator EndDashRoutine()
    {
        float dashDuration = 0.2f; // Duração do dash
        float dashCooldown = 0.25f; // Cooldown do dash

        yield return new WaitForSeconds(dashDuration); // Aguarda a duração do dash
        moveSpeed = startingMoveSpeed; // Ajusta a velocidade do jogador para a velocidade normal
        myTrailRenderer.emitting = false; // Desabilita o TrailRenderer
        yield return new WaitForSeconds(dashCooldown); // Aguarda o cooldown do dash
        isDashing = false; // Permite que o jogador realize outro dash
    }
}
