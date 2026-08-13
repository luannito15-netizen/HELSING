# HELSING — Claude Code bootstrap

Este repositório é a fonte oficial do HELSING. Responder em português, salvo solicitação diferente.

## Papel padrão

- Claude Code é reviewer de checkpoints e riscos reais, read-only por padrão.
- Codex é o implementer principal e writer padrão do Unity MCP.
- Implementar somente com `OWNER: CLAUDE CODE` explícito.
- Apenas um agente pode escrever nos mesmos arquivos/assets ou pelo Unity MCP por vez.

## Contexto e roteamento

`AGENTS.md` é a regra central. Usar `/helsing-route-task`, executar o freshness check e tentar Fast Context primeiro:

1. `AGENTS.md`;
2. `handoff/CURRENT_HANDOFF.md`;
3. especialista principal;
4. arquivos diretamente envolvidos.

Usar Full Context quando qualquer gatilho de `AGENTS.md` ocorrer. O handoff é contexto operacional curto, não fonte de decisões nem autorização de escrita.

## Revisão por risco

- `NONE`: não revisar; Codex faz self-validation.
- `CHECKPOINT`: revisar uma vez no gatilho registrado, não após cada mudança.
- `REQUIRED`: revisar antes da progressão e exigir `PASS`.
- Review `PARTIAL` ou `BLOCKED` força Full Context.

Atualizar `CURRENT_HANDOFF.md` durante implementação somente com `OWNER: CLAUDE CODE`; em revisão read-only, apenas verificar e reportar.

## Regras permanentes

- Preservar `LOCKED`; identificar `WORKING`, `OPEN` e `TUNING / OPEN`.
- Usar somente `unity/`; `unity-bootstrap/` é `LEGACY / DO NOT USE`.
- Não modificar o Alucard congelado sem evidência do Unity e autorização.
- Preservar mudanças preexistentes e o escopo.
- Não configurar MCP, fazer commit/push ou modificar Blender sem tarefa explícita.

## Skills

- `/helsing-route-task`: contexto, risco, review mode, especialista e ownership.
- `/helsing-review-change`: revisão read-only quando o modo pedir.
- `/helsing-inspect-unity`: inspeção Unity read-only.
- `/helsing-owned-implementation`: escrita apenas com ownership explícito.

Perfis em `agents/specialists/` governam o domínio; skills governam o procedimento.
