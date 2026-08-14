# Recursos — Sangue, Almas e Restrição

## Sangue

Recurso de uso frequente.

Funções já definidas:
- regeneração;
- poderes.

Objetivo:
Criar tensão entre manter Alucard vivo e gastar recurso ofensivamente.

`WORKING` — Blood pode existir como recurso frequente da run e ser apenas parcialmente consolidável como patrimônio persistente. A proporção, o processamento e os sinks econômicos são `TUNING / OPEN`.

OPEN:
- valor máximo;
- como é obtido;
- regeneração passiva ou não;
- custo de cada poder;
- custo/velocidade da cura.

## Almas

Recurso raro.

### Beta
Máximo: **3 Almas**.

Usos já previstos:
- ressurreição;
- habilidades específicas, como Familiar Sombrio.

Objetivo:
Gerar decisões de alto impacto.

OPEN:
- como uma Alma é adquirida;
- se ressurreição gasta sempre 1;
- se Familiar Sombrio usa Alma temporariamente ou consome;
- o que acontece ao chegar a 0.
- se Almas não consolidadas ficam expostas à perda na run.

## Restrição / Liberação

É um dos recursos/estados centrais do personagem.

Referência canônica:
níveis de restrição descem em direção a maior liberação de poder.

Para gameplay, a implementação exata ainda está OPEN.

Princípios:
- Liberação deve alterar claramente sensação e presença.
- Precisa ter benefício concreto de combate.
- Não transformar Level Zero em botão comum de rotina no início.
- Estado máximo pode ser reservado para momentos especiais/expansão.

`VISION PRINCIPLE` — Liberação não deve ser uma ultimate gratuita de rotina; seu aumento de capacidade precisa de custo ou consequência observável.

## Threat — sistema distinto

`WORKING` — Threat representa a resposta crescente do mapa ao risco/poder escolhido pelo jogador. Não é sinônimo de Sangue, Almas ou Restrição.

Integrações devem ocorrer por comandos, eventos ou contexto explícito:

- powers/Liberação podem reportar ganho de Threat;
- encounters, loot e extração podem ler o estado de Threat;
- esses consumidores não alteram diretamente o estado interno do sistema.

Estados 0–3 são direção `WORKING`. Thresholds, ganhos, nomes, decaimento e modificadores são `TUNING / OPEN`.

O contrato completo de run está em [Run, Extraction and Economy](RUN_EXTRACTION_AND_ECONOMY.md).
