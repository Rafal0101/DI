using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWDataAccess;
using UWDomain;
using UWPresentationLogic;
using UWPresentationLogic.ViewModels;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWPresentation
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application, INavigationService
    {
        private readonly INavigationService navigationService;
        private readonly IProductRepository productRepository;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;

            this.navigationService = this;
            this.productRepository = new FakeProductRepository();
            //    new CircuitBreakerProductRepositoryDecorator(
            //        new CircuitBreaker(TimeSpan.FromMinutes(1)),
            //        new FakeProductRepository());
        }
        public void NavigateTo<TViewModel>(Action whenDone = null, object model = null) where TViewModel : IViewModel
        {
            Page page = this.CreatePage(typeof(TViewModel));
            var viewModel = (IViewModel)page.DataContext;

            viewModel.Initialize(whenDone, model);

            var frame = (Frame)Window.Current.Content;
            frame.Content = page;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            if(Window.Current.Content == null)
            {
                Window.Current.Content = new Frame();
                Window.Current.Activate();
                NavigateTo<MainViewModel>();
            }
        }

        private Page CreatePage(Type viewModelType)
        {
            if (viewModelType == typeof(MainViewModel))
            {
                return new MainPage(
                    new MainViewModel(
                        this.navigationService,
                        this.productRepository));
            }
            else if (viewModelType == typeof(EditProductViewModel))
            {
                return new EditProductPage(
                    new EditProductViewModel(
                        this.productRepository));
            }
            else if (viewModelType == typeof(NewProductViewModel))
            {
                return new NewProductPage(
                    new NewProductViewModel(
                        this.productRepository));
            }
            else
            {
                throw new InvalidOperationException($"Unknown view model: {viewModelType}.");
            }
        }


        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
