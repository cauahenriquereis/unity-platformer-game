using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        index = 0;

        while (index < lines.Length)
        {
            yield return TypeLine();

            // Adiciona delays específicos entre as linhas
            float delay = GetDelayBetweenLines(index);
            yield return new WaitForSeconds(delay);

            NextLine();
        }

        //gameObject.SetActive(false);
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        index++;
    }

    float GetDelayBetweenLines(int currentIndex)
    {
        // Adiciona delays específicos com base no índice atual
        switch (currentIndex)
        {
            case 0:
                return 3f; // Delay de 3 segundos para a transição do primeiro para o segundo diálogo
            case 1:
                return 10f; // Delay de 5 segundos para a transição do segundo para o terceiro diálogo
            case 2:
                return 20f; // Delay de 6 segundos para a transição do terceiro para o quarto diálogo
            default:
                return 0f; // Nenhum delay padrão
        }
    }
}
