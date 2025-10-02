# ExpForge CLI

A **.NET 9.0** CLI, built following **Clean Architecture** principles, designed to assist developers in creating **Experience Builder widgets**. This project is **open source** and aims to simplify widget creation and maintenance, reducing manual work and standardizing processes.

---

## 🚀 Features

ExpForge CLI provides commands to optimize widget development, allowing efficient creation and renaming of components.

---

## 📥 Installation

ExpForge CLI can be installed globally via npm, making it accessible from any terminal directory:

```bash
npm install -g expforge
```

---

## 💡 How to Use

After installation, invoke the CLI using the `expforge` command. To see the list of available commands and general options, run:

```bash
expforge --help
```

### Example Output:

```
expforge (version): (1.0.10.0)
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

## 📋 Available Commands

### `New-Component`

Creates a new component for your widget.

**Usage:**

```bash
expforge New-Component [options] <ComponentName>
```

**Arguments:**

| Argument        | Description                                      | Required |
| :-------------- | :----------------------------------------------- | :------- |
| `ComponentName` | Name of the component (prompted if not provided) | No       |

**Options:**

| Option | Description |         |                        |
| :----- | :---------- | ------- | ---------------------- |
| `-?    | -h          | --help` | Shows help information |

**Example:**

```bash
expforge New-Component MyNewComponent
```

### `New-Widget`

Creates a new widget ready to use, based on a template.

**Usage:**

```bash
expforge New-Widget [options] <WidgetName> <TemplateName>
```

**Arguments:**

| Argument       | Description                                   | Required |
| :------------- | :-------------------------------------------- | :------- |
| `WidgetName`   | Name of the widget (prompted if not provided) | No       |
| `TemplateName` | Template name (prompted if not provided)      | No       |

**Options:**

| Option | Description |         |                        |
| :----- | :---------- | ------- | ---------------------- |
| `-?    | -h          | --help` | Shows help information |

**Example:**

```bash
expforge New-Widget MyAwesomeWidget BasicTemplate
```

### `Rename`

Renames an existing widget and automatically updates the `manifest.json`.

**Usage:**

```bash
expforge Rename [options] <NewWidgetName> <WidgetPath>
```

**Arguments:**

| Argument        | Description                                          | Required |
| :-------------- | :--------------------------------------------------- | :------- |
| `NewWidgetName` | New name for the widget (prompted if not provided)   | No       |
| `WidgetPath`    | Path to the widget folder (prompted if not provided) | No       |

**Options:**

| Option | Description |         |                        |
| :----- | :---------- | ------- | ---------------------- |
| `-?    | -h          | --help` | Shows help information |

**Example:**

```bash
expforge Rename NewWidgetName ./widgets/OldWidgetName
```

---

## 🔗 Useful Links

* [GitHub Repository](https://github.com/celinhodaltro/experience-widget)
* [NPM Package](https://www.npmjs.com/package/expforge)

---

## 📞 Contact

For questions, suggestions, or contributions, please use the [GitHub issues](https://github.com/celinhodaltro/experience-widget) system.
