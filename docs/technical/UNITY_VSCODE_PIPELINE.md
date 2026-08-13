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

Direção atual:
**Unity 6 + URP**.

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

## Primeiro projeto Unity

Criar via Unity Hub:
- Unity 6;
- template URP;
- projeto 3D;
- Git recomendado;
- abrir a pasta raiz do projeto no VS Code.
