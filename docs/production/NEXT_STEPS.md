# Next Steps

## Prioridade ativa — primeiro loop jogável

Especialista principal: **Unity Architect**. Implementação não começa automaticamente: cada sprint precisa de owner, escopo e validação declarados. O projeto oficial é `unity/`; `unity-bootstrap/` permanece `LEGACY / DO NOT USE`.

`REVIEW MODE: CHECKPOINT` — concluir um gate jogável antes de ampliar conteúdo. A arquitetura deve seguir `REVERSIBILITY FIRST` e [Reversibility Architecture](../technical/REVERSIBILITY.md).

## Reconciliation gate — antes de fechar o roadmap da Sprint 01

`VISION / LOCKED ORDER RECONCILIATION — OPEN` — o Production Pack propõe um Combat Slice enxuto seguido imediatamente pelo loop de extração, enquanto o marco atual `ALUCARD — PLAYABLE PRE-ALPHA 01` exige também Jackal, weapon swap e um poder. Não remover nem adiar silenciosamente itens do marco. O Game Director deve decidir se haverá dois gates formais ou se o marco atual permanece indivisível.

Enquanto isso, `CORE-001` (movement + câmera) é compatível com ambas as sequências e continua sendo a primeira implementação recomendada.

### Unity
- [x] `DONE` — Criar projeto Unity 6 com URP (`unity/`, Unity 6000.5.8f1, URP 17.5.0).
- [ ] Configurar landscape mobile.
- [ ] Criar cena `Prototype_Arena_01`.
- [ ] Criar Player placeholder.
- [ ] Implementar movimento 8 direções.
- [ ] Implementar câmera 3/4 elevada.
- [ ] Implementar joystick virtual esquerdo.
- [ ] Implementar ataque principal.
- [ ] Implementar auto-target no toque.
- [ ] Implementar mira manual por arrasto.
- [ ] Implementar troca Casull/Jackal.
- [ ] Implementar dash.
- [ ] Criar DummyEnemy.
- [ ] Implementar vida/dano/morte.
- [ ] Implementar um poder provisório — escolha do primeiro poder `OPEN`; valores `TUNING / OPEN`.

## Roadmap por gates — WORKING

### P0 — Foundation

- `CORE-001`: movement 360° + câmera estável/configurável;
- `CORE-002`: abstração de input touch + fallback de editor;
- `CORE-003`: definitions/registry somente quando houver consumidor concreto.

Gate: Player controlável em greybox com input orientado a intenções.

#### `CORE-001` — matriz de câmera mobile landscape

Matriz `WORKING` de teste, não lista fechada de aparelhos:

| Resolução | Proporção representada | Foco |
|---|---:|---|
| 1280×720 | 16:9 | baseline e legibilidade mínima |
| 2400×1080 | 20:9 | celular largo e composição lateral |
| 2560×1600 | 16:10 | tablet e densidade espacial |

Em todas: validar estabilidade em movimento/dash, legibilidade de Player/inimigos/projéteis, ausência de deformação excessiva, obstáculos, independência do targeting e ajuste do rig sem modificar gameplay.

### P1 — Combat Slice

- health/damage;
- Casull inicial;
- auto-target + drag aim;
- ammo/reload quando aprovado;
- dash;
- Blood runtime mínimo;
- Ghoul/dummy com ataque e morte;
- feedback essencial.

Gate: matar e morrer em sandbox; target legível; custos e morte resolvidos uma vez.

### P2 — Extraction Loop

`NEXT DESIGN TRIGGER: antes de implementar P2 — Extraction Loop, revisar rotas de extração, Threat, requisitos, duração, cancelamento, UX, settlement e integridade econômica.`

- validar a proposta de cada rota contra o contrato `LOCKED` em `docs/gameplay/RUN_EXTRACTION_AND_ECONOMY.md`;
- item definition/runtime;
- inventário e loot mínimos;
- run state;
- morte e settlement;
- ponto ou âncora física de extração;
- seleção e tentativa de rota pelo jogador;
- profile/stash/save local;
- telemetria mínima.

