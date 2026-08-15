# Next Steps

## Prioridade ativa — primeiro loop jogável

Especialista principal: **Unity Architect**. Implementação não começa automaticamente: cada sprint precisa de owner, escopo e validação declarados. O projeto oficial é `unity/`; `unity-bootstrap/` permanece `LEGACY / DO NOT USE`.

`REVIEW MODE: CHECKPOINT` — concluir um gate jogável antes de ampliar conteúdo. A arquitetura deve seguir `REVERSIBILITY FIRST` e [Reversibility Architecture](../technical/REVERSIBILITY.md).

## Referência de controle — WORKING, pendente de registro formal

O Game Director definiu em 2026-08-15 a família MOBA mobile — Mobile Legends, LoL Wild Rift — como referência de controle e câmera, com ressalvas a tratar durante o beta. A primeira consequência já implementada é o facing seguir o movimento quando não há mira manual, o que o `LOCKED` permite por restringir a independência a "durante mira válida". Nenhuma decisão `LOCKED` foi alterada. Registrar em `DECISIONS_LOG.md` antes que essa referência sustente outras decisões, para não virar contrato por acúmulo de implementação.

## Reconciliation gate — antes de fechar o roadmap da Sprint 01

`VISION / LOCKED ORDER RECONCILIATION — OPEN` — o Production Pack propõe um Combat Slice enxuto seguido imediatamente pelo loop de extração, enquanto o marco atual `ALUCARD — PLAYABLE PRE-ALPHA 01` exige também Jackal, weapon swap e um poder. Não remover nem adiar silenciosamente itens do marco. O Game Director deve decidir se haverá dois gates formais ou se o marco atual permanece indivisível.

Enquanto isso, `CORE-001` (movement + aim/facing + câmera) está `PASS`. `CORE-002` recebeu o smoke manual de touch simulado com `PASS` do Game Director e passou pela revisão read-only do Claude Code, que retornou `PARTIAL` com dois `P1` de ciclo de vida de input. As correções foram aplicadas e validadas no Editor: compilação, Console, reload de cena e câmera `PASS`, e o caminho de dois pointers `PASS VIA HARNESS` em Play Mode. Permanecem pendentes movimento e mira em runtime — bloqueados pelo player loop parado com o Editor sem foco — e o teste em device real.

### Unity
- [x] `DONE` — Criar projeto Unity 6 com URP (`unity/`, Unity 6000.5.8f1, URP 17.5.0).
- [x] `DONE / WORKING` — Configurar landscape mobile: portrait e portrait-upside-down desativados; auto-rotação mantida apenas entre `LandscapeLeft` e `LandscapeRight`.
- [x] `DONE` — Criar cena `Prototype_Arena_01`.
- [x] `DONE` — Criar Player placeholder com `CharacterController`.
- [x] `DONE` — Movimento 360° validado por automação e smoke manual com WASD, setas, diagonais, desaceleração e colisão.
- [x] `DONE` — Mouse aim/facing independente aprovado em smoke manual.
- [x] `DONE / TUNING OPEN` — Implementar câmera perspectiva 3/4 elevada, configurável e desacoplada.
- [x] `DONE / DEVICE TEST PENDENTE` — Joystick virtual esquerdo: automação, harness de dois pointers e smoke manual `PASS`; correção de ciclo de vida validada; resta apenas o teste em device real.
- [ ] `PARTIAL` — Ataque principal hitscan implementado: toque usa auto-target, arrasto mira e dispara continuamente, e toque sem alvo atira na direção atual. Dano, cadência, cobertura por obstáculo e distinção entre toque e toque fantasma validados em Play Mode. Falta o smoke manual do laço completo, que depende de frames, e a Jackal.
- [ ] `PARTIAL` — Auto-target: seleção implementada e validada (`AutoTargetSelector`, cone à frente com peso de ângulo/distância, gizmos, cadáver nunca selecionado). Falta ligá-la ao toque, o que depende do ataque principal. `Algoritmo final de target selection` permanece `OPEN`.
- [x] `DONE / DEVICE TEST PENDENTE` — Mira manual greybox por arrasto, sem auto-target/ataque: automação, harness de dois pointers e smoke manual `PASS`; correção de ciclo de vida validada; resta apenas o teste em device real.
- [ ] Implementar troca Casull/Jackal.
- [ ] Implementar dash.
- [x] `DONE` — Criar DummyEnemy: três dummies parados em `Gameplay/Dummies`, cada um com collider, `Health` e `Targetable`. Composição, sem script dedicado — um `DummyEnemy.cs` vazio só para nomear seria overengineering.
- [x] `DONE / TUNING OPEN` — Vida/dano/morte: `Health` com `TakeDamage` idempotente na morte, dano nunca acima do HP disponível e `Died` disparado uma única vez. `maxHealth = 100` é `TUNING / OPEN`. Falta o consumidor real — nenhuma arma aplica dano ainda.
- [ ] Implementar um poder provisório — escolha do primeiro poder `OPEN`; valores `TUNING / OPEN`.

