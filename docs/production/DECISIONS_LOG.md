# Decisions Log

## LOCKED — Fechado

Todas as decisões desta seção estão `LOCKED`. Podem ser implementadas e validadas, mas só mudam com aprovação explícita do Game Director e atualização deste registro.

### Plataforma e perspectiva
- Beta: mobile.
- Orientação: landscape.

### Câmera de gameplay

`LOCKED` por decisão do Game Director:

1. A câmera principal usa projeção em perspectiva.
2. Não usar câmera ortográfica ou isométrica pura.
3. O plano é 3/4 elevado.
4. A câmera possui inclinação forte para o chão e profundidade visual perceptível.
5. A rotação diagonal permanece fixa inicialmente.
6. A câmera segue o Player.
7. A composição mostra área suficiente ao redor para leitura de inimigos, projéteis, telegraphs, loot, rotas, POIs e pontos de extração.
8. O Player permanece visualmente legível em ambientes densos.
9. A câmera não assume perspectiva over-the-shoulder durante o gameplay normal.
10. O movimento não depende dos valores internos da câmera.
11. O targeting não depende da implementação concreta do rig.
12. A implementação permanece configurável e substituível.

A câmera de gameplay de Diablo IV é referência somente para a família visual de perspectiva, altura e composição. Isso não autoriza copiar assets, cenário, iluminação, interface, level design, identidade visual ou qualquer conteúdo protegido.

Permanecem `TUNING / OPEN`: altura; distância; pitch; yaw exato; FOV; damping; suavização; look-ahead; deslocamento por movimento; deslocamento por mira; zoom contextual; tratamento de obstáculos; transparência de objetos; enquadramento exato do Player; adaptação para proporções de tela; valores específicos para celulares e tablets.

### Personagem inicial
- Nosferatu Alucard.

### Kit pré-run
- 2 armas.
- 2 poderes ativos escolhidos entre 4 disponíveis.
- 1 veste/equipamento.
- 1 configuração de Liberação.
- Dash fixo.

### Controles
- Analógico esquerdo.
- Ataque principal grande.
- Toque: auto-target.
- Arrasto no ataque: mira manual.
- 2 botões de poderes.
- Dash.
- Troca de arma.
- Liberação.

### Recursos
- Sangue.
- Almas.
- Restrição/Liberação.

### Limite de Almas no Beta
- Máximo: 3.

### Armas Beta
- Casull: uso frequente, precisão e marca.
- Jackal: impacto, perfuração, execução/anti-monstro.
- Troca de arma deve ter valor estratégico.

### Pool aprovado de poderes do Beta
- Predação.
- Familiar Sombrio.
- Marca Carmesim.
- Maré de Sangue.

Pool aprovado (não "candidatos"). Continuam OPEN: valores, custos, cooldown, comportamento final, balanceamento e ordem de implementação.

### Builds de referência
- Gunslinger.
- Vampiro.
- Híbrido.

### Pipeline
- Blender: assets 3D.
- Unity: jogo.
- VS Code: lógica C# e organização do código.
- Formato padrão planejado para transporte 3D: FBX.

### Governança multi-agent e projeto oficial
- O único projeto Unity de produção é `unity/`.
- `unity-bootstrap/` é `LEGACY / DO NOT USE`.
- Os quatro perfis oficiais do primeiro playable estão em `agents/specialists/`.
- Codex é o implementer principal; Claude Code é o reviewer principal e read-only por padrão.
- Claude Code só implementa quando uma tarefa declarar `OWNER: CLAUDE CODE`.
- Apenas um agente pode escrever no Unity MCP por vez.

### Asset 3D oficial do Alucard
- Existe um asset Pré-Alpha oficial do Alucard no repositório: `blender/characters/alucard/source/ALUCARD_PREALPHA_V01.blend`.
- A pasta externa da entrega original do Blender é preservada como fonte histórica e não deve ser alterada.
- Sources de Pré-Alpha usam a convenção `ALUCARD_PREALPHA_V##.blend`.
- Sources futuros de produção game-ready usam a convenção `ALUCARD_GAMEPLAY_V##.blend`.
- Versões anteriores não devem ser sobrescritas destrutivamente.
- Jackal permanece na mão direita.
- Casull permanece na mão esquerda.
- O arquivo Blender oficial deve ser versionado no repositório; backups automáticos `.blend1`, `.blend2`, `.blend3` e `.blend@` não devem ser versionados.

