# HELSING — Current Handoff

## Handoff metadata

HANDOFF STATUS: `CORE-001` PASS; `CORE-002` PASS — **`REAL DEVICE: PASS`** em 2026-08-15, no POCO X7 Pro (Android 16, `arm64-v8a`), com dois ciclos de build/instalação/gameplay e `adb logcat` sem uma única exceção. Repositório movido para `C:\HELSING`, build Android desbloqueado, backend corrigido para `IL2CPP / ARM64`. `P0 — Foundation` fechado. Os pacotes de IA não autorizados foram removidos em 2026-08-15, junto com o `com.unity.purchasing`, depreciado e sem nenhum uso no projeto — ambos com autorização explícita do Game Director e com `COMPILE: PASS` no Editor. `COMMIT: PASS`
HANDOFF REVISION: DERIVE FROM GIT HISTORY
WORKTREE AT HANDOFF: LIMPO. Todo o trabalho até a remoção dos pacotes está commitado em `main`. O histórico anterior a esta etapa já está em `origin/main` — `bd9eb0e` é o último commit comum entre local e remoto, e a branch `feat/mobile-controls-real-device` foi integrada. `PUSH: NOT RUN` para o commit desta etapa, que existe apenas localmente. O ruído crônico de `unity/ProjectSettings/PackageManagerSettings.asset` foi resolvido e não deve mais aparecer como pendência
IMPLEMENTER: CLAUDE CODE nesta etapa, sob `OWNER: CLAUDE CODE` explícito do Game Director; Codex indisponível e sem writer concorrente. O padrão volta a ser `IMPLEMENTER: CODEX` quando ele retornar.
REVIEW MODE: CHECKPOINT
LAST REVIEW STATUS: `NOT RUN` para o escopo de 2026-08-15 — `ShotTracerView`, `DummyRespawner`, `HitscanWeapon.Fired`, joystick dinâmico, `Boundaries` e a troca para `IL2CPP / ARM64` foram escritos pelo Claude Code e **não foram revisados por terceiro**. O `PARTIAL` anterior, dos dois `P1` de ciclo de vida de input, também segue sem auditoria independente pelo mesmo motivo: o autor não deve revisar a própria mudança
REVIEW SCOPE: input unificado, UI greybox, multi-pointer, safe area, movimento, aim/facing, câmera, reversibilidade e validação do P0
NEXT REVIEW TRIGGER: **DISPARADO** — os três gatilhos registrados (movimento e mira em runtime, teste em device real e fechamento do `P0`) ocorreram em 2026-08-15. O checkpoint do `P0 — Foundation` está **devido agora** e deve preceder qualquer expansão para `P1`/`P2`. Revisor: Codex ou outro agente que não seja o autor. Único gatilho anterior ainda não atendido: o `HARNESS DEFECT — P2 ABERTO`
LAST UPDATED: 2026-08-15
NEXT REQUIRED READS: `docs/production/DECISIONS_LOG.md`, `docs/production/NEXT_STEPS.md`, `docs/technical/ARCHITECTURE_V01.md`, `docs/technical/REVERSIBILITY.md` e `docs/production/PREALPHA_VALIDATION.md`

## Objective

Validar controles mobile greybox sobre a fundação aprovada: analógico esquerdo e drag manual direito produzindo intents independentes e substituíveis.

## Last major change

`CORE-001` recebeu `PASS` manual do Game Director. `CORE-002` adicionou intents virtuais ao `PlayerInputReader`, prioridade/release manual em `PlayerAimFacing` e um Canvas greybox com `VirtualJoystickControl`, `ManualAimDragControl`, `SafeAreaFitter` e EventSystem do Input System.

O smoke manual de touch simulado recebeu `PASS` do Game Director. A revisão read-only do Claude Code retornou `PARTIAL`, com dois `P1` de ciclo de vida de input: sem `pointer-up` — perda de foco, pausa ou cancelamento — o joystick mantinha o movimento preso e o drag de mira mantinha `isManualAimActive` latched, bloqueando indefinidamente o fallback de mouse. Ambos foram corrigidos nesta etapa com `ICancelHandler`, `OnApplicationFocus(false)`, `OnApplicationPause(true)` e reset idempotente, preservando `OnDisable`. `IPointerExitHandler` foi deliberadamente evitado para não interromper um arrasto legítimo que sai do limite visual.

`PlayerAimFacing` recebeu a correção do `P2`: uma única mensagem de erro quando não há câmera válida, sem repetição por frame, e o aviso volta a poder ser emitido se a câmera for perdida novamente.

`SampleScene`, `Prototype_Arena_01`, Alucard, Blender, packages e configurações de projeto não foram alterados.

## Decision state

- Câmera: contrato `LOCKED` preservado — Perspective, plano 3/4 elevado, rotação diagonal fixa inicialmente, follow do Player e desacoplamento de movimento/targeting.
- Rig inicial: FOV `40`, pitch `55`, yaw `45`, distância orbital `14`, damping `0,18`, target offset `(0,1,0)` e composition offset `(0,0,0)` — todos `TUNING / OPEN`.
- Movimento e mira: contrato `LOCKED` — independentes; mouse governa aim/facing no fallback desktop; futuro drag touch alimenta o mesmo aim intent; movimento não determina facing durante mira válida.
- Implementação atual: movimento com velocidade `6`, aceleração `32`, desaceleração `40`, gravidade `-25` e grounded velocity `-2`; facing com velocidade de rotação `1080` e distância mínima `0,05` — todos `TUNING / OPEN`.
- Mobile: analógico esquerdo e drag manual direito preservam o contrato `LOCKED`; área, dead zone `0,15`, raio `82`, threshold `24`, raio visual `92` e hold após release `0,25 s` são `TUNING / OPEN`.
- Tap sem drag permanece sem ação; auto-target não foi implementado.
- A ordem entre o marco jogável e P2 — Extraction Loop continua `OPEN`.

## Current state

O projeto oficial permanece `unity/`, Unity 6000.5.8f1. `Prototype_Arena_01` está salva/recarregada com as raízes anteriores mais `MobileControlsGreybox` e `MobileInputEventSystem`. `SafeAreaRoot` contém joystick à esquerda e área de drag à direita. WASD, setas, gamepad e mouse aim permanecem no mesmo Input Action Asset; controles mobile entram pelo reader, sem referências da UI a movement/aim/câmera.

O Alucard V01 permanece congelado e `unity-bootstrap/` permanece `LEGACY / DO NOT USE`.

## Validation performed

