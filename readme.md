[![Build status](https://ci.appveyor.com/api/projects/status/ucxwgqb0ypj73tt9?svg=true)](https://ci.appveyor.com/project/adeildo-oliveira/SimpleApiIntegrationTests)

>## Simple Integration Test
Este projeto tem como objetivo, exemplificar o uso de testes integrados com base de dados SQL Server.

>## Banco de Dados
Para esse projeto, será preciso criar um banco de dados dentro do docker.

[SQL Server Docker](https://docs.microsoft.com/pt-br/sql/linux/sql-server-linux-configure-docker?view=sql-server-2017)

>### DOCKER - SQLServer2017 
* User: ```sa```
* Password: ```Teste@1234```
* ```docker pull microsoft/mssql-server-linux:2017-latest```
* ```docker run --name SQLServer2017 -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Teste@1234" -e "MSSQL_PID=Developer" --cap-add SYS_PTRACE -p 11433:1433 -d microsoft/mssql-server-linux:2017-latest```

>### DOCKER - SQLServer2019
* User: ```sa```
* Password: ```Teste@1234```
* ```docker pull mcr.microsoft.com/mssql/server:2019-CU3-ubuntu-18.04```
* ```docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Teste@1234" -p 1433:1433 --name SqlServer2019 -d mcr.microsoft.com/mssql/server:2019-CU3-ubuntu-18.04```

### Estrutura da Base Para Teste
* Base de dados aonde os dados da API são inseridos;
    ```
    CREATE DATABASE SimpleApi
    GO

    USE SimpleApi
    GO

    Create table Cliente (Id int primary key not null identity, Nome varchar(30), SobreNome varchar(30))
    ```
* Como boa prática, prefira usar sempre uma base de dados segragada para testes **integrados** ou de **aceitação**;
    ```
    CREATE DATABASE SimpleApi_IntegrationTest
    GO

    USE SimpleApi_IntegrationTest
    GO

    Create table Cliente (Id int primary key not null identity, Nome varchar(30), SobreNome varchar(30))
    ```
Com a estrutura criada, basta substituir as suas credencias de acesso a base de dados e rodar os tests.

Na classe ```DatabaseFixture.cs``` há uma connectionstring na qual será preciso atualizar o **usuário** e **senha**.

Lembrando que a conexão pode ficar em um arquivo de configuação também, contudo, para exemplificar, alteramos a connection através da classe ```DatabaseFixture.cs```.

Testes de integração podem ser usados em diferentes cenários, um deles, é testar seus script como, **insert**, **delete**, **update**, **select**... Cenários aonde temos que realizar algum tipo de manutenção na base de dados, que podem vir a impactar o resultado de algum serviço, os testes integrados podem ajudar a identificar bugs.