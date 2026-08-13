# Alucard — Blockout e Estado 3D

## Asset oficial atual

O protótipo 3D Pré-Alpha foi concluído e incorporado ao repositório como:

`ALUCARD_PREALPHA_V01`

Source oficial:

`blender/characters/alucard/source/ALUCARD_PREALPHA_V01.blend`

A entrega histórica original permanece preservada em:

`C:\Users\lcabr\OneDrive\Documents\Game Helsing\Pré‑Alpha jogável do Alucard`

O render anterior continua preservado em `references/alucard/current_model/alucard_blockout_current.png` como referência histórica, mas não representa mais o único estado disponível do personagem.

## Direção visual preservada

- Altura de referência LOCKED: 1,98 m.
- Corpo alto e esguio.
- Braços e pernas longos.
- Chapéu vermelho de aba muito larga.
- Óculos redondos.
- Sobretudo vermelho até aproximadamente o meio da canela.
- Terno preto, gravata/cravat vermelha, luvas claras e botas pretas.
- Cabelo preto irregular.
- Jackal preta na mão direita.
- Casull clara/prateada na mão esquerda.

## Inspeção do source

Inspeção não destrutiva realizada com Blender 5.2 LTS, sem salvar o arquivo.

### Cena e organização

- Cena: `ALUCARD_MIDPOLY_VERTICAL_SLICE`.
- Unidades: sistema métrico, metros, `scale_length = 1.0`.
- 85 objetos, 70 meshes, 15 materiais e 24 Actions no arquivo completo.
- Collection raiz: `ALUCARD_MIDPOLY`.
- Collections funcionais: `BODY`, `CLOTHING`, `HEAD_HAIR_HAT`, `WEAPONS`, `RIG`, `FX`, `ARENA` e `CAMERA_TEST`.
- Personagem: 42 meshes, 3.018 vértices e 3.400 polígonos, sem contar armas, arena e FX.

### Modelo

O asset possui corpo, pescoço, rosto, nariz, boca, cabelo, óculos, chapéu, vestuário, luvas, botas e armas como componentes organizados.

O sobretudo está dividido em costas, painéis frontais, caudas, capas de ombro, golas, lapelas e mangas. Chapéu possui aba, copa e faixa separadas. Existe massa de cabelo funcional.

Classificação do modelo Pré-Alpha: `DONE`.

### Proporção e escala

A silhueta está alta e esguia, mas a medição geométrica atual não coincide com a altura LOCKED de 1,98 m:

- aproximadamente 2,19 m até o topo do cabelo;
- aproximadamente 2,38 m incluindo o chapéu;
- armature e objetos principais estão com scale `(1, 1, 1)`;
- arquivo configurado em metros.

Classificação da proporção: `PARTIAL`.

Não redimensionar automaticamente. A diferença deve ser validada em uma futura passada controlada.

### Armas

- `ALU_Jackal`: objeto separado, material `MAT_Jackal_Black`, parentado ao bone `Hand_R`.
- `ALU_Casull`: objeto separado, material `MAT_Casull_Silver`, parentado ao bone `Hand_L`.
- Existem sockets de muzzle separados para as duas armas.
- A Jackal possui dimensão longitudinal maior que a Casull no source.

A decisão LOCKED de handedness está implementada corretamente no `.blend`.

### Rig

- Armature: `ALU_Rig_MidPoly`.
- 40 bones.
- Estrutura humanoide com root, pelvis, coluna, peito, pescoço, cabeça, clavículas, braços, antebraços, mãos, dedos, coxas, canelas, pés e dedos dos pés.
- Bones auxiliares: `Hat`, `Cape_L`, `Cape_R`, `CoatFront_L`, `CoatFront_R`, `CoatTail_L` e `CoatTail_R`.
- Não foram encontradas pose constraints nos bones.

Classificação do rig Pré-Alpha: `DONE`.

### Skinning

- Os 39 meshes deformáveis possuem Armature Modifier apontando para `ALU_Rig_MidPoly`.
- Todos os vértices desses meshes possuem atribuição de peso.
- Chapéu e armas usam bone parenting em vez de Armature Modifier.
- Existem vertex groups específicos para dedos, membros, tronco, capas e partes do sobretudo.

