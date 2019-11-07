using System;

namespace UWPresentationLogic
{
    public interface IViewModel
    {
        void Initialize(Action whenDone, object model);
    }
}
