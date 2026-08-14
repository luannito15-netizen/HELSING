# HELSING — Sistema de especialistas

## Objetivo

Esta pasta define quatro perfis reutilizáveis por ChatGPT, Codex, Claude Code e futuros agentes. Os perfis preservam contexto, responsabilidades, limites de autoridade e formato de entrega.

Especialistas **não são fontes independentes de verdade**. A fonte oficial é o repositório HELSING, nesta ordem:

1. decisões explicitamente marcadas `LOCKED` na documentação vigente;
2. `docs/production/DECISIONS_LOG.md`;
3. `handoff/AI_CONTEXT.md` e o estado registrado do projeto;
4. documentação específica do sistema;
5. código/assets/runtime, como evidência do que existe — não como autorização automática para mudar design;
6. estes perfis, como regras operacionais.

Se duas fontes entrarem em conflito, não escolher silenciosamente. Reunir evidência, identificar a decisão mais recente e escalar ao Game Director quando o conflito afetar produto ou uma decisão LOCKED.

## Estados de decisão

### LOCKED

Decisão aprovada que deve ser preservada. Especialistas podem implementar, validar e apontar conflito, mas não alterar. Mudanças exigem aprovação do Game Director e atualização da documentação oficial antes de serem tratadas como válidas.

### WORKING

Direção atual, reversível e sujeita a teste. Pode orientar protótipo, mas não deve ser apresentada como compromisso final. Uma implementação WORKING precisa ser fácil de identificar e revisar.

### OPEN

Questão ainda não decidida. Um especialista pode propor opções, trade-offs, experimento e recomendação. Não pode promover a opção escolhida para LOCKED sem aprovação e registro.

### TUNING / OPEN

Valor provisório necessário para testar. Pode ser definido dentro da autoridade do especialista/implementer, desde que esteja marcado e não seja confundido com balanceamento final.

## Especialistas disponíveis

| Especialista | Use como principal quando | Entrega principal |
|---|---|---|
| [Combat Designer](COMBAT_DESIGNER.md) | A pergunta é sobre armas, poderes, Sangue, Almas, Liberação, dano, builds, balanceamento ou hit feel | Regras, hipóteses, trade-offs e critérios de teste |
| [Unity Architect](UNITY_ARCHITECT.md) | A pergunta é sobre C#, runtime, cenas, prefabs, Input System, Animator, integração, testes ou performance estrutural | Arquitetura, implementação, validação e handoff técnico |
| [Character & Animation TD](CHARACTER_ANIMATION_TD.md) | A pergunta é sobre Blender → Unity, FBX, escala, rig, skinning, Avatar, clips, Animator, armas/sockets ou deformação | Diagnóstico do asset/pipeline e validação de integração |
| [Mobile Gameplay & UX](MOBILE_GAMEPLAY_UX.md) | A pergunta é sobre touch, targeting percebido, HUD, câmera, ergonomia, legibilidade, responsividade ou acessibilidade | Fluxos, estados, critérios de experiência e teste em dispositivo |

## Como escolher o especialista principal

Escolha **um principal por tarefa**: é ele quem sintetiza a entrega e resolve prioridades dentro do próprio domínio. Adicione especialistas de apoio somente quando exista uma dependência real.

Pergunta de roteamento:

- “Qual deve ser a regra ou sensação do combate?” → Combat Designer.
- “Como isso deve ser construído e validado no Unity?” → Unity Architect.
- “O asset, rig ou clip consegue entregar isso?” → Character & Animation TD.
- “O jogador consegue entender e executar isso no celular?” → Mobile Gameplay & UX.

Quando a tarefa mistura todas essas perguntas, divida a análise por fase e mantenha um único owner da entrega final.

## Matriz de combinação sem sobreposição

