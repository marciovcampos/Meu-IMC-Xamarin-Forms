using CalcFreelancer.Domain.Profissionais.Repository;
using CalcFreelancer.Domain.Projetos.Repository;
using CalcFreelancer.Models;
using CalcFreelancer.Services.Interfaces;

namespace CalcFreelancer.Services
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IProfissionalRepository ProfissionalRepository;

        public ProfissionalService(IProfissionalRepository profissionalRepository)
        {
            ProfissionalRepository  = profissionalRepository;
        }

        public void Inserir(Profissional profissional)
        {
            ProfissionalRepository.Insert(profissional);
        }
    }
}
