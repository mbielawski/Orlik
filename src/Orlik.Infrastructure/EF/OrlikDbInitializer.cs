using System.Collections.Generic;
using System.Data.Entity;
using Orlik.Model.Domain;

namespace Orlik.Infrastructure.EF
{
    public class OrlikDbInitializer : DropCreateDatabaseIfModelChanges<OrlikContext>
    {
        protected override void Seed(OrlikContext context)
        {
            IList<User> defaultUsers = new List<User>();

            foreach (var user in defaultUsers)
                context.Users.Add(user);

            base.Seed(context);
        }
    }
}
