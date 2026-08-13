# Next Steps

## Prioridade ativa — primeiro loop jogável

Especialista principal: **Unity Architect**. Implementação não começa automaticamente: cada sprint precisa de owner, escopo e validação declarados. O projeto oficial é `unity/`; `unity-bootstrap/` permanece `LEGACY / DO NOT USE`.

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
