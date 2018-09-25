using CalcFreelancer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalcFreelancer.Services.Interfaces
{
    public interface IProjetoService
    {
        void Inserir(Projeto projeto);
        Task<IEnumerable<Projeto>> ObterTodos();
    }
}