Gate: iniciar a tentativa não consolida patrimônio; loot extraído aparece no stash somente após conclusão válida e exatamente uma vez; morte/falha não transfere patrimônio exposto; rotas podem evoluir sem reconstruir run, inventário, stash ou save. Qualquer boundary persistente exige revisão arquitetural antes da implementação.

### P3 — Economy

Crafting, credits/materiais, loadout value, Arsenal inicial, Secure Slot e tela de loadout. Tudo permanece `WORKING` ou `TUNING / OPEN` até o gate anterior funcionar.

### P4 — Threat

Threat 0–3, leitura no HUD, encontro/reward/extraction context. Thresholds e multiplicadores são `TUNING / OPEN`.

### P5 — Character Depth

Jackal, Anti-Freak se aprovada, poderes, Souls e Restrição/Liberação. Esta posição no roadmap depende da reconciliação do marco atual.

### P6 — Cheddar Content

Cheddar, POIs econômicos, extrações, famílias de inimigos, bosses, eventos, contratos e Last Death. `FUTURE SCOPE` para a Sprint 01.

### P7 — UX and Telemetry

Clareza do HUD, risco pré-run, morte/recuperação, onboarding, device pass e payloads completos.

### P8 — Balance

Somente com evidência: TTK, loot/valor por minuto, custo de reposição, Threat, extração, stash, Secure Slot, recuperação e recursos.

Critérios e regressões: [Pre-Alpha Validation](PREALPHA_VALIDATION.md).

### Blender / Alucard — FROZEN / DEFERRED

Nenhum item abaixo é tarefa ativa. `ALUCARD_PREALPHA_V01` permanece congelado até um teste real no Unity comprovar um problema concreto e uma nova tarefa autorizar a intervenção.

- [ ] `DEFERRED / PARTIAL` — Validar proporções e altura: o asset está alto/esguio, mas mede aproximadamente 2,19 m até o cabelo, acima dos 1,98 m LOCKED.
- [ ] `DEFERRED / PARTIAL` — Validar pernas, braços, tronco, cintura, mãos e botas contra as referências canônicas na câmera real.
- [x] Sobretudo dividido em costas, painéis frontais, caudas, capas, gola, lapelas e mangas.
- [x] Duas caudas traseiras separadas.
- [x] Massa simples de cabelo presente.
- [x] Jackal e Casull como objetos distintos, com dimensões, materiais e handedness próprios.
- [x] Mid-poly funcional incorporado como `ALUCARD_PREALPHA_V01`.
- [x] Rig humanoide provisório com 40 bones.
- [x] Bones auxiliares para capas, painéis e caudas do sobretudo.
- [x] Skinning provisório com Armature Modifiers, vertex groups e vértices ponderados.
- [ ] `DEFERRED / PARTIAL` — Revisar o FBX existente: personagem, rig e animações estão presentes, mas os meshes/materiais das armas não foram encontrados na importação.
- [ ] `DEFERRED` — Validar Avatar, deformações e materiais depois que o placeholder comprovar o loop e a integração do personagem for autorizada.

### Animação mínima
- [x] Idle — `ALU_Idle`.
- [x] Run — `ALU_Run`.
- [x] Strafe — `ALU_Strafe`.
- [x] Aim — `ALU_Aim`.
- [x] Fire Casull — `ALU_Fire_Casull`.
- [x] Fire Jackal — `ALU_Fire_Jackal`.
- [x] Dual fire — `ALU_DualFire`.
- [x] Dash — `ALU_Dash`.
- [x] Hit reaction — `ALU_Hit`.
- [x] Cast de poder — `ALU_CastPower`.
- [x] Activation de Liberação provisória — `ALU_ReleaseStart`.
