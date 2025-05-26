using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Create New Dialogue")]
public class DialogueSettings : ScriptableObject  // ScriptableObject é uma classe que serve para criar objetos que podem ser salvos como arquivos asset
{
    [Header("Dialogue Settings")]
    public GameObject actor;

    [Header("Dialogue Text")]
    public Sprite speakerSprite;
    public string sentence;

    public List<Sentences> dialogues = new List<Sentences>();
}

[System.Serializable]  // Serve para que a classe possa ser vista no inspector
public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Language sentence;

}

[System.Serializable]  // Serve para que a classe possa ser vista no inspector
public class Language
{
    public string portuguese;
    public string spanish;
    public string english;
}

// Esse código abaixo serve para que o botão de criar um novo objeto do tipo DialogueSettings apareça no menu de contexto do projeto
#if UNITY_EDITOR

[CustomEditor(typeof(DialogueSettings))]
public class  BuilderEditor : Editor
{
    // Override serve para sobrescrever um método
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();  // Desenha o inspector padrão
        DialogueSettings ds = (DialogueSettings)target;  // Pega o objeto que está sendo editado
        Language l = new Language();  // Cria um novo objeto do tipo Language
        l.portuguese = ds.sentence;  // Atribui a frase do objeto DialogueSettings ao objeto Language

        Sentences s = new Sentences();  // Cria um novo objeto do tipo Sentences
        s.profile = ds.speakerSprite;  // Atribui a imagem do objeto DialogueSettings ao objeto Sentences
        s.sentence = l;  // Atribui o objeto Language ao objeto Sentences

        if (GUILayout.Button("Create Dialogue"))  // Se o botão "Add Dialogue" for clicado
        {
            if (ds.sentence != null)  // Se a frase do objeto DialogueSettings não for nula
            {
                ds.dialogues.Add(s);  // Adiciona o objeto Sentences à lista de diálogos do objeto DialogueSettings
                ds.speakerSprite = null;  // Reseta a imagem do objeto DialogueSettings
                ds.sentence = "";  // Reseta a frase do objeto DialogueSettings
            }
        }
    }
}


#endif