## Roadmap por gates — WORKING

### P0 — Foundation

- `CORE-001`: `PASS` — movimento, mouse aim e câmera aprovados manualmente pelo Game Director;
- `CORE-002`: `PASS — REAL DEVICE PENDENTE` — automação e smoke manual `PASS`; os dois `P1` da revisão do Claude Code corrigidos e validados, com `MULTITOUCH HANDLER PATH: PASS VIA HARNESS` em Play Mode; compilação, Console, reload e câmera `PASS`; landscape mobile travado e fonte única de pointer resolvida; entrega de pointer, movimento, mira, recuperação de perda de foco e safe area aprovados em smoke manual do Game Director. Resta `REAL DEVICE: NOT RUN`;
- `CORE-003`: definitions/registry somente quando houver consumidor concreto.

Gate: Player controlável em greybox com input orientado a intenções.

#### `CORE-002` — histórico de validação

- Smoke manual de touch simulado pelo Game Director: `PASS` — oito direções do joystick, drag direito em múltiplos ângulos, movimento e mira simultâneos, retenção após release, colisão, câmera estável, layout landscape e Console sem novos erros.
- Revisão read-only do Claude Code: `PARTIAL`, com dois `P1` de ciclo de vida de input — joystick e drag de mira permaneciam presos quando o pointer-up não chegava (perda de foco, pausa ou cancelamento), travando movimento e bloqueando o fallback de mouse.
- Correções aplicadas: `ICancelHandler`, `OnApplicationFocus(false)`, `OnApplicationPause(true)` e reset idempotente em ambos os controles; `PlayerAimFacing` passou a registrar uma única mensagem quando não há câmera válida, em vez de falhar em silêncio.
- Harness de dois pointers criado em `unity/Assets/_Game/UI/Editor/MultiTouchHandlerHarness.cs`, exercitando os handlers reais com dois `pointerId` distintos — cenário que um mouse único na Game View não reproduz.
- `MULTITOUCH HANDLER PATH: PASS VIA HARNESS` — executado no Editor em 2026-08-14, **em Play Mode**, com os nove checks `PASS` e reproduzido em duas execuções independentes. Saída literal do Console:

```text
MULTITOUCH HANDLER HARNESS
PASS — left pointer drives movement
PASS — right pointer drives manual aim
PASS — both pointers active simultaneously
PASS — releasing left keeps aim active
PASS — releasing right clears aim
PASS — cancelling right keeps movement active
PASS — focus loss resets both controls
PASS — fallback gates released after reset
PASS — repeated reset stays idempotent
RESULT: PASS VIA HARNESS — REAL DEVICE: NOT RUN
```

