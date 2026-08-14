# Project State

## Status geral

**Fase:** pré-produção jogável — visão de produto consolidada, fundação Unity pronta, primeiro loop ainda não implementado.

## Visão de produto consolidada

O Production Pack V1.0 foi auditado como intenção oficial do Game Director. HELSING passa a estar documentado como extraction action RPG PvE mobile: combate vampírico, risco de loadout, decisão de extração, economia/stash persistentes e escalada voluntária por Threat.

Essa visão não promove automaticamente mecânicas ou números para `LOCKED`. Contratos novos permanecem `WORKING`, questões sem decisão ficam `OPEN`, parâmetros são `TUNING / OPEN` e conteúdo posterior é `FUTURE SCOPE`.

Dois contratos transversais estão `LOCKED`: extração física, tentada e consolidada somente após conclusão válida; e câmera em perspectiva 3/4 elevada, na família de enquadramento de Diablo IV, configurável e desacoplada de movimento/targeting. Rotas concretas e valores do rig continuam abertos.

Fontes canônicas principais:

- visão: `docs/GAME_VISION.md`;
- run/economia: `docs/gameplay/RUN_EXTRACTION_AND_ECONOMY.md`;
- arquitetura reversível: `docs/technical/REVERSIBILITY.md`;
- validação: `docs/production/PREALPHA_VALIDATION.md`.

A direção deixou de ser apenas conceitual. O protótipo 3D Pré-Alpha do Alucard está oficialmente incorporado ao repositório como:

`ALUCARD_PREALPHA_V01`

Source:

`blender/characters/alucard/source/ALUCARD_PREALPHA_V01.blend`

O asset contém modelo mid-poly, armature, skinning, materiais, Jackal, Casull, câmera de teste e o conjunto mínimo de animações. A pasta externa da entrega original permanece preservada como fonte histórica.

O trabalho imediato está concentrado em uma frente ativa e uma frente congelada:

### Frente ativa — Jogo

Começar o núcleo jogável com placeholder dentro do projeto Unity já inicializado:
- câmera;
- movimento;
- input mobile;
- mira;
- auto-target;
- armas;
- dash;
- inimigo básico.

Próximo ticket seguro: `CORE-001` — movimento 360° e câmera configurável, validados na matriz mobile landscape sem fechar parâmetros de rig.

O primeiro gate de produto completo acrescenta loot, inventário, morte/extração, stash e persistência. A ordem entre esse gate e a conclusão integral do marco `ALUCARD — PLAYABLE PRE-ALPHA 01` está em `VISION / LOCKED ORDER RECONCILIATION — OPEN`; nenhuma implementação deve resolver essa prioridade silenciosamente.

Especialista principal: **Unity Architect**. Codex é o implementer padrão; Claude Code revisa em modo read-only por padrão.

### Frente congelada — Personagem

`ALUCARD_PREALPHA_V01` está **FROZEN FOR FIRST GAMEPLAY TESTS**. A discrepância de escala, o FBX sem os meshes/materiais das armas, o Avatar Humanoid e as deformações continuam documentados, mas não são tarefas ativas.

Não alterar o `.blend`, gerar V02, reexportar ou “corrigir preventivamente” o personagem. A frente só reabre quando um teste concreto no Unity demonstrar um problema e houver uma tarefa autorizada. Nesse caso, o especialista principal passa a ser **Character & Animation TD**, com apoio do Unity Architect.

O projeto Unity real já foi inicializado em `unity/` (Unity 6000.5.8f1, URP 17.5.0, Input System 1.20.0, Unity MCP 10.0.0). A estrutura `Assets/_Game/` já existe, mas nenhum script, prefab ou asset de gameplay próprio foi criado ainda — há somente pastas, seus `.meta` e placeholders de versionamento. Única cena existente: `Assets/Scenes/SampleScene.unity` (`Prototype_Arena_01` ainda não foi criada).

`unity-bootstrap/` é um projeto Unity anterior, classificado como **LEGACY / DO NOT USE** — não é a base ativa de desenvolvimento.

O sistema oficial de especialistas está ativo em `agents/specialists/`. As decisões `LOCKED` são preservadas; direções `WORKING`, questões `OPEN` e valores `TUNING / OPEN` devem continuar rotulados durante implementação e revisão.

## Decisão de processo

Não esperar o personagem ficar finalizado para iniciar o jogo.

O gameplay deve ser testado cedo porque câmera, escala, controles e animação podem revelar necessidades no modelo. Qualquer alteração continua sujeita à evidência, autorização e versionamento definidos para o Alucard congelado.

Agentes futuros não devem assumir que Alucard existe somente como placeholder ou render PNG. Antes de trabalhar no personagem, consultar:

- `docs/character/ALUCARD_CHARACTER_BIBLE.md`;
- `docs/character/ALUCARD_BLOCKOUT_AND_3D.md`;
- `blender/characters/alucard/README.md`.

## Marco imediato

**ALUCARD — PLAYABLE PRE-ALPHA 01**

Entrega funcional esperada:
- movimentação;
- câmera;
- Casull;
- Jackal;
- weapon swap;
- dash;
- um poder;
- inimigo simples;
- morte/dano básico.

## Gate de produto — WORKING

Após reconciliação de prioridade, provar com placeholder:

- loadout exposto;
- loot e inventário mínimo;
- decisão de extração;
- morte sem transferência indevida;
- stash persistente;
- reinício preservando somente o estado elegível.

Threat, economia expandida, Cheddar, Anti-Freak, Last Death e conteúdo adicional permanecem posteriores ou pendentes de promoção explícita de escopo.
