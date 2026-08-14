# Combat System

## Armas

O personagem carrega duas armas simultaneamente (`LOCKED`). O pool de armas disponíveis e sua ordem de produção não são a mesma decisão que a quantidade de slots equipados.

### Casull — identidade funcional
- arma de uso frequente;
- precisão;
- marca;
- resposta rápida;
- deve sustentar o combate básico.

### Jackal — identidade funcional
- impacto;
- perfuração;
- anti-monstro;
- execução;
- ritmo mais pesado.

### Anti-Freak Combat Pistol — `WORKING / FUTURE SCOPE`

O Production Pack propõe uma opção intermediária anti-elite/stagger com economia de Silver. Ela não substitui Casull ou Jackal, não integra o marco atual automaticamente e ainda não foi promovida a decisão `LOCKED`.

## Weapon Swap
A troca deve ser parte da estratégia, não cosmética.

Possíveis gatilhos futuros:
- inimigos blindados;
- inimigos marcados;
- janela de execução;
- bônus por alternância;
- efeitos de build.

Esses gatilhos ainda estão OPEN.

## Dash
- fixo no kit;
- função principal: reposicionamento e evasão;
- direção visual pode usar sombra/névoa/vermelho-preto;
- implementação exata de invulnerabilidade permanece OPEN.

## Dano e hit feel
Prioridades futuras:
- muzzle flash legível;
- impacto diferente por arma;
- feedback de dano claro;
- Jackal deve parecer substancialmente mais pesada.

## Custo e risco — `WORKING`

Armas e munição fazem parte do patrimônio exposto da run. Blueprint pode representar conhecimento persistente, enquanto cada unidade física ainda precisa ser obtida/fabricada e pode ser perdida. Regras de ammo, reload, crafting, custo e reposição permanecem `OPEN` ou `TUNING / OPEN`.

O sistema de arma não deve possuir inventário, stash ou save. Ele solicita consumo ao owner do estado da run e delega dano por contrato explícito.

## Primeiro protótipo
Não implementar balanceamento complexo.

Basta:
- dano base;
- HP;
- cooldown/cadência;
- morte;
- alvo;
- troca de arma;
- dash.

O slice inicial pode validar Casull antes da profundidade completa, mas isso não remove o marco `LOCKED` que exige Casull, Jackal, weapon swap e um poder. A ordem entre concluir esse marco e antecipar extração está pendente de reconciliação oficial.
