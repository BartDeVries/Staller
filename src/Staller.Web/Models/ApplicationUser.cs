using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElCamino.AspNet.Identity.AzureTable;
using ElCamino.AspNet.Identity.AzureTable.Model;

namespace Staller.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityCloudContext
    {
        public ApplicationDbContext() : base() { }

        public ApplicationDbContext(IdentityConfiguration config) : base(config) { }
    }
}
