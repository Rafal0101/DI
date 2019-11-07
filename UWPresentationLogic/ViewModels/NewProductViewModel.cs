using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWDomain;
using UWPresentationLogic.ViewModels.Helpers;

namespace UWPresentationLogic.ViewModels
{
    public class NewProductViewModel : BaseObservableObject, IViewModel
    {
        private readonly IProductRepository productRepository;

        private Action whenDone;

        public NewProductViewModel(IProductRepository productRepository)
        {
            if (productRepository == null) throw new ArgumentNullException("productRepository");

            this.productRepository = productRepository;

            this.SaveProductCommand = new RelayCommand(this.SaveProduct);
            this.CancelCommand = new RelayCommand(this.Cancel);
        }

        public Product Model { get; private set; }

        public RelayCommand CancelCommand { get; }
        public RelayCommand SaveProductCommand { get; }

        public void Initialize(Action whenDone, object model)
        {
            if (whenDone == null) throw new ArgumentNullException("whenDone");

            this.whenDone = whenDone;

            this.Model = new Product { Id = Guid.NewGuid() };

            NotifyPropertyChanged(nameof(Model));
        }

        private void Cancel()
        {
            this.whenDone();
        }

        private void SaveProduct()
        {
            this.productRepository.Insert(this.Model);
            this.whenDone();
        }
    }
}
