# HELSING

Pacote de continuidade do projeto do jogo mobile inspirado em **Hellsing**, organizado para ser aberto no VS Code e permitir retomar o desenvolvimento sem depender do histórico da conversa.

## Estado atual

- Primeiro personagem jogável definido: **Nosferatu Alucard**.
- O protótipo 3D oficial **ALUCARD_PREALPHA_V01** está incorporado ao repositório com modelo mid-poly, rig, skinning, materiais e animações.
- Direção de câmera: **3/4 elevada, estilo Diablo**.
- Plataforma-alvo do Beta: **mobile landscape**.
- Motor: **Unity 6 (6000.5.8f1) + URP 17.5.0**, projeto real já inicializado em `unity/`.
- IDE de programação: **VS Code**.
- Blender permanece responsável por modelagem, rig e animações.
- O primeiro grande marco de gameplay é um **Playable Pre-Alpha** com movimentação, mira, disparo, troca de arma, dash, um poder e um inimigo básico.

## Comece por aqui

1. Leia `handoff/HANDOFF_TO_VSCODE.md`.
2. Leia `docs/production/PROJECT_STATE.md`.
3. Leia `docs/technical/UNITY_VSCODE_PIPELINE.md`.
4. Consulte `docs/character/ALUCARD_CHARACTER_BIBLE.md`.
5. Consulte `docs/gameplay/GAMEPLAY_CORE.md`.

## Importante

Este pacote contém documentação, referências visuais, o asset Blender oficial do Alucard, exports preservados, e um projeto Unity real.  
O source de produção atual está em `blender/characters/alucard/source/ALUCARD_PREALPHA_V01.blend`. A entrega externa original permanece preservada como fonte histórica, e o render antigo do blockout continua em `references/alucard/current_model/`.

## Projeto Unity

- `unity/` — **projeto Unity oficial e ativo** (único projeto de produção do HELSING). Unity 6000.5.8f1, URP 17.5.0, Input System 1.20.0, Unity MCP 10.0.0 instalado. Estrutura `Assets/_Game/` já existe, ainda sem scripts/prefabs de gameplay próprios.
- `unity-bootstrap/` — **LEGACY / DO NOT USE**. Projeto Unity anterior, preservado apenas como referência histórica. Não usar como base para desenvolvimento.
