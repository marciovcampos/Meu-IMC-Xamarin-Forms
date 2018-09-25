using CalcFreelancer.Domain.Core.Models;
using Microsoft.WindowsAzure.MobileServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFreelancer.Models
{
    [Table("Profissional")]
    public class Profissional : Entity
    {
        public double ValorGanhoMes { get; set; }
        public int HorasTrabalhadasPorDia { get; set; }
        public int DiasTrabalhadosPorMes { get; set; }
        public int DiasFeriasPorAno { get; set; }
        public double ValorPorHora { get; set; }
    }
}
