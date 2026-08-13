# Handoff to VS Code

Copie este arquivo para o contexto do agente de código se precisar retomar o projeto sem histórico.

## Missão

Continuar o desenvolvimento do projeto HELSING como jogo mobile de ação em Unity.

## Stack atual
- Unity 6000.5.8f1 — projeto real já inicializado em `unity/` (URP 17.5.0, Input System 1.20.0, Unity MCP 10.0.0). Este é o **único projeto Unity oficial de produção**.
- `unity-bootstrap/` é um projeto Unity anterior, classificado **LEGACY / DO NOT USE** — não usar como base.
- C#.
- VS Code.
- Blender para assets 3D.
- FBX como formato padrão planejado de exportação.
- `Assets/_Game/` já existe em `unity/`, mas ainda sem scripts, prefabs ou gameplay próprios.

## Primeiro objetivo técnico

Criar um protótipo jogável com:
1. câmera 3/4 elevada;
2. movimento;
3. joystick mobile;
4. ataque principal;
5. auto-target no toque;
6. mira manual por arrasto;
7. Casull;
8. Jackal;
9. weapon swap;
10. dash;
11. DummyEnemy;
12. vida/dano/morte;
13. um poder.

## Personagem

Nosferatu Alucard.

### Visual
- 1,98 m.
- alto/esguio;
- pernas e braços longos;
- chapéu vermelho muito largo;
- óculos redondos;
- sobretudo vermelho até meio da canela;
- terno preto;
- gravata vermelha;
- luvas claras;
- botas pretas;
- Jackal preta na direita;
- Casull clara/prateada na esquerda.

### Estado 3D atual
O protótipo 3D oficial está incorporado ao repositório como:

`blender/characters/alucard/source/ALUCARD_PREALPHA_V01.blend`

Agentes futuros NÃO devem assumir que Alucard existe somente como placeholder, blockout isolado ou PNG.

Conteúdo verificado no source:

- modelo mid-poly organizado em 42 meshes de personagem;
- armature `ALU_Rig_MidPoly` com 40 bones;
- bones auxiliares para chapéu, capas, painéis e caudas do sobretudo;
- Armature Modifiers, vertex groups e pesos nos meshes deformáveis;
- materiais de Pré-Alpha para roupa, pele, cabelo, luvas, botas, óculos e armas;
- Jackal parentada a `Hand_R`;
- Casull parentada a `Hand_L`;
- Actions para idle, run, strafe, aim, disparos, dual fire, dash, hit, cast, weapon switch e ativação de Liberação;
- câmera mobile de teste e arena vertical slice.

Limitações verificadas:

- altura geométrica aproximada de 2,19 m até o cabelo, acima dos 1,98 m LOCKED;
- FBX existente contém personagem, rig e animações, mas não apresentou os meshes/materiais das armas na inspeção de importação;
- nenhuma integração Unity realizada.

O render antigo permanece preservado em:

`references/alucard/current_model/alucard_blockout_current.png`

Antes de trabalhar no personagem, consultar:

- `docs/character/ALUCARD_CHARACTER_BIBLE.md`;
- `docs/character/ALUCARD_BLOCKOUT_AND_3D.md`;
- `blender/characters/alucard/README.md`.

## Gameplay

### Mobile
Landscape.

### Controle
- joystick esquerdo;
- ataque grande à direita;
- toque = auto-target;
- arrasto = mira manual;
- 2 skills;
- dash;
- weapon swap;
- Liberação.

### Recursos
Sangue:
- uso frequente;
- cura e poderes.

Almas:
- máximo 3 no Beta;
- ressurreição e skills especiais.

Restrição/Liberação:
- estado central de poder;
- implementação exata OPEN.

### Armas
Casull:
- frequente;
- precisa;
- marca.

Jackal:
- pesada;
- perfuração;
- anti-monstro;
- execução.

### Poderes Beta
- Predação.
- Familiar Sombrio.
- Marca Carmesim.
- Maré de Sangue.

Equipar 2 de 4.

### Builds
- Gunslinger.
- Vampiro.
- Híbrido.

## Regra de desenvolvimento

Não esperar arte final.

Gameplay deve funcionar com placeholder e receber o Alucard depois.

Evitar overengineering antes de haver um loop jogável.

## Próxima tarefa futura no código

O projeto Unity 6 URP já está inicializado (`unity/`). A implementação de gameplay abaixo permanece bloqueada até nova autorização explícita — inicialização do Unity não é mais o bloqueio, autorização de sprint é.

Criar:
- `PlayerMovement`;
- `PlayerInputRouter`;
- `GameplayCamera`;
- `TargetingSystem`;
- `WeaponController`;
- `CasullWeapon`;
- `JackalWeapon`;
- `DashController`;
- `Health`;
- `DummyEnemy`.

Depois testar tudo em `Prototype_Arena_01`.