- Smoke manual do Game Director para WASD, setas, diagonais, desaceleração, colisão, câmera e Console: PASS.
- Smoke manual do Game Director para mouse aim parado/em movimento, avanço, recuo, strafes, diagonais, colisão, câmera e Console: PASS; `CORE-001` fechado.
- Refresh/recompile e validação de `PlayerInputReader`, `PlayerMovement` e `PlayerAimFacing`: PASS — zero erros.
- Cena salva, recarregada e validada: PASS — zero missing scripts, zero broken prefabs e referências preservadas.
- Aim por projeção: PASS — norte, sul, leste, oeste e diagonal produziram `dot=1`; intenção inválida preservou a última direção.
- Player parado: PASS — facing mudou com deslocamento `0`; `AimDirection` permanece exposta.
- Movimento sem sobrescrever aim: PASS — teste até obstáculo terminou em `(2,5899, 0,0800, 2,5899)`, colisão lateral/solo e grounded válidos, com delta de rotação `0°` e aim preservada.
- Follow da câmera: PASS automatizado — offset `14`, rotação fixa `(55,45,0)`, Perspective e FOV `40`.
- Referências após reload: PASS — Input Actions, reader, movement, aim, câmera e marker preservados; campo antigo `turnSpeed` ausente.
- `CORE-002`: oito direções de joystick e oito ângulos de drag: PASS; diagonais normalizadas.
- Tap abaixo do threshold: PASS — nenhuma mira manual/auto-target acionada.
- Movimento + mira simultâneos e prioridade do drag: PASS; release preservou a direção atual.
- Fallback: PASS estrutural/runtime — bindings de WASD, setas, gamepad e pointer preservados; input físico retorna após liberar joystick/drag.
- Safe area e resoluções landscape: PASS automatizado para targets 1280×720 e 2400×1080; Game View restaurada para `Free Aspect`.
- Colisão/câmera: PASS — lateral+solo, grounded, rotação de aim preservada; Perspective, FOV `40`, `(55,45,0)` e offset `14`.
- Reload de `CORE-002`: PASS — zero missing scripts/broken prefabs; referências UI/Input System persistiram.
- Console após recompile/testes: PASS para o projeto — zero erros/exceptions próprios; existe um warning externo conhecido do MCP WebSocket (`WebSocket is not initialised`).
- Smoke manual de touch simulado pelo Game Director: PASS — joystick em oito direções, drag direito em múltiplos ângulos, movimento e mira simultâneos, retenção após release, colisão, câmera estável, layout landscape e Console sem novos erros.
- Review Claude Code: PARTIAL — dois `P1` de ciclo de vida de input e um `P2` de diagnóstico; nenhuma violação de decisão `LOCKED`; arquitetura aderente a `REVERSIBILITY.md`.

### Etapa de correção dos `P1` — validação no Editor (2026-08-14)

Executada no Unity 6000.5.8f1 aberto, via Unity MCP, sem writer concorrente.

- Importação e compilação: PASS — zero `error CS` e zero `warning CS`; harness importado pelo Unity (`GUID 8fbc4b64a2b973e4eb156f96bc1ddc69`) e domain reload concluído.
- `.meta` gerados pelo Unity: PASS — `MultiTouchHandlerHarness.cs.meta` (18:43:06) e `Editor.meta` (18:43:55) foram criados após a abertura do Editor (18:42:39) e após a escrita do `.cs` (18:38:51). Nenhum `.meta` foi escrito à mão. Os `.meta` de script do projeto são mínimos (`fileFormatVersion` + `guid`), padrão consistente em todo o repositório.
- `MULTITOUCH HANDLER PATH: PASS VIA HARNESS` — nove de nove checks `PASS`, **em Play Mode**, reproduzido em duas execuções independentes. Saída literal:

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

  Os `PASS` são substantivos, não vazios: os três primeiros checks exigem flags `true`, provando ativação real dos handlers com `pointerId` `0` e `1` distintos; e não houve nenhuma assertion, provando que `OnApplicationFocus(false)` foi de fato entregue.
- `REAL DEVICE: NOT RUN` — permanece pendente e obrigatório.
- Console: PASS — estado final com zero erros e zero warnings. O warning de Adaptive Performance é externo e só aparece em transições de Play Mode.
- Reload de `Prototype_Arena_01`: PASS — `isDirty=False`, cinco raízes originais, zero missing scripts, referências preservadas (`PlayerInputReader` em `Player`; `MoveJoystick` e `ManualAimDragArea` com `inputReader`, `handle` e `visualRoot` resolvidos).
- Câmera: PASS — `orthographic=False`, FOV `40`, rotação `(55,00, 45,00, 0,00)` e distância orbital exatamente `14,0000` a partir do target offset `(0,1,0)`. Contrato `LOCKED` preservado.
- Nenhum objeto ou debug temporário na cena: PASS — varredura de todos os `GameObject` em memória retornou zero resíduos do harness e zero componentes ausentes; a cena não ficou dirty e o worktree não mudou.
- `MOVEMENT / AIM RUNTIME: PASS` — por smoke manual do Game Director com a janela em foco (ver etapa seguinte). Não foi possível cobrir por automação: com o Editor sem foco e `Application.runInBackground=False`, o player loop não avança — `Time.frameCount` ficou preso em `2` e `Time.time` em `0,020`, e `EditorApplication.QueuePlayerLoopUpdate()` não destravou. Fica registrado que validação de runtime neste projeto depende de foco humano na janela ou de mudar `Run In Background`, que é configuração de projeto.
- `PlayerAimFacing` (`P2`, log único sem câmera válida): compila e não emitiu nenhum erro, mas o caminho de erro `NOT RUN` — exercitá-lo exigiria invalidar a câmera em runtime, o que não foi feito. Continua coberto apenas por inspeção de código.
- Saída do Play Mode: controlada. Estado final `is_playing=false`, `is_changing=false`, `phase=idle`, fora do Play Mode.
- Revisão independente das correções: NOT RUN — o autor das correções foi o mesmo agente que produziu a revisão original e não deve auditar a própria mudança como se fosse terceiro.

### Etapa seguinte — landscape mobile e fonte única de pointer (2026-08-14)

`OWNER: CLAUDE CODE` explícito do Game Director. Escopo fechado em dois itens; ataque, auto-target, combat, dash e enemies não foram tocados.

