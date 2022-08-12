namespace CrudDelicious.Models;

/* 
Disabled Warning: "Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable."
We can disable this safely because we know the framework will assign non-null values when it constructs this class for us.
*/
#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
// the MyContext class representing a session with our MySQL database, allowing us to query for or save data
 //same as the name space that you give in the program file
// this change from MyContext to DB
public class DB: DbContext{

    public DB(DbContextOptions options):base(options){}
     // the "Monsters" table name will come from the DbSet property name

    public DbSet<dishes> dish {get; set;}
}