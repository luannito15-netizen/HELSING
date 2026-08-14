# HELSING

Pacote de continuidade do HELSING, um extraction action RPG PvE mobile inspirado em **Hellsing**, organizado para ser aberto no VS Code e permitir retomar o desenvolvimento sem depender do histórico da conversa.

## Estado atual

- Primeiro personagem jogável definido: **Nosferatu Alucard**.
- O protótipo 3D oficial **ALUCARD_PREALPHA_V01** está incorporado ao repositório com modelo mid-poly, rig, skinning, materiais e animações.
- Direção de câmera: **perspectiva 3/4 elevada**, na família de enquadramento de Diablo IV, sem câmera ortográfica/isométrica pura.
- Plataforma-alvo do Beta: **mobile landscape**.
- Motor: **Unity 6 (6000.5.8f1) + URP 17.5.0**, projeto real já inicializado em `unity/`.
- IDE de programação: **VS Code**.
- Blender permanece responsável por modelagem, rig e animações.
- O primeiro grande marco de gameplay é um **Playable Pre-Alpha** com movimentação, mira, disparo, troca de arma, dash, um poder e um inimigo básico.
- O primeiro gate crítico do produto de extração deve provar loadout exposto, loot, morte/extração, stash e persistência; sua ordem em relação ao marco jogável ainda exige reconciliação do Game Director.

## Comece por aqui

1. Leia `AGENTS.md` e `agents/specialists/README.md`.
2. Leia `handoff/AI_CONTEXT.md` e `handoff/HANDOFF_TO_VSCODE.md`.
3. Leia `docs/production/DECISIONS_LOG.md`, `docs/production/PROJECT_STATE.md` e `docs/production/NEXT_STEPS.md`.
4. Carregue o perfil especialista adequado à tarefa.
5. Para trabalho técnico no Unity, consulte `docs/technical/UNITY_VSCODE_PIPELINE.md` e `docs/technical/UNITY_MCP.md`.
6. Consulte a documentação específica de personagem ou gameplay somente conforme o escopo.

Visão e arquitetura consolidadas:

- `docs/GAME_VISION.md`;
- `docs/gameplay/RUN_EXTRACTION_AND_ECONOMY.md`;
- `docs/technical/REVERSIBILITY.md`;
- `docs/production/PREALPHA_VALIDATION.md`.

## Importante

Este pacote contém documentação, referências visuais, o asset Blender oficial do Alucard, exports preservados, e um projeto Unity real.  
O source de produção atual está em `blender/characters/alucard/source/ALUCARD_PREALPHA_V01.blend`. A entrega externa original permanece preservada como fonte histórica, e o render antigo do blockout continua em `references/alucard/current_model/`.

## Projeto Unity

- `unity/` — **projeto Unity oficial e ativo** (único projeto de produção do HELSING). Unity 6000.5.8f1, URP 17.5.0, Input System 1.20.0, Unity MCP 10.0.0 instalado. Estrutura `Assets/_Game/` já existe, ainda sem scripts/prefabs de gameplay próprios.
- `unity-bootstrap/` — **LEGACY / DO NOT USE**. Projeto Unity anterior, preservado apenas como referência histórica. Não usar como base para desenvolvimento.

## Colaboração por agentes

Codex é o implementer principal e Unity MCP writer padrão. Claude Code é o reviewer principal e read-only por padrão; só implementa com `OWNER: CLAUDE CODE` explícito. O sistema oficial de especialistas e os estados `LOCKED`, `WORKING`, `OPEN` e `TUNING / OPEN` estão definidos em `agents/specialists/README.md`.
