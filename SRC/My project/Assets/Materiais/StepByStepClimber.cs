using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepByStepClimber : MonoBehaviour
{
    public Transform stairParent;// O objeto pai que agrupa todos os degraus da escada
    public float stepSpeed = 2f;
    public float pauseBetweenSteps = 0.2f;

    private List<Transform> steps = new List<Transform>();  // Lista para armazenar os Transform de cada degrau
    private bool isClimbing = false; // Flag para indicar se a c�psula est� atualmente subindo
    private int degrauAtual = 0; // �ndice do degrau atual que a c�psula est� prestes a subir ou j� subiu

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
    // respons�vel por mover a c�psula de degrau em degrau
    public IEnumerator ClimbSteps(int quantidade)
    {
        isClimbing = true;

        // Calcula quantos degraus ainda restam na escada
        int degrausRestantes = steps.Count - degrauAtual;

        // Garante que a quantidade a subir n�o seja negativa ou maior que os degraus restantes
        int degrausASubir = Mathf.Clamp(quantidade, 0, degrausRestantes);

        // Loop para subir a quantidade de degraus especificada
        for (int i = 0; i < degrausASubir; i++)
        {
            // Obt�m o Transform do degrau atual para o qual a c�psula deve se mover
            Transform step = steps[degrauAtual];

            while (Vector3.Distance(transform.position, step.position) > 0.05f)
            {
                // Move a posi��o da c�psula em dire��o � posi��o do degrau a uma velocidade constante
                transform.position = Vector3.MoveTowards(transform.position, step.position, stepSpeed * Time.deltaTime);
                yield return null;
            }
            // Espera um pequeno intervalo de tempo entre a subida de cada degrau
            yield return new WaitForSeconds(pauseBetweenSteps);

            // Incrementa o �ndice do degrau atual, indicando que a c�psula subiu um degrau
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

    // Opcional: m�todo pra resetar a c�psula se quiser reiniciar a corrida
    public void Resetar()
    {
        // Verifica se existem degraus na lista antes de tentar resetar
        if (steps.Count > 0)
        {
            // Move a c�psula para a posi��o do primeiro degrau
            transform.position = steps[0].position;

            // Reseta o �ndice do degrau atual para 0
            degrauAtual = 0;
            isClimbing = false;
        }
    }
}