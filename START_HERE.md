# START HERE

Objetivo: retomar o projeto sem depender do histórico da conversa.

Antes de agir, leia `AGENTS.md`, `agents/specialists/README.md`, `handoff/AI_CONTEXT.md`, `docs/production/DECISIONS_LOG.md`, `docs/production/PROJECT_STATE.md` e `docs/production/NEXT_STEPS.md`. Depois carregue o perfil especialista adequado.

## 1. O que estamos construindo

Um jogo de ação mobile com câmera 3/4 elevada, forte influência de leitura espacial de Diablo e esquema de controle inspirado em Wild Rift/Diablo Immortal.

O Beta começa com Nosferatu Alucard como personagem principal.

## 2. Próximo marco

**ALUCARD — PLAYABLE PRE-ALPHA 01**

Critério de saída:
- mover;
- mirar;
- atirar com Casull;
- atirar com Jackal;
- trocar arma;
- dash;
- usar pelo menos um poder;
- matar um inimigo simples;
- tudo funcionando na câmera real de gameplay.

## 3. O que NÃO é prioridade agora

- rosto final;
- texturas finais;
- selos das luvas;
- inscrições das armas;
- botões/costuras;
- hair cards;
- física sofisticada de tecido;
- cinematics;
- menus finais;
- mapa grande;
- árvore de progressão completa.

## 4. Ordem recomendada

1. Operar somente o projeto oficial já existente em `unity/`.
2. Declarar especialista, owner, escopo, estados de decisão e validação da sprint.
3. Implementar Player + Input + Camera com placeholder.
4. Implementar Targeting + Weapons + Dash.
5. Criar inimigo dummy e vida/dano/morte.
6. Integrar um poder provisório, mantendo valores como `TUNING / OPEN`.
7. Testar na câmera real e no celular cedo.
8. Integrar o Alucard somente depois que o loop com placeholder existir e a integração for autorizada.

`ALUCARD_PREALPHA_V01` está congelado. Não alterar o `.blend` nem reexportar o personagem sem evidência concreta do Unity e autorização específica.
