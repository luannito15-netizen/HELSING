# AI CONTEXT — HELSING

Use este arquivo como contexto persistente para um assistente de código.

Você está trabalhando no projeto HELSING, um jogo mobile de ação em Unity 6 + URP, programado em C# com VS Code. A câmera é 3/4 elevada estilo Diablo. O Beta é landscape e o primeiro personagem é Nosferatu Alucard.

Não reinvente decisões fechadas sem motivo técnico comprovado.

## ACTIVE UNITY PROJECT

`unity/` — projeto Unity oficial e ativo, único projeto de produção do HELSING. Já inicializado e aberto no Editor: Unity 6000.5.8f1, URP 17.5.0, Input System 1.20.0, Unity MCP (CoplayDev) 10.0.0. `Assets/_Game/` já existe, ainda sem scripts/prefabs de gameplay próprios (apenas `.gitkeep`). Única cena existente: `Assets/Scenes/SampleScene.unity`.

`unity-bootstrap/` = **LEGACY / DO NOT USE**. Projeto Unity anterior, preservado como referência histórica. Não usar como base de desenvolvimento nem apagar sem autorização.

## MULTI-AGENT SYSTEM

- ChatGPT = Game Director.
- Codex = default Implementer.
- Claude Code = default Reviewer.

Papéis podem ser trocados explicitamente por tarefa. Toda tarefa deve declarar ROLE, OWNER, WRITE SCOPE, READ SCOPE. Ver `agents/AGENT_COORDINATION.md` para o protocolo completo.

## UNITY MCP RULE

One writer at a time. Default: Codex = Unity MCP WRITE OWNER; Claude = Unity MCP READ/REVIEW. Claude só escreve no Unity quando uma tarefa futura declarar explicitamente `UNITY MCP WRITE OWNER: CLAUDE`.

## AUTOMATION RULE

Ordem de prioridade: MCP → arquivos/API → CLI/terminal. Evitar computer-use/controle genérico do Windows; usar apenas como último recurso explícito quando não houver alternativa estruturada.

## ALUCARD

`ALUCARD_PREALPHA_V01` está **FROZEN** para os primeiros testes de gameplay no Unity. Não realizar refinamento adicional do asset até que testes reais revelem necessidade concreta. Pendências conhecidas (escala ~2,19–2,38 m vs. 1,98 m LOCKED, FBX sem meshes/materiais da Jackal e Casull, avatar Humanoid não validado) permanecem registradas, mas não são tarefas imediatas.

## Estado 3D atual

O protótipo 3D oficial do Nosferatu Alucard está incorporado ao repositório HELSING como:

`blender/characters/alucard/source/ALUCARD_PREALPHA_V01.blend`

Não assumir que Alucard existe somente como placeholder, blockout isolado ou PNG. O source contém modelo mid-poly, rig de 40 bones, skinning, materiais, Jackal, Casull, câmeras de teste e as animações mínimas do Pré-Alpha.

Antes de trabalhar no personagem, consultar:

- `docs/character/ALUCARD_CHARACTER_BIBLE.md`;
- `docs/character/ALUCARD_BLOCKOUT_AND_3D.md`;
- `blender/characters/alucard/README.md`.

Limitações verificadas:

- altura geométrica aproximada de 2,19 m até o cabelo, divergente da altura LOCKED de 1,98 m;
- FBX existente sem os meshes/materiais da Jackal e Casull;
- integração Unity ainda OPEN.

Decisões fechadas:
- Alucard é o primeiro personagem.
- 1,98 m; corpo alto/esguio; braços/pernas longos.
- Chapéu vermelho de aba muito larga.
- Sobretudo vermelho até meio da canela.
- Óculos redondos, terno preto, gravata vermelha, luvas claras, botas pretas.
- Jackal preta na mão direita.
- Casull clara/prateada na mão esquerda.
- Controle mobile com joystick esquerdo, ataque principal grande, 2 skills, dash, weapon swap e Liberação.
- Ataque: toque usa auto-target; arrasto permite mira manual.
- Kit pré-run: 2 armas, 2 poderes de 4, 1 veste/equipamento, 1 configuração de Liberação.
- Sangue: recurso frequente para regeneração e poderes.
- Almas: máximo 3 no Beta, usadas em ressurreição e poderes como Familiar Sombrio.
- Poderes Beta: Predação, Familiar Sombrio, Marca Carmesim, Maré de Sangue.
- Builds: Gunslinger, Vampiro, Híbrido.
- Casull = precisão/ritmo/marca.
- Jackal = impacto/perfuração/anti-monstro/execução.
- Blender cria assets 3D; Unity é o motor; VS Code é a IDE principal.
- Não esperar o personagem final para testar gameplay.
- Existe um asset Pré-Alpha oficial do Alucard no repositório.
- A entrega externa original do Blender deve permanecer preservada.
- Sources Blender usam versionamento `ALUCARD_PREALPHA_V##` e, futuramente, `ALUCARD_GAMEPLAY_V##`.
- O arquivo Blender oficial não deve ser sobrescrito destrutivamente.

Primeiro milestone:
ALUCARD — PLAYABLE PRE-ALPHA 01

Deve permitir:
mover, mirar, atirar com as duas armas, trocar arma, dash, usar um poder e matar um inimigo simples.
