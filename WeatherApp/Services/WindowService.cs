﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeatherApp.Interfaces;

namespace WeatherApp.Services
{
	public class WindowService : IWindowService
	{
		public bool? ShowDialog(object viewModel)
		{
			throw new NotImplementedException();
		}

		public void ShowWindow(object viewModel)
		{
			throw new NotImplementedException();
		}

		private Window CreateWindow(object viewModel, bool isDialog)
		{
			var window = FindWindowInAssebly(viewModel);
			var view = SetUpWindow(window, isDialog);

			return view;
		}

		private Window FindWindowInAssebly(object viewModel)
		{
			Type viewModelType = viewModel.GetType();
			string viewName = viewModelType.Name.Replace("ViewModel", "View");
			Type? viewType = Assembly.GetEntryAssembly().GetTypes().FirstOrDefault(x => x.Name.Equals(viewName));

			if (viewType != null)
			{
				//var view = Activator.CreateInstance(viewType) as Window;
				var view = App.ServiceProvider.GetRequiredService(viewType) as Window;

				if (view != null)
				{
					view.DataContext = viewModel;

					var viewAware = viewModel as IViewAware;
					if (viewAware != null)
					{
						viewAware.View = view;
					}

					return view;
				}
				else { throw new Exception("View is not of type Window"); }

			}
			else { throw new Exception("View of Viewmodel in Assebly not found"); }

		}
	}
}
