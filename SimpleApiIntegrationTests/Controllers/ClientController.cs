using Microsoft.AspNetCore.Mvc;
using SimpleApiIntegrationTests.Models;
using SimpleApiIntegrationTests.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleApiIntegrationTests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository) => _clientRepository = clientRepository;

        [HttpGet]
        public async Task<IEnumerable<Cliente>> Get() => await _clientRepository.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<Cliente> Get(int id) => await _clientRepository.GetByIdAsync(id);

        [HttpPost]
        public void Post([FromBody] string value) => throw new NotImplementedException();

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) => throw new NotImplementedException();

        [HttpDelete("{id}")]
        public void Delete(int id) => throw new NotImplementedException();

    }
}
