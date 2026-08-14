# Arquitetura V01

Esta arquitetura é `WORKING`, inicial e substituível. Nenhum gameplay próprio foi implementado no projeto Unity oficial até a consolidação deste documento. A visão define o resultado; classes, componentes e dados futuros são meios reversíveis.

Aplicar [Reversibility Architecture](REVERSIBILITY.md): contratos estáveis, responsabilidades pequenas, dependências explícitas e abstração somente quando houver necessidade concreta.

## Sistemas

### Player
Responsável por:
- movimento;
- estado do jogador;
- conexão com input.

### Input
Responsável por:
- joystick;
- ataque;
- aim drag;
- skills;
- dash;
- weapon swap;
- Liberação.

### Camera

Responsável por:

- seguir o target de composição;
- aplicar configuração substituível de enquadramento em perspectiva 3/4 elevada;
- preservar leitura espacial sem controlar movimento ou targeting;
- adaptar apresentação e obstáculos sem alterar regras de gameplay.

O rig é um adaptador de apresentação. Movimento não lê seus valores internos, e targeting não depende de sua implementação concreta. Parâmetros permanecem `TUNING / OPEN`.

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
- câmera consome target/configuração próprios e não governa movimento ou targeting;
- UI observa estado e não possui regra de gameplay;
- apresentação de armas/poderes não define dano ou economia;
- cenas/prefabs não são fonte oculta de regras centrais;
- tuning fica localizado, com estado `TUNING / OPEN`;
- rotas de extração são módulos substituíveis sobre um único contrato terminal de run;
- qualquer schema de save, settlement ou service map transversal é `ARCHITECTURAL COMMITMENT` até revisão do Game Director/Unity Architect.
