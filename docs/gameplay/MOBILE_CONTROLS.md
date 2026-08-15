# Mobile Controls

## Orientação
Landscape.

## Câmera de gameplay

`LOCKED` — perspectiva 3/4 elevada, inclinação forte para o chão, profundidade perceptível, rotação diagonal fixa inicialmente e seguimento do Player. A composição precisa manter Player, inimigos, projéteis, telegraphs, loot, rotas, POIs e pontos de extração legíveis em tela pequena e ambientes densos.

Não usar câmera ortográfica/isométrica pura ou over-the-shoulder no gameplay normal. Movimento e targeting permanecem independentes do rig. Enquadramento exato, FOV, damping, look-ahead, offsets, zoom, obstáculos, transparência e adaptação entre celulares/tablets são `TUNING / OPEN`.

## Movimento e mira

`LOCKED` — movimento e mira são intenções independentes. O analógico esquerdo controla movimento relativo à tela/câmera sem determinar o facing. O Player pode avançar, recuar ou fazer strafe mantendo a direção de mira, inclusive enquanto está parado.

WASD/setas/gamepad e o analógico virtual alimentam o mesmo `move intent`. A posição do mouse e o arrasto manual alimentam o mesmo conceito de aim intent/facing; enquanto existir uma mira válida, movimento nunca a sobrescreve.

## Implementação greybox — `CORE-002`

`CURRENT IMPLEMENTATION / WORKING`:

- analógico fixo no lado esquerdo, com dead zone e raio serializados;
- área ampla de arrasto no lado direito como placeholder do futuro gesto sobre ataque;
- UI escreve somente intents em `PlayerInputReader`, sem conhecer `PlayerMovement`, `PlayerAimFacing` ou câmera;
- drag manual tem prioridade enquanto ativo;
- ao soltar, a última direção permanece por uma janela curta antes de o mouse retomar;
- toque sem ultrapassar o threshold não produz auto-target nem outra ação;
- mouse na Game View percorre os mesmos eventos de pointer usados para simular touch;
- `CanvasScaler` e `SafeAreaRoot` preservam o layout em landscape e safe areas.

Essa área de drag não é o botão de ataque final, HUD ou arte de produto. Dead zone, threshold, janela após release, raio, tamanho, posição, opacidade, sensibilidade e tratamento de cancelamento permanecem `TUNING / OPEN`.

Velocidade de rotação, raycast versus plano matemático, layers de superfície, distância mínima, comportamento fora da Game View, indicador greybox e assistência futura de targeting permanecem `TUNING / OPEN`.

## Referências de ergonomia
- Wild Rift.
- Diablo Immortal.

## Layout funcional

### Lado esquerdo
- Analógico virtual de movimentação — greybox funcional em `CORE-002`.

### Lado direito
- Ataque principal grande.
- Poder 1.
- Poder 2.
- Dash.
- Troca de arma.
- Liberação.

### Contextual

- interação com loot, objetos, eventos e extrações;
- apresentação deve evitar um botão permanente adicional quando uma ação contextual inequívoca for suficiente, sem fechar a solução antes de teste.

## Ataque principal

### Toque
- Dispara no alvo escolhido automaticamente.
- Auto-target deve priorizar ameaças válidas e visíveis.

### Arrasto
- Converte o botão em mira manual.
- Direção do arrasto determina direção/alvo do disparo.
- Alimenta a mesma intenção planar usada pelo facing, sem acoplar mira ao movimento.
- Em `CORE-002`, uma área direita provisória valida somente direção/facing; disparo e target permanecem fora de escopo.

## Objetivos
- Permitir jogar sem depender de precisão de mouse.
- Manter controle manual disponível para jogador avançado.
- Evitar excesso de botões.
- Fazer weapon swap ser rápido e intencional.
- Preservar multi-touch entre movimento, ataque/mira e ação secundária.
- Manter Player, target prioritário e telegraphs fora das principais zonas de oclusão pelos dedos.

## Informação de run — `WORKING`

Durante a incursão, o HUD precisa tornar legíveis HP/Blood, Almas, arma/ammo, dois poderes, dash e Threat. Extração, inventário/valor e contrato aparecem conforme contexto e prioridade.

Antes da run, a interface deve comunicar loadout, itens expostos, proteção/secure slot quando existir, objetivo e alertas críticos. Após morte, deve mostrar com clareza o que foi perdido e se há recuperação disponível.

Layout, tamanhos, safe areas, feedback, haptics e hierarquia visual final permanecem `OPEN`. Debug UI não representa a direção visual de produto.

## OPEN
- Distância máxima de aquisição do auto-target.
- Prioridade exata de alvo.
- Comportamento ao arrastar e soltar fora do raio.
- Aim assist.
- Dead zones.
- Sensibilidade.
- Cancelamento de skill.
- comportamento da interação contextual;
- visualização de risco/valor e fluxo de extração;
- layout para telas largas, tablets, safe areas e canhotos.
