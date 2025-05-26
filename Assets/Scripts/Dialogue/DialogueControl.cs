using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{

    [Header("Componets")]
    public GameObject dialogueObj;  // Objeto que contém o diálogo
    public Image profileSprite;  // Imagem do perfil do ator
    public Text speechText;  // Texto do diálogo
    public Text actorNameText;  // Nome do ator

    [Header("Settings")]
    public float typingSpeed;  // Velocidade de digitação

    // Variáveis de controle
    private bool isShowing;  // Variável que controla se o diálogo está sendo mostrado
    private int index;  // Variável que controla o índice do diálogo
    private string[] sentences;  // Variável que controla a frase do diálogo

    public static DialogueControl instance;  // Variável estática que controla o diálogo

    // Awake é chamado sempre antes do Start na hierarquia de execução de scripts
    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);  // Yield é uma palavra-chave que é usada para pausar a execução de um método e retornar um valor
        }
    }

    // Método que vai servir para pular para a próxima fala.
    public void NextSentence()
    {

    }

    // Método que vai chamar a fala do NPC.
    public void Speech(string[] txt)
    {
        if (!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
}