- Landscape mobile: PASS — `allowedAutorotateToPortrait` e `allowedAutorotateToPortraitUpsideDown` passaram a `0`; `LandscapeLeft` e `LandscapeRight` seguem `1` com `AutoRotation`. Aplicado pela API `PlayerSettings` e confirmado em disco: diff de exatamente duas linhas em `unity/ProjectSettings/ProjectSettings.asset`. Valor `WORKING`, reversível.
- `P3` — fonte única de pointer: PASS, com a causa real diferente da registrada. `InputSystemUiBootstrap` nunca disparava, porque `actionsAsset == null` já era falso. Quem criava `DefaultInputActions` era o próprio `InputSystemUIInputModule`, que roda também em Edit Mode por ser `[ExecuteAlways]`. Existia ainda uma terceira fonte não documentada: `Assets/InputSystem_Actions.inputactions`, template do Unity 6, definido como Project-wide Actions.
- Correção aplicada: action map `UI` acrescentado a `HelsingGameplay.inputactions` pela API do Input System — nada de edição de JSON à mão — com `Point`, `Click`, `MiddleClick`, `RightClick`, `ScrollWheel`, `Navigate`, `Submit`, `Cancel`, `TrackedDevicePosition` e `TrackedDeviceOrientation`. `Point` e `Click` incluem `<Touchscreen>/touch*/position` e `<Touchscreen>/touch*/press`, preservando multi-touch. O mapa `Gameplay` sobreviveu intacto ao roundtrip (`Move` com 11 bindings, `PointerPosition` com 1, control schemes `Keyboard&Mouse` e `Gamepad`).
- `InputSystemUIInputModule.actionsAsset` passou a apontar para `HelsingGameplay` de forma serializada; todas as ações resolveram sozinhas por nome (`module.point = HelsingGameplay/UI/Point`).
- `InputSystemUiBootstrap` removido da cena e o script movido para a lixeira do sistema — recuperável, não apagado em definitivo.
- Verificação de fonte única em Play Mode: PASS — assets de ações não persistentes em memória = `0`; restam apenas `HelsingGameplay` e `InputSystem_Actions`, ambos persistentes.
- Compilação e Console: PASS — zero erros; apenas o warning externo conhecido do WebSocket do MCP.
- Reload de `Prototype_Arena_01`: PASS — `isDirty=False`, cinco raízes, zero missing scripts, `actionsAsset` e `module.point` preservados, `inputReader` de `MoveJoystick` e `ManualAimDragArea` intactos.
- Regressão do harness multi-touch com o novo asset: PASS — nove de nove checks, em Play Mode. Saída fora do Play Mode, `phase=idle`.
- `POINTER DELIVERY END-TO-END: PASS` — smoke manual do Game Director em 2026-08-14, com a janela do Editor em foco, cobrindo: entrega de pointer pelo EventSystem ao joystick e à área de mira após a troca do action asset; recuperação de perda de foco com pointer pressionado, tanto no joystick quanto no drag de mira; movimento por WASD/setas nas oito direções, desaceleração e colisão; mouse aim, prioridade do drag e retomada do fallback após release; e safe area em 2400×1080. Console sem erros. Aprovado pelo Game Director.
- Com isso, o caminho de pointer, movimento e mira deixa de depender de automação: os três itens que estavam bloqueados pelo player loop parado foram exercitados manualmente e aprovados.
- Efeito incidental: o Unity gerou `unity/ProjectSettings/SceneTemplateSettings.json` ao salvar o projeto. Arquivo padrão do Editor, não editado por mim; fica registrado para não ser confundido com mudança de escopo.

### Base do Combat Slice — PASS (2026-08-14)

`OWNER: CLAUDE CODE` explícito. Escopo: vida mínima, alvo e auto-target simples, para desbloquear o ataque principal. Armas, dash, poderes e inimigos com IA não foram tocados.

Criados em `unity/Assets/_Game/Scripts/Combat/`:

- `Health.cs` — pontos de vida apenas. Não decide regra de dano, custo nem reward, e não acessa run/stash/save, conforme `COMBAT_SYSTEM.md`. `TakeDamage` ignora alvo morto e dispara `Died` uma única vez. `maxHealth = 100` é `TUNING / OPEN`.
- `Targetable.cs` — marca o que o auto-target pode selecionar e mantém um registro próprio; os alvos se registram em `OnEnable`/`OnDisable` para que a seleção não varra a cena a cada tiro. `IsValidTarget` exige vivo e habilitado, cumprindo a regra de nunca mirar cadáver. O registro é limpo em `SubsystemRegistration` para não guardar alvos destruídos quando o domain reload está desligado.
- `AutoTargetSelector.cs` — primeira passada `WORKING` de seleção: cone à frente, pontuando ângulo e distância com peso configurável. Seleção sob demanda, nunca em `Update`. `CurrentTarget` retorna `null` sozinho quando o alvo morre. Gizmos de alcance, cone e alvo atual. `maxRange = 12`, `maxAngle = 60` e `angleWeight = 0,7` são `TUNING / OPEN`.

O `Algoritmo final de target selection` permanece `OPEN`: esta implementação é a partida sancionada como `WORKING` — seleção simples por distância/direção com gizmos — e não fecha a decisão.

Validação executada no Editor após a instalação do Android Build Support:

- Compilação: PASS — zero `error CS` e zero `warning CS`. Os três tipos carregaram em `Assembly-CSharp` e cada `MonoScript` resolve para a própria classe, o que é prova mais forte que ausência de erro no Console.
- Cena: `Dummies` criado sob `Gameplay` com `Dummy_North (0, 1, 6)`, `Dummy_East (7, 1, 0)` e `Dummy_SouthWest (-6, 1, -3)`, cada um capsule com collider, `Health` e `Targetable` (`aimHeightOffset = 0`, porque o pivô da capsule é central). `AutoTargetSelector` adicionado ao Player. Três dummies, e não um, para que a seleção precise de fato escolher entre candidatos.
- Seleção, em Play Mode: PASS — registro com `3` alvos; virado ao norte seleciona `Dummy_North`; virado a leste seleciona `Dummy_East`; virado ao sul retorna `null`, porque o `Dummy_SouthWest` cai a 63,4° e o cone é de 60° — limite exercitado de propósito.
- Morte e alvo inválido: PASS — `100 → 60 de dano → 40`, vivo; mais `60` leva a `0` e `IsValidTarget` vira `false`; a seleção virada ao norte passa a retornar `null` e `CurrentTarget` se anula sozinho. Cadáver nunca é selecionado.
- Contrato de dano: PASS — `Died` disparou exatamente `1` vez com quatro chamadas de `TakeDamage`, incluindo overkill e valor negativo; a soma do dano aplicado foi `100`, nunca acima do HP disponível; dano em cadáver não lança exceção.
- Registro em Edit Mode: `0`, como esperado — confirma que `OnDisable` e a limpeza em `SubsystemRegistration` não deixam alvos destruídos na lista.
- Console: PASS — zero erros e zero warnings durante todo o ciclo. Saída controlada do Play Mode; cena com `isDirty=False`, cinco raízes e zero missing scripts.
- Nenhum `.meta` foi escrito à mão.
- `ATAQUE: NOT RUN` — não existe arma ainda. `Health`, `Targetable` e `AutoTargetSelector` estão validados isoladamente, mas nenhum consumidor real os liga; o dano foi aplicado por chamada direta no teste, não por gameplay.

