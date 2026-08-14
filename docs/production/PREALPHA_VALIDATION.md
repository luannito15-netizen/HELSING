# Pre-Alpha Validation

Este documento é a fonte canônica para perguntas de produto, telemetria mínima e regressões do Pré-Alpha. Todas as faixas numéricas são `TUNING / OPEN` até existir evidência de playtest.

## Perguntas de produto

1. O jogador entende o valor exposto antes de entrar?
2. Existe um momento real em que extrair se torna tentador?
3. Threat gera escolha, em vez de ser obrigatório ou irrelevante?
4. A morte cria um novo plano ou uma tentativa de recuperação, em vez de abandono?
5. Crafting e progressão criam objetivos espontâneos de farm?
6. Armas premium ampliam opções sem garantir sobrevivência?
7. O mapa produz escolhas de rota e identidade econômica?
8. A duração de uma run contém decisões suficientes sem fadiga?

## Gates de validação

### Foundation

Player controlável em greybox, câmera estável e ações independentes de bindings concretos.

### `CORE-001` — câmera

- enquadramento reconhecível como ARPG em perspectiva 3/4 elevada, na família visual de Diablo IV;
- Player, inimigos e projéteis legíveis em tela pequena;
- nenhuma deformação excessiva de lente;
- câmera estável durante movimento e dash;
- cenário não bloqueia persistentemente o Player;
- targeting permanece independente do rig;
- valores podem ser ajustados sem modificar gameplay;
- a câmera não é copiada pixel a pixel quando isso prejudica a legibilidade mobile.

Executar a matriz mobile landscape definida em `docs/production/NEXT_STEPS.md`. Altura, distância, pitch, yaw, FOV, damping, offsets, zoom, obstáculos e enquadramento exato permanecem `TUNING / OPEN`.

### Combat slice

O jogador mata e pode ser morto por um inimigo simples; arma consome o recurso definido; target é compreensível em touch/editor simulation; dash e feedback mínimo são legíveis.

### Extraction loop

O jogador entra com loadout, coleta loot, extrai, vê itens no stash e recupera o estado após reiniciar. Em outra run, morte não transfere patrimônio exposto. Este é um gate crítico do produto de extração; sua posição relativa ao marco jogável atual permanece `OPEN`.

Critérios do contrato `LOCKED`:

- a rota é escolhida e iniciada pelo jogador em ponto ou âncora física do mapa;
- iniciar a tentativa não garante sucesso nem consolida patrimônio;
- cada rota validada exige condição, recurso, exposição ou combinação desses elementos;
- nenhuma ação de menu ou botão global conclui a retirada fora de uma rota física válida;
- o settlement ocorre somente após a conclusão válida da extração;
- a arquitetura aceita mais de uma família de extração e permite trocar, adicionar ou remover rotas sem reconstruir run, inventário, stash ou save.

### Economy

Uma extração financia decisão relevante; morte altera a preparação seguinte; crafting não duplica nem perde recursos em fluxos normais.

### Threat

A mesma rota produz risco e recompensa perceptivelmente diferentes em estados de Threat distintos, sem tornar a escalada resposta automática.

### Content and UX

Um tester externo entende risco, objetivo, arma, recursos e extração sem instrução verbal do desenvolvedor.

## Telemetria mínima — `WORKING`

Eventos previstos:

- `run_start`: run, loadout, gear e objetivo;
- `poi_enter`: POI, tempo e Threat;
- `loot_pickup`: item, quantidade, valor e origem;
- `threat_change`: estado anterior/novo, causa e tempo;
- `ability_use`: ability, custo e contexto;
- `enemy_kill`: inimigo, origem do dano e Threat;
- `extract_start`: ponto, valor carregado, Threat e tempo;
- `extract_success`: duração e valor transferido;
- `death`: causa, local, valor exposto e Threat;
- `craft`: receita e custos;
- `recovery_attempt`: valor em risco e resultado.

Formato, nomes finais e payloads são `WORKING`. Telemetria é observadora e nunca modifica gameplay.

## Regressões obrigatórias

### Integridade econômica

- pickup não persiste antes da extração;
- iniciar, cancelar ou falhar uma tentativa não transfere loot;
- settlement de sucesso, morte ou abandono resolve uma única vez e de forma testável;
- morte e abandono não enviam patrimônio exposto ao stash;
- falhas parciais não duplicam nem apagam silenciosamente patrimônio;
- craft falho preserva inputs; craft válido não consome nem cria duas vezes;
- save/load preserva IDs e quantidades;
- Secure Slot rejeita classes proibidas;
- recuperação não duplica o equipamento original.
- se prototipada, a rota de emergência não funciona arbitrariamente em qualquer lugar e não torna a extração comum irrelevante;
- se prototipada, a rota de reforço só conclui após sua interação final válida.

### Threat

- mudanças de nível ocorrem apenas nos thresholds configurados;
- encontros, rewards e extração leem o estado atual sem mutá-lo diretamente;
- uma nova run reinicia o estado temporário;
- controles de debug permanecem isolados de builds de teste externas.

### Combate

- disparo consome o recurso configurado e falha visivelmente sem ele;
- morte de inimigo acontece uma vez;
- morte do Player e settlement terminal acontecem uma vez;
- auto-target ignora alvos mortos ou inválidos.

## Build gate

Uma build é inválida para teste de produto se:

- stash/save duplica ou apaga itens de modo imprevisível;
- extração ou morte resolve mais de uma vez;
- falha parcial deixa settlement ambíguo, duplica ou apaga patrimônio;
- fluxo normal de menu evita a consequência de morte;
- menu ou botão global permite extração instantânea;
- início da extração consolida patrimônio antes da conclusão válida;
- controles essenciais são instáveis no device alvo;
- logs não distinguem extração, morte e abandono.

`NEXT DESIGN TRIGGER: antes de implementar P2 — Extraction Loop, revisar rotas de extração, Threat, requisitos, duração, cancelamento, UX, settlement e integridade econômica.`

## Faixas exploratórias

Taxa de extração, tempo até decisão de saída, runs em Threat alto, retorno por loadout e recuperação após perda permanecem `TUNING / OPEN`. As faixas do Production Pack são hipóteses diagnósticas, não metas para otimização cega.

## Perguntas pós-sessão

- Quando você pensou em extrair pela primeira vez?
- O que fez você continuar?
- Qual item você mais temeu perder?
- Qual é seu próximo objetivo?
- Onde você buscaria o recurso necessário?
- Threat pareceu opcional, tentador ou obrigatório?
- Alguma morte pareceu injusta? Por quê?
- A interface escondeu alguma consequência importante?

Se o jogador não consegue identificar o próximo objetivo econômico sem ajuda, o sistema ainda não produz intenção suficiente.
