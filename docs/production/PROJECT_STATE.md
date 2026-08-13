# Project State

## Status geral

**Fase:** pré-produção jogável.

A direção deixou de ser apenas conceitual. O protótipo 3D Pré-Alpha do Alucard está oficialmente incorporado ao repositório como:

`ALUCARD_PREALPHA_V01`

Source:

`blender/characters/alucard/source/ALUCARD_PREALPHA_V01.blend`

O asset contém modelo mid-poly, armature, skinning, materiais, Jackal, Casull, câmera de teste e o conjunto mínimo de animações. A pasta externa da entrega original permanece preservada como fonte histórica.

O próximo passo continua dividido em duas frentes coordenadas:

### Frente A — Personagem
Validar e preparar o asset Pré-Alpha para integração:

- validar a discrepância entre a altura LOCKED de 1,98 m e os aproximadamente 2,19 m medidos até o cabelo;
- validar deformações e qualidade das animações na câmera real;
- revisar o FBX atual, que não contém os meshes/materiais das armas;
- preservar o source e criar novas versões apenas quando houver uma passada autorizada;
- não substituir `ALUCARD_PREALPHA_V01` destrutivamente.

### Frente B — Jogo
Começar o núcleo jogável com placeholder dentro do projeto Unity já inicializado:
- câmera;
- movimento;
- input mobile;
- mira;
- auto-target;
- armas;
- dash;
- inimigo básico.

O projeto Unity real já foi inicializado em `unity/` (Unity 6000.5.8f1, URP 17.5.0, Input System 1.20.0, Unity MCP 10.0.0). A estrutura `Assets/_Game/` já existe, mas nenhum script, prefab ou asset de gameplay próprio foi criado ainda — as pastas contêm apenas `.gitkeep`. Única cena existente: `Assets/Scenes/SampleScene.unity` (`Prototype_Arena_01` ainda não foi criada).

`unity-bootstrap/` é um projeto Unity anterior, classificado como **LEGACY / DO NOT USE** — não é a base ativa de desenvolvimento.

## Decisão de processo

Não esperar o personagem ficar finalizado para iniciar o jogo.

O gameplay deve ser testado cedo porque câmera, escala, controles e animação podem exigir alterações no modelo.

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
