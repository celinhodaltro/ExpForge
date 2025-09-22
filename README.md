# Experience Widget CLI

Uma CLI em **.NET 9.0**, construída seguindo os princípios de **Clean Architecture**, criada para auxiliar desenvolvedores de **widgets para Experience Builder**.  
Este projeto é **open source** e tem como objetivo simplificar a criação e manutenção de widgets, reduzindo trabalho manual e padronizando processos.

---

## 🚀 Funcionalidades atuais

Atualmente, a CLI conta com dois comandos principais:

- **`new`** → Cria uma nova widget pronta para ser usada.  
- **`rename`** → Renomeia uma widget existente e ajusta automaticamente o `manifest.json`.

---

## 📦 Publicação

Para gerar os binários e preparar o pacote NPM:

```bash
dotnet publish -c Release -p:GenerateNpm=true
```

---

## 📥 Instalação via NPM

Você também pode baixar diretamente no [npm](https://www.npmjs.com/package/expforge):

```bash
npm install -g expforge
```

Após instalar, basta rodar:

```bash
expforge --help
```

---

## 🔗 Links úteis

- [Repositório no GitHub](https://github.com/celinhodaltro/experience-widget)  
- [Pacote no NPM](https://www.npmjs.com/package/expforge)

---
