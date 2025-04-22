using UnityEngine;

public class TurnBasedSorter : MonoBehaviour
{
    public StepByStepClimber[] capsulas; // Coloque as 2 cápsulas aqui no Inspector
    public int maxDegraus = 5;

    private int vezAtual = 0; // 0 = cápsula 1, 1 = cápsula 2

    void Update()
    {
        // Verifica se a tecla 'K' foi pressionada neste frame
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Obtém a referência ao script StepByStepClimber da cápsula que tem o turno atual
            StepByStepClimber atual = capsulas[vezAtual];

            // Verifica se a referência à cápsula atual é válida (não é nula).
            if (atual != null)
            {
                // Gera um número aleatório de degraus que a cápsula atual tentará subir
                int sorteado = Random.Range(1, maxDegraus + 1);

                // Envia uma mensagem para o Console da Unity informando qual cápsula sorteou quantos degraus
                Debug.Log($"Cápsula {vezAtual + 1} sorteou: {sorteado}");

                // Chama a função no script da cápsula atual para que ela tente subir o número sorteado de degraus
                atual.SubirDegraus(sorteado);

                // Alterna o turno só se a cápsula estiver disponível
                vezAtual = (vezAtual + 1) % capsulas.Length;
            }
        }
    }
}