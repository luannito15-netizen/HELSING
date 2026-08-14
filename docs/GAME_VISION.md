# Game Vision

## Product vision

`VISION PRINCIPLE` — HELSING é um **extraction action RPG PvE mobile**, em landscape e câmera em perspectiva 3/4 elevada, centrado em incursões de alto risco, combate vampírico, economia persistente e escalada de poder controlada pelo jogador.

O jogador prepara um loadout real na Base Hellsing, entra em uma zona hostil, combate e coleta recursos, decide se aceita mais risco por mais recompensa e escolhe quando tentar uma rota física de extração. Somente a conclusão válida converte risco em progresso; iniciar a tentativa não garante retirada nem consolida patrimônio.

Esta definição expressa a intenção oficial de produto. Regras ainda não registradas como `LOCKED` permanecem `WORKING` ou `OPEN` nos documentos de sistema.

## Promessa central

Entrar carregando algo valioso, encontrar algo ainda mais valioso e decidir se vale arriscar tudo que está exposto por mais poder, objetivo ou recompensa.

## Fantasia do jogador

Controlar Nosferatu Alucard como um **Vampire Gunslinger** extremamente poderoso, mas sujeito a logística, recursos, restrições e risco real. Poder não elimina tensão: munição, Sangue, Almas, Threat, loot e equipamento carregado criam consequências.

Estados desejados em uma incursão:

1. preparação como cálculo;
2. entrada como cautela;
3. primeiro loot relevante como oportunidade;
4. escalada de poder como confiança perigosa;
5. retorno à extração como proteção do patrimônio;
6. Base como conversão de risco em progresso.

## Pilares

1. **Risco legível.** O jogador entende aproximadamente o que está expondo e perdendo.
2. **Poder tem custo ou consequência.** Armas, munição, Sangue, Almas e Liberação/Threat precisam gerar decisão.
3. **Extração é escolha e risco.** A run não termina por um timer de sobrevivência; o jogador escolhe quando e por qual rota física tentar sair, sem garantia automática.
4. **Progressão estrutural persiste.** A morte dói no patrimônio da run sem apagar conhecimento, XP, blueprints, Base e stash.
5. **O mapa participa da economia.** POIs possuem identidades de recursos e risco.
6. **Skill reduz risco, não o remove.** Execução melhora eficiência, mas não torna ganância ou loadout irrelevantes.
7. **Poucos recursos, usos concorrentes.** Evitar moedas artificiais e decisões sem trade-off.
8. **Sistemas antes de conteúdo.** Um personagem, um mapa e uma economia pequena devem provar o loop antes da expansão.
9. **Gunslinger sobrenatural.** Tiro, weapon swap e vampirismo precisam ser importantes, legíveis e satisfatórios.
10. **Mobile sem simplificação vazia.** Auto-target facilita entrada; mira por arrasto preserva agência.

## Anti-pilares

Não transformar o Pré-Alpha em:

- survivor de hordas orientado por timer;
- inflação genérica de item power;
- habilidades críticas gratuitas e infinitas;
- loot sem identidade ou risco observável;
- wipe estrutural completo na morte;
- PvP/multiplayer, live service ou monetização antecipados;
- construção livre de Base;
- múltiplos personagens ou mapas antes da validação do loop;
- acabamento de conteúdo usado para esconder falhas de sistema.

## Presença do personagem

Mesmo na câmera distante, Alucard deve permanecer reconhecível por chapéu, sobretudo, postura, armas e VFX. O asset `ALUCARD_PREALPHA_V01` continua congelado até evidência concreta no Unity e autorização.

## Família visual da câmera — `LOCKED`

A câmera usa perspectiva 3/4 elevada, inclinação forte para o chão, profundidade perceptível e rotação diagonal fixa inicialmente. Ela segue o Player e enquadra área suficiente para ler inimigos, projéteis, telegraphs, loot, rotas, POIs e extrações, sem assumir câmera ortográfica/isométrica pura ou over-the-shoulder no gameplay normal.

Diablo IV é referência somente de família visual para perspectiva, altura e composição; não autoriza cópia de assets, cenário, iluminação, interface, level design ou identidade visual. O contrato completo está em `docs/production/DECISIONS_LOG.md`. Parâmetros do rig e adaptações por tela permanecem `TUNING / OPEN`.

## Horizontes de validação

### Marco atual — `LOCKED`

`ALUCARD — PLAYABLE PRE-ALPHA 01`: mover, mirar, disparar Casull e Jackal, trocar arma, usar dash, usar ao menos um poder e matar um inimigo simples.

### Gate de produto — `WORKING`

Provar o circuito completo com um personagem, um mapa e economia pequena:

`PREPARAR → ENTRAR → COMBATER → COLETAR → DECIDIR → EXTRAIR OU MORRER → STASH/PERSISTÊNCIA → NOVA DECISÃO`

A ordem relativa entre concluir todo o marco atual e antecipar o gate de extração exige reconciliação registrada em `docs/production/DECISIONS_LOG.md`.

### Beta — visão de produto

O Beta deve provar controle, combate, identidade das armas, poderes, Sangue/Almas, Liberação, risco/extração, economia persistente e leitura mobile. Quantidade de conteúdo não é critério de validação.

## Future scope

`FUTURE SCOPE` — PvP/multiplayer, backend/cloud save, monetização, guildas/social, múltiplos personagens/mapas, durabilidade complexa, seguro completo, procedural generation complexo e live ops não pertencem ao Pré-Alpha atual.

Detalhes canônicos do loop e da economia estão em [Run, Extraction and Economy](gameplay/RUN_EXTRACTION_AND_ECONOMY.md).
