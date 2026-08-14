# Reversibility Architecture

## Principle

`REVERSIBILITY FIRST` — preservar separações suficientes para substituir uma escolha provisória com impacto localizado e previsível. Isso não autoriza frameworks genéricos, abstrações sem uso ou infraestrutura antecipada.

Para cada sistema, distinguir visão, contrato observável e implementação. Hoje, nenhum gameplay próprio foi implementado em `unity/`; soluções citadas abaixo são direções, não descrição de runtime existente.

## Guardrails

- dados configuráveis quando existe benefício concreto de tuning ou reutilização;
- comportamento em componentes com responsabilidade clara;
- dependências explícitas e preferencialmente unidirecionais;
- input expõe intenções, não bindings concretos ao gameplay;
- apresentação observa estado por interfaces/eventos/view models;
- cenas e prefabs compõem sistemas, mas não escondem regras centrais;
- save usa DTOs versionados e IDs estáveis quando for implementado;
- nenhuma regra econômica é aplicada em dois lugares;
- não criar service locator, DI container, ECS, backend ou framework genérico sem necessidade demonstrada.

## System assessments

### Movement

- **PRODUCT VISION:** controle responsivo de Alucard em mobile landscape.
- **GAMEPLAY CONTRACT:** movimento 360°, previsível e independente dos valores internos ou da implementação concreta da câmera.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** contrato `WORKING`; velocidade e aceleração `TUNING / OPEN`.
- **REVERSAL PATH:** trocar `CharacterController`, Rigidbody ou solução própria atrás de um componente motor e uma entrada vetorial estável.
- **COUPLING RISK:** câmera, dash, animação e colisão.
- **MIGRATION COST:** `LOW` antes de Animator/root motion; `MEDIUM` depois.

### Targeting

- **PRODUCT VISION:** toque acessível com agência preservada por mira manual.
- **GAMEPLAY CONTRACT:** selecionar somente alvos válidos e permitir override por direção/arrasto.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** gesto `LOCKED`; algoritmo `OPEN`.
- **REVERSAL PATH:** manter coleta, scoring e seleção separados; consumidores recebem um alvo/aim intent, não detalhes do algoritmo.
- **COUPLING RISK:** input, armas, abilities, câmera e UI.
- **MIGRATION COST:** `MEDIUM`.

### Camera

- **PRODUCT VISION:** perspectiva 3/4 elevada com profundidade perceptível e leitura espacial adequada a tela pequena, na família de composição de Diablo IV sem cópia visual.
- **GAMEPLAY CONTRACT:** seguir o Player com rotação diagonal fixa inicialmente; manter Player, inimigos, projéteis, telegraphs, loot, rotas, POIs e extrações legíveis; não usar ortográfica/isométrica pura nem over-the-shoulder no gameplay normal.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** família visual, perspectiva, seguimento, legibilidade, desacoplamento e substituibilidade `LOCKED`; valores do rig `TUNING / OPEN`.
- **REVERSAL PATH:** câmera consome um target e configuração próprios; movimento e targeting não leem detalhes do rig, permitindo substituir sua implementação sem alterar gameplay.
- **COUPLING RISK:** targeting, HUD, telegraphs e escala visual.
- **MIGRATION COST:** `LOW` se parâmetros estiverem isolados.

### Touch input

- **PRODUCT VISION:** baixa carga cognitiva sem remover precisão avançada.
- **GAMEPLAY CONTRACT:** analógico esquerdo; ações do cluster direito; toque ataca por auto-target e arrasto mira manualmente.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** estrutura `LOCKED`; thresholds/layout `OPEN`.
- **REVERSAL PATH:** gameplay consome ações/intents; bindings touch e fallback de editor permanecem adaptadores substituíveis.
- **COUPLING RISK:** targeting, movement, abilities, HUD e device lifecycle.
- **MIGRATION COST:** `MEDIUM` se gameplay depender diretamente de callbacks de UI; `LOW` com actions centralizadas.

### Dash

- **PRODUCT VISION:** reposicionamento/evasão responsivo e legível.
- **GAMEPLAY CONTRACT:** ação direcional com início, deslocamento e fim claros.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** existência `LOCKED`; distância, duração, cooldown e invulnerabilidade `OPEN`/`TUNING / OPEN`.
- **REVERSAL PATH:** estado de dash solicita movimento/locks ao motor; invulnerabilidade, se aprovada, usa contrato separado de dano.
- **COUPLING RISK:** motor, dano, animação e input.
- **MIGRATION COST:** `LOW–MEDIUM`.