### Ataque principal da Casull — PASS parcial (2026-08-15)

`OWNER: CLAUDE CODE` explícito. Escopo: ataque principal hitscan consumindo auto-target no toque e mira manual no arrasto. Jackal, weapon swap, dash, poderes e munição não foram tocados.

Contrato de gameplay aprovado pelo Game Director nesta etapa, ambos `WORKING`:

- arrasto no ataque mira e dispara continuamente na cadência enquanto o dedo estiver na tela;
- toque sem alvo válido no cone dispara na direção atual, em vez de não fazer nada — um toque silencioso seria lido como controle travado.

Criados e alterados:

- `Scripts/Combat/HitscanWeapon.cs` (novo) — cadência e aplicação de dano apenas. Não lê input, não escolhe alvo e não conhece munição, run ou reward, então Casull e Jackal podem divergir sem mexer em quem puxa o gatilho. `dano 25`, `alcance 25`, `intervalo 0,22 s` são `TUNING / OPEN`.
- `Scripts/Player/PlayerAttack.cs` (novo) — único lugar que conhece a regra `LOCKED` "toque usa auto-target, arrasto usa mira manual". A UI só reporta gesto, o selector só ordena alvos e a arma só dispara.
- `Input/PlayerInputReader.cs` (alterado) — novo intent de toque, com `RequestAttackTap` e `ConsumeAttackTap` de consumo único. O ataque entra como intent, igual a movimento e mira; a UI continua sem referência a gameplay.
- `UI/ManualAimDragControl.cs` (alterado) — `OnPointerUp` passa a distinguir toque de arrasto. Cancelamento, perda de foco e pausa seguem indo direto para `ResetControl`, portanto não produzem tiro.

Validação em Play Mode:

- Compilação: PASS — zero `error CS` e zero `warning CS`; ambos os tipos carregados e a nova API do reader presente.
- Hitscan: PASS — tiro ao norte tirou `100 → 75` no `Dummy_North`. Confirma também que a cápsula do próprio Player não bloqueia o disparo.
- Cadência: PASS — segundo tiro imediato retornou `False` e não causou dano.
- Cobertura: PASS — tiro na diagonal parou no `Obstacle_NE` e o `Dummy_East` ficou intacto. Obstáculo bloqueia de fato.
- Intent de toque: PASS — `ConsumeAttackTap` devolve `true` uma única vez por pedido.
- Toque legítimo contra toque fantasma: PASS nos quatro casos — release sem arrasto gera toque; cancelamento não gera; perda de foco não gera; release depois de arrastar não gera. Isto protege diretamente a correção `P1`: sair do app com o dedo na tela não dispara.
- Regressão do harness multi-touch: PASS — nove de nove, após a alteração em `OnPointerUp`.
- Console: PASS — zero erros e zero warnings no ciclo; saída controlada do Play Mode; cena `isDirty=False`, cinco raízes, zero missing scripts.

- `ATTACK LOOP END-TO-END: PASS por evidência` — o Game Director entrou em Play Mode e atirou de fato. A inspeção em runtime encontrou `Dummy_East` em `0`, `Dummy_North` em `50` e `Dummy_SouthWest` em `25`, ou seja, dano em múltiplos de `25` distribuído por três alvos distintos. Isso só é possível percorrendo toda a cadeia: gesto na UI, intent no reader, `PlayerAttack.Update`, auto-target, hitscan e `Health`. A sensação de toque contra arrasto ainda não foi descrita pelo Game Director e continua sem registro.

### Feedback visual de dano — PASS (2026-08-15)

Pedido do Game Director para tornar o dano perceptível. Avaliada a barra de vida em world space e descartada: exigiria Canvas por inimigo, billboard e script de fill, custa mais em mobile e inventaria direção de HUD que continua `OPEN`.

- `Scripts/Combat/DamageVisualFeedback.cs` (novo) — tinge o próprio dummy de cinza a vermelho conforme o HP e o desativa ao morrer. Usa `MaterialPropertyBlock`, então não instancia material: `sharedMaterial` permanece `Lit`. Cores e `hideOnDeath` são `TUNING / OPEN`. É feedback de desenvolvimento, não arte.
- Bug encontrado e corrigido na própria etapa: com `ApplyTint` apenas em `OnEnable`, o dummy nascia vermelho com vida cheia, porque a ordem de componentes pode colocar o feedback antes de `Health.Awake` inicializar `currentHealth`, fazendo `NormalisedHealth` valer `0`. Corrigido aplicando o tint também em `Start`, que roda depois de todos os `Awake`. Sem isso o recurso falharia no seu único objetivo, já que todos os alvos pareceriam feridos.
- Validação: PASS — vida cheia `RGBA(0,750, 0,750, 0,750)`; metade `RGBA(0,800, 0,425, 0,425)`; morte desativa o objeto e o registro cai de três para dois alvos. Console sem erros, saída controlada do Play Mode, cena `isDirty=False` e zero missing scripts.
- Observação para quem inspecionar a cena: `Health.CurrentHealth` aparece como `0` fora do Play Mode porque o campo não é serializado e `Awake` não roda em Edit Mode. `MaxHealth` é a fonte de verdade ali; não é dano persistido.

### Fallback de pointer restrito ao desktop e facing por movimento — PASS (2026-08-15)

Levantado pelo Game Director ao notar que, no mouse, o eixo do personagem seguia o cursor sem clique. No desktop isso é o contrato `LOCKED`. A investigação encontrou um bug real que apareceria no celular.

- Causa: `PointerPosition` estava ligada a `<Pointer>/position`, e `Pointer` é a classe base de Mouse, Pen **e Touchscreen**. No celular a action resolveria para o toque primário, cuja posição **persiste depois que o dedo levanta**. O resultado seria o personagem virando para o canto inferior esquerdo enquanto o polegar arrasta o joystick, e congelando encarando o último ponto tocado — violando o `LOCKED` de que movimento e mira são independentes. O rótulo `groups: Keyboard&Mouse` não protegia, porque o reader chama `Enable()` direto na action, sem máscara de control scheme.
- Correção 1: binding trocado para `<Mouse>/position` e `<Pen>/position`. Verificado em Play Mode: a action resolve somente para `/Mouse/position`.
- Correção 2: `PlayerInputReader.HasPointerAim` só é verdadeiro com Mouse ou Pen presentes, e `PlayerAimFacing` só usa o fallback de pointer quando ele existe. Sem esse guard o build de toque miraria na projeção da tela em `(0,0)` a cada frame — o mesmo bug com outro sintoma.
- Correção 3, decorrente: com o fallback desligado no toque, o facing ficaria **congelado** e o personagem andaria de lado sem nunca virar. Seguindo a referência de MOBA mobile dada pelo Game Director — Mobile Legends e LoL Wild Rift —, o facing passa a seguir o movimento enquanto não há mira manual. Isso é compatível com o `LOCKED`, que restringe a independência a "durante mira válida". Controlado por `faceMovementWhenNotAiming`, `WORKING` e reversível pelo Inspector.
- Validação: PASS — simulando o celular em Play Mode, com o Mouse removido e um Touchscreen adicionado, `HasPointerAim` virou `false` e a mira deixou de ser sobrescrita; joystick ao norte produziu aim `(0,71, 0, 0,71)` e a leste `(0,71, 0, -0,71)`, relativos à câmera e idênticos à base usada por `PlayerMovement`; soltar o stick preservou o facing. O Mouse foi devolvido na mesma execução. Compilação sem erros, harness 9/9 como regressão, Console limpo e saída controlada do Play Mode.
- `DEVICE: NOT RUN` — todo o comportamento de toque acima foi validado por simulação de dispositivo no Editor, não em aparelho real.

