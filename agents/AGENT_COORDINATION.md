# HELSING MULTI-AGENT PROTOCOL

Este documento define como múltiplos agentes de IA colaboram no projeto HELSING sem conflitos.

## Papéis atuais (default)

**GAME DIRECTOR**
ChatGPT / direção central do projeto. Decide visão, escopo e prioridades.

**CODEX**
IMPLEMENTER principal. Escreve código de gameplay, opera o Unity MCP como WRITE OWNER por padrão.

**CLAUDE CODE**
REVIEWER principal. Audita, documenta, revisa código e decisões. Read-only por padrão em relação a gameplay e ao Editor do Unity.

Esses papéis podem ser alterados explicitamente por tarefa. Nenhum agente deve assumir um papel diferente do declarado sem que a tarefa diga isso explicitamente.

## Declaração obrigatória por tarefa

Toda nova tarefa atribuída a um agente deve declarar, no mínimo:

```
ROLE:
OWNER:
WRITE SCOPE:
READ SCOPE:
```

- **ROLE** — `IMPLEMENTER` ou `REVIEWER` (ou outro papel explicitamente definido).
- **OWNER** — qual agente é responsável por executar a tarefa.
- **WRITE SCOPE** — quais arquivos/pastas/sistemas o agente pode modificar nesta tarefa.
- **READ SCOPE** — o que o agente pode/deve ler para executar a tarefa (pode ser mais amplo que o WRITE SCOPE).

## Tipos de papel

### ROLE = IMPLEMENTER
Pode modificar **apenas** o que estiver dentro do `WRITE SCOPE` declarado na tarefa. Qualquer alteração fora desse escopo exige nova autorização explícita.

### ROLE = REVIEWER
É **read-only**. Não modifica arquivos, não cria, não apaga, não salva cenas/prefabs/assets. Produz auditorias, relatórios e recomendações.

## Unity MCP ownership

Ver seção dedicada em `handoff/AI_CONTEXT.md` (`## UNITY MCP RULE`). Resumo:

- Apenas **um** agente pode realizar operações de escrita via Unity MCP por vez.
- Default: **Codex = Unity MCP WRITE OWNER**.
- Default: **Claude = Unity MCP READ/REVIEW** (leitura de estado do projeto, cena ativa, hierarquia — nunca escrita).
- Claude só pode escrever no Unity quando uma tarefa futura declarar explicitamente:
  ```
  ROLE: IMPLEMENTER
  UNITY MCP WRITE OWNER: CLAUDE
  ```
- Codex e Claude nunca devem editar simultaneamente a mesma cena, prefab ou estado do Editor. Se houver dúvida sobre quem detém a escrita no momento, tratar como bloqueado e perguntar antes de agir.

## Ordem de automação

1. Arquivos/configuração direta.
2. CLI/terminal.
3. MCP.
4. Computer-use (controle genérico do Windows) — **somente como último recurso explícito**, quando não houver alternativa estruturada. Não faz parte do pipeline normal.

## Escopo desta versão

Esta versão do protocolo define apenas os três papéis atuais (Game Director, Codex, Claude Code). Agentes especialistas adicionais (Unity Lead, Combat Designer, Enemy AI, Technical Artist, etc.) serão definidos em uma etapa futura e não fazem parte deste documento ainda.
