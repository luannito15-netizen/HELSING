# HELSING — Protocolo central para agentes

Este repositório é a fonte oficial do HELSING. Estas regras valem para Codex, Claude Code e futuros agentes.

## Contexto progressivo

Declarar `CONTEXT MODE: FAST` ou `CONTEXT MODE: FULL` no início da tarefa. O handoff e o runtime são evidência operacional; não substituem decisões oficiais.

### FAST CONTEXT — padrão

Ler:

1. `AGENTS.md`;
2. `handoff/CURRENT_HANDOFF.md`;
3. o especialista principal;
4. os arquivos diretamente envolvidos.

Antes de confiar no handoff:

1. ler `HANDOFF STATUS`, `REVIEW MODE`, `LAST REVIEW STATUS` e `NEXT REQUIRED READS`;
2. derivar a última revisão versionada com `git log -1 --format=%H -- handoff/CURRENT_HANDOFF.md`;
3. se não houver resultado, tratar como `HANDOFF COMMIT: NONE — UNTRACKED`;
4. inspecionar commits e alterações posteriores à revisão derivada;
5. comparar `git status --short` com `WORKTREE AT HANDOFF` e os arquivos conhecidos;
6. identificar mudanças relevantes não documentadas.

Mudança pequena que, pelas regras abaixo, não exige atualizar o handoff não o torna `STALE`. Mudança relevante não documentada torna o handoff `STALE` e força Full Context.

Uma decisão `LOCKED` pertinente sempre exige consulta à entrada oficial. Fast Context nunca autoriza ignorar, deduzir ou reinterpretar essa decisão.

### FULL CONTEXT — escalada

Além do Fast Context, ler integralmente `handoff/AI_CONTEXT.md`, `docs/production/DECISIONS_LOG.md`, `docs/production/PROJECT_STATE.md`, `docs/production/NEXT_STEPS.md` e os documentos pertinentes quando:

- o handoff estiver ausente, `PARTIAL` ou `STALE`;
- a revisão derivada do Git ou o worktree forem incompatíveis com o handoff;
- houver mudança relevante não documentada;
- uma decisão `LOCKED` puder mudar ou ser reinterpretada;
- começar sprint, sistema ou arquitetura nova;
- houver troca de owner;
- qualquer review estiver `PARTIAL` ou `BLOCKED`;
- `REVIEW MODE: REQUIRED` não possuir `LAST REVIEW STATUS: PASS`;
- documentação, código e runtime divergirem;
- a tarefa envolver visão, prioridade ou escopo global;
- `NEXT REQUIRED READS` exigir escalada.

`REVIEW MODE: NONE` não impede Fast Context. `CHECKPOINT` permite trabalho normal até o gatilho; passa a exigir Full Context se seu review ficar `PARTIAL` ou `BLOCKED`. Na dúvida sobre freshness, usar Full Context.

## Política de revisão por risco

Toda tarefa declara:

```text
REVIEW MODE: NONE / CHECKPOINT / REQUIRED
LAST REVIEW STATUS: NOT RUN / PASS / PARTIAL / BLOCKED
REVIEW SCOPE:
NEXT REVIEW TRIGGER:
```

### NONE

Usar para documentação pequena, sincronização de contexto, handoff, typo/formatação, ajuste isolado, correção pequena sem mudança de contrato, tuning provisório, refatoração local segura ou mudança sem impacto em decisão, runtime ou asset compartilhado. Codex executa self-validation e continua sem Claude.

### CHECKPOINT

Usar ao acumular tarefas relacionadas na mesma sprint, ao tornar um sistema testável ou perto de encerrar sprint, marco ou integração. Claude revisa uma vez no checkpoint, não após cada alteração.

### REQUIRED

Exigir `PASS` antes de continuar quando houver proposta de alterar decisão `LOCKED`, package/versão/configuração estrutural, mudança destrutiva, migração ampla, arquitetura transversal de alto impacto, vários sistemas críticos simultâneos, segurança/segredos, risco sério de perda, release ou regressão crítica sem causa.

Qualquer review `PARTIAL` ou `BLOCKED` força Full Context. `REQUIRED` sem `PASS` também bloqueia progressão.

## Estados, papéis e contrato

- `LOCKED`: só muda com aprovação do Game Director e atualização da fonte oficial.
- `WORKING`: direção reversível em teste.
- `OPEN`: questão não decidida.
- `TUNING / OPEN`: valor provisório de protótipo.
- É proibida promoção silenciosa entre estados.
- Codex é o implementer principal.
- Claude Code é reviewer de checkpoints/riscos e read-only por padrão; só implementa com `OWNER: CLAUDE CODE`.
- Apenas um agente edita os mesmos arquivos/assets ou escreve pelo Unity MCP por vez.

Antes de escrever, declarar:

```text
ROLE:
OWNER:
SCOPE:
OUT OF SCOPE:
DECISION STATE:
VALIDATION:
```

## Regras permanentes

- Projeto Unity oficial: `unity/`; `unity-bootstrap/` é `LEGACY / DO NOT USE`.
- Beta mobile landscape; câmera 3/4 elevada com rotação fixa inicialmente.
- Nosferatu Alucard é o primeiro personagem; `ALUCARD_PREALPHA_V01` está congelado até problema concreto no Unity e autorização.
- Jackal na mão direita; Casull na esquerda.
- Existência do Dash: `LOCKED`. Distância, duração, cooldown, direção precisa e invulnerabilidade: `OPEN` ou `TUNING / OPEN`.
- Não esperar arte final; evitar overengineering.
- Preservar alterações preexistentes; não tocar itens fora do `SCOPE`.
- Não criar commit ou push sem autorização explícita.
- Um ajuste documental não autoriza Unity, Blender ou gameplay.

## Atualização do handoff

`handoff/CURRENT_HANDOFF.md` é curto, rotativo e substituível. O implementer atualiza; o reviewer verifica quando houver review.

Atualizar após sprint/marco, alteração relevante multissistema, arquitetura/pipeline/package, cena/prefab relevante, mudança de decisão/ownership, handoff entre agentes ou commit que encerre etapa.

Não atualizar por typo, formatação, review sem novo estado, ajuste pequeno isolado, microbug sem mudança de contrato ou tuning trivial. O handoff nunca substitui `DECISIONS_LOG.md` ou `PROJECT_STATE.md`.
