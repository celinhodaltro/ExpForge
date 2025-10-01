# Experience Widget CLI

Uma CLI em **.NET 9.0**, construída seguindo os princípios de **Clean Architecture**, criada para auxiliar desenvolvedores de **widgets para Experience Builder**.  

Este projeto é **open source** e tem como objetivo simplificar a criação e manutenção de widgets, reduzindo trabalho manual e padronizando processos.

---

## 📦 Publicação

Para gerar os binários e preparar o pacote NPM:

```bash
dotnet publish -c Release -p:GenerateNpm=true
```

---

## 📥 Instalação via NPM

Você também pode baixar diretamente no [npm](https://www.npmjs.com/package/expforge):



📥 Instalação via NPM

Você também pode baixar diretamente no npm
:
```bash
npm install -g expforge
```

Após instalar, basta rodar:

```bash
expforge --help
