1- Utilizado o MySql como base de dados
   Banco: desenv
   Senha: adm123
   
   Caso deseje alterar o nome do banco ou a senha:
   Modificar o arquivo appsettings.json informando os dados novos
	   "MySqlConnectionString": "Server=localhost;Port=3306;Database=desenv;Uid=root;Pwd=adm123;SslMode=none;"

   Modificar também o arquivo Evolve.json
	   "Evolve.ConnectionString": "Server=localhost;Port=3306;Database=desenv;Uid=root;Pwd=adm123;SslMode=none;",


2- Arquivos SQL Utilizados estão na Pasta Db do Projeto