`REFERÊNCIA DE CONTROLE — WORKING`: o Game Director definiu MOBA mobile (Mobile Legends, LoL Wild Rift) como família de referência de controle e câmera, com ressalvas a tratar durante o beta. Isso **não** altera nenhuma decisão `LOCKED` existente e ainda não foi registrado em `DECISIONS_LOG.md`; merece entrada formal antes de virar base de outras decisões.

### Build Android bloqueado por caminho não-ASCII — MUDANÇA DE PASTA EM ANDAMENTO (2026-08-15)

O primeiro build Android falhou em 110 s, antes de compilar qualquer coisa:

```text
UnityException: Invalid project path
Project path '...\ARQUIVOS TEMPORÁRIOS\HELSING\HELSING\unity' contains non-ASCII
characters at position 62, Android Tools don't work properly with non-ASCII paths.
```

A causa é o `Á` de `TEMPORÁRIOS`. É limitação das Android Tools, não do Unity nem do código do projeto, e não tem contorno por configuração. A mojibake que aparecia nos logs do Editor desde o início (`TEMPORÃRIOS`) era sintoma do mesmo problema.

Decisão do Game Director: mover o repositório para `C:\HELSING`, fora do OneDrive. Além de resolver o bloqueio, tira a pasta `unity/Library` da sincronização do OneDrive, que é fonte conhecida de lentidão e corrupção em projetos Unity.

Estado da mudança quando este handoff foi escrito:

- Preparação concluída: Unity fechado e os cinco processos do servidor MCP encerrados — eles seguravam `unity/Library/MCPForUnity/RunState/mcp_http_8080.pid` mesmo com o Editor fechado.
- Restava o VS Code, que mantém a pasta aberta junto com os servidores de linguagem C#.
- A movimentação em si é feita manualmente pelo Game Director.

Pendências obrigatórias **depois** do move, antes de qualquer build:

1. Remover a entrada antiga do projeto no Unity Hub e adicionar `C:\HELSING\unity`.
2. Corrigir a chave de projeto do `UnityMCP` em `~/.claude.json`: hoje ela aponta para o caminho antigo, e o servidor não carrega se a sessão abrir em outro lugar.
3. Reabrir a sessão do agente já em `C:\HELSING`.
4. Confirmar que `C:\HELSING\.git` e `C:\HELSING\unity\Assets` existem antes de reabrir o Editor.

Configuração de build já aplicada e preservada pelo move, porque vive em `ProjectSettings` e `EditorBuildSettings`:

- cena `[0] Assets/_Game/Scenes/Prototype_Arena_01.unity` habilitada; `SampleScene` mantida no fim e desabilitada, sem alterar o asset;
- `applicationIdentifier = com.helsing.game`; `productName = HELSING`, corrigindo o antigo `unity-bootstrap`, que apontava para o projeto `LEGACY`; `companyName` continua `DefaultCompany` e não foi tocado;
- backend `Mono2x`, arquiteturas `ARMv7, ARM64`, `minSdk 26`;
- plataforma ativa já trocada para Android.

`REAL DEVICE: NOT RUN` continua, agora também porque o `adb devices` não listou nenhum aparelho na tentativa — verificar cabo de dados, depuração USB e a autorização no aparelho.

### `REAL DEVICE: PASS` — mudança de pasta, build Android e dois testes em aparelho (2026-08-15)

`OWNER: CLAUDE CODE` explícito do Game Director. Esta etapa fecha o item que estava pendente desde `CORE-002`.

**Mudança de pasta concluída.** O repositório está em `C:\HELSING`, caminho ASCII puro sem espaço. O move parcial inicial falhou com o VS Code segurando o `.git`; verificado que os 127 arquivos já tinham sido copiados e a origem ficara com zero arquivos, sem perda. O restante foi movido por rename atômico. `git status` conferido contra baseline salvo antes do move: idêntico, `HEAD` preservado em `66d3335`.

`unity/Library` e `unity/Temp` foram deixados para trás por estarem travados; decisão consciente, já que o `Library` antigo continha `RunState` do MCP apontando para o caminho velho. Consequência registrada: **a plataforma ativa voltou para `StandaloneWindows64`**, porque `EditorUserBuildSettings` mora no `Library`. Foi retrocada para Android.

Religados: chave do projeto no `.claude.json` e registro do Unity Hub — ambos ainda apontavam para o caminho do **OneDrive**, anterior até ao move anterior.

**`ARM64 / MONO2X — DEFEITO DE CONFIGURAÇÃO ENCONTRADO`.** A configuração registrada como pronta (`Mono2x` + `ARMv7, ARM64`) era inconsistente: o backend Mono no Android **só gera ARMv7**. O primeiro APK saiu `armeabi-v7a` puro, apesar de `PlayerSettings` reportar as duas arquiteturas — o valor foi aceito por ter sido aplicado via API, onde a UI o desabilitaria. O aparelho de teste reporta `ro.product.cpu.abilist = arm64-v8a` **exclusivo**, e a instalação falhou com `INSTALL_FAILED_NO_MATCHING_ABIS`. Backend trocado para `IL2CPP` com `ARM64`. Essa falha só era detectável em aparelho real e é a justificativa retroativa do `REAL DEVICE` como gate obrigatório.

**Aparelho:** POCO X7 Pro (`2412DPC0AG`, `rodin`), Android 16 / API 36, `arm64-v8a`.

**`REAL DEVICE: PASS`** — dois ciclos completos de build, instalação e gameplay, com `adb logcat` capturado em ambos: **zero exceções, zero erros em runtime**. Multi-touch real confirmado por comportamento: movimento e ataque simultâneos funcionaram, exercitando pela primeira vez os bindings `<Touchscreen>/touch*/` e o facing por movimento no toque, invisíveis no desktop por contrato.

