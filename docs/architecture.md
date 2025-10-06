# Arquitetura

O ExpForge CLI foi desenvolvido seguindo os princípios da **Clean Architecture**, o que promove um design de software limpo, testável e de fácil manutenção. A arquitetura é dividida em quatro projetos principais:

- **ExpForge.Domain**: Contém as entidades de negócio, enums e a lógica de domínio mais central. Não possui dependências de outras camadas.

- **ExpForge.Application**: Orquestra os fluxos de dados e executa os casos de uso da aplicação. Contém a lógica de aplicação e depende da camada de Domínio.

- **ExpForge.Infrastructure**: Implementa os serviços definidos na camada de Aplicação, como acesso a arquivos e interação com o sistema de arquivos. Depende da camada de Aplicação.

- **ExpForge.Presentation**: É a camada de interface com o usuário, neste caso, a interface de linha de comando (CLI). É responsável por receber os comandos do usuário e apresentar os resultados. Depende da camada de Aplicação.

