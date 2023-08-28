using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Domain.Repositories.Abstract;

namespace VartanMVCv2.ViewModels
{
    public class ClientViewModel
    {
        public Client ClientExample { get; set; } = new Client();

        public IEnumerable<Client> clients { get; set; } = null!;

        public (Guid, string) selector;

        public ClientViewModel()
        {
           
        }
    }
}