**Achados do primeiro teste, pelo Game Director, e respectivas correções:**

- `TRACER INVISÍVEL EM BUILD` — o traçante usava `Debug.DrawLine`, que só renderiza na Scene View e **nunca em build**. O disparo era literalmente invisível no aparelho, o que impedia qualquer avaliação de mira. Corrigido com `ShotTracerView` (novo), um `LineRenderer` em world space alimentado pelo novo evento `HitscanWeapon.Fired`. Presentation separada da arma: cadência e dano seguem sem conhecer apresentação.
- `TOQUE ATIRA NA DIREÇÃO DO MOVIMENTO` — andar de costas e atirar para frente falhava **no toque seco**, porque `FireAtAutoTarget` avalia o cone de 60° em torno de `transform.forward`, e esse forward segue o movimento quando não há mira manual. **Arrastando na direita já funcionava** e foi confirmado pelo Game Director no segundo teste. Nenhuma mudança de comportamento foi feita: a regra permanece como o `LOCKED` descreve. Fica `OPEN` se o toque deve passar a considerar alvos fora do cone.
- Joystick fixo → **dinâmico**: `MoveJoystick` virou área de toque da metade esquerda (`0.02–0.46` × `0.04–0.96`, espelhando a geometria do lado direito, sem sobreposição) e o desenho do stick passou para `StickRoot`, reposicionado sob o dedo. `dynamicOrigin` e `hideWhenIdle` são reversíveis pelo Inspector.
- `moveSpeed` `6 → 4.5`, `TUNING / OPEN`.
- `DummyRespawner` (novo) no container `Dummies`: revive em `3 s` com `ResetHealth`. Vive no **container** e não nos dummies, porque eles se desativam ao morrer e um componente desativado não conta o próprio tempo. Ferramenta de teste — spawn real, encontros e Threat continuam `OPEN` e não devem ser inferidos disto.
- `Boundaries` (novo) sob `Environment`: quatro colisores invisíveis, arena medida em 30×30. Primeira tentativa posicionou as paredes a partir do topo da arena e elas ficaram **flutuando acima do jogador**, sem bloquear nada; corrigido para cobrir `y` de `-1` a `3`.

**Veredito do Game Director no segundo teste:** joystick dinâmico `perfeito`; andar de costas atirando `funcionando bem`; velocidade `bem melhor`, reajustável no futuro; respawn e bordas `funcionaram bem`. `Estamos prontos pra avançar`.

**Registrado como pedido futuro, não implementado:** sensibilidade da mira da direita pode melhorar ~6–7%, tratado pelo Game Director como questão de gosto — candidato a **slider de sensibilidade nas configurações do jogo**, não a valor fixo. E o próximo interesse de teste declarado é **movimento/animação**.

Validação executada por MCP com cliente HTTP próprio contra `127.0.0.1:8080`: compilação `0 error CS` / `0 warning CS`; cena `Prototype_Arena_01` `clean — no issues`, zero missing scripts, cinco raízes; câmera `LOCKED` preservada (`orthographic=False`, FOV `40`, rotação `(55,45,0)`, distância orbital `14.0031`); `inputReader` resolvido nos dois controles; `actionsAsset = HelsingGameplay`.

`COMMIT: NOT RUN` — nada desta etapa foi commitado.

### Rodada de combate — `DEVICE: PARCIAL`, `COMMIT: NOT RUN` (2026-08-15, fim da sessão)

`OWNER: CLAUDE CODE` explícito. Implementada na ordem de prioridade dada pelo Game Director. **Não commitada de propósito**, para manter visível no histórico a fronteira entre o que foi validado em aparelho e o que não foi.

Criados: `PlayerRespawn`, `EnemyChaseAttack`, `DashController`. Alterados: `PlayerInputReader` (intent de dash com direção), `VirtualJoystickControl` (gesto de dash), cena.

- **Player mortal**: `Health` (100) + `PlayerRespawn` (2 s, volta ao ponto inicial). Movimento, ataque, mira e dash desligados enquanto morto. Não toca loadout nem perda — `RUN-002` segue `OPEN`. `DamageVisualFeedback` reaproveitado no `PlayerVisual` com `hideOnDeath=false`; a referência de `Health` teve de ser apontada à mão, porque o componente resolve `Health` apenas no próprio GameObject e o `Health` vive no pai.
- **`EnemyChaseAttack`** nos três dummies: idle → persegue → ataca, com wind-up de `0,35 s` e distância reconferida no impacto, para que sair de perto realmente esquive. Sem pathfinding, sem aggro compartilhado. Famílias de inimigos e design de encontro continuam `OPEN`.
- **Dash**: `DashController`, direção vinda do stick, `cooldown 0,9 s`, suspende `PlayerMovement` por `0,18 s` e mantém gravidade. **Sem custo de recurso** — Sangue e Almas são economia e seguem `OPEN`.

Resultado do teste em aparelho pelo Game Director: **Player mortal PASS**, **respawn PASS**, **dash PASS parcial** (distância excessiva e gatilho ruim), **esquiva NOT RUN**.

Ajustes aplicados depois desse teste e **ainda não validados em aparelho**: `distance 4.5 → 2.25`; gatilho trocado de duplo toque para **arrasto**, exigindo velocidade (`900`) **e** distância (`45`) simultâneas, com rearme de `0,25 s`. Todos `TUNING / OPEN`. O risco conhecido desse gesto é falso positivo durante corrida normal, levantado antes da escolha e aceito pelo Game Director como teste.

Corrigido no mesmo passo, antes de chegar ao aparelho: a direção do dash passou a viajar junto com o pedido. Lida no momento da execução, um arrasto rápido termina antes e o stick já zerou, o que faria o dash sair na direção errada.

`DEVICE: NOT RUN` para o dash por arrasto e para toda a esquiva. `REVIEW: NOT RUN`.

### `PACOTE NÃO AUTORIZADO — com.unity.ai.assistant` (2026-08-15, 15:45)

Detectado ao fim da sessão, **não introduzido pelo agente** e não autorizado por tarefa. Contraria `Do not touch`, que lista packages e versões.

- Instalou o Unity App UI como dependência.
- Criou `unity/Assets/Plugins/Android/AndroidManifest.xml`, que não existia.
- **Trocou a activity de entrada do app** de `com.unity3d.player.UnityPlayerGameActivity` para `com.unity3d.player.appui.AppUIGameActivity`. O `adb shell am start` falhou com `Activity class does not exist` até a activity nova ser descoberta pelo `aapt2 dump badging`.
- Alterou `Packages/manifest.json` e `packages-lock.json`; criou `ProjectSettings/Packages/com.unity.ai.assistant/Settings.json`.
- Tempo de build subiu de ~200 s para ~570 s.

