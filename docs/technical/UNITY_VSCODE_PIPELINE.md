# Unity + VS Code Pipeline

## Divisão de responsabilidades

### Unity
Responsável por:
- cenas;
- GameObjects;
- prefabs;
- câmera;
- física;
- animação em runtime;
- UI;
- VFX;
- áudio;
- build Android/iOS;
- testes do jogo.

### VS Code
Responsável por:
- scripts C#;
- arquitetura;
- organização;
- revisão de código;
- sistemas de gameplay.

### Blender
Responsável por:
- modelagem;
- UV;
- rig;
- skinning;
- animação;
- exportação de assets.

## Motor

Projeto oficial atual:
**`unity/` — Unity 6000.5.8f1 + URP 17.5.0 + Input System 1.20.0**.

O projeto já foi criado. `unity-bootstrap/` é `LEGACY / DO NOT USE`.

## Formato 3D
Padrão planejado:
**FBX**.

## Estrutura proposta

Assets/
└── _Game/
    ├── Characters/
    │   └── Alucard/
    ├── Combat/
    ├── Enemies/
    ├── Input/
    ├── Camera/
    ├── UI/
    ├── VFX/
    ├── Audio/
    ├── Core/
    └── Scripts/
        ├── Player/
        ├── Weapons/
        ├── Abilities/
        ├── Targeting/
        ├── Resources/
        └── Enemies/

## Estratégia

Programar gameplay com placeholder enquanto o Alucard evolui no Blender.

Depois substituir o placeholder pelo personagem sem reescrever o núcleo do jogo.

## Estado da fundação

- projeto Unity criado e validado em `unity/`;
- estrutura `Assets/_Game/` criada;
- Unity MCP 10.0.0 instalado;
- cena `Prototype_Arena_01` ainda não criada;
- scripts, prefabs e gameplay próprios ainda não implementados.

O próximo trabalho técnico é implementar o primeiro loop com placeholder dentro de `unity/`, sob o perfil Unity Architect. Operações do Editor seguem `docs/technical/UNITY_MCP.md`.