### Weapons

- **PRODUCT VISION:** armas com identidade, custo e valor econômico distintos.
- **GAMEPLAY CONTRACT:** arma ativa inequívoca, cadência controlada, recurso consumido e dano delegado; weapon swap preservado.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** Casull/Jackal e swap `LOCKED`; Anti-Freak `WORKING / FUTURE SCOPE`; números e ammo/reload `OPEN`.
- **REVERSAL PATH:** controller opera uma definição e um executor de disparo; hitscan/projétil e apresentação podem ser trocados sem alterar inventário ou health.
- **COUPLING RISK:** targeting, inventory, damage, animação, VFX e economia.
- **MIGRATION COST:** `MEDIUM`.

### Powers

- **PRODUCT VISION:** vampirismo cria decisões de alto impacto, não cooldowns gratuitos.
- **GAMEPLAY CONTRACT:** validar condição/custo atomicamente, executar efeito e reportar consequência/Threat.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** pool de quatro e dois slots `LOCKED`; comportamento e tuning `OPEN`.
- **REVERSAL PATH:** separar seleção/custo, targeting e efeito; cada ability depende de contratos pequenos, não de uma classe central.
- **COUPLING RISK:** recursos, targeting, combat, Threat, animação e UI.
- **MIGRATION COST:** `MEDIUM`.

### Damage and Health

- **PRODUCT VISION:** impacto legível e consequência confiável para Player e inimigos.
- **GAMEPLAY CONTRACT:** pedido de dano validado uma vez; hit, stagger e morte não resolvem duplicados.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** `WORKING`; fórmulas e tipos `OPEN`.
- **REVERSAL PATH:** fontes enviam um request; receptores devolvem resultado; feedback observa o resultado.
- **COUPLING RISK:** armas, powers, AI, dash, HUD e settlement de morte.
- **MIGRATION COST:** `MEDIUM`.

### Enemies and encounters

- **PRODUCT VISION:** composição cria problemas legíveis; Threat aumenta pressão sem depender apenas de HP.
- **GAMEPLAY CONTRACT:** inimigo possui vida, intenção e morte única; encounter controla orçamento/cap separadamente da AI individual.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** dummy/Ghoul inicial `WORKING`; famílias, bosses e quantidades `FUTURE SCOPE`/`TUNING / OPEN`.
- **REVERSAL PATH:** AI individual lê configuração e contexto; director decide composição por contratos, sem controlar estados internos do inimigo.
- **COUPLING RISK:** navigation, damage, loot, Threat e performance.
- **MIGRATION COST:** `MEDIUM`.

### Animation

- **PRODUCT VISION:** Alucard permanece reconhecível e ações têm causalidade/legibilidade.
- **GAMEPLAY CONTRACT:** gameplay solicita intenções; animação apresenta e sinaliza janelas sem ser única fonte de lógica crítica.
- **CURRENT IMPLEMENTATION:** source Blender existe; integração Unity `NONE`.
- **DECISION STATE:** asset V01 `FROZEN/LOCKED`; Humanoid/Generic, root motion e Animator `OPEN`.
- **REVERSAL PATH:** adaptar parâmetros/triggers entre domínio e Animator; manter fallback de timing fora de Animation Events críticos.
- **COUPLING RISK:** movement, weapons, powers e sockets.
- **MIGRATION COST:** `MEDIUM–HIGH` após produção de muitos clips/transitions.

### Resources and Threat

- **PRODUCT VISION:** Sangue, Almas, Liberação e Threat tornam poder uma escolha com custo.
- **GAMEPLAY CONTRACT:** cada recurso possui owner; custos são atômicos; outros sistemas leem estado ou enviam comandos explícitos.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** Sangue/Almas/Liberação e máximo de 3 Almas `LOCKED`; regras exatas e Threat `WORKING`/`OPEN`.
- **REVERSAL PATH:** separar resource stores e regras de ganho/gasto; não fundir Threat com Restrição.
- **COUPLING RISK:** powers, combat, run, UI, loot e progression.
- **MIGRATION COST:** `MEDIUM`.

### Inventory, loot and crafting

