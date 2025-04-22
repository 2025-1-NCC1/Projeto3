# Projeto3

# Projeto 3 - Fecapolis

Fecapolis é um jogo educacional em estilo tabuleiro com minigames de perguntas e respostas, focado em ensinar sustentabilidade (economia de água e energia). A cada turno, um dos personagens é selecionado aleatoriamente para sortear quantos degraus ela irá subir, e em cada degrau existe uma pergunta com um nível de dificuldade diferente.


## Como Funciona

1.  Ao iniciar o jogo, o script `StepByStepClimber` em cada personagem encontrará todos os degraus filhos e os ordenará pela sua altura.
2.  O script `TurnBasedSorter` gerencia os turnos entre as duas cápsulas.
3.  Para avançar para o próximo turno, pressione a tecla **`K`**.
4.  Quando a tecla `K` é pressionada:
    * O `TurnBasedSorter` seleciona o personagem da vez.
    * Um número aleatório de degraus (entre 1 e o valor de `Max Degraus`) é sorteado para esse personagem.
    * O número sorteado é exibido no Console da Unity.
    * O script `StepByStepClimber` da cápsula sorteada inicia o movimento da cápsula para subir o número de degraus sorteado.
    * O turno é então passado para a outra cápsula.
    
