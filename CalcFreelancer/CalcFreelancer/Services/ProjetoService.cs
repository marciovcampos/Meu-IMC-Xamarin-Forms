using CalcFreelancer.Domain.Projetos.Repository;
using CalcFreelancer.Models;
using CalcFreelancer.Repository;
using CalcFreelancer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalcFreelancer.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly IProjetoRepository ProjetoRepository;

        public ProjetoService(IProjetoRepository projetoRepository)
        {
            ProjetoRepository = projetoRepository;
        }

        public void Inserir(Projeto projeto)
        {
            ProjetoRepository.Insert(projeto);
        }

        public Task<IEnumerable<Projeto>> ObterTodos()
        {
            return ProjetoRepository.GetAll();
        }
    }
}