- **PRODUCT VISION:** loot muda rota, extração e objetivos de progressão.
- **GAMEPLAY CONTRACT:** definitions são imutáveis; instâncias/stacks representam propriedade; mutações são transacionais.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** domínio `WORKING`; grid, recipes, raridade e Secure Slot `TUNING / OPEN`.
- **REVERSAL PATH:** inventário não conhece crafting ou RNG; loot retorna grants; crafting valida e confirma uma transação.
- **COUPLING RISK:** save, run settlement, UI e economy.
- **MIGRATION COST:** `HIGH` depois de dados persistidos.

### Run, extraction and save

- **PRODUCT VISION:** risco só vira progresso persistente por settlement válido.
- **GAMEPLAY CONTRACT:** extração é tentativa física iniciada pelo jogador por uma rota com requisito/risco; somente conclusão válida transfere patrimônio; sucesso, morte e abandono possuem resolução terminal única e testável; falha parcial não duplica nem apaga silenciosamente.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** contrato de extração `LOCKED`; famílias concretas, política detalhada de perda/desconexão e schema `WORKING`/`OPEN`/`TUNING / OPEN`.
- **REVERSAL PATH:** separar definição/avaliação da rota, `RunSnapshot`, settlement puro, profile DTO e IO; rotas podem ser adicionadas, substituídas ou removidas sem reconstruir run, inventário, stash ou save; migrações por `saveVersion`.
- **COUPLING RISK:** todos os sistemas econômicos e de progressão.
- **MIGRATION COST:** `HIGH`.
- **REVIEW:** `ARCHITECTURAL COMMITMENT — GAME DIRECTOR / UNITY ARCHITECT REVIEW REQUIRED` antes da persistência real.

### Progression and Base

- **PRODUCT VISION:** progressão estrutural persiste e cria metas de farm sem apagar consequência.
- **GAMEPLAY CONTRACT:** XP/blueprints/módulos concedem elegibilidade; unidades físicas ainda exigem recursos.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** `WORKING / FUTURE SCOPE`; custos e curvas `TUNING / OPEN`.
- **REVERSAL PATH:** Base consome serviços de profile/economy; módulos não ficam codificados em cenas.
- **COUPLING RISK:** inventory, save, crafting, contracts e UI.
- **MIGRATION COST:** `HIGH` após persistência.

### HUD and UX

- **PRODUCT VISION:** risco, custo, target e estado são compreensíveis em mobile sem aparência de painel administrativo.
- **GAMEPLAY CONTRACT:** UI observa facades/view models e nunca possui a regra de gameplay.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** informações essenciais `WORKING`; layout/feedback `OPEN`.
- **REVERSAL PATH:** presentation substituível sobre modelos de leitura estáveis; debug separado do produto.
- **COUPLING RISK:** todos os sistemas expostos ao jogador.
- **MIGRATION COST:** `LOW–MEDIUM` se a UI não mutar domínio diretamente.

### Balance

- **PRODUCT VISION:** escolhas têm trade-offs claros e risco recompensado sem solução dominante.
- **GAMEPLAY CONTRACT:** parâmetros relevantes são localizáveis, rotulados e observáveis por telemetria.
- **CURRENT IMPLEMENTATION:** `NONE`.
- **DECISION STATE:** todo número do Production Pack é `TUNING / OPEN`, salvo valor já `LOCKED` em fonte oficial.
- **REVERSAL PATH:** dados simples por domínio; evitar uma tabela universal antes de existir necessidade.
- **COUPLING RISK:** baixo se dados estiverem localizados; alto se valores forem duplicados.
- **MIGRATION COST:** `LOW` com fonte única por parâmetro.

## Commitments requiring approval

Antes de implementar, exigem revisão explícita:

1. boundary final de run/profile/stash/save e schema persistente;
2. semântica de ownership e settlement de morte/extração;
3. arquitetura transversal de definitions/registry se virar dependência de todos os sistemas;
4. política de IDs e migração após o primeiro save distribuído;
5. framework central de services, event bus, DI ou data layer;
6. mudança de root motion/Avatar que acople animação ao domínio;
7. qualquer alteração de decisão `LOCKED` para acomodar a arquitetura.

## Review checklist

- A solução provisória está identificada como `CURRENT IMPLEMENTATION`, não como visão?
- O comportamento pode ser testado sem sua apresentação?
- A troca da solução afeta apenas o domínio e seus adaptadores?
- Existe uma única autoridade para estado/mutação?
- Valores importantes têm fonte única e estado explícito?
- O benefício do desacoplamento é concreto para o próximo gate?
- O desenho evita overengineering?
