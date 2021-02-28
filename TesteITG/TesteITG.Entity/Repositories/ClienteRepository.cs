using System;
using System.Collections.Generic;
using System.Text;
using TesteITG.Entity.Entity;
using TesteITG.Entity.Repositories.Interfaces;
using TesteITG.Entity.Context;
using TesteITG.Entity.Repository;

namespace TesteITG.Entity.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        private ClienteContext _appContext => (ClienteContext)_context;

        public ClienteRepository(ClienteContext context) : base(context)
        { }
    }
}
