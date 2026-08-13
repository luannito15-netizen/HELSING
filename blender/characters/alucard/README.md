# Nosferatu Alucard — 3D Asset

## Current Production Version

`ALUCARD_PREALPHA_V01`

## Character

Nosferatu Alucard

## Height

1.98 m

Essa é a altura de referência LOCKED. A inspeção do source encontrou aproximadamente 2,19 m até o topo do cabelo e 2,38 m incluindo o chapéu; a escala deve ser validada antes da integração, sem redimensionamento destrutivo do source.

## Visual Base

- corpo alto e esguio;
- braços e pernas longos;
- chapéu vermelho de aba muito larga;
- óculos redondos;
- sobretudo vermelho;
- terno preto;
- gravata/cravat vermelha;
- luvas claras;
- botas pretas;
- cabelo preto.

## Weapons

Right Hand:  
Jackal

Left Hand:  
Hellsing Arms .454 Casull

No source, `ALU_Jackal` está parentada ao bone `Hand_R` e `ALU_Casull` ao bone `Hand_L`.

## Purpose

Asset Pré-Alpha para integração e testes de gameplay.

## Gameplay Camera

3/4 elevada, estilo Diablo.

O source contém `ALU_Camera_Gameplay_Mobile`, câmera perspectiva com lente de 58 mm, posição autorada em `(3.7, -6.0, 4.9)`, `COPY_LOCATION` no bone `Root` e `DAMPED_TRACK` para `ALU_Camera_Target_Mobile`.

## Source

`blender/characters/alucard/source/ALUCARD_PREALPHA_V01.blend`

## Original Delivery

`C:\Users\lcabr\OneDrive\Documents\Game Helsing\Pré‑Alpha jogável do Alucard`

A pasta externa é preservada, sem alterações, como entrega histórica produzida no Blender. O repositório contém cópias organizadas dos arquivos dessa entrega.

## Pipeline

```text
Blender
↓
FBX
↓
Unity 6 URP
↓
Animator / Runtime
↓
Gameplay
```

## Export Rules

- preservar escala;
- verificar transforms antes da exportação;
- não modificar destrutivamente o source;
- exports sempre em `/exports`;
- animações separadas quando isso trouxer vantagem operacional;
- preservar armature quando necessário;
- não sobrescrever versões anteriores.

## Versioning

Pré-Alpha:

```text
ALUCARD_PREALPHA_V01.blend
ALUCARD_PREALPHA_V02.blend
ALUCARD_PREALPHA_V03.blend
```

Produção gameplay:

```text
ALUCARD_GAMEPLAY_V01.blend
ALUCARD_GAMEPLAY_V02.blend
```

## Current Status

| Item | Status | Evidência |
|---|---|---|
| Proporção | PARTIAL | Silhueta alta/esguia presente; altura medida não coincide com 1,98 m. |
| Modelo | DONE | 42 meshes de personagem organizados por partes. |
| Mid-poly | DONE | Source e rig identificados como `ALUCARD_MIDPOLY`; 3.018 vértices e 3.400 polígonos no personagem, sem armas. |
| Chapéu | DONE | Aba, copa e faixa são objetos separados. |
| Cabelo | DONE | `ALU_HairMass` presente. |
| Sobretudo | DONE | Costas, painéis frontais, caudas, capas de ombro, gola, lapelas e mangas presentes. |
| Jackal | DONE | Objeto separado, material próprio, parentado a `Hand_R`. |
| Casull | DONE | Objeto separado, material próprio, parentado a `Hand_L`. |
| Materiais | DONE | 15 materiais no source; materiais específicos para roupa, pele, cabelo, luvas, botas, óculos e armas. |
| Rig | DONE | Armature `ALU_Rig_MidPoly` com 40 bones, incluindo mãos, dedos, chapéu, capas e caudas do sobretudo. |
| Skinning | DONE | Meshes deformáveis possuem Armature Modifier e vértices ponderados; chapéu e armas usam bone parenting. |
| Idle | DONE | Action `ALU_Idle`. |
| Run | DONE | Action `ALU_Run`. |
| Strafe | DONE | Action `ALU_Strafe`. |
| Aim | DONE | Action `ALU_Aim`. |
| Casull fire | DONE | Action `ALU_Fire_Casull`. |
| Jackal fire | DONE | Action `ALU_Fire_Jackal`. |
| Dual fire | DONE | Action `ALU_DualFire`. |
| Dash | DONE | Action `ALU_Dash`. |
| Hit reaction | DONE | Action `ALU_Hit`. |
| Cast | DONE | Action `ALU_CastPower`. |
| Liberação | DONE | Action de ativação `ALU_ReleaseStart`. |
| Export FBX | PARTIAL | FBX existente contém armature e animações, mas a inspeção não encontrou os meshes/materiais da Jackal e Casull. |
| Integração Unity | OPEN | Projeto Unity ainda não inicializado. |

## Existing Exports

- `exports/ALUCARD_PreAlpha_Character.fbx` — personagem, rig e animações; armas ausentes no conteúdo importado durante a inspeção.
- `exports/ALUCARD_PreAlpha_Character.glb` — personagem, rig, armas, materiais e 13 ações de personagem.
- `exports/ALUCARD_PreAlpha_Arena.glb` — vertical slice completo com personagem, arena, FX, alvos e câmeras.

Não gerar nem sobrescrever exports nesta etapa documental.
