namespace Witchblades.Exceptions
{
    public class PrepareTheDatabaseContextException : Exception
    {
        public PrepareTheDatabaseContextException(Exception x)
            : base("An error occurred while preparing the database context. Check inner exception for details", x)
        {

        }
    }
}
