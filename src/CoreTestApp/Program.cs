using System;
using DbUp;

namespace CoreTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var cstring = "Host=127.0.0.1;Port=5432;Database=fromcore2;User Id=postgres;Password=postgres;";
            EnsureDatabase.For.PostgresqlDatabase(cstring);

            var upgrader = DeployChanges.To
                .PostgresqlDatabase(cstring)
                .WithScript("d233", $@"
	
	            CREATE TABLE contact
            (
	            id uuid primary key not null,
	            date date not null
            );
	
	            CREATE TABLE cycle
            (
              id uuid not null,
              data json not null,
              CONSTRAINT contactfk FOREIGN KEY(id)

	              REFERENCES contact(id) MATCH SIMPLE

	              ON UPDATE CASCADE ON DELETE NO ACTION
            );
	            ")
                .LogToConsole()
                .Build();

            upgrader.PerformUpgrade();
        }
    }
}
