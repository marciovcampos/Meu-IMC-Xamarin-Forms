using CalcFreelancer.Domain.Profissionais.Repository;
using CalcFreelancer.Models;
using CalcFreelancer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFreelancer.Infra.Data.Repository
{
    public class ProfissionalRepository : SqLiteRepository<Profissional>, IProfissionalRepository
    {
    }
}
