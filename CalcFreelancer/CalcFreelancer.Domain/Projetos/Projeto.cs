using CalcFreelancer.Domain.Core.Models;
using Microsoft.WindowsAzure.MobileServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFreelancer.Models
{
    [Table("Projeto")]
    public class Projeto : Entity
    {
        public string Nome { get; set; }
        public double ValorPorHora { get; set; }
        public int HorasPorDia { get; set; }
        public int DiasDuracaoProjeto { get; set; }
        public double ValorTotal { get; set; }
    }
}
