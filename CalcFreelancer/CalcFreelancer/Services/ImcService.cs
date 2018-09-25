using CalcFreelancer.Domain.Imcs.Repository;
using CalcFreelancer.Models;
using CalcFreelancer.Repository;
using CalcFreelancer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalcFreelancer.Services
{
    public class ImcService : IImcService
    {
        private readonly IImcRepository ImcRepository;

        public ImcService(IImcRepository imcRepository)
        {
            ImcRepository = imcRepository;
        }

        public void Inserir(Imc imc)
        {
            ImcRepository.Insert(imc);
        }

        public Task<IEnumerable<Imc>> ObterTodos()
        {
            return ImcRepository.GetAll();
        }
    }
}