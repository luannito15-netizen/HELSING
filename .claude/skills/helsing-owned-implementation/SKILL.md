---
name: helsing-owned-implementation
description: Implementar uma tarefa do HELSING somente quando o pedido declarar explicitamente OWNER: CLAUDE CODE. Usar para alterações autorizadas em C#, documentação, Unity, assets ou configuração com escopo e validação definidos. Se ownership explícito estiver ausente, permanecer read-only e não modificar nada.
---

# Implementar tarefa owned no HELSING

## Bloqueio de autoridade

Procurar no pedido atual uma declaração explícita equivalente a:

```text
OWNER: CLAUDE CODE
```

Se não existir:

- não modificar arquivos, Unity, Blender ou configuração;
- assumir papel de reviewer;
- usar `helsing-route-task` para informar o contrato necessário.

Não inferir ownership de frases como “veja isso”, “revise”, “investigue” ou “o que acha?”.

## Preparar execução

1. Executar `helsing-route-task`.
2. Ler os especialistas e documentos obrigatórios.
3. Confirmar `SCOPE`, `OUT OF SCOPE`, estados de decisão e `VALIDATION`.
4. Inspecionar status do repositório e mudanças preexistentes.
5. Preservar trabalho do usuário e de outros agentes.
6. Confirmar que nenhuma decisão `LOCKED` precisa ser alterada.

Se uma escolha de produto bloqueante estiver `OPEN`, apresentar opções e escalar; não inventar decisão permanente.

## Implementar

- Alterar somente arquivos/assets necessários.
- Preferir solução pequena, reversível e proporcional ao pre-alpha.
- Marcar valores experimentais como `TUNING / OPEN`.
- Não adicionar packages, frameworks ou sistemas laterais sem autorização.
- Não alterar `unity-bootstrap/`.
- Não modificar o Alucard V01/source Blender sem autorização específica e versionamento novo.
- Não apagar ou sobrescrever mudanças desconhecidas.
- Não fazer commit, push ou criar PR sem pedido explícito.

## Unity MCP

Antes de escrever:

- confirmar que Claude Code é o único writer ativo;
- confirmar projeto `unity/`, cena e assets-alvo;
- inspecionar estado existente e referências.

Durante a escrita:

- fazer checkpoints pequenos;
- salvar conscientemente somente o que estiver no escopo;
- aguardar import/compilação antes de continuar.

Depois:

- verificar Console;
- executar Play Mode/smoke test pertinente;
- confirmar persistência de referências após reload quando necessário;
- remover objetos e debug temporários.

## Validar

Executar verificações proporcionais ao risco. Nunca declarar PASS com teste não executado. Registrar impedimentos como `NOT RUN` ou `BLOCKED`.

Solicitar revisão de Claude por outro reviewer ou Codex quando a tarefa modificar runtime sensível; não revisar a própria mudança como se fosse independente.

## Formato

```text
STATUS: PASS / PARTIAL / BLOCKED
ROLE / OWNER / REVIEWER
DECISION STATE
IMPLEMENTATION
VALIDATION
FILES CREATED
FILES MODIFIED
UNITY SCENES / ASSETS SAVED
CONSOLE
ISSUES / LIMITATIONS
LOCKED DECISIONS CHANGED: NONE
NEXT RECOMMENDED ACTION
```

Indicar uma única próxima ação e não executá-la sem autorização.