**Dois pontos deste registro estavam errados**, corrigidos na etapa de remoção contra o diff real de `d4b9a61`:

- `ProjectSettings/GraphicsSettings.asset` **não** foi alterado. O último commit que tocou esse arquivo é `5571727`, muito anterior ao pacote. A suspeita não se sustenta no histórico.
- O pacote **não veio sozinho**: `com.unity.ai.inference` entrou no mesmo commit, e é ele — não o `assistant` — que declara `com.unity.dt.app-ui` como dependência. A atribuição do Unity App UI ao `assistant` estava incorreta, e remover só o `assistant` teria deixado a regressão de `EnhancedTouch` no lugar.

**Dois rastros não estavam registrados:** `scriptingDefineSymbols` ganhou `Standalone: SENTIS_ANALYTICS_ENABLED` em `ProjectSettings.asset`, e `com.unity.dt.app-ui` foi inserido em `m_configObjects` no `EditorBuildSettings.asset`.

### `DEVICE: PASS` total do Combat Slice (2026-08-15, fecho da sessão)

Build final instalado e aprovado pelo Game Director no POCO X7 Pro. **Todos os itens pendentes fechados:**

- Correção do tint preso no respawn: `PASS` — o Player volta cinza, com vida cheia.
- Esquiva do wind-up do Ghoul: `PASS` — sair de perto durante os `0,35 s` evita o dano, confirmando que a reconferência de distância no impacto funciona e que o combate tem espaço para reação.
- Joystick, dash por arrasto, morte e respawn: `PASS`.

`REGRESSÃO CONHECIDA, NÃO NOSSA`: o build emite **15 exceções** `ArgumentException: 'InputUpdateType.None' is not a valid update mask`, vindas de `InputSystem.EnhancedTouch.Finger` dentro dos callbacks de `InputSystem.onDeviceChange`. Nenhum código do HELSING usa `EnhancedTouch`; a origem é o Unity App UI, que entra como dependência do `com.unity.ai.inference` — e **não** do `assistant`, como registrado antes. **O input não foi afetado** — o Game Director confirmou joystick, mira, ataque e dash funcionando. O impacto é poluição de log e custo desconhecido em runtime, o que atrapalha usar o Console como sinal limpo em testes futuros. Os três pacotes foram removidos na etapa seguinte; o desaparecimento das exceções é esperado mas **ainda não medido**.

### `PACOTES DE IA REMOVIDOS` — `COMMIT: PASS`, `COMPILE: PASS`, `DEVICE: NOT RUN` (2026-08-15)

`OWNER: CLAUDE CODE` com autorização explícita do Game Director, que para este escopo supera o `Do not touch` de packages e de `PackageManagerSettings.asset`. Executado com o Unity **fechado**: o Editor mantém `ProjectSettings.asset` e `EditorBuildSettings.asset` em memória e sobrescreveria as edições ao salvar.

Removidos `com.unity.ai.assistant`, `com.unity.ai.inference` e o órfão `com.unity.dt.app-ui`. `com.unity.ai.navigation` foi preservado — é NavMesh legítimo, presente desde `5571727`.

Revertidos os rastros: a árvore `Assets/Plugins/` inteira, `Plugins.meta` incluído, que não existia antes do pacote; `ProjectSettings/Packages/com.unity.ai.assistant/`; o define `SENTIS_ANALYTICS_ENABLED`; e a entrada `com.unity.dt.app-ui` em `m_configObjects`.

**A activity de entrada volta ao padrão.** Sem `Assets/Plugins/Android/AndroidManifest.xml`, o Unity regenera o manifest e o app entra de novo por `com.unity3d.player.UnityPlayerGameActivity` — a configuração que teve `REAL DEVICE: PASS` em `ced3ebf`. Scripts de lançamento automatizado devem voltar à activity padrão. O arquivo trazia `VIBRATE`, `allowBackup=false` e `usesCleartextTraffic=false`, que nunca foram decisão do Game Director; se esse hardening for desejado, entra como mudança deliberada e revisável, não de carona numa remoção de pacote.

Verificação executada com o Editor fechado:

- Varredura por `ai.assistant|ai.inference|app-ui|AppUIGameActivity|SENTIS` em todo `unity/` fora de `Library`: zero ocorrências.
- `manifest.json` e `packages-lock.json` parseiam como JSON válido.
- Configuração de build preservada: `applicationIdentifier Android = com.helsing.game`, `productName = HELSING`, backend `IL2CPP`, `AndroidTargetArchitectures = 2` (ARM64), `minSdk 26`, landscape exclusivo, cena `[0] Prototype_Arena_01` habilitada e `SampleScene` desabilitada.

`COMPILE: PASS` — o Editor foi reaberto em `C:\HELSING\unity` e re-resolveu os pacotes com **zero `error CS` e zero `warning CS`**. As edições sobreviveram intactas à reabertura: `scriptingDefineSymbols` continua `{}`, `m_configObjects` continua sem o App UI, `Assets/Plugins/` não foi recriada e o manifest tem apenas `com.unity.ai.navigation`. Os dois `error:` no log são handshake de licença no boot, resolvidos em seguida pelo próprio cliente.

`DEVICE: NOT RUN` — nada foi instalado em aparelho após a remoção. O ganho esperado, **não medido**, é o build voltar de ~570 s para ~200 s e as 15 exceções de `EnhancedTouch` desaparecerem. Só um build no POCO fecha isso.

**Ruído de fim de linha eliminado.** Cinco dos seis arquivos que apareciam modificados nunca tinham mudado: com `core.autocrlf=true` e o Unity gravando LF, o git relatava diferença inexistente. Comprovado por hash — índice e worktree com o mesmo blob. O `.gitattributes` passou a declarar `text eol=lf` para os assets de texto do Unity. `PackageManagerSettings.asset` foi revertido: o diff era `oneTimePackageErrorsPopUpShown` e dois IDs internos de entidade, sem significado de projeto.

`REVIEW: NOT RUN` — escrita pelo mesmo agente, entra na fila do checkpoint do `P0` junto com o resto.

### `com.unity.purchasing REMOVIDO` — `COMPILE: PASS`, `DEVICE: NOT RUN` (2026-08-15)

Descoberto ao reabrir o Editor: na re-resolução, o Unity **subiu `com.unity.purchasing` de `4.15.1` para `5.4.2` por conta própria**. O log é explícito quanto ao motivo — `com.unity.purchasing@4.15.1 is deprecated: Unity IAP 4 is unsupported as of June 8, 2026`. O estado congelado em `bd9eb0e` já era insustentável: qualquer reabertura do projeto forçaria a migração.

Decisão do Game Director: **remover o pacote inteiro**, em vez de aceitar o salto de versão maior ou voltar para uma versão sem suporte.

