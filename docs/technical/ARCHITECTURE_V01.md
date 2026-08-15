# Arquitetura V01

Esta arquitetura é `WORKING`, inicial e substituível. `CORE-001` introduziu a fundação de input, movimento, aim/facing e câmera; `CORE-002` acrescentou adaptadores greybox para analógico virtual e drag manual. Os demais sistemas continuam sem runtime próprio. A visão define o resultado, e classes, componentes e dados são meios reversíveis.

Aplicar [Reversibility Architecture](REVERSIBILITY.md): contratos estáveis, responsabilidades pequenas, dependências explícitas e abstração somente quando houver necessidade concreta.

## Sistemas

### Player
Responsável por:
- movimento;
- estado do jogador;
- conexão com input.

Implementação atual: `PlayerMovement` consome intenção vetorial, usa `CharacterController` para deslocamento/colisão e aceita somente um `Transform` opcional como base planar. Velocidade, aceleração, desaceleração e gravidade são `TUNING / OPEN`.

`PlayerMovement` não controla rotação. Movimento e aim/facing são responsabilidades independentes; velocidade, aceleração, desaceleração e gravidade permanecem `TUNING / OPEN`.

### Input
Responsável por:
- joystick;
- ataque;
- aim drag;
- skills;
- dash;
- weapon swap;
- Liberação.

Implementação atual: `HelsingGameplay.inputactions` preserva WASD, setas, left stick e pointer. `PlayerInputReader` agrega esses bindings com `virtualMoveIntent` e `manualAimIntent`, dando prioridade aos adaptadores mobile somente enquanto estão ativos. Movimento e facing não conhecem controles concretos.

### Aim / Facing

Responsável por:

- converter uma intenção de mira em direção planar válida;
- orientar o Player sem mover o personagem ou controlar a câmera;
- preservar e expor a última `AimDirection` válida para armas e poderes futuros.

`LOCKED` — movimento não determina facing durante mira válida; mouse controla aim/facing no fallback desktop e o futuro arrasto manual touch alimentará o mesmo conceito de aim intent.

Implementação atual: `PlayerAimFacing` projeta `PointerPosition` por um raio da câmera ou converte `manualAimIntent` em direção planar relativa à câmera. Drag ativo tem prioridade; após release, o componente preserva brevemente a última direção antes de devolver controle ao mouse. A câmera é dependência explícita de projeção, mas não conhece o componente de mira. Velocidade, janela após release, plano versus superfície, layers e distância mínima são `TUNING / OPEN`.

### Mobile input UI

Responsável por:

- interpretar pointer/touch em intenção vetorial de movimento ou mira;
- apresentar somente feedback greybox dos controles;
- respeitar safe area e escala de resolução sem conter regras de gameplay.

Implementação atual: `VirtualJoystickControl`, `ManualAimDragControl`, `SafeAreaFitter` e `InputSystemUiBootstrap` compõem um Canvas overlay em `Prototype_Arena_01`. Os controles dependem somente de `PlayerInputReader`; não referenciam `PlayerMovement`, `PlayerAimFacing` ou `GameplayCamera`. Tap sem drag é deliberadamente inerte até auto-target ser autorizado.

### Camera

Responsável por:

- seguir o target de composição;
- aplicar configuração substituível de enquadramento em perspectiva 3/4 elevada;
- preservar leitura espacial sem controlar movimento ou targeting;
- adaptar apresentação e obstáculos sem alterar regras de gameplay.

O rig é um adaptador de apresentação. Movimento não lê seus valores internos, e targeting não depende de sua implementação concreta. Parâmetros permanecem `TUNING / OPEN`.

Implementação atual: `GameplayCamera` recebe apenas um target, mantém projeção Perspective e follow em `LateUpdate`. Distância orbital, altura, pitch, yaw, FOV, damping e offsets permanecem serializados como `TUNING / OPEN`.

### Targeting
Responsável por:
- encontrar alvos válidos;
- selecionar alvo automático;
- respeitar mira manual.

### Weapons
Responsável por:
- arma atual;
- Casull;
- Jackal;
- cadência;
- disparo;
- weapon swap.

### Combat
Responsável por:
- dano;
- hit;
- vida;
- morte.

### Resources
Responsável por:
- Sangue;
- Almas;
- Restrição/Liberação.

### Abilities
Responsável por:
- duas skills equipadas;
- cooldown/custo;
- execução de poderes.

### Enemies
Responsável por:
- dummy inicial;
- aquisição de alvo;
- deslocamento;
- ataque simples;
- morte.

## Implementação dos sistemas do loop de extração — `WORKING / FUTURE SCOPE`

Entram somente quando o gate correspondente for autorizado:

### Definitions

Responsável por IDs estáveis, validação e dados imutáveis necessários. Não criar registry transversal antes de haver mais de um consumidor real.

### Run

Responsável pelo ciclo temporário, terminal único e snapshot de settlement.

### Inventory / Loot

Responsável por propriedade exposta, capacidade, grants e transações; não grava diretamente no stash.

### Extraction

Responsável por definições substituíveis de rota, disponibilidade, requisitos, progresso da tentativa, cancelamento e solicitação de settlement; não contém inventário, stash, regras de save ou IO persistente.

### Profile / Persistence

Responsável por stash e progressão persistente com DTOs versionados e escrita atômica quando autorizado.

### Threat / Encounters

Threat mantém estado e emite contexto; encounters e loot leem modificadores sem mutar Threat diretamente.

### Economy / Crafting

Valida e confirma transações sem duplicação ou perda parcial.

## Regra arquitetural

Evitar construir frameworks complexos antes do primeiro playable.

Foco:
funcionar, testar, substituir e iterar.

Regras adicionais:

- separar definition imutável, runtime da run e profile persistente quando esses domínios existirem;
- input expõe intenções e não bindings concretos;
- movimento não escreve facing; aim/facing não move o Player nem controla a câmera;
- UI de input escreve intents no reader e não chama componentes de gameplay;
- câmera consome target/configuração próprios e não governa movimento ou targeting;
- UI observa estado e não possui regra de gameplay;
- apresentação de armas/poderes não define dano ou economia;
- cenas/prefabs não são fonte oculta de regras centrais;
- tuning fica localizado, com estado `TUNING / OPEN`;
- rotas de extração são módulos substituíveis sobre um único contrato terminal de run;
- qualquer schema de save, settlement ou service map transversal é `ARCHITECTURAL COMMITMENT` até revisão do Game Director/Unity Architect.
