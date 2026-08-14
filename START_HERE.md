# START HERE

Objetivo: retomar o projeto sem depender do histórico da conversa.

Antes de agir, leia `AGENTS.md`, `agents/specialists/README.md`, `handoff/AI_CONTEXT.md`, `docs/production/DECISIONS_LOG.md`, `docs/production/PROJECT_STATE.md` e `docs/production/NEXT_STEPS.md`. Depois carregue o perfil especialista adequado.

## 1. O que estamos construindo

Um extraction action RPG PvE mobile com câmera em perspectiva 3/4 elevada, combate vampírico, loadout exposto, decisão de extração e economia persistente. A família de enquadramento se inspira em Diablo IV e o controle em referências como Wild Rift/Diablo Immortal, sem cópia visual.

O Beta começa com Nosferatu Alucard como personagem principal.

Leia a visão consolidada em `docs/GAME_VISION.md` e o contrato reversível da run em `docs/gameplay/RUN_EXTRACTION_AND_ECONOMY.md`.

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

O Production Pack também define um gate crítico de loot → morte/extração → stash → persistência. A ordem entre antecipar esse gate e concluir integralmente o marco acima está em `VISION / LOCKED ORDER RECONCILIATION — OPEN`; não reordenar silenciosamente.

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
4. Implementar Targeting + Casull + Dash no Combat Slice mínimo.
5. Criar inimigo dummy e vida/dano/morte.
6. Aguardar a reconciliação do roadmap antes de escolher entre completar Jackal/weapon swap/poder ou iniciar o gate de extração.
7. Testar na câmera real e no celular cedo.
8. Integrar o Alucard somente depois que o loop com placeholder existir e a integração for autorizada.

`ALUCARD_PREALPHA_V01` está congelado. Não alterar o `.blend` nem reexportar o personagem sem evidência concreta do Unity e autorização específica.