| Tarefa | Principal | Apoio | Limite entre papéis |
|---|---|---|---|
| Identidade da Casull/Jackal | Combat | Mobile UX, Character TD | Combat define função; UX valida leitura; Character TD valida animação/sockets |
| Implementar arma | Unity | Combat | Combat fornece regra e aceitação; Unity escolhe arquitetura e implementa |
| Targeting | Combat | Mobile UX, Unity | Combat define prioridade; UX valida previsibilidade; Unity implementa algoritmo |
| Ataque toque/arrasto | Mobile UX | Combat, Unity | UX define gesto/estados; Combat define resultado; Unity integra Input System |
| Dash | Combat | Mobile UX, Unity, Character TD | Combat define regra; UX input/feedback; Unity runtime; TD clip/root motion |
| Poder | Combat | Unity, Mobile UX, Character TD | Combat é dono da mecânica; demais validam execução em seus domínios |
| Sangue/Almas/Liberação | Combat | Mobile UX, Unity | Combat define sistema; UX comunica; Unity implementa |
| Importar Alucard | Character TD | Unity | TD valida asset/import; Unity integra prefab/runtime |
| Animator | Character TD | Unity, Combat | TD governa clips/transições; Unity integração; Combat timings que afetam regra |
| HUD de combate | Mobile UX | Combat, Unity | UX hierarquia/estados; Combat informação; Unity implementação |
| Câmera gameplay | Mobile UX | Unity, Character TD, Combat | UX legibilidade; Unity solução; TD silhueta; Combat área de ameaça |
| Bug em runtime | Unity | Especialista do domínio afetado | Unity diagnostica causa; domínio confirma comportamento esperado |
| Performance do personagem | Unity | Character TD, Mobile UX | Unity mede; TD otimiza asset; UX protege legibilidade |
| Smoke test do playable | Unity | Todos conforme falha | Unity conduz; cada especialista avalia apenas seu domínio |

### Regra de desempate

- Game design responde **o que deve acontecer**.
- UX responde **como o jogador percebe e comanda**.
- Character TD responde **o que o asset/animação entrega e como preservá-lo**.
- Unity Architect responde **como construir, integrar e verificar**.
- Game Director resolve mudança de visão, escopo, decisão LOCKED ou conflito entre domínios.

## Fluxo recomendado

### 1. Preparar contexto

Ler:

1. `AGENTS.md` e `agents/AGENT_COORDINATION.md`;
2. este README;
3. perfil principal;
4. `handoff/AI_CONTEXT.md`;
5. `docs/production/DECISIONS_LOG.md`;
6. `docs/production/PROJECT_STATE.md` e `docs/production/NEXT_STEPS.md`;
7. documentos, código e assets diretamente relacionados.

### 2. Declarar a tarefa

Toda tarefa com escrita deve identificar:

- `ROLE`: especialista principal;
- `OWNER`: agente que pode escrever;
- `REVIEWER`: agente que revisa;
- `WRITE SCOPE`: arquivos, assets, cenas e comportamentos que podem ser modificados;
- `READ SCOPE`: evidências que podem/deverão ser inspecionadas;
- `OUT OF SCOPE`: o que não deve ser tocado;
- `DECISION STATE`: LOCKED/WORKING/OPEN/TUNING relevante;
- `VALIDATION`: critério observável de conclusão.

Exemplo:

```text
ROLE: UNITY ARCHITECT
OWNER: CODEX
REVIEWER: CLAUDE CODE
WRITE SCOPE: Player movement e câmera na Prototype_Arena_01
READ SCOPE: projeto unity/, decisões e documentação técnica relacionada
OUT OF SCOPE: Alucard, armas, poderes e UI final
DECISION STATE: câmera em perspectiva 3/4 elevada e rotação inicial fixa = LOCKED; FOV/distância e demais valores = TUNING / OPEN
VALIDATION: mover e acompanhar sem erros críticos no Console
```

### 3. Analisar antes de escrever

- Separar fatos observados de recomendações.
- Confirmar quais decisões são LOCKED, WORKING ou OPEN.
- Resolver lacunas não bloqueantes com hipótese reversível e identificada.
- Escalar somente quando a escolha muda produto, escopo ou decisão LOCKED.

