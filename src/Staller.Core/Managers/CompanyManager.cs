using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder.Internal;
using Staller.Core.Entities;

namespace Staller.Core.Managers
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class CompanyManager : ManagerBase<CompanyEntity>
    {
        
        

        public CompanyManager() : base("company")
        {
          
        }




    }
}
