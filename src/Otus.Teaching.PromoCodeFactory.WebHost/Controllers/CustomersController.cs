using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Customer;
using Services.Abstractions;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee;
using Services.Implementations;
using System.Threading;
using System.Linq;
using Services.Contracts.Customer;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Preference;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Services.Contracts.CustomerPreference;
using Services.Contracts.Preference;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.PromoCode;
using Services.Contracts.PromoCode;
using Services.Contracts.Emploee;
using System.ComponentModel.Design;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController
        : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IPreferenceService _preferenceService;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService customerService,
                                   IPreferenceService preferenceService,
                                   IMapper mapper, 
                                   ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _preferenceService= preferenceService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<CustomerShortResponse>> GetCustomersAsync(CancellationToken cancellationToken)
        {
            var customerDto = await _customerService.GetAllAsync(cancellationToken);

            var custimersList = customerDto.Select(x =>
             _mapper.Map<CustomerDto, CustomerShortResponse>(x)).ToList();

            return custimersList;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var customersDto = await _customerService.GetByIdAsync(id, cancellationToken);

            var customerResponse = new CustomerResponse(); // _mapper.Map<CustomerResponse>(customersDto);
            customerResponse.Id = customersDto.Id;
            customerResponse.FullName = customersDto.FirstName + " " + customersDto.LastName;
            customerResponse.Email = customersDto.Email;
            if((customersDto.Preferences != null)&&(customersDto.Preferences.Count>0))
                customerResponse.Preferences = customersDto.Preferences.Select(x=>
            _mapper.Map<PreferenceDto, PreferenceResponse>(x.Preference)).ToList();

            if((customersDto.PromoCodes != null )&&(customersDto.PromoCodes.Count>0))
                customerResponse.PromoCodes = customersDto.PromoCodes.Select(x =>
            _mapper.Map<PromoCodeDto, PromoCodeShortResponse>(x.PromoCode)).ToList();

            return Ok(customerResponse);
        }

        [HttpPost("{newFirstName}/{newLastName}/{newEmail}")]
        public async Task<ActionResult<string>> CreateCustomerAsync(string newFirstName, string newLastName, string newEmail, Guid[] PreferenceId, CancellationToken cancellationToken)
        {
            var creatingCustomerDto = new CreatingCustomerDto
            {
                FirstName = newFirstName,
                LastName = newLastName,
                Email = newEmail,
                PreferenceIds = new List<Guid>(PreferenceId)
            };
            //TODO: Добавить создание нового клиента вместе с его предпочтениями
            var customersDto = await _customerService.CreateAsync(creatingCustomerDto, cancellationToken);
            var customerResponse = new CustomerResponse(); // _mapper.Map<CustomerResponse>(customersDto);
            customerResponse.Id = customersDto.Id;
            customerResponse.FullName = customersDto.FirstName + " " + customersDto.LastName;
            customerResponse.Email = customersDto.Email;

            if ((customersDto.Preferences != null)&&(customersDto.Preferences.Count>0))
            {
                var PreferenceResponses = new List<PreferenceResponse>();
                foreach (var preferences in customersDto.Preferences)
                {
                    var preferenceDto = await _preferenceService.GetByIdAsync(preferences.PreferenceId, cancellationToken);
                    PreferenceResponses.Add(_mapper.Map<PreferenceDto, PreferenceResponse>(preferenceDto));
                }
                customerResponse.Preferences = PreferenceResponses;
            }
            

            if (customersDto.PromoCodes != null)
                customerResponse.PromoCodes = customersDto.PromoCodes.Select(x =>
            _mapper.Map<PromoCodeDto, PromoCodeShortResponse>(x.PromoCode)).ToList();
            
            // throw new NotImplementedException();
            return Ok(customerResponse);
        }

        
        [HttpPut("{id:guid}/{newFirstName}/{newLastName}/{newEmail}")]
        public async Task<ActionResult<string>> UpdateCustomerAsync(Guid id, string newFirstName, string newLastName, string newEmail, Guid?[] PreferenceId, CancellationToken cancellationToken)
        {
            var updateCustomerDto = new UpdateCustomerDto
            {
                Id = id,
                FirstName = newFirstName,
                LastName = newLastName,
                Email = newEmail,
                PreferenceIds = new List<Guid?>(PreferenceId)
            };
            
            //TODO: Добавить создание нового клиента вместе с его предпочтениями
            var customersDto = await _customerService.UpdateAsync(updateCustomerDto, cancellationToken);
            var customerResponse = new CustomerResponse(); // _mapper.Map<CustomerResponse>(customersDto);
            customerResponse.Id = customersDto.Id;
            customerResponse.FullName = customersDto.FirstName + " " + customersDto.LastName;
            customerResponse.Email = customersDto.Email;

            if ((customersDto.Preferences != null) && (customersDto.Preferences.Count > 0))
            {
                var PreferenceResponses = new List<PreferenceResponse>();
                foreach (var preferences in customersDto.Preferences)
                {
                    var preferenceDto = await _preferenceService.GetByIdAsync(preferences.PreferenceId, cancellationToken);
                    PreferenceResponses.Add(_mapper.Map<PreferenceDto, PreferenceResponse>(preferenceDto));
                }

                customerResponse.Preferences = PreferenceResponses;
            }

            if (customersDto.PromoCodes != null)
                customerResponse.PromoCodes = customersDto.PromoCodes.Select(x =>
            _mapper.Map<PromoCodeDto, PromoCodeShortResponse>(x.PromoCode)).ToList();

            //TODO: Обновить данные клиента вместе с его предпочтениями
            //throw new NotImplementedException();
            return Ok(customerResponse);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<string>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var answer = await _customerService.DeleteByIdAsync(id, cancellationToken);

            return answer.answerStatus switch
            {
                AnswerStatus.OK => Ok(answer.text),
                AnswerStatus.Error => Problem(answer.text),
                AnswerStatus.NotFound => NotFound(answer.text),
                _ => BadRequest("Неизвестная ошибка")
            };

            //TODO: Удаление клиента вместе с выданными ему промокодами
            //throw new NotImplementedException();
        }
    }
}