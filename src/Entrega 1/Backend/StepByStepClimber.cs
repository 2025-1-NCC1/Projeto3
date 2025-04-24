using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepByStepClimber : MonoBehaviour
{
public Transform stairParent; // O objeto pai que agrupa todos os degraus da escada
public float stepSpeed = 0.5f;
public float pauseBetweenSteps = 0.2f;
public float offsetLateral = 0f; // Deslocamento lateral para evitar colisão entre cápsulas
public CameraFollower cameraFollower;
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

// Responsável por mover a cápsula de degrau em degrau
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
        
    

        Vector3 destinoComOffset = step.position + new Vector3(offsetLateral, 0f, 0f);

        while (Vector3.Distance(transform.position, destinoComOffset) > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinoComOffset, stepSpeed * Time.deltaTime);
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
    if (cameraFollower != null)
    {
        cameraFollower.SetTarget(this.transform);
    }

    // Inicia uma corrotina que espera 1 segundo antes de começar a subir
    StartCoroutine(EsperarESubirDegraus(quantidade));
}

private IEnumerator EsperarESubirDegraus(int quantidade)
{
    // Espera 1 segundo
    yield return new WaitForSeconds(1f);

    if (!isClimbing && degrauAtual < steps.Count)
    {
        StartCoroutine(ClimbSteps(quantidade)); // Inicia a subida
    }
}

// Opcional: método pra resetar a cápsula se quiser reiniciar a corrida
public void Resetar()
{
    if (steps.Count > 0)
    {
        transform.position = steps[0].position + new Vector3(offsetLateral, 0f, 0f);
        degrauAtual = 0;
        isClimbing = false;
    }
}
}