using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteITG.Domain.Domain;
using TesteITG.Domain.Service.Generic;
using TesteITG.Entity.Entity;
using TesteITG.Entity.Repositories.Interfaces;
using TesteITG.Entity.UnitofWork;

namespace TesteITG.Domain.Service
{
    public class ClienteService<Tv, Te> : GenericServiceAsync<Tv, Te>
                                              where Tv : ClienteModel
                                              where Te : Cliente
    {
        IClienteRepository _clienteRepository;

        public ClienteService(IUnitOfWork unitOfWork, IMapper mapper,
                             IClienteRepository pessoaRepository)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            if (_mapper == null)
                _mapper = mapper;

            if (_clienteRepository == null)
                _clienteRepository = pessoaRepository;
        }


        public async Task<Cliente> ModelarCliente(ClienteModel cliente)
        {


            Cliente result = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nome = cliente.Nome,
                Idade = cliente.Idade
            };

            return result;
        }

        public async Task<List<ClienteModel>> ListarPessoas()
        {
            var clientesAtivas = _clienteRepository.GetAll();

            List<ClienteModel> clientes = new List<ClienteModel>();
            foreach (var elem in clientesAtivas)
            {
                var lista = new ClienteModel();
                lista.Id = elem.Id;
                lista.Nome = elem.Nome;
                lista.Idade = elem.Idade;

                clientes.Add(lista);
            }

            return clientes.OrderBy(x => x.Nome).ToList();
        }

        public async Task<RetornoControllerViewModel<ExibicaoMensagemViewModel, Guid>> AdicionarCliente(ClienteModel cliente)
        {
            RetornoControllerViewModel<ExibicaoMensagemViewModel, Guid> retornoController = new RetornoControllerViewModel<ExibicaoMensagemViewModel, Guid>();
            try
            {
                var clienteInserir = await ModelarCliente(cliente);
                var entityCliente = clienteInserir;

                _clienteRepository.Add(entityCliente);
                _clienteRepository.Save();


                return retornoController;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Guid> DeletarCliente(Guid idCliente)
        {

            var result = _clienteRepository.GetSingleOrDefault(x => x.Id == idCliente);


            if (result == null)
                throw new Exception("Cliente não encontrado.");

            _clienteRepository.Remove(result);
            _clienteRepository.Save();

            return idCliente;
        }

        public async Task<Guid> EditarCliente(ClienteModel editarCliente, Guid idCliente)
        {
            try
            {

                var cliente = _clienteRepository.GetSingleOrDefault(x => x.Id == idCliente);
                if (cliente == null)
                {
                    throw new Exception("O cliente não esta cadastrado na nossa base de dados!");

                }

                cliente.Nome = editarCliente.Nome;
                cliente.Idade = editarCliente.Idade;

                _clienteRepository.Update(cliente);
                _clienteRepository.Save();

                return cliente.Id;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
