# HELSING — Current Handoff

## Handoff metadata

HANDOFF STATUS: VALID — integração documental concluída; checkpoint de publicação autorizado
HANDOFF REVISION: DERIVE FROM GIT HISTORY
WORKTREE AT HANDOFF: DIRTY — somente a mudança preexistente e excluída em `unity/ProjectSettings/PackageManagerSettings.asset` deve permanecer após o commit
IMPLEMENTER: CODEX
REVIEW MODE: CHECKPOINT
LAST REVIEW STATUS: NOT RUN — Claude Code indisponível
REVIEW SCOPE: integração da visão, contratos de extração/câmera, reversibilidade e roadmap
NEXT REVIEW TRIGGER: retorno do Claude Code ou próximo checkpoint relevante
LAST UPDATED: 2026-08-14
NEXT REQUIRED READS: `docs/production/DECISIONS_LOG.md`, `docs/gameplay/RUN_EXTRACTION_AND_ECONOMY.md`, `docs/gameplay/GAMEPLAY_CORE.md`, `docs/production/NEXT_STEPS.md` e `docs/production/PREALPHA_VALIDATION.md`

## Objective

Consolidar a visão do HELSING, registrar os contratos `LOCKED` de extração e câmera e preservar como abertos parâmetros, rotas concretas e ordem do roadmap.

## Last major change

A integração documental do Production Pack foi concluída. Extração agora possui contrato físico, não garantido e economicamente íntegro; a câmera possui família visual em perspectiva 3/4 elevada, configurável e desacoplada do gameplay. Nenhum runtime ou asset foi alterado.

## Decision state

- Extração: `LOCKED` — tentativa física escolhida/iniciada pelo jogador, sem garantia; patrimônio só consolida após conclusão válida; resolução terminal única/testável; falha parcial não duplica nem apaga; múltiplas famílias e rotas substituíveis.
- Câmera: `LOCKED` — perspectiva 3/4 elevada na família de Diablo IV, rotação diagonal fixa inicialmente, segue o Player, preserva leitura espacial e não acopla movimento/targeting ao rig.
- Valores de câmera: `TUNING / OPEN`. Detalhes das rotas de extração: `WORKING`, `OPEN` ou `TUNING / OPEN`.
- Reconciliação formal da visão de extração: `RESOLVED`.
- Ordem entre o marco `ALUCARD — PLAYABLE PRE-ALPHA 01` e o gate P2 — Extraction Loop: `OPEN`.
- Não reordenar Combat Slice, Extraction Loop, Jackal, weapon swap ou primeiro poder sem nova decisão do Game Director.

## Current state

Fundação Unity pronta em `unity/`; nenhum gameplay próprio implementado. `CORE-001` — movimento 360° + câmera configurável — é o próximo ticket seguro e inclui matriz mobile landscape.

O Alucard V01 permanece congelado. O projeto legado `unity-bootstrap/` permanece proibido.

## Validation performed

- Full Context, três documentos novos e diff completo: INSPECTED.
- Contratos `LOCKED`, estados abertos, caminhos de exploração, matriz `CORE-001` e gatilho P2: PASS.
- `git diff --check`, links locais, UTF-8/NFC, mojibake, contradições e escopo: PASS.
- Integridade do pack: PASS — 9 arquivos; SHA-256 agregado `7682BF9BF3246071602C62D97C2C4609BEC5ED399858BEC23A8F7F4560F4E869`.
- Imagem temporária não copiada: PASS.
- Unity, Unity MCP, Blender, Play Mode, build e testes de runtime: NOT RUN — fora do escopo.
- Review Claude: NOT RUN — Claude Code indisponível.

## Known issues

- `unity/ProjectSettings/PackageManagerSettings.asset` permanece dirty, preexistente, fora do stage e fora do escopo.
- O Console Unity observado no smoke test anterior continha uma exception de UnityConnect e um warning de WebSocket; não diagnosticados nesta tarefa documental.
- Review do checkpoint permanece pendente até retorno do Claude Code ou próximo gatilho relevante.

## Next owner action

Executar `CORE-001` com Unity Architect e Mobile Gameplay & UX. Antes de implementar P2, revisar rotas, Threat, requisitos, duração, cancelamento, UX, settlement e integridade econômica.

## Do not touch

- decisões `LOCKED` sem aprovação;
- Unity, Blender, packages ou `unity-bootstrap/` nesta integração;
- Production Pack somente leitura;
- `PackageManagerSettings.asset` sem diagnóstico;
- commit/push sem autorização explícita.
