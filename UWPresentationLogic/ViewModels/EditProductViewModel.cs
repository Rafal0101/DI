using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UWDomain;
using UWPresentationLogic.ViewModels.Helpers;

namespace UWPresentationLogic.ViewModels
{
    public class EditProductViewModel : BaseObservableObject, IViewModel
    {
        private readonly IProductRepository productRepository;

        private Action whenDone;

        public EditProductViewModel(IProductRepository productRepository)
        {
            if (productRepository == null) throw new ArgumentNullException("productRepository");

            this.productRepository = productRepository;

            SaveProductCommand = new RelayCommand(SaveProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            CancelCommand = new RelayCommand(Cancel);
        }

        public Product Model { get; private set; }

        public RelayCommand SaveProductCommand { get; }
        public RelayCommand DeleteProductCommand { get; }
        public RelayCommand CancelCommand { get; }

        public void Initialize(Action whenDone, object model)
        {
            if (!(model is Product)) throw new ArgumentException("model should be a Product.");
            if (whenDone == null) throw new ArgumentNullException("whenDone");

            Guid productId = ((Product)model).Id;

            this.whenDone = whenDone;
            this.Model = this.productRepository.GetById(productId);

            NotifyPropertyChanged(nameof(Model));

        }

        private void SaveProduct()
        {
            this.productRepository.Update(this.Model);
            this.whenDone();
        }

        private void DeleteProduct()
        {
            this.productRepository.Delete(this.Model.Id);
            this.whenDone();
        }

        private void Cancel()
        {
            this.whenDone();
        }
    }
}
