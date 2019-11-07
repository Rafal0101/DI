using UWDomain;
using UWPresentationLogic.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPresentationLogic.ViewModels
{
    public class MainViewModel : BaseObservableObject, IViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IProductRepository _productRepository;

        public MainViewModel(INavigationService navigationService, IProductRepository productRepository)
        {
            _navigationService = navigationService ?? throw new Exception("navigation service");
            _productRepository = productRepository ?? throw new Exception("product repository");

            AddProductCommand = new RelayCommand(AddProduct);
            EditProductCommand = new RelayCommand(EditProduct);
        }

        private void EditProduct(object product)
        {
            _navigationService.NavigateTo<EditProductViewModel>(whenDone: GoBack, model: product);
        }

        private void AddProduct(object obj)
        {
            _navigationService.NavigateTo<NewProductViewModel>(whenDone: GoBack);
        }

        private void GoBack()
        {
            _navigationService.NavigateTo<MainViewModel>();
        }

        public void Initialize(Action whenDone, object model)
        {
            Model = _productRepository.GetAll();
            NotifyPropertyChanged(nameof(Model));
        }



        public IEnumerable<Product> Model { get; set; }
        public RelayCommand AddProductCommand { get; }
        public RelayCommand EditProductCommand { get; }
    }
}
