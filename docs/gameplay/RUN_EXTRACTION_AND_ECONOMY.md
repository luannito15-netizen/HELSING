# Run, Extraction and Economy

Este documento é a fonte canônica para o contrato `LOCKED` de extração e para os contratos ainda reversíveis do loop de incursão, risco, morte, economia e progressão do HELSING. O registro mestre de decisões `LOCKED` continua em `docs/production/DECISIONS_LOG.md`.

## Product vision

`VISION PRINCIPLE` — cada run é uma aposta compreensível. O jogador leva um loadout real, busca recursos e objetivos, escolhe quanto risco adicional aceita e decide quando tentar converter o patrimônio exposto em progresso persistente por meio da extração.

## Run contract

`WORKING` — uma incursão deve suportar intenções diferentes: farm seguro, contrato, recuperação, elite, componente específico, teste de build ou boss. A mesma área deve permitir mais de uma rota e não possuir uma opção universalmente ótima.

Fluxo observável:

1. escolher necessidade econômica ou contrato;
2. montar loadout e visualizar o que está exposto;
3. entrar, navegar, combater e coletar;
4. controlar ou elevar Threat;
5. reavaliar loot, recursos e rota;
6. ativar uma extração física ou continuar;
7. cumprir os requisitos e concluir a tentativa sob risco;
8. converter somente o resultado elegível em stash e progressão.

Bandas de duração de run permanecem `TUNING / OPEN`. As referências do Production Pack — farm rápido, incursão padrão, caça/boss e greed run — são hipóteses de teste, não metas finais.

## Risco e propriedade

### Progressão segura — `WORKING`

Persistem entre runs: XP/nível, skills aprendidas, blueprints, módulos da Base, stash, conhecimento do mapa, receitas/codex e configurações.

### Patrimônio exposto — `WORKING`

Podem ser perdidos: armas equipadas, munição, consumíveis, materiais coletados, relíquias carregadas e recursos ainda não consolidados quando aplicável.

### Princípio de perda — `VISION PRINCIPLE`

A morte deve gerar uma decisão diferente na próxima run. A perda não pode ser irrelevante nem impedir a reconstrução; sempre deve existir um caminho budget de retorno ao jogo.

## Extração e settlement

### Contrato de extração

`LOCKED`:

1. A extração acontece fisicamente dentro do mapa.
2. A extração é uma tentativa iniciada pelo jogador, mas nunca é automaticamente garantida.
3. O jogador escolhe qual rota tentar e quando assumir o risco.
4. Toda rota exige condição, recurso, exposição ou combinação desses elementos.
5. Não existe saída instantânea pelo menu.
6. Não existe botão de fuga disponível arbitrariamente em qualquer ponto do mapa.
7. Iniciar uma extração não consolida patrimônio.
8. Loot e patrimônio exposto somente são transferidos após conclusão válida da extração.
9. O jogo deve suportar mais de uma família de extração.
10. Rotas podem ser substituídas, adicionadas ou removidas sem reconstruir run, inventário, stash ou save.
11. Settlement de sucesso, morte ou abandono possui resolução única e testável.
12. Falhas parciais não podem duplicar nem apagar silenciosamente patrimônio.

O contrato define o resultado e os limites do sistema, não uma implementação concreta. Nomes das extrações; item necessário; duração das janelas; número e posição dos pontos; regras de cancelamento; intensidade e composição dos inimigos; influência exata de Threat; restrições de carga; consumo do item de emergência; representação temática; transporte, portal, alçapão ou mecanismo utilizado; custos, probabilidades, tempos e valores permanecem `WORKING`, `OPEN` ou `TUNING / OPEN`.

Contrato de integridade:

- pickup não persiste diretamente no stash;
- somente um settlement terminal pode ocorrer;
- extração bem-sucedida transfere itens elegíveis exatamente uma vez;
- morte ou abandono não transferem patrimônio exposto;
- exceções, como Secure Slot, exigem caminho explícito e testável;
- falha parcial nunca pode duplicar nem apagar silenciosamente propriedade.

### Famílias obrigatórias para exploração futura

Estas famílias são caminhos obrigatórios de design, mas seus nomes, valores e regras concretas não estão `LOCKED`.

#### Extração de reforço

- ponto físico no mapa;
- solicitação de recolhimento;
- posição denunciada;
- janela de exposição;
- pressão inimiga enquanto os reforços chegam;
- interação final para concluir a retirada.
- tempo exato permanece `TUNING / OPEN`.

#### Extração de emergência

- ponto ou âncora especial;
- exige item raro ou consumível;
- pode ser mais rápida ou flexível, mas nunca sem risco;
- não funciona arbitrariamente em qualquer lugar;
- não pode tornar a extração comum irrelevante.
- consumo, restrições e momento de confirmação permanecem `OPEN`.

#### Extrações contextuais futuras

- restaurar energia;
- encontrar chave;
- derrotar guardião;
- cumprir contrato;
- ativar mecanismos;
- concluir evento;
- alcançar uma saída condicionada pelo estado do mapa.

