using CalcFreelancer.Domain.Imcs.Repository;
using CalcFreelancer.Models;
using CalcFreelancer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFreelancer.Infra.Data.Repository
{
    public class ImcRepository : SqLiteRepository<Imc>, IImcRepository
    {
    }
}
