using UnityEngine;

public class TurnBasedSorter : MonoBehaviour
{
    public StepByStepClimber[] capsulas;

    private int vezAtual = 0;

    void Update()
    {
        // Tecla K → sobe 1 degrau
        if (Input.GetKeyDown(KeyCode.K))
        {
            StepByStepClimber atual = capsulas[vezAtual];

            if (atual != null)
            {
                Debug.Log($"Cápsula {vezAtual + 1} sobe 1 degrau");
                atual.SubirDegraus(1);

                // Só depois que a cápsula se mover, troca a vez
                vezAtual = (vezAtual + 1) % capsulas.Length;
            }
        }

        // Tecla J → passa a vez sem andar
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log($"Cápsula {vezAtual + 1} passou a vez");

            vezAtual = (vezAtual + 1) % capsulas.Length;
        }
    }
}
