using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Staller.Core.Entities
{
    public class AddressEntity : TableEntity
    {
        //public AddressEntity(Guid companyId, Guid addressId) : base(companyId.ToString(), addressId.ToString())
        //{
        //    Id = 
        //}

        [Required]
        public Guid Id { get { return Guid.Parse(RowKey); } set { RowKey = value.ToString(); } }

        [Required]
        public Guid CompanyId { get { return Guid.Parse(PartitionKey); } set { PartitionKey = value.ToString(); } }

        [Required]
        public string Name { get; set; }

        public string AttentionTo { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Postal { get; set; }

        public string City { get; set; }

        public int Country { get; set; }

        public string PhoneNumber { get; set; }
    }
}
