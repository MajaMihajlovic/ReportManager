# ReportManager1
An unhandled exception of type 'System.InvalidOperationException' occurred in EntityFramework.dll

Additional information: Multiple object sets per type are not supported. The object sets 'StatisticRecords' and 'ErrorRecords' can both contain instances of type 'ReportManager.Model.Record'. 


error 2:

An unhandled exception of type 'System.Data.Entity.Infrastructure.DbUpdateException' occurred in EntityFramework.dll

Additional information: An error occurred while updating the entries. See the inner exception for details.

error 3:

Additional information: The type 'System.Collections.Generic.Dictionary`2[System.String,System.Int32]' was not mapped. Check that the type has not been explicitly excluded 
by using the Ignore method or NotMappedAttribute data annotation. Verify that the type was defined as a class, is not primitive or generic, and does not inherit from EntityObject.


1. Cicuit name je konstantno kljuc, ne mogu promijeniti da to bude npr ID 
2. Kod dodavanja Summary nije moguce dodati List<Tuple<,>> 
3. Ime baze se izgenerise samo i ne mogu se dodati npr DbSet<Record> record1 i  DbSet<Record> record 2... sto je problem sa WarningEror dodavanjem
4.Dictionary se takodje ne moze upisati , i kad pretvorim u klasu npr KeyValue ni to nece :(

5. Nekad se tabela uopste ne napravi i zato se i ne moze dodati nista
6. Ne mogu naci dijagram 


Sta sam usppjela: 
1. Uspostaviti konekciju
2. Upisati jedan dio u bazu, kljuc je glavni problem
