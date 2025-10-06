# Comandos

O ExpForge CLI oferece um conjunto de comandos para gerenciar o ciclo de vida dos seus widgets.

## Criar um Novo Widget

Para criar um novo widget, utilize o comando `new-widget`. Este comando gera a estrutura de diretórios e arquivos necessários para um novo widget com base em um template.

```bash
expforge new-widget <widget-name> <template-name>
```

- `<widget-name>`: O nome do seu novo widget.
- `<template-name>`: O nome do template a ser utilizado (por exemplo, `default`).

## Criar um Novo Componente

Para adicionar um novo componente a um widget existente, utilize o comando `new-component`.

```bash
expforge new-component <component-name>
```

- `<component-name>`: O nome do novo componente.

## Renomear um Widget

Para renomear um widget existente, utilize o comando `rename-widget`.

```bash
expforge rename-widget <old-name> <new-name>
```

- `<old-name>`: O nome atual do widget.
- `<new-name>`: O novo nome do widget.

