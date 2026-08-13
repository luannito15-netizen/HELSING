---
name: helsing-route-task
description: Rotear toda tarefa do HELSING antes de analisar, revisar, documentar ou implementar. Usar no início de pedidos sobre gameplay, Unity, C#, Blender, Alucard, animação, combate, mobile, documentação ou coordenação para validar freshness, escolher Fast ou Full Context, classificar risco e review mode, selecionar especialista e bloquear escrita não autorizada.
---

# Rotear tarefa HELSING

## Verificar freshness

1. Ler `AGENTS.md` e `handoff/CURRENT_HANDOFF.md`.
2. Derivar a revisão do handoff com `git log -1 --format=%H -- handoff/CURRENT_HANDOFF.md`; se vazio, registrar `HANDOFF COMMIT: NONE — UNTRACKED`.
3. Inspecionar commits e mudanças posteriores à revisão derivada.
4. Comparar `git status --short` com `WORKTREE AT HANDOFF` e os arquivos conhecidos.
5. Ler `HANDOFF STATUS`, `REVIEW MODE`, `LAST REVIEW STATUS`, `NEXT REVIEW TRIGGER` e `NEXT REQUIRED READS`.
6. Tornar o handoff `STALE` se houver mudança relevante não documentada. Não invalidá-lo por mudança pequena que não exige atualização segundo `AGENTS.md`.

## Escolher contexto

Usar Fast Context quando o handoff for confiável e nenhum gatilho de Full Context existir. Ler `AGENTS.md`, handoff, um especialista principal, arquivos diretamente envolvidos e a entrada oficial de qualquer `LOCKED` pertinente.

Usar Full Context nos gatilhos de `AGENTS.md`, incluindo handoff ausente/`PARTIAL`/`STALE`, worktree incompatível, mudança relevante desconhecida, conflito `LOCKED`, nova sprint/arquitetura, review `PARTIAL`/`BLOCKED` ou `REQUIRED` sem `PASS`. Acrescentar `AI_CONTEXT`, `DECISIONS_LOG`, `PROJECT_STATE`, `NEXT_STEPS` e documentos pertinentes.

Fast Context nunca permite ignorar ou reinterpretar decisão `LOCKED`.

## Classificar tarefa, risco e review

Classificar `TASK TYPE` como `DESIGN`, `REVIEW`, `IMPLEMENT`, `VALIDATE` ou `DOCUMENT` e `RISK LEVEL` como `LOW`, `MEDIUM` ou `HIGH`.

- `REVIEW MODE: NONE`: mudança pequena/isolada sem impacto em contrato, decisão, runtime ou asset compartilhado. Codex faz self-validation.
- `REVIEW MODE: CHECKPOINT`: sequência de sprint ou sistema caminhando para estado testável. Trabalhar normalmente e agrupar review no gatilho.
- `REVIEW MODE: REQUIRED`: risco alto definido em `AGENTS.md`. Exigir review e `PASS` antes de progredir.

Qualquer review `PARTIAL` ou `BLOCKED` força Full Context. Bloquear somente quando o modo for `REQUIRED` sem `PASS`, o review estiver `PARTIAL`/`BLOCKED`, ownership faltar ou houver risco não resolvido. Não recomendar Claude automaticamente em `NONE`.

## Selecionar especialista e owner

Carregar um especialista principal: Combat Designer para combate; Unity Architect para Unity/runtime; Character & Animation TD para Blender/personagem; Mobile Gameplay & UX para touch/HUD/câmera. Carregar apoios somente por dependência real.

Antes de escrever, exigir `OWNER`, `SCOPE`, `OUT OF SCOPE`, `DECISION STATE` e `VALIDATION`. Claude permanece read-only sem `OWNER: CLAUDE CODE`. Nunca ampliar escopo por inferência.

## Entregar roteamento

Listar exatamente os arquivos lidos e usar:

```text
CONTEXT MODE
HANDOFF STATUS
FRESHNESS CHECK
TASK TYPE
RISK LEVEL
REVIEW MODE
NEXT REVIEW TRIGGER
PRIMARY SPECIALIST
OWNER
FILES READ
SCOPE
VALIDATION
BLOCKERS
```

Continuar quando contexto, ownership e risco permitirem. Escalar para Full Context sem pedir confirmação quando freshness falhar.