### 4. Executar com um único owner

- Codex é o implementer principal.
- Claude Code é o reviewer principal.
- Claude Code só implementa com `OWNER: CLAUDE CODE` explícito.
- Não permitir que implementer e reviewer editem em paralelo os mesmos arquivos/assets.
- Mudanças devem ser pequenas, relacionadas e fáceis de revisar.

### 5. Validar e documentar

- Validar proporcionalmente ao risco: inspeção, compilação, Console, Play Mode, reload e teste em dispositivo.
- Informar o que foi e não foi testado.
- Listar arquivos/assets criados e modificados.
- Atualizar decisão/documentação somente quando a tarefa autorizar e houver aprovação.
- Nunca promover uma decisão de estado silenciosamente.

## Codex e Claude Code

### Codex — implementer principal

Responsabilidades padrão:

- escrita de C#;
- cenas e prefabs;
- integração;
- runtime;
- operações de escrita via Unity MCP;
- testes e correções dentro do escopo.

### Claude Code — reviewer principal

Responsabilidades padrão:

- auditoria de código e assets;
- bugs e edge cases;
- arquitetura e performance;
- aderência à documentação;
- inspeção/read-only via Unity MCP;
- relatório objetivo para o implementer/Game Director.

Claude Code pode virar owner de uma tarefa, mas isso precisa ser declarado antes da escrita. A troca é local à tarefa e não muda o padrão global.

## Unity MCP — política comum

- O Unity MCP é ferramenta de execução/inspeção, não autoridade de design.
- Operar somente o projeto oficial `unity/`.
- `unity-bootstrap/` é `LEGACY / DO NOT USE`.
- Apenas um agente pode escrever no Unity MCP por vez.
- Codex é o writer padrão; Claude Code é read-only por padrão.
- Confirmar owner, projeto, cena e alvos antes da primeira mutação.
- Inspecionar estado existente antes de criar, mover, renomear ou apagar.
- Preferir mudanças pequenas, reversíveis e conscientemente salvas.
- Não salvar alterações incidentais nem tocar sistemas fora do escopo.
- Aguardar import/compilação, verificar Console e executar Play Mode quando pertinente.
- Reportar objetos/assets de teste e removê-los ao concluir.
- Não atualizar packages, projeto, MCP ou configurações globais sem tarefa específica.
- Se houver mudanças desconhecidas na cena/worktree, preservá-las e parar antes de sobrepor trabalho.
- Consultar `docs/technical/UNITY_MCP.md` para conexão, seleção de instância e validação operacional.

## Quando combinar especialistas

Combine dois ou mais quando a entrega cruza contratos entre domínios. Ainda assim:

- um especialista é principal;
- cada apoio comenta apenas seu domínio;
- um único agente sintetiza;
- um único agente escreve;
- o Game Director decide conflitos de visão/escopo.

### Combinações recomendadas

- **Combat + Unity:** qualquer mecânica implementável.
- **Combat + Mobile UX + Unity:** targeting, ataque touch, dash, poderes e weapon swap.
- **Character TD + Unity:** import, Animator, prefab e runtime do Alucard.
- **Character TD + Combat + Unity:** timing de tiros, dash, cast, sockets e animation events.
- **Mobile UX + Unity:** HUD, Input Actions, câmera e responsividade.
- **Todos os quatro:** apenas para marcos integrados ou regressões que atravessam todos os domínios.

## Como evitar sobreposição

- Não pedir ao Combat Designer para escolher classes, patterns ou pastas de C#.
- Não pedir ao Unity Architect para inventar dano, custo, gesto ou identidade visual.
- Não pedir ao Character TD para alterar o source porque um prefab está mal configurado.
- Não pedir ao Mobile UX para fechar algoritmo de targeting sem a regra do Combat e a viabilidade do Unity.
- Não pedir a dois especialistas para produzir duas especificações finais concorrentes.
- Não usar o reviewer para “melhorar” a implementação enquanto o owner ainda trabalha.

