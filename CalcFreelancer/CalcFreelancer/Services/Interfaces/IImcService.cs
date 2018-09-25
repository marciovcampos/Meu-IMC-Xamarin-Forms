using CalcFreelancer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalcFreelancer.Services.Interfaces
{
    public interface IImcService
    {
        void Inserir(Imc imc);
        Task<IEnumerable<Imc>> ObterTodos();
    }
}
