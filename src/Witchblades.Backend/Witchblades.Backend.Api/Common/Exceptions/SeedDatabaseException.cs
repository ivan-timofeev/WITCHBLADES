namespace Witchblades.Exceptions
{
    public class SeedDatabaseException : Exception
    {
        public SeedDatabaseException(Exception x)
            : base("An error occurred while database seeding. Check inner exception for details", x)
        {

        }
    }
}
