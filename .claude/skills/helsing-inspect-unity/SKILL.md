---
name: helsing-inspect-unity
description: Inspecionar e validar o projeto Unity oficial do HELSING em modo read-only. Usar para auditorias de cena, GameObjects, prefabs, scripts, importação do Alucard, Animator, Console, Play Mode, packages, referências e smoke tests quando Claude Code não é o implementer autorizado.
---

# Inspecionar Unity do HELSING

## Pré-condições

1. Executar `helsing-route-task` e carregar o especialista pertinente.
2. Confirmar `ROLE: REVIEWER`, `OWNER: NONE`.
3. Confirmar que o projeto é `unity/`.
4. Nunca operar `unity-bootstrap/` (`LEGACY / DO NOT USE`).
5. Confirmar que nenhum writer está alterando a mesma cena/asset durante a inspeção.

## Política read-only

Permitir:

- Consultar projeto, cena, hierarquia, componentes, prefabs e import settings.
- Ler Console, logs, scripts e referências.
- Entrar em Play Mode somente quando solicitado/necessário e seguro.
- Executar smoke tests que não persistam alterações.
- Capturar evidência para a revisão.

Proibir:

- Criar, renomear, mover ou apagar GameObjects/assets.
- Alterar componentes, valores, import settings, scripts, scenes ou prefabs.
- Salvar cena/asset.
- Reimportar tudo, atualizar packages ou mudar configuração do Editor.
- Deixar Play Mode, seleção ou objeto de teste em estado que prejudique outro agente.

Se uma ferramenta aparentemente read-only puder persistir mudanças, não usá-la sem ownership explícito.

## MCP indisponível

Não instalar nem configurar Unity MCP automaticamente.

Se o MCP do Claude Code não estiver disponível:

- inspecionar arquivos e logs acessíveis;
- registrar `UNITY MCP UNAVAILABLE`;
- classificar validações dependentes do Editor como `NOT RUN`;
- não substituir a inspeção estruturada por controle genérico do Windows.

## Roteiro de inspeção

Selecionar apenas itens pertinentes:

1. Confirmar versão do Unity, packages e projeto ativo.
2. Identificar cena aberta/alvo e estado salvo.
3. Inspecionar objetos, componentes e referências.
4. Verificar import/Avatar/Animator quando houver personagem.
5. Ler Console antes do Play Mode.
6. Executar cenário de teste aprovado.
7. Ler Console e estado do runtime depois.
8. Sair do Play Mode sem salvar mudanças.
9. Comparar resultado com critérios e decisões do especialista.

## Formato

```text
INSPECTION STATUS: PASS / PARTIAL / FAIL / BLOCKED
UNITY PROJECT / VERSION
SCENE / ASSETS INSPECTED
PLAY MODE TESTS
CONSOLE
FINDINGS
NOT RUN
PERSISTENT CHANGES: NONE
RECOMMENDED NEXT ACTION
```

Nunca declarar PASS para uma verificação não executada.
