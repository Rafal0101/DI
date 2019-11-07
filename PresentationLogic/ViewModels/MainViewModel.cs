using PresentationLogic.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLogic.ViewModels
{
    class MainViewModel : BaseObservableObject, IViewModel
    {
        private readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new Exception("navigation service");
                
        }
        public void Initialize(Action whenDone, object model)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<Product> Model { get; set; }
        public RelayCommand AddProductCommand { get; }
        public RelayCommand EditProductCommand { get; }
    }
}
