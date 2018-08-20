using Sales.Common.Models;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class ProductsViewModel: BaseViewModel
    {
        private ApiServices apiService;

        private ObservableCollection<Product> products;


        public ObservableCollection <Product> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }
        public ProductsViewModel()
        {
            this.apiService = new ApiServices();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {

            var response = await this.apiService.GetList<Product>("http://salesapipru.azurewebsites.net", "/api", "/Products");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Acept");
                return;
            };

            var list = (List<Product>)response.Result;

            this.Products = new ObservableCollection<Product>(list);

        }
    }
}
