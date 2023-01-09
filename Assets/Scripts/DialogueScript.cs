using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public GameObject dialogueUIBox;
    public List<MonoBehaviour> entities;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameComponent;
    public AudioSource nextText;

    [SerializeField]
    public Message[] lines;
    public float textSpeed;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        nextText.Play(0);
        // TODO: move this to a trigger for when level starts
        dialogueUIBox.SetActive(true);
        textComponent.text = string.Empty;

        foreach (MonoBehaviour entity in entities)
            entity.enabled = false;

        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (textComponent.text == lines[index].sentence)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index].sentence;
                nextText.Stop();
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        nextText.Play(0);
        nameComponent.text = lines[index].name;
        foreach (char c in lines[index].sentence.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        nextText.Stop();
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            foreach (MonoBehaviour entity in entities)
                entity.enabled = true;

            dialogueUIBox.SetActive(false);
        }
    }

    [System.Serializable]
    public class Message {
        public string name;
        public string sentence;
    }
}
