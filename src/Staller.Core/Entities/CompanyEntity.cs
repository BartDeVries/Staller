using Microsoft.WindowsAzure.Storage.Table;
using NodaTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Staller.Core.Entities
{
    public class CompanyEntity : TableEntity
    {
        [Required]
        public Guid Id { get { return string.IsNullOrEmpty(RowKey) ? Guid.Empty : Guid.Parse(RowKey); } set { RowKey = value.ToString(); } }

        [Required]
        public string Name { get { return PartitionKey; } set { PartitionKey = value; } }

        public Guid AddressId { get; set; }
        
        public string Email { get; set; }

        public string Website { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public LocalDateTime ActiveFrom { get; set; }

        [Required]
        public LocalDateTime ActiveTo { get; set; }
    }
}
