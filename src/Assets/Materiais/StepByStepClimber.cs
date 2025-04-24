using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepByStepClimber : MonoBehaviour
{
    public Transform stairParent;// O objeto pai que agrupa todos os degraus da escada
    public float stepSpeed = 2f;
    public float pauseBetweenSteps = 0.2f;

    private List<Transform> steps = new List<Transform>();  // Lista para armazenar os Transform de cada degrau
    private bool isClimbing = false; // Flag para indicar se a cápsula está atualmente subindo
    private int degrauAtual = 0; // Índice do degrau atual que a cápsula está prestes a subir ou já subiu

    void Start()
    {
        // Preenche a lista de degraus com os filhos do stairParent
        foreach (Transform step in stairParent)
        {
            steps.Add(step);
        }

        // Ordena os degraus por altura (Y)
        steps.Sort((a, b) => a.position.y.CompareTo(b.position.y));
        Debug.Log($"{gameObject.name}: Degraus encontrados: " + steps.Count);
    }

    void Update()
    {
        // Teste manual (subir todos os degraus de onde parou)
        if (Input.GetKeyDown(KeyCode.E) && !isClimbing)
        {
            StartCoroutine(ClimbSteps(steps.Count - degrauAtual));
        }
    }
    // responsável por mover a cápsula de degrau em degrau
    public IEnumerator ClimbSteps(int quantidade)
    {
        isClimbing = true;

        // Calcula quantos degraus ainda restam na escada
        int degrausRestantes = steps.Count - degrauAtual;

        // Garante que a quantidade a subir não seja negativa ou maior que os degraus restantes
        int degrausASubir = Mathf.Clamp(quantidade, 0, degrausRestantes);

        // Loop para subir a quantidade de degraus especificada
        for (int i = 0; i < degrausASubir; i++)
        {
            // Obtém o Transform do degrau atual para o qual a cápsula deve se mover
            Transform step = steps[degrauAtual];

            while (Vector3.Distance(transform.position, step.position) > 0.05f)
            {
                // Move a posição da cápsula em direção à posição do degrau a uma velocidade constante
                transform.position = Vector3.MoveTowards(transform.position, step.position, stepSpeed * Time.deltaTime);
                yield return null;
            }
            // Espera um pequeno intervalo de tempo entre a subida de cada degrau
            yield return new WaitForSeconds(pauseBetweenSteps);

            // Incrementa o índice do degrau atual, indicando que a cápsula subiu um degrau
            degrauAtual++;
        }

        isClimbing = false;
    }

    // Chamado externamente (pelo sorteador)
    public void SubirDegraus(int quantidade)
    {
        if (!isClimbing && degrauAtual < steps.Count)
        {
            StartCoroutine(ClimbSteps(quantidade));
        }
    }

    // Opcional: método pra resetar a cápsula se quiser reiniciar a corrida
    public void Resetar()
    {
        // Verifica se existem degraus na lista antes de tentar resetar
        if (steps.Count > 0)
        {
            // Move a cápsula para a posição do primeiro degrau
            transform.position = steps[0].position;

            // Reseta o índice do degrau atual para 0
            degrauAtual = 0;
            isClimbing = false;
        }
    }
}