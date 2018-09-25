using CalcFreelancer.Domain.Core.Models;
using Microsoft.WindowsAzure.MobileServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFreelancer.Models
{
    [Table("Imc")]
    public class Imc : Entity
    {
        public double Peso { get; set; }
        public double Altura { get; set; }
        public double ImcCalculado { get; set; }
    }
}
