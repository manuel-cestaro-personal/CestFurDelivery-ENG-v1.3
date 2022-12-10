using CestFurDelivery.Domain.Entities;
using CestFurDelivery.Services.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CestFurDelivery.Services.Services
{
    public class ChangeStatusService : IChangeStatusService
    {
        private readonly string _connectionstring;
        private readonly ILogger<ChangeStatusService> _logger;
        private readonly IDeliveryStateService _deliveryStateService;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ChangeStatusService(ILogger<ChangeStatusService> logger,
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            IDeliveryStateService deliveryStateService,
            IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("CestFurDeliveryDB");
            _logger = logger;
            _deliveryStateService = deliveryStateService;
        }

        public async Task<Guid> FindStatus(Delivery delivery, string username)
        {
            try
            {
                DeliveryState currentState = await _deliveryStateService.GetById(delivery.IdDeliveryState, username);
                IEnumerable<DeliveryState> stateList = await _deliveryStateService.GetAll(username);
                if (currentState.State == "Not confirmed")//edit
                {
                    foreach (var state in stateList)
                    {
                        if (state.State == "Awaiting confirmation")//edit
                        {
                            _logger.LogInformation($"{DateTime.Now} - ChengeStatusService - {username} - New status found");
                            return state.Id;
                        }
                    }
                }
                else if (currentState.State == "Awaiting confirmation")//edit
                {
                    foreach (var state in stateList)
                    {
                        if (state.State == "Confirmed")//edit
                        {
                            _logger.LogInformation($"{DateTime.Now} - ChengeStatusService - {username} - New status found");
							return state.Id;
                        }
                    }
                }
                else if (currentState.State == "Confirmed")//edit
                {
                    foreach (var state in stateList)
                    {
                        if (state.State == "Not confirmed")//edit
                        {
                            _logger.LogInformation($"{DateTime.Now} - ChengeStatusService - {username} - New status found");
							return state.Id;
                        }
                    }
                }
                _logger.LogError($"{DateTime.Now} - ChengeStatusService - {username} - New state not found");
                throw new Exception("400, Bad request");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - ChengeStatusService - {username} - Error: {ex.Message}");
                throw;
            }
        }
        public async Task ChangeStatus(Delivery model, Guid StausID, string username)
        {
            try
            {
                model.IdDeliveryState = StausID;
                const string query = @"
                    UPDATE [dbo].[Delivery]
                    SET [IdDeliveryState] = @IdDeliveryState
                    WHERE [Id] = @Id;";
                using var connection = new SqlConnection(_connectionstring);
                var res = await connection.ExecuteAsync(query, model);
                if (res == 0)
                {
                    _logger.LogError($"{DateTime.Now} - ChengeStatusService - {username} - No rows affected, bad request");
                    throw new Exception("400, Bad request");
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - ChengeStatusService - {username} - Update complete successfully");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - ChengeStatusService - {username} - Error: {ex.Message}");
                throw;
            }
        }
    }
} 
