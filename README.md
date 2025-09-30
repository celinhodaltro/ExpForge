# ExpForge CLI

Uma CLI em **.NET 9.0**, construída seguindo os princípios de **Clean Architecture**, criada para auxiliar desenvolvedores de **widgets para Experience Builder**. Este projeto é **open source** e tem como objetivo simplificar a criação e manutenção de widgets, reduzindo trabalho manual e padronizando processos.

---

## 🚀 Funcionalidades

O ExpForge CLI oferece comandos para otimizar o desenvolvimento de widgets, permitindo a criação e renomeação de componentes de forma eficiente.

---

## 📥 Instalação

O ExpForge CLI pode ser instalado globalmente via npm, tornando-o acessível a partir de qualquer diretório no seu terminal.

```bash
npm install -g expforge
```

---

## 💡 Como Usar

Após a instalação, você pode invocar o CLI usando o comando `expforge`. Para ver a lista de comandos disponíveis e opções gerais, utilize:

```bash
expforge --help
```

### Exemplo de Saída:

```
exo-forge Version: (1.0.10.0)
CLI Experience Widget Builder
Usage: expforge [command] [options]
Options:
  --version      Show version information.
  -?|-h|--help   Show help information.
Commands:
  New-Component  Create Component
  New-Widget     Create Widget
  Rename         Rename Widget
Run 'expforge [command] -?|-h|--help' for more information about a command.
```

---

## 📋 Comandos Disponíveis

### `New-Component`

Cria um novo componente para sua widget.

**Uso:**

```bash
expforge New-Component [options] <ComponentName>
```

**Argumentos:**

| Argumento     | Descrição                                  | Obrigatório |
| :------------ | :----------------------------------------- | :---------- |
| `ComponentName` | Nome do componente (será solicitado se não for fornecido) | Não         |

**Opções:**

| Opção        | Descrição              |
| :----------- | :--------------------- |
| `-?|-h|--help` | Mostra informações de ajuda. |

**Exemplo:**

Para criar um novo componente chamado `MyNewComponent`:

```bash
expforge New-Component MyNewComponent
```

### `New-Widget`

Cria uma nova widget pronta para ser usada, baseada em um template.

**Uso:**

```bash
expforge New-Widget [options] <WidgetName> <TemplateName>
```

**Argumentos:**

| Argumento     | Descrição                                  | Obrigatório |
| :------------ | :----------------------------------------- | :---------- |
| `WidgetName`    | Nome da widget (será solicitado se não for fornecido) | Não         |
| `TemplateName`  | Nome do template (será solicitado se não for fornecido) | Não         |

**Opções:**

| Opção        | Descrição              |
| :----------- | :--------------------- |
| `-?|-h|--help` | Mostra informações de ajuda. |

**Exemplo:**

Para criar uma nova widget chamada `MyAwesomeWidget` usando o template `BasicTemplate`:

```bash
expforge New-Widget MyAwesomeWidget BasicTemplate
```

### `Rename`

Renomeia uma widget existente e ajusta automaticamente o `manifest.json`.

**Uso:**

```bash
expforge Rename [options] <NewWidgetName> <WidgetPath>
```

**Argumentos:**

| Argumento       | Descrição                                  | Obrigatório |
| :-------------- | :----------------------------------------- | :---------- |
| `NewWidgetName` | Novo nome da widget (será solicitado se não for fornecido) | Não         |
| `WidgetPath`    | Caminho da pasta da widget (será solicitado se não for fornecido) | Não         |

**Opções:**

| Opção        | Descrição              |
| :----------- | :--------------------- |
| `-?|-h|--help` | Mostra informações de ajuda. |

**Exemplo:**

Para renomear uma widget localizada em `./widgets/OldWidgetName` para `NewWidgetName`:

```bash
expforge Rename NewWidgetName ./widgets/OldWidgetName
```

---

## 🔗 Links Úteis

*   [Repositório no GitHub](https://github.com/celinhodaltro/experience-widget)
*   [Pacote no NPM](https://www.npmjs.com/package/expforge)

---

## 📞 Contato

Para dúvidas, sugestões ou contribuições, por favor, utilize o sistema de issues do [repositório GitHub](https://github.com/celinhodaltro/experience-widget).

