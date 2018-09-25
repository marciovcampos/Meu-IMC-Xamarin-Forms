using CalcFreelancer.Models;
using CalcFreelancer.Services.Interfaces;
using CalcFreelancer.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalcFreelancer.ViewModels
{
    public class ProjetosPageViewModel : ViewModelBase
    {
        private readonly IProjetoService ProjetoService;

        public ObservableCollection<Projeto> Projetos { get; set; }

        private bool atualizando;
        public bool Atualizando
        {
            get { return atualizando; }
            set { SetProperty(ref atualizando, value); }
        }

        private Projeto projeto;
        public Projeto Projeto
        {
            get { return projeto; }
            set
            {
                SetProperty(ref projeto, value);

                if (projeto != null)
                    App.Current.MainPage.DisplayAlert("Projeto", projeto.Nome, "ok");
            }
        }

        public Command AtualizarDadosCommand { get; }

        public ProjetosPageViewModel(IProjetoService projetoService)
        {
            ProjetoService = projetoService;
            Projetos = new ObservableCollection<Projeto>();
            ObterProjetos();
            AtualizarDadosCommand = new Command(ExecuteAtualizarDadosCommand);
        }

        private async void ExecuteAtualizarDadosCommand()
        {
            Atualizando = true;
            await ObterProjetos();
            Atualizando = false;
        }

        private async Task ObterProjetos()
        {
            var projetos = await ProjetoService.ObterTodos();

            if (Projetos.Count > 0)
            {
                Projetos.Clear();
            }

            foreach (var projeto in projetos)
            {
                Projetos.Add(projeto);
            }
        }
    }
}