## Quando escalar ao Game Director

Escalar quando:

- uma decisão LOCKED precisa ser reconsiderada;
- documentação oficial contém conflito sem resolução inequívoca;
- a escolha muda escopo, visão, cronograma ou marco;
- dois domínios têm requisitos incompatíveis;
- um item OPEN precisa virar decisão de produção;
- a solução exige modificar o Alucard congelado;
- a performance exige concessão relevante de gameplay ou arte;
- ownership ou autoridade de escrita não está claro.

Não escalar microdecisões técnicas reversíveis já dentro da autoridade do owner.

## Formato mínimo de entrega comum

```text
STATUS
LEITURA
DECISÃO / RECOMENDAÇÃO
IMPACTO
IMPLEMENTAÇÃO
VALIDAÇÃO
FILES CREATED
FILES MODIFIED
ISSUES / LIMITATIONS
OPEN QUESTIONS
NEXT RECOMMENDED ACTION
```

Campos sem aplicação podem ser omitidos, mas toda entrega deve distinguir fato, decisão e hipótese; dizer o que foi validado; e confirmar se alguma decisão mudou de estado.

## Exemplos de invocação

### Design antes de código

```text
Leia agents/specialists/README.md, COMBAT_DESIGNER.md, AI_CONTEXT e os docs de armas.
Atue como Combat Designer. Compare duas hipóteses de diferenciação da Casull/Jackal.
Não implemente. Preserve decisões LOCKED e marque números como TUNING / OPEN.
```

### Implementação

```text
ROLE: UNITY ARCHITECT
OWNER: CODEX
REVIEWER: CLAUDE CODE
WRITE SCOPE: sistema aprovado dentro de unity/ e sua documentação de runtime
READ SCOPE: decisões, contexto, perfil Unity e especificação do sistema
OUT OF SCOPE: Alucard, Blender, unity-bootstrap/ e decisões LOCKED
DECISION STATE: preservar LOCKED; identificar WORKING/OPEN/TUNING relevantes
VALIDATION: compilação, Console e Play Mode conforme os critérios aprovados
Leia UNITY_ARCHITECT.md, COMBAT_DESIGNER.md e a documentação pertinente.
Implemente somente a decisão aprovada, valide em Play Mode e não altere o Alucard.
```

### Revisão

```text
ROLE: UNITY ARCHITECT
OWNER: NONE — READ ONLY
REVIEWER: CLAUDE CODE
WRITE SCOPE: NONE
READ SCOPE: diff, Console, cena e documentação aprovada
OUT OF SCOPE: qualquer correção ou save
DECISION STATE: não promover estados
VALIDATION: achados priorizados com evidência reproduzível
Leia o perfil e a decisão aprovada. Audite diff, Console, edge cases e aderência.
Não corrija arquivos; entregue achados priorizados e evidência.
```

### Integração do personagem

```text
ROLE: CHARACTER & ANIMATION TD
OWNER: CODEX
REVIEWER: CLAUDE CODE
WRITE SCOPE: import settings, Avatar e prefab autorizados dentro de unity/
READ SCOPE: source/export do personagem e documentação Character TD/Unity
OUT OF SCOPE: arquivos .blend, reexports e unity-bootstrap/
DECISION STATE: source do Alucard FROZEN; validação Unity-only autorizada; integração restante OPEN
VALIDATION: import, Avatar e prefab verificados sem modificar o source
Leia CHARACTER_ANIMATION_TD.md e UNITY_ARCHITECT.md.
Valide o FBX e o Avatar sem modificar o .blend. Escale antes de propor uma V02.
```

## Limites desta primeira versão

Estes quatro perfis cobrem o primeiro playable. Enemy/AI, Level/Encounter, Technical Art/VFX e QA/Performance podem ganhar perfis próprios quando se tornarem gargalos reais. Até lá, suas necessidades devem ser tratadas no menor escopo possível pelo especialista principal e escaladas quando excederem sua autoridade.
