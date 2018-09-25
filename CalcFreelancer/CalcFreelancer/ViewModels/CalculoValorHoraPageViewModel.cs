using CalcFreelancer.Models;
using CalcFreelancer.Repository;
using CalcFreelancer.Services;
using CalcFreelancer.Services.Interfaces;
using CalcFreelancer.ViewModels.Base;
using CalcFreelancer.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CalcFreelancer.ViewModels
{
    public class CalculoValorHoraPageViewModel : ViewModelBase
    {
        private double valorGanhoMes;
        public double ValorGanhoMes
        {
            get { return valorGanhoMes; }
            set
            {
                SetProperty(ref valorGanhoMes, value);
                CalcularValorHora();
            }
        }

        private int horasTrabalhadasPorDia;
        public int HorasTrabalhadasPorDia
        {
            get { return horasTrabalhadasPorDia; }
            set
            {
                SetProperty(ref horasTrabalhadasPorDia, value);
                CalcularValorHora();
            }
        }

        private int diasTrabalhadosPorMes;
        public int DiasTrabalhadosPorMes
        {
            get { return diasTrabalhadosPorMes; }
            set
            {
                SetProperty(ref diasTrabalhadosPorMes, value);
                CalcularValorHora();
            }
        }

        private int diasFeriasPorAno;
        public int DiasFeriasPorAno
        {
            get { return diasFeriasPorAno; }
            set
            {
                SetProperty(ref diasFeriasPorAno, value);
                CalcularValorHora();
            }
        }

        private double valorDaHora;
        public double ValorDaHora
        {
            get { return valorDaHora; }
            set { SetProperty(ref valorDaHora, value); }
        }

        private Profissional profissional;
        public Profissional Profissional
        {
            get { return profissional; }
            set
            {
                SetProperty(ref profissional, value);
            }
        }

        public Command GravarCommand { get; }

        private readonly IProfissionalService ProfissionalService;

        public CalculoValorHoraPageViewModel(IProfissionalService profissionalService)
        {
            GravarCommand = new Command(ExecuteGravarCommand);
            Profissional = new Profissional();
            ProfissionalService = profissionalService;
        }

        private void CalcularValorHora()
        {

            if (ValorGanhoMes > 0 && DiasTrabalhadosPorMes > 0 && HorasTrabalhadasPorDia > 0)
            {
                double valorGanhoAnual = ValorGanhoMes * 12;
                int totalDiasTrabalhadosPorAno = DiasTrabalhadosPorMes * 12;

                if (DiasFeriasPorAno > 0)
                {
                    totalDiasTrabalhadosPorAno -= DiasFeriasPorAno;
                }

                ValorDaHora = valorGanhoAnual / (totalDiasTrabalhadosPorAno * HorasTrabalhadasPorDia);
            }
        }

        private async void ExecuteGravarCommand(object obj)
        {
            Profissional.ValorGanhoMes = ValorGanhoMes;
            Profissional.HorasTrabalhadasPorDia = HorasTrabalhadasPorDia;
            Profissional.DiasTrabalhadosPorMes = DiasTrabalhadosPorMes;
            Profissional.DiasFeriasPorAno = DiasFeriasPorAno;
            Profissional.ValorPorHora = ValorDaHora;

            ProfissionalService.Inserir(Profissional);

            await App.Current.MainPage.Navigation.PushAsync(new CalculoValorHoraSucessoPage());
            //await App.Current.MainPage.DisplayAlert("Sucesso", "Valor por hora gravado!", "Ok");
        }
    }
}
