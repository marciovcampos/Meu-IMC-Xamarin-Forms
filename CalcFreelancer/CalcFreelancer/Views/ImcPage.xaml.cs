﻿using CalcFreelancer.Repository;
using CalcFreelancer.ViewModels;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalcFreelancer
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImcPage : ContentPage
	{
		public ImcPage ()
		{
			InitializeComponent ();
            var viewModel = ServiceLocator.Current.GetInstance<ImcPageViewModel>();
            BindingContext = viewModel;
        }

    }
}