`NEXT DESIGN TRIGGER: antes de implementar P2 — Extraction Loop, revisar rotas de extração, Threat, requisitos, duração, cancelamento, UX, settlement e integridade econômica.`

`ARCHITECTURAL COMMITMENT — GAME DIRECTOR / UNITY ARCHITECT REVIEW REQUIRED` — o boundary definitivo entre run, profile, stash e save terá alto custo de migração depois que dados persistentes existirem. Antes de implementá-lo, aprovar contratos, versionamento e settlement atômico.

## Morte, recuperação e ressurreição

`WORKING` — morte remove patrimônio exposto e preserva progressão segura. O contrato ainda não é `LOCKED` no registro oficial.

`WORKING / FUTURE SCOPE` — `Last Death` pode oferecer uma única oportunidade de recuperação, com aumento de pressão no local; morrer ou abandonar a recuperação resolve o equipamento como perdido.

`OPEN` — política de desconexão, detalhes do abandono, expiração da recuperação e conteúdo exato do registro de morte.

`FUTURE SCOPE` — ressurreição por Alma e ferimento pós-morte só devem ser testados após o loop básico de perda funcionar; não podem anular a decisão de extração.

## Economia

`VISION PRINCIPLE` — poucos recursos com usos concorrentes devem gerar objetivos espontâneos de farm. O mapa participa da economia por meio de identidades claras de POI e fontes coerentes.

Sangue, Almas e Restrição/Liberação permanecem `LOCKED` como sistemas centrais; o máximo de 3 Almas no Beta também permanece `LOCKED`. Os demais recursos econômicos abaixo são `WORKING`:

- Hellsing Credits;
- Scrap Metal;
- Gun Parts;
- Military Components;
- Silver;
- Blood — existência central `LOCKED`; papel econômico ampliado `WORKING`;
- Pure Blood — `WORKING`;
- Souls — existência e máximo `LOCKED`; usos econômicos ampliados `WORKING`/`OPEN`.

Valores, receitas, taxas, faucets, sinks e relação loadout/retorno do Production Pack são `TUNING / OPEN`. Não devem ser hardcoded nem tratados como balanceamento aprovado.

### Blueprint e unidade física

`WORKING` — blueprint representa conhecimento persistente; cada unidade física continua sendo fabricada, encontrada ou adquirida e pode ser perdida quando exposta.

### Inventário, stash e Secure Slot

- grid de run `6×5`: `TUNING / OPEN`;
- stash inicial e upgrades de capacidade: `TUNING / OPEN`;
- um Secure Slot pequeno: `WORKING`, com tamanho, elegibilidade e economia `OPEN`;
- armas, vestes e relíquias grandes não devem receber proteção silenciosa.

O inventário deve produzir custo de oportunidade, não microgerenciamento excessivo.

## Base Hellsing e progressão

`WORKING / FUTURE SCOPE` — a Base é um hub funcional de conversão, não construção livre. Domínios previstos: Arsenal, Laboratory, Archive, Restriction Chamber e Stash.

`WORKING` — XP abre elegibilidade; blueprints preservam conhecimento; recursos e módulos convertem intenção em unidade física ou capacidade. Curvas de nível, tempos e custos são `TUNING / OPEN`.

## Threat

`VISION PRINCIPLE` — Threat transforma poder em risco voluntário dentro da mesma área. O jogador pode permanecer discreto ou provocar respostas mais perigosas em busca de melhores oportunidades.

`WORKING` — modelo de quatro estados, de `0` a `3`, com leitura crescente e impacto em encontros, recompensas e extração.

`TUNING / OPEN` — nomes, thresholds, pontos por ação, multiplicadores de spawn/recompensa, ausência de decaimento e intensidade da extração. Esses valores devem permanecer configuráveis e orientados por evidência.

Threat, Restrição/Liberação e recursos não são o mesmo sistema. As integrações entre eles precisam de comandos ou eventos explícitos, sem acesso direto aos detalhes internos.

## Cheddar Village

`WORKING / FUTURE SCOPE` — Cheddar é a primeira região de produto proposta para validar navegação, identidade econômica, rotas, Threat, recuperação e extração. `Prototype_Arena_01` continua sendo uma arena técnica separada e não representa Cheddar.

POIs, contagens, extrações, eventos, bosses e rotas do Production Pack são direções de conteúdo `WORKING` ou `TUNING / OPEN`; não fazem parte da Sprint 01 sem promoção explícita de escopo.

## Escopo futuro

`FUTURE SCOPE` — durabilidade complexa, seguro completo, backend/cloud save, PvP/multiplayer, monetização, construção livre de Base, múltiplos personagens/mapas, live ops e procedural generation complexo.

## Open decisions

- definir o estado formal da consequência de morte e de Last Death;
- reconciliar a ordem do marco `ALUCARD — PLAYABLE PRE-ALPHA 01` com o gate de extração;
- definir política de desconexão/abandono;
- aprovar o boundary de persistência antes de criar save compatível;
- decidir inclusão e ordem da Anti-Freak no Pré-Alpha;
- definir thresholds, economia e conteúdo somente após testes.
