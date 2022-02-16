using AuthorizationService.Models;

namespace AuthorizationService.Data
{
    public class Accounts
    {
        public static List<Account> AccountList = new List<Account>()
        {
            new Account()
            {
                Id = Guid.NewGuid(),
                Email = "account1@mail.ru",
                Name = "account 1",
                Password = "123",
                Roles = new List<Role>() { Role.User }
            },
            new Account()
            {
                Id = Guid.NewGuid(),
                Email = "account2@mail.ru",
                Name = "account 2",
                Password = "123",
                Roles = new List<Role>() { Role.User }
            }
        };
    }
}
