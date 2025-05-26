using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]  // ExecuteInEditMode faz com que o script seja executado no Editor
public class Testes : MonoBehaviour
{
    // Header permite criar um cabeçalho para as variáveis no Inspector.
    // SerializeField permite que a variável seja visível no Inspector mesmo sendo privada. Ela é útil para testes e para configurar valores sem precisar mexer no código.
    // Range limita o valor da variável no Inspector
    // Tooltip permite adicionar uma descrição para a variável no Inspector
    [Header("Configurações de movimento")]
    [SerializeField]
    [Range(2.0f, 12.0f)]
    [Tooltip("Velocidade do movimento da espada.")]
    private float speed = 5.0f;

    //[Header("Configurações de vida")]
    //[SerializeField]
    //[Range(50, 200)]
    //[Tooltip("Vida da espada.")]
    //private int life = 100;

    public List<int> idades = new List<int>();

    void Start()
    {
        //Debug.Log("Speed: " + speed);
        //Debug.Log("Vida: " + life);
        idades.Add(10);
        idades.Add(20);
        idades.Add(30);
        foreach (var item in idades)
        {
            //Debug.Log("Idade: " + item);
        }

    }

    /// <summary>
    /// Update será chamado a cada frame. É onde a lógica do movimento da espada será implementada.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
