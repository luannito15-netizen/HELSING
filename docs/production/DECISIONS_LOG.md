# Decisions Log

## Fechado

### Plataforma e perspectiva
- Beta: mobile.
- Orientação: landscape.
- Câmera: 3/4 elevada, próxima da leitura de Diablo.
- Rotação da câmera fixa inicialmente.

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
