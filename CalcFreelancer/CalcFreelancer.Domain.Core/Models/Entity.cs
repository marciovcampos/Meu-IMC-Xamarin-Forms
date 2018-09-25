using Microsoft.WindowsAzure.MobileServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFreelancer.Domain.Core.Models
{
    public class Entity
    {
        [PrimaryKey]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
