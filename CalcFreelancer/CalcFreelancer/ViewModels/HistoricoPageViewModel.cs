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
    public class HistoricoPageViewModel : ViewModelBase
    {
        private readonly IImcService ImcService;

        public ObservableCollection<Imc> Historico { get; set; }

        private bool atualizando;
        public bool Atualizando
        {
            get { return atualizando; }
            set { SetProperty(ref atualizando, value); }
        }

        private Imc imc;
        public Imc Imc
        {
            get { return imc; }
            set
            {
                SetProperty(ref imc, value);

                if (imc != null)
                    App.Current.MainPage.DisplayAlert("IMC: ", imc.ImcCalculado.ToString() , "ok");
            }
        }

        public Command AtualizarDadosCommand { get; }

        public HistoricoPageViewModel(IImcService imcService)
        {
            ImcService = imcService;
            Historico = new ObservableCollection<Imc>();
            ObterHistorico();
            AtualizarDadosCommand = new Command(ExecuteAtualizarDadosCommand);
        }

        private async void ExecuteAtualizarDadosCommand()
        {
            Atualizando = true;
            await ObterHistorico();
            Atualizando = false;
        }

        private async Task ObterHistorico()
        {
            var historico = await ImcService.ObterTodos();

            if (Historico.Count > 0)
            {
                Historico.Clear();
            }

            foreach (var imc in historico)
            {
                Historico.Add(imc);
            }
        }
    }
}