Justificativa: nenhum script do HELSING referencia `UnityEngine.Purchasing`, e não há compra dentro do app no escopo. É sobra do template URP. O pacote compilava **14 assemblies** a cada build, para uso zero. Mesmo raciocínio aplicado ao `ai.assistant`: nada entra no caminho crítico do APK sem ser usado. Reversível a qualquer momento se houver loja no futuro.

- Após a remoção: **zero** assemblies `*Purchasing*.dll` em `Library/ScriptAssemblies`, contra 14 antes.
- Varredura em `Assets/`, `ProjectSettings/` e `Packages/`: nenhuma referência a `com.unity.purchasing` nem a `UnityEngine.Purchasing`.
- `UnityConnectSettings.asset` mantém a seção `UnityPurchasingSettings` com `m_Enabled: 0` e `m_TestMode: 0`. **Não é resíduo do pacote:** a seção já existia em `5571727`, é configuração estoque do Unity Services e está desligada.
- `com.unity.services.core` preservado — segue sendo dependência de `com.unity.services.analytics`, e por consequência de `com.unity.analytics`.
- `COMPILE: PASS` — zero `error CS` e zero `warning CS` após a re-resolução.

O `5.4.2` nunca chegou a ser commitado: o diff sai direto de `4.15.1` para a ausência do pacote.

`REVIEW: NOT RUN`.

## Known issues

- `CORE-002` fechado no Editor. O único item pendente é `REAL DEVICE: NOT RUN` — teste touch em aparelho real, com dois dedos simultâneos, que nem o harness nem o mouse na Game View reproduzem. Continua obrigatório antes do beta mobile.
- `HARNESS DEFECT — P2 ABERTO, não corrigido nesta etapa`: o menu item `HELSING/Validation/Run Multi-Touch Handler Harness` também roda em Edit Mode, onde produz resultado enganoso. Fora do Play Mode o Unity não executa `Awake()` em `MonoBehaviour` comum, então `VirtualJoystickControl.controlRect` e `ManualAimDragControl.interactionRect` ficam `null`, `UpdatePointer`/`TryGetLocalPoint` retornam cedo e os controles nunca ativam. A execução em Edit Mode retornou `RESULT: FAIL` com quatro `Assertion failed on expression: 'ShouldRunBehaviour()'` — o `SendMessage` de `OnApplicationFocus` não é entregue — e deixou um erro transitório `The referenced script (Unknown) on this Behaviour is missing!` no domain reload seguinte. Esse erro não se reproduz em ciclos de Play Mode, com ou sem harness, e não há componente ausente em memória. Naquela execução os quatro `FAIL` eram artefato do contexto e os cinco `PASS` eram vazios, pois só afirmavam flags já `false`. Correção proposta e **não aplicada**: guardar o menu item com `EditorApplication.isPlaying`, ou resolver os `RectTransform` sob demanda, para que o harness nunca reporte resultado sem significado.
- `REAL DEVICE` continua `NOT RUN`. Game View com mouse fornece um único pointer e não cobre dois pointers concorrentes.
- O player loop não avança com o Editor sem foco (`runInBackground=False`), o que impede validar movimento e mira por automação. Requer um humano com a janela do Editor em foco.
- `P3` **RESOLVIDO** nesta etapa; a descrição anterior estava incorreta quanto à causa. Ver a seção de landscape e fonte única acima.
- `PROJECT-WIDE ACTIONS — OPEN`: `Assets/InputSystem_Actions.inputactions` continua definido como Project-wide Actions do Unity 6 e não é lido pelo runtime do HELSING. Mantê-lo, removê-lo ou substituí-lo por `HelsingGameplay` é decisão de configuração de projeto e não foi tocado.
- `unity/ProjectSettings/PackageManagerSettings.asset` **RESOLVIDO**: revertido na etapa de remoção dos pacotes. O diff arrastado por várias sessões era `oneTimePackageErrorsPopUpShown` e dois IDs internos de entidade. Se reaparecer, é ruído de UI do Package Manager, não mudança de projeto.
- O Unity MCP produziu warnings externos de WebSocket e `GameObjectSerializer`; nenhum erro de gameplay foi encontrado.

## Next owner action

**`COMMIT` deixou de ser pendência.** O Combat Slice, o ataque da Casull, o feedback de dano e as correções de ABI estão commitados e presentes em `origin/main` até `bd9eb0e`. A remoção dos pacotes está commitada apenas **localmente**: `PUSH: NOT RUN`, e exige autorização explícita do Game Director.

**`COMPILE` está fechado.** O Editor foi reaberto, re-resolveu os pacotes e compilou com zero `error CS` e zero `warning CS`, tanto após a remoção dos pacotes de IA quanto após a remoção do `purchasing`.

**A ação mais urgente é um build de checagem no POCO X7 Pro**, que é a única coisa que fecha `DEVICE` para as duas remoções. Ele mede de uma vez três hipóteses ainda não confirmadas: se o tempo de build voltou de ~570 s para ~200 s, se as 15 exceções de `EnhancedTouch` desapareceram, e se o app volta a entrar por `com.unity3d.player.UnityPlayerGameActivity` sem que o `adb shell am start` falhe. Nenhuma delas foi medida — todas são expectativa.

Fica registrado, para não se repetir: a sessão do agente chegou a ser aberta em `C:\Game Helsing`, sobra da pasta anterior, onde restaram apenas `Library/` e `Temp/` sem `Assets`, `Packages` nem `ProjectSettings`. A pendência nº 3 do move — reabrir a sessão já em `C:\HELSING` — não tinha sido cumprida.

Com o `P0 — Foundation` e o `REAL DEVICE` fechados, a decisão seguinte é de escopo, não técnica: o `VISION / LOCKED ORDER RECONCILIATION` continua `OPEN` em `NEXT_STEPS.md`. O Production Pack propõe seguir para `P2 — Extraction Loop`, enquanto o marco `ALUCARD — PLAYABLE PRE-ALPHA 01` ainda exige Jackal, weapon swap e um poder. O Game Director precisa decidir se são dois gates formais ou um marco indivisível, antes de abrir a próxima frente.

Pendências menores registradas: slider de sensibilidade de mira nas configurações; e a `REFERÊNCIA DE CONTROLE — WORKING` (MOBA mobile) ainda sem entrada formal em `DECISIONS_LOG.md`. O `.utmp/` já está no `.gitignore` e sai desta lista.

## Do not touch

- decisões `LOCKED` sem aprovação;
- Alucard, Blender, FBX, animações, auto-target, combat, weapons, dash, enemies, extraction, inventory, stash ou save neste fechamento;
- packages, versões, `unity-bootstrap/` ou `PackageManagerSettings.asset`;
- commit/push sem autorização explícita.
