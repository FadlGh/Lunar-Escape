using System.Collections;
using UnityEngine;
using TMPro;

public class TypingAnimation : MonoBehaviour
{
    [SerializeField] private float typingSpeed;
    private string originalText;
    private TMP_Text textComponent;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>(); 
        originalText = textComponent.text;
        textComponent.text = string.Empty;
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in originalText)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
