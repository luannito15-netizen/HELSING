# Arquitetura V01

Esta arquitetura é inicial e deve permanecer simples até o primeiro protótipo jogável.

## Sistemas

### Player
Responsável por:
- movimento;
- estado do jogador;
- conexão com input.

### Input
Responsável por:
- joystick;
- ataque;
- aim drag;
- skills;
- dash;
- weapon swap;
- Liberação.

### Targeting
Responsável por:
- encontrar alvos válidos;
- selecionar alvo automático;
- respeitar mira manual.

### Weapons
Responsável por:
- arma atual;
- Casull;
- Jackal;
- cadência;
- disparo;
- weapon swap.

### Combat
Responsável por:
- dano;
- hit;
- vida;
- morte.

### Resources
Responsável por:
- Sangue;
- Almas;
- Restrição/Liberação.

### Abilities
Responsável por:
- duas skills equipadas;
- cooldown/custo;
- execução de poderes.

### Enemies
Responsável por:
- dummy inicial;
- aquisição de alvo;
- deslocamento;
- ataque simples;
- morte.

## Regra arquitetural

Evitar construir frameworks complexos antes do primeiro playable.

Foco:
funcionar, testar, substituir e iterar.
