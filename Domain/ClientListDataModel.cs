using Serilog;
using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Domain
{
    public class ClientListDataModel
    {
        private static readonly Lazy<ClientListDataModel> _instance = new Lazy<ClientListDataModel>(() => new ClientListDataModel());

        private int Id { get; set; }

        public bool InitFlag { get; private set; } = false; // флаг для управления инициализацией

        public IQueryable<Client> clientsQuery { get; set; } = null!;
        public IEnumerable<Client> clientsList { get; set; } = null!;

        private ClientListDataModel()
        {
          Id = new Random().Next();
        }

        public static ClientListDataModel GetInstance()
        {
            return _instance.Value; 
        }

        public void InstanceInit(Func<IQueryable<Client>> func)
        {
            clientsQuery = func();
            clientsList = clientsQuery.ToList();
            InitFlag = true;
        }

    }
}
