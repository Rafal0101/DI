using System;

namespace UWPresentationLogic
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>(Action whenDone = null, object model = null) where TViewModel : IViewModel;
    }
}
