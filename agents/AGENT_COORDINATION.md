# HELSING MULTI-AGENT PROTOCOL

Este documento define como múltiplos agentes de IA colaboram no projeto HELSING sem conflitos.

## Papéis operacionais padrão

**GAME DIRECTOR**
ChatGPT / direção central do projeto. Decide visão, escopo e prioridades.

**CODEX**
IMPLEMENTER principal. Escreve código de gameplay, opera o Unity MCP como WRITE OWNER por padrão.

**CLAUDE CODE**
REVIEWER principal. Audita, documenta, revisa código e decisões. Read-only por padrão em relação a gameplay e ao Editor do Unity.

Esses papéis definem autoridade de execução. O domínio técnico de cada tarefa é definido por um dos perfis em `agents/specialists/`. Papéis e ownership podem ser alterados explicitamente por tarefa; nenhum agente deve assumir autoridade diferente da declarada.

## Declaração obrigatória por tarefa

Toda tarefa com escrita deve declarar, no mínimo:

```
ROLE:
OWNER:
REVIEWER:
WRITE SCOPE:
READ SCOPE:
OUT OF SCOPE:
DECISION STATE:
VALIDATION:
```

- **ROLE** — especialista principal: `COMBAT DESIGNER`, `UNITY ARCHITECT`, `CHARACTER & ANIMATION TD` ou `MOBILE GAMEPLAY & UX`.
- **OWNER** — agente autorizado a executar e, quando aplicável, escrever.
- **REVIEWER** — agente responsável pela revisão; por padrão, Claude Code.
- **WRITE SCOPE** — quais arquivos/pastas/sistemas o agente pode modificar nesta tarefa.
- **READ SCOPE** — o que o agente pode/deve ler para executar a tarefa (pode ser mais amplo que o WRITE SCOPE).
- **OUT OF SCOPE** — sistemas e artefatos que devem permanecer intocados.
- **DECISION STATE** — decisões `LOCKED`, `WORKING`, `OPEN` e valores `TUNING / OPEN` relevantes.
- **VALIDATION** — evidência observável necessária para concluir a tarefa.

Para uma revisão sem escrita, declarar `OWNER: NONE — READ ONLY` e manter `WRITE SCOPE: NONE`.

## Tipos de papel

### OWNER com autorização de escrita
Pode modificar **apenas** o que estiver dentro do `WRITE SCOPE` declarado. Qualquer alteração fora desse escopo exige nova autorização explícita.

### OWNER = NONE — READ ONLY
É read-only. Não modifica arquivos, não cria, não apaga e não salva cenas, prefabs ou assets. Produz auditorias, relatórios e recomendações.

## Unity MCP ownership

Ver `docs/technical/UNITY_MCP.md` e a seção dedicada em `handoff/AI_CONTEXT.md`. Resumo:

- Apenas **um** agente pode realizar operações de escrita via Unity MCP por vez.
- Default: **Codex = Unity MCP WRITE OWNER**.
- Default: **Claude = Unity MCP READ/REVIEW** (leitura de estado do projeto, cena ativa, hierarquia — nunca escrita).
- Claude só pode escrever no Unity quando uma tarefa futura declarar explicitamente:
  ```
  ROLE: UNITY ARCHITECT
  OWNER: CLAUDE CODE
  UNITY MCP WRITE OWNER: CLAUDE
  ```
- Codex e Claude nunca devem editar simultaneamente a mesma cena, prefab ou estado do Editor. Se houver dúvida sobre quem detém a escrita no momento, tratar como bloqueado e perguntar antes de agir.

## Ordem de automação

A ferramenta deve acompanhar o tipo de estado que está sendo alterado:

1. **Unity MCP** para ler ou alterar estado estruturado do Unity Editor: cenas, GameObjects, prefabs, componentes, Console e Play Mode.
2. **Arquivos/API direta** para documentação, código-fonte e configurações versionadas que não exigem o Editor.
3. **CLI/terminal** para busca, versionamento, build, testes e diagnósticos apropriados.
4. **Computer-use** (controle genérico do Windows) somente como último recurso explícito, quando não existir alternativa estruturada.

O Unity MCP não substitui edição direta de texto, e a CLI não deve manipular às cegas estado serializado do Editor.

## Especialistas oficiais desta versão

Os quatro perfis oficiais do primeiro playable estão em `agents/specialists/`:

- Combat Designer;
- Unity Architect;
- Character & Animation TD;
- Mobile Gameplay & UX.

O perfil define o domínio; `OWNER` e `REVIEWER` definem quem pode executar ou revisar. Perfis adicionais só devem ser criados quando um gargalo real justificar sua inclusão.
