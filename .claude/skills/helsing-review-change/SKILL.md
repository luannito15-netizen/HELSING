---
name: helsing-review-change
description: Revisar mudanças, código, documentação, configuração e assets do HELSING sem editar. Usar quando o usuário pedir auditoria, code review, detecção de bugs, edge cases, aderência às decisões, performance, regressão, revisão de diff ou validação do trabalho feito por Codex/outro implementer.
---

# Revisar mudança HELSING

## Manter modo read-only

Atuar como `REVIEWER` com `OWNER: NONE`.

Não editar arquivos, não resolver achados, não salvar cenas, não fazer commit e não reformatar conteúdo. Se o usuário pedir correção junto com a revisão, primeiro concluir o diagnóstico; implementar somente se também houver `OWNER: CLAUDE CODE` explícito.

## Preparar a revisão

1. Executar o fluxo de `helsing-route-task`.
2. Ler o especialista principal e a decisão aprovada.
3. Inspecionar status/diff e somente os arquivos/assets relacionados.
4. Identificar mudanças preexistentes do usuário e preservá-las.
5. Distinguir comportamento documentado de comportamento inferido do runtime.

## Revisar por risco

Verificar:

- Violação de decisão `LOCKED` ou promoção silenciosa de estado.
- Comportamento incorreto, regressão e casos-limite.
- Erros de lifecycle, serialização e referências Unity.
- Arquitetura desproporcional ao pre-alpha.
- Alocações por frame, buscas globais e custo mobile evitável.
- Divergência entre input desktop provisório e contrato touch futuro.
- Ausência ou fragilidade de validação.
- Mudanças fora do escopo e arquivos incidentais.
- Valores de tuning tratados como definitivos.
- Documentação que não corresponde ao estado real.

Não listar preferências estilísticas como defeitos. Priorizar somente problemas reproduzíveis ou riscos concretos.

## Usar evidência

Para cada achado, informar:

- prioridade `P0` a `P3`;
- arquivo/asset e localização precisa;
- comportamento atual;
- comportamento esperado;
- cenário de reprodução ou consequência;
- decisão/documento que sustenta o achado.

Se não houver evidência suficiente, registrar como dúvida de validação, não como bug confirmado.

## Formato

```text
REVIEW STATUS
SCOPE REVIEWED
FINDINGS
VALIDATION GAPS
LOCKED / WORKING / OPEN CHECK
OUT-OF-SCOPE CHANGES
RECOMMENDED FIX ORDER
```

Ordenar findings por severidade. Se nenhum achado acionável existir, dizer isso claramente e mencionar somente riscos residuais/testes não executados.
