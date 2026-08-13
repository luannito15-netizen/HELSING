# Unity MCP — HELSING

## Tool

CoplayDev MCP for Unity.

## Version

`v10.0.0`.

## Package

```text
https://github.com/CoplayDev/unity-mcp.git?path=/MCPForUnity#v10.0.0
```

## Project

- Projeto autorizado: `unity/`.
- Unity auditado: `6000.5.8f1`.
- URP auditado: `17.5.0`.
- Input System auditado: `1.20.0`.
- `unity-bootstrap/` é `LEGACY / DO NOT USE` e não deve ser operado.

## Purpose

O MCP pode ser usado para:

- inspecionar cenas;
- consultar GameObjects e componentes;
- trabalhar com prefabs e assets;
- integrar scripts;
- verificar o Console;
- executar Play Mode;
- realizar smoke tests;
- apoiar automação controlada do Editor.

## Authority

- O MCP é ferramenta de execução e inspeção, não autoridade de game design.
- Decisões seguem `AGENTS.md`, os perfis em `agents/specialists/` e a documentação oficial.
- Codex é o writer padrão.
- Claude Code é read-only por padrão e só escreve com ownership explícito.
- Apenas um writer pode operar o Unity MCP por vez.
- Nenhum acesso técnico amplia o `SCOPE` autorizado da tarefa.

## Safety workflow

### Antes de escrever

1. Confirmar que o projeto é `unity/`.
2. Confirmar `ROLE`, `OWNER`, `SCOPE` e `VALIDATION`.
3. Verificar o estado do repositório.
4. Identificar a cena e os assets exatos.
5. Inspecionar e preservar alterações preexistentes.
6. Confirmar que nenhum outro agente está escrevendo.

### Durante

- Fazer mudanças pequenas e reversíveis.
- Não tocar itens fora do escopo.
- Salvar cenas e prefabs conscientemente.
- Não atualizar packages sem autorização.
- Não criar, mover, renomear ou apagar assets incidentalmente.
- Não salvar mudanças desconhecidas ou misturá-las ao trabalho atual.

### Depois

- Aguardar importação e compilação.
- Verificar o Console.
- Executar Play Mode quando pertinente.
- Validar referências de cenas, prefabs e assets afetados.
- Remover objetos e assets temporários de teste.
- Listar todas as cenas, prefabs e assets salvos.
- Informar explicitamente testes não executados; nunca inferir `PASS`.

## Client status

Instalação no projeto, configuração do cliente, conectividade da CLI e disponibilidade das tools na conversa são estados distintos:

- `UNITY PROJECT PACKAGE: VERIFIED` — o pacote `v10.0.0` está declarado em `unity/Packages/manifest.json`.
- `CODEX UNITY MCP CONFIGURATION: VERIFIED` — a entrada `unityMCP` está habilitada na configuração atual do Codex.
- `CODEX UNITY MCP RUNTIME ACCESS: UNVERIFIED IN THIS TASK` — esta tarefa documental não abriu nem consultou o Unity Editor pelo MCP.
- `CLAUDE CODE UNITY MCP CONFIGURATION: VERIFIED` — a entrada local `UnityMCP` aponta para `http://127.0.0.1:8080/mcp`.
- `CLAUDE CODE UNITY MCP CLI CONNECTIVITY: VERIFIED ON 2026-08-13` — `claude mcp list` confirmou o servidor como conectado.
- `CLAUDE CODE ACTIVE CONVERSATION TOOL ACCESS: UNVERIFIED` — configuração e conectividade não provam que uma conversa já iniciada carregou as tools; confirmar dentro da sessão antes de depender delas.

Nenhum cliente deve declarar acesso funcional apenas porque o package está instalado. Cada cliente precisa validar configuração, conexão e disponibilidade das tools no contexto em que serão usadas.

## Prohibited operations

- Operar `unity-bootstrap/`.
- Alterar uma decisão `LOCKED`.
- Salvar mudanças desconhecidas.
- Usar dois writers simultaneamente.
- Configurar, registrar ou reconfigurar clientes Unity MCP sem tarefa explícita.
- Executar upgrades de Unity, URP, Input System, packages ou MCP sem autorização específica.
- Modificar o Alucard congelado sem problema concreto comprovado no Unity e autorização explícita.
- Usar controle genérico do Windows quando houver uma operação estruturada disponível.
- Declarar um teste como `PASS` quando ele não foi executado.
- Fazer commit ou push sem autorização explícita.

## Configuração observada do Codex

Na auditoria documental de 2026-08-13, a entrada `unityMCP` estava habilitada e configurada como proxy stdio para `http://127.0.0.1:8080/mcp`, usando `mcpforunityserver==10.0.0`. Essa observação não prova que o Editor, o servidor local ou a conexão runtime estejam ativos em uma sessão futura.
