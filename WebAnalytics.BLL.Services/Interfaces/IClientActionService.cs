using System.Collections.Generic;
using WebAnalytics.Presentation.ViewModels;

namespace WebAnalytics.Services.Interfaces
{
    public interface IClientActionService
    {
        void Add(AddClientActionViewModel clientActionViewModel);
        List<ClientActionViewModel> GetAll();
    }
}