### Congelamento do Alucard Pré-Alpha
- `ALUCARD_PREALPHA_V01` está **FROZEN FOR FIRST GAMEPLAY TESTS** (2026-08-13).
- Nenhum refinamento adicional do asset (escala, FBX, materiais, avatar Humanoid) até que os primeiros testes reais no Unity revelem necessidade concreta.
- Pendências conhecidas (escala ~2,19–2,38 m vs. 1,98 m LOCKED; FBX sem meshes/materiais da Jackal e Casull; avatar Humanoid ainda não validado) permanecem registradas, mas não são tarefas imediatas.
- Nenhum arquivo `.blend` deve ser alterado enquanto esta decisão estiver em vigor.

### Contrato de extração

`LOCKED` por decisão do Game Director:

1. A extração acontece fisicamente dentro do mapa.
2. A extração é uma tentativa iniciada pelo jogador, mas nunca é automaticamente garantida.
3. O jogador escolhe qual rota tentar e quando assumir o risco.
4. Toda rota exige condição, recurso, exposição ou combinação desses elementos.
5. Não existe saída instantânea pelo menu.
6. Não existe botão de fuga disponível arbitrariamente em qualquer ponto do mapa.
7. Iniciar uma extração não consolida patrimônio.
8. Loot e patrimônio exposto somente são transferidos após conclusão válida da extração.
9. O jogo deve suportar mais de uma família de extração.
10. A implementação deve permitir substituir, adicionar ou remover rotas sem reconstruir run, inventário, stash ou save.
11. Settlement de sucesso, morte ou abandono deve possuir resolução única e testável.
12. Falhas parciais não podem duplicar nem apagar silenciosamente patrimônio.

Continuam `WORKING`, `OPEN` ou `TUNING / OPEN`: nomes das extrações; item necessário; duração das janelas; número e posição dos pontos; regras de cancelamento; intensidade e composição dos inimigos; influência exata de Threat; restrições de carga; consumo do item de emergência; representação temática; transporte, portal, alçapão ou mecanismo utilizado; custos, probabilidades, tempos e valores.

## VISION / LOCKED RECONCILIATION

### Ordem do marco jogável versus gate de extração — OPEN

- **Intenção do Production Pack:** validar primeiro um combat slice com Casull/Ghoul e tornar o circuito de loot → morte/extração → stash → persistência o primeiro gate crítico do produto; Jackal e powers completos aparecem depois no roadmap proposto.
- **Decisão atualmente registrada nos perfis/contexto:** `ALUCARD — PLAYABLE PRE-ALPHA 01` exige Casull, Jackal, weapon swap, dash, um poder e inimigo simples.
- **Natureza:** conflito de prioridade/ordem, não de identidade do produto.
- **Impacto:** muda tickets, gate da Sprint e o momento em que economia/persistência entram; não exige remover arma ou poder.
- **Recomendação:** preservar o marco atual e não reordenar implementação até o Game Director decidir entre: (a) concluir todo o marco antes da extração; ou (b) dividir formalmente o marco em Combat Slice e Product Extraction Gate.
- **Decisão necessária:** sequência oficial e nome dos gates.

### Estado formal da visão de extração — RESOLVED

- **Decisão do Game Director:** somente o contrato de extração registrado acima foi promovido a `LOCKED`.
- **Escopo resolvido:** presença física no mapa, tentativa com risco, escolha de rota e momento, requisitos por rota, proibição de fuga instantânea, settlement somente após conclusão válida, resolução terminal única, integridade contra falhas parciais, suporte a múltiplas famílias e desacoplamento das rotas em relação a run, inventário, stash e save.
- **Fora desta reconciliação:** consequência detalhada de morte, Cheddar, Threat, Last Death e Anti-Freak permanecem sem promoção automática.
- **Ainda `OPEN`:** a ordem entre o marco jogável atual e o gate P2 — Extraction Loop.

## OPEN

- Cadência final da Casull.
- Cadência final da Jackal.
- Valores de dano.
- Recarga vs munição infinita/cooldown.
- Custo exato de Sangue.
- Regras finais de ressurreição por Alma.
- Implementação exata dos níveis de Restrição.
- Qual poder será o primeiro protótipo funcional.
- Estilo visual final do cenário.
- Número de inimigos do Beta.
- Meta de FPS e aparelhos mínimos.
- Sequência entre o marco `ALUCARD — PLAYABLE PRE-ALPHA 01` e o gate de extração.
- Estado formal da consequência de morte e de Last Death; o contrato `LOCKED` de extração não resolve esses itens.
- Inclusão e ordem de produção da Anti-Freak Combat Pistol.
- Thresholds, nomes e efeitos finais de Threat.
- Política de desconexão e abandono de run.
- Contrato definitivo de run/profile/stash/save e migração de dados.