Classificação do skinning Pré-Alpha: `DONE`.

### Animações

Actions de personagem realmente encontradas:

- `ALU_Idle`;
- `ALU_Run`;
- `ALU_Strafe`;
- `ALU_Aim`;
- `ALU_Fire_Casull`;
- `ALU_Fire_Jackal`;
- `ALU_DualFire`;
- `ALU_Dash`;
- `ALU_Hit`;
- `ALU_CastPower`;
- `ALU_ReleaseStart`;
- `ALU_WeaponSwitch`;
- `ALU_CombatDemo`.

As Actions possuem curvas e keyframes reais. O arquivo também inclui animações auxiliares para muzzle flashes, dash trail, regeneração, vampiric power e release ring.

Classificação das animações mínimas do Pré-Alpha: `DONE`.

### Materiais

Materiais específicos encontrados:

- sobretudo: `MAT_Coat_Crimson` e `MAT_Coat_Shadow`;
- roupa: `MAT_Cloth_Black`;
- pele: `MAT_Skin_Pale`;
- cabelo: `MAT_Hair_Black`;
- luvas e camisa: `MAT_Shirt_Gloves`;
- botas: `MAT_Boot_Leather`;
- óculos: `MAT_Glasses_OrangeRed`;
- Jackal: `MAT_Jackal_Black`;
- Casull: `MAT_Casull_Silver`.

Não existem texturas externas na entrega. Os materiais atuais são materiais de Pré-Alpha configurados no próprio arquivo.

Classificação dos materiais Pré-Alpha: `DONE`.

### Câmeras

- Câmera ativa: `ALU_Camera_Gameplay_Mobile`.
- Tipo: perspectiva.
- Posição autorada: `(3.7, -6.0, 4.9)`.
- Lente: 58 mm.
- `COPY_LOCATION` seguindo `ALU_Rig_MidPoly`, bone `Root`.
- `DAMPED_TRACK` mirando `ALU_Camera_Target_Mobile`.
- Câmeras auxiliares: `ALU_Camera_Close_ThreeQuarter` e `ALU_Camera_Front`.

A lente de 58 mm difere da faixa inicial de 40–45 mm anteriormente sugerida. Como os valores de câmera eram explicitamente experimentais, isso deve ser validado na futura cena Unity, não corrigido neste source durante a ingestão.

## Exports incorporados

- `blender/characters/alucard/exports/ALUCARD_PreAlpha_Character.fbx`.
- `blender/characters/alucard/exports/ALUCARD_PreAlpha_Character.glb`.
- `blender/characters/alucard/exports/ALUCARD_PreAlpha_Arena.glb`.

O GLB de personagem contém personagem, rig, Jackal, Casull, materiais e 13 ações de personagem. O GLB de arena contém o vertical slice completo, incluindo arena, FX, alvos e câmeras.

O FBX atual contém personagem, armature de 40 bones e animações, mas a inspeção de importação não encontrou os meshes nem os materiais da Jackal e da Casull. Por isso, `export FBX` permanece `PARTIAL` até uma futura exportação controlada.

## Status de produção

`ALUCARD_PREALPHA_V01` está **FROZEN FOR FIRST GAMEPLAY TESTS** (decisão LOCKED, ver `docs/production/DECISIONS_LOG.md`). Nenhum refinamento adicional deste asset até que os primeiros testes reais no Unity revelem necessidade concreta. Não alterar o `.blend`.

## Limitações atuais

- altura geométrica acima dos 1,98 m definidos;
- FBX atual sem os meshes/materiais das armas;
- nenhuma integração Unity realizada;
- nenhuma validação de Animator, avatar humanoide, root motion ou runtime;
- nenhuma textura externa fornecida;
- qualidade visual e deformações ainda precisam ser validadas na câmera e no dispositivo reais.

## Câmera de validação futura

Direção preservada:

- perspectiva;
- aproximadamente 35° afastada da vertical como ponto inicial;
- rotação fixa inicialmente;
- personagem ocupando aproximadamente 12–15% da altura útil da tela em gameplay normal.

Os valores definitivos serão definidos após validação no Unity 6 URP.
