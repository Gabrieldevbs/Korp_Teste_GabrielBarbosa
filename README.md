DOCUMENTAÇÃO TÉCNICA  
Sistema de Produtos e Notas Fiscais  
1. Visão Geral  
O sistema consiste em uma arquitetura baseada em microsserviços, composta  
por duas APIs REST independentes:  
• API de Produtos   
• API de Notas Fiscais   
O objetivo da aplicação é permitir o gerenciamento de produtos e a emissão de  
notas fiscais, incluindo o controle de estoque e a vinculação entre produtos e  
notas.  
2. Arquitetura  
A aplicação segue o padrão de microsserviços desacoplados, onde cada 
serviço  possui responsabilidade única.  
2.1 Componentes  
• Serviço de Produtos: responsável pelo CRUD de produtos e controle de  
estoque   
• Serviço de Notas Fiscais: responsável pelo CRUD de notas e vinculação  
de produtos   
2.2 Comunicação entre serviços  
A comunicação ocorre via HTTP.  
Ao fechar uma nota fiscal:  
• A API de Notas Fiscais envia uma requisição para a API de Produtos  • 
O serviço de Produtos realiza a baixa no saldo dos itens   
3. Tecnologias Utilizadas  
Backend  
• .NET 10   
• Bibliotecas utilizadas: 
o Entity Framework Core   
o PostgreSQL   
o Npgsql.EntityFrameworkCore.PostgreSQL   
o Swashbuckle.AspNetCore (Swagger)   
o QuestPDF   
Frontend  
• Angular   
• TypeScript   
4. Modelagem de Dados  
4.1 Produto  
Campos:  
• ID (identificador único)   
• Código   
• Descrição   
• Saldo   
4.2 Nota Fiscal  
Campos:  
• ID   
• Numeração Sequencial   
• Status (ABERTA / FECHADA)   
• Data de Criação   
4.3 ProdutoNotaFiscal  
Tabela auxiliar responsável pelo relacionamento entre produtos e notas. 
Campos:  
• ID ProdutoNotaFiscal  
• ID Produto  
• ID Nota   
• Quantidade   
5. Funcionalidades  
5.1 API de Produtos  
• Criar produto   
• Listar produtos   
• Atualizar produto   
• Deletar produto   
• Controle de saldo em estoque   
5.2 API de Notas Fiscais  
• Criar nota fiscal (status inicial: ABERTA)   
• Adicionar produtos à nota   
• Fechar nota fiscal   
o Ao fechar, ocorre a baixa de estoque via integração com a API de  
Produtos   
o Valida se a nota está aberta  
6. Boas Práticas Aplicadas  
6.1 Uso de DTOs  
Foi utilizada a camada de DTO (Data Transfer Object) para:  
• Evitar exposição direta das entidades   
• Aumentar a segurança da aplicação   
• Controlar os dados trafegados na API   
6.2 Middleware de Tratamento de Erros  
• Padronização das respostas de erro   
• Melhor rastreabilidade  
• Centralização do tratamento de exceções   
6.3 Injeção de Dependência  
• Uso de interfaces   
• Redução de acoplamento   
• Melhor testabilidade e manutenção   
6.4 Gerenciamento de Configuração  
• Informações sensíveis armazenadas no arquivo appsettings.json • 
Exemplo: string de conexão com banco de dados   
7. Execução do Projeto  
7.1 Banco de Dados  
• Criar banco no PostgreSQL (ou outro compatível)   
• Executar migrations, se configuradas   
7.2 Configuração  
• Ajustar o appsettings.json com a string de conexão   
• Configurar os arquivos de environment no frontend Angular com as URLs  
das APIs   
7.3 Execução  
• Iniciar as duas APIs (.NET)   
• Iniciar o frontend Angular com o comando “npm run start” 
8. Geração de PDF  
O sistema utiliza a biblioteca QuestPDF para geração de documentos PDF,  
permitindo a exportação de notas fiscais.  
9. Melhorias Propostas 
9.1 Controle de Usuários e Auditoria  
• Criar entidade de usuários   
• Relacionar usuários com produtos e notas fiscais  • 
Permitir rastreamento de criação e alteração de dados   
9.2 Histórico de Movimentação  
• Criar tabela de histórico de estoque   
• Registrar entradas e saídas   
• Associar movimentações às notas fiscais   
9.3 Cálculo financeiro na nota  
• Adicionar campos:  
o Valor unitário do produto   
o Valor total por item   
o Valor total da nota   
• Funcionalidades:   
o Cálculo automático do total   
o Aplicação de:   
▪ Descontos   
▪ Acréscimos  
9.4 Impostos (simulação fiscal básica)  
• Adicionar campos como:   
o ICMS   
o ISS (dependendo do caso)   
• Calcular automaticamente:   
o Valor de imposto por item   
o Total de impostos da nota 
9.5 Testes Automatizados  
• Testes unitários   
• Testes de integração   
• Uso de mocks   
9.6 Relatórios  
• Relatório de vendas por período  • 
Produtos mais vendidos   
• Notas por dia/mês   
• Faturamento total   
Filtros:  
• Data   
• Produto   
• Cliente  
9.7 Versionamento de API  
• Versionamento de endpoints  • 
Exemplo: /api/v1/produtos   
9.8 Busca e filtros 
avançados Busca por:   
• Código do produto   
• Descrição   
• Número da nota   
Filtros:   
• Status da nota   
• Intervalo de datas   
• Produtos específicos 
9.9 Importação e Exportação  
• Importar produtos via CSV/Excel  • 
Exportar:   
• Lista de produtos   
• Notas fiscais  
9.10 Dashboard no 
Frontend Indicadores como:  
• Total de vendas no dia   
• Quantidade de notas emitidas  • 
Produtos com baixo estoque  • 
Top produtos  
9.11 Multiusuário com 
permissões • Perfis:   
o Admin   
o Operador   
Permissões:  
• Criar produto   
• Editar produto   
• Fechar nota   
• Cancelar nota  
9.12 Notificações  
• Alertas no sistema:   
o Estoque baixo   
o Nota pendente   
• Possível envio de:   
o Email ao fechar nota  
o Aviso de erro  
10. Considerações Finais  
O sistema apresenta uma arquitetura consistente baseada em microsserviços,  
com separação clara de responsabilidades e adoção de boas práticas 
modernas  de desenvolvimento.  
As melhorias propostas visam elevar o nível da aplicação em termos de  
escalabilidade, segurança, desempenho e manutenibilidade. 
