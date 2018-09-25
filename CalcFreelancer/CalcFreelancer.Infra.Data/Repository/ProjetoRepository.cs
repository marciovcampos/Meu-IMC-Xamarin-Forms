using CalcFreelancer.Domain.Projetos.Repository;
using CalcFreelancer.Models;
using CalcFreelancer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFreelancer.Infra.Data.Repository
{
    public class ProjetoRepository : SqLiteRepository<Projeto>, IProjetoRepository
    {
    }
}
