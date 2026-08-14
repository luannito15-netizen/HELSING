# Mobile Controls

## Orientação
Landscape.

## Câmera de gameplay

`LOCKED` — perspectiva 3/4 elevada, inclinação forte para o chão, profundidade perceptível, rotação diagonal fixa inicialmente e seguimento do Player. A composição precisa manter Player, inimigos, projéteis, telegraphs, loot, rotas, POIs e pontos de extração legíveis em tela pequena e ambientes densos.

Não usar câmera ortográfica/isométrica pura ou over-the-shoulder no gameplay normal. Movimento e targeting permanecem independentes do rig. Enquadramento exato, FOV, damping, look-ahead, offsets, zoom, obstáculos, transparência e adaptação entre celulares/tablets são `TUNING / OPEN`.

## Referências de ergonomia
- Wild Rift.
- Diablo Immortal.

## Layout funcional

### Lado esquerdo
- Analógico virtual de movimentação.

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
