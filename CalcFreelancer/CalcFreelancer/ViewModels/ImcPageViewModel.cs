using CalcFreelancer.Repository;
using CalcFreelancer.Services;
using CalcFreelancer.Services.Interfaces;
using CalcFreelancer.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CalcFreelancer.ViewModels
{
    public class ImcPageViewModel : ViewModelBase
    {
        
        private double peso;
        public double Peso
        {
            get { return peso; }
            set
            {
                SetProperty(ref peso, value);
                CalcularImc();
            }
        }

        private double altura;
        public double Altura
        {
            get { return altura; }
            set
            {
                SetProperty(ref altura, value);
                CalcularImc();
            }
        }

        private double imcCalculado;
        public double ImcCalculado
        {
            get { return imcCalculado; }
            set
            {
                SetProperty(ref imcCalculado, value);               
            }
        }


        public Command GravarCommand { get; }
        public Command LimparCommand { get; }

        private readonly IImcService ImcService;

        public ImcPageViewModel(IImcService imcService)
        {
            GravarCommand = new Command(ExecuteGravarCommand);
            LimparCommand = new Command(ExecuteLimparCommand);
            ImcService = imcService;
        }

        private void ExecuteLimparCommand()
        {           
            Altura = 0;
            Peso = 0;
        }

        private async void ExecuteGravarCommand()
        {
            ImcService.Inserir(new Models.Imc()
            {
                Peso = Peso,
                Altura = Altura,
                ImcCalculado = ImcCalculado


            });


            ExecuteLimparCommand();

            await App.Current.MainPage.DisplayAlert("Sucesso", "IMC gravado!", "Ok");
        }

        private void CalcularImc()
        {
            if (Peso > 0 && Altura > 0)
            {
                ImcCalculado = Peso / ((Altura/100) * (Altura/100));
            }
            else
            {
                ImcCalculado = 0;
            }
        }

    }
}