- Smoke manual do Game Director após a correção, com a janela do Editor em foco: `PASS` — entrega de pointer pelo EventSystem ao joystick e à área de mira depois da troca do action asset; recuperação de perda de foco com pointer pressionado nos dois controles, que era exatamente o bug dos `P1`; WASD/setas nas oito direções, desaceleração e colisão; mouse aim, prioridade do drag e retomada do fallback após release; safe area em 2400×1080; Console sem erros.
- `REAL DEVICE: NOT RUN` — permanece como validação futura obrigatória; Game View com mouse fornece um único pointer e não substitui dois dedos simultâneos em aparelho real.
- `REAL DEVICE — MÉTODO DECIDIDO` pelo Game Director em 2026-08-14: build Android instalado no aparelho. O Device Simulator foi avaliado e descartado — exigiria o pacote `com.unity.device-simulator.devices` e continuaria fornecendo um único pointer, sem cobrir dois dedos simultâneos. O Unity Remote foi descartado por não ter suporte confiável ao novo Input System, que é o único caminho de input do projeto.
- Pré-requisito registrado: o Editor tem apenas `WebGLSupport` e `windowsstandalonesupport` instalados; `AndroidPlayer` não está presente. É necessário instalar o Android Build Support com SDK, NDK e OpenJDK pelo Unity Hub antes de qualquer build.
- Além do multi-touch, o teste em device é a primeira execução real dos bindings `<Touchscreen>/touch*/position` e `<Touchscreen>/touch*/press` acrescentados ao mapa `UI`: o smoke manual em desktop passou por `<Mouse>/position` e `<Mouse>/leftButton`, que são bindings distintos da mesma action.
- Compilação: `PASS` — importação e recompilação concluídas com zero `error CS` e zero `warning CS`. O harness foi importado pelo Unity (`GUID 8fbc4b64a2b973e4eb156f96bc1ddc69`) e o domain reload terminou sem erro.
- `.meta`: `PASS` — `MultiTouchHandlerHarness.cs.meta` e `Editor.meta` foram criados pelo Unity durante a importação (18:43:06 e 18:43:55), depois da abertura do Editor às 18:42:39 e depois da escrita do `.cs` às 18:38:51. Nenhum `.meta` foi escrito à mão. Os `.meta` de script do projeto são mínimos (`fileFormatVersion` + `guid`), padrão consistente em todos os scripts do repositório.
- Console: `PASS` — estado final com zero erros e zero warnings. O warning externo de Adaptive Performance aparece apenas em transições de Play Mode e não pertence ao projeto.
- Reload de `Prototype_Arena_01`: `PASS` — cena recarregada com `isDirty=False`, cinco raízes originais, zero missing scripts e referências preservadas (`PlayerInputReader` em `Player`; `MoveJoystick` e `ManualAimDragArea` com `inputReader`, `handle` e `visualRoot` resolvidos).
- Câmera: `PASS` — contrato `LOCKED` preservado em Play Mode: `orthographic=False` (Perspective), FOV `40`, rotação fixa `(55,00, 45,00, 0,00)` e distância orbital exatamente `14,0000` medida a partir do target offset `(0,1,0)`.
- Nenhum objeto ou debug temporário permaneceu na cena: `PASS` — o harness destrói a própria hierarquia; varredura de todos os `GameObject` em memória retornou zero componentes ausentes e zero resíduos.
- `MOVEMENT / AIM RUNTIME: NOT RUN` — **bloqueado, não reprovado**. Com o Editor sem foco e `Application.runInBackground=False`, o player loop não avança: `Time.frameCount` ficou preso em `2` e `Time.time` em `0,020`. Um intent virtual de movimento foi aplicado e o Player não se deslocou porque nenhum frame roda, não por falha do controle. `EditorApplication.QueuePlayerLoopUpdate()` também não destravou o loop. Validar exige foco humano na janela do Editor; alterar `Run In Background` é configuração de projeto e ficou fora de escopo.
- `HARNESS DEFECT — P2 ABERTO`: o menu item roda também em Edit Mode, onde o resultado é enganoso. Fora do Play Mode o Unity não executa `Awake()` em `MonoBehaviour` comum, então `VirtualJoystickControl.controlRect` e `ManualAimDragControl.interactionRect` ficam `null`, `UpdatePointer`/`TryGetLocalPoint` retornam cedo e os controles nunca ativam. A execução em Edit Mode retornou `RESULT: FAIL` com quatro `Assertion failed on expression: 'ShouldRunBehaviour()'` (o `SendMessage` de `OnApplicationFocus` não é entregue) e deixou um erro transitório `The referenced script (Unknown) on this Behaviour is missing!` no domain reload seguinte — erro que não se reproduz em ciclos de Play Mode com ou sem harness. Nessa execução os quatro `FAIL` eram artefato do contexto e os cinco `PASS` eram vazios, pois apenas afirmavam flags já `false`. Correção proposta e ainda não aplicada: guardar o menu item com `EditorApplication.isPlaying` (ou resolver os `RectTransform` sob demanda), para que o harness nunca possa reportar resultado sem significado.
- `P3` **RESOLVIDO** em 2026-08-14, com a causa corrigida em relação ao que estava registrado. O segundo asset não era criado por `InputSystemUiBootstrap`: sua condição `actionsAsset == null` já era falsa. Quem criava o `DefaultInputActions` era o próprio `InputSystemUIInputModule`, que roda também em Edit Mode por ser `[ExecuteAlways]`. Havia ainda uma terceira fonte não registrada: `Assets/InputSystem_Actions.inputactions`, o template do Unity 6, configurado como Project-wide Actions. Correção aplicada: um action map `UI` (`Point`, `Click`, `MiddleClick`, `RightClick`, `ScrollWheel`, `Navigate`, `Submit`, `Cancel`, `TrackedDevicePosition`, `TrackedDeviceOrientation`) foi acrescentado a `HelsingGameplay.inputactions` via API do Input System, com bindings de Mouse, Pen e `<Touchscreen>/touch*/` para preservar multi-touch; o módulo passou a apontar para esse asset de forma serializada; e `InputSystemUiBootstrap` foi removido da cena e do projeto. Verificado em Play Mode: assets de ações criados em runtime = `0`.
- `PROJECT-WIDE ACTIONS — OPEN`: `Assets/InputSystem_Actions.inputactions` continua definido como Project-wide Actions e não é lido pelo runtime do HELSING. Remover, substituir por `HelsingGameplay` ou mantê-lo é decisão de configuração de projeto, fora do escopo desta etapa.

Próximo checkpoint: `P0 — Foundation` está pronto no Editor e o `P1 — Combat Slice` pode começar pelo ataque principal. Continuam em aberto, sem bloquear o P1: o teste em device real, o defeito do harness (roda em Edit Mode e produz resultado sem significado) e a decisão sobre o Project-wide Actions.

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
