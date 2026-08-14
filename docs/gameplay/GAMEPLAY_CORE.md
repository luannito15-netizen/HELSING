# Gameplay Core

## Visão

Ação em tempo real com câmera 3/4 elevada dentro de incursões PvE de extração. O jogador controla Alucard diretamente em ambientes com leitura rápida de ameaça, movimentação responsiva, armas de fogo, poderes vampíricos e risco econômico legível.

## Câmera de gameplay — `LOCKED`

A câmera principal usa perspectiva 3/4 elevada, inclinação forte para o chão, profundidade visual perceptível e rotação diagonal fixa inicialmente. Ela segue o Player e preserva a leitura de inimigos, projéteis, telegraphs, loot, rotas, POIs e pontos de extração. Não usar projeção ortográfica/isométrica pura nem over-the-shoulder no gameplay normal.

Movimento e targeting consomem intenções e dados de mundo, não valores internos nem a implementação concreta do rig. Altura, distância, pitch, yaw, FOV, damping, offsets, zoom, obstáculos e adaptação por tela permanecem `TUNING / OPEN`.

## Micro loop

1. Localizar ameaça e posicionar-se.
2. Selecionar alvo ou mirar manualmente.
3. Atacar com a arma atual.
4. Trocar arma conforme a ameaça.
5. Usar dash para reposicionamento.
6. Gastar Sangue em regeneração ou poderes.
7. Administrar poderes, Almas e Liberação.
8. Coletar Blood/loot e decidir consumir, guardar ou avançar.

## Run loop — `WORKING`

`PREPARAR → ENTRAR → COMBATER/FARMAR → CONTROLAR OU ELEVAR THREAT → OBTER OBJETIVO/LOOT → CONTINUAR OU EXTRAIR → CONVERTER`

Extração é decisão do jogador, não encerramento automático por timer. Contratos dão intenção antes da run; eventos e loot criam tentações durante a run.

## Meta loop — `WORKING`

`EXTRAIR RECURSOS → MELHORAR BASE/ARSENAL → LIBERAR BLUEPRINT/SKILL → ASSUMIR MAIS RISCO → AMPLIAR BUILDS`

As regras de propriedade, morte, stash e persistência estão em [Run, Extraction and Economy](RUN_EXTRACTION_AND_ECONOMY.md).

## Princípio central do combate

A troca de arma deve fazer parte da decisão de combate.

### Casull
Favorece:
- ritmo;
- precisão;
- uso frequente;
- marcação.

### Jackal
Favorece:
- impacto;
- perfuração;
- alvos resistentes;
- execução.

## Kit de Beta

Antes da run:
- 2 armas;
- 2 poderes ativos escolhidos entre 4;
- 1 veste/equipamento;
- 1 configuração de Liberação.

Durante a run:
- dash fixo;
- troca entre Casull e Jackal;
- administração de Sangue e Almas.

## Builds de referência

### Gunslinger
Mais peso em armas, precisão, cadência e weapon swap.

### Vampiro
Mais peso em Sangue, regeneração e poderes.

### Híbrido
Combina os dois estilos.
