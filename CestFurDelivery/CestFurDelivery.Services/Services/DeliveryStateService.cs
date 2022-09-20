using CestFurDelivery.Domain.Entities;
using CestFurDelivery.Services.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CestFurDelivery.Services.Services
{
    public class DeliveryStateService : IDeliveryStateService
    {
        private readonly string _connectionstring;
        private readonly ILogger<DeliveryStateService> _logger;

        public DeliveryStateService(IConfiguration configuration, ILogger<DeliveryStateService> logger)
        {
            _connectionstring = configuration.GetConnectionString("CestFurDeliveryDB");
            _logger = logger;
        }
        public async Task<IEnumerable<DeliveryState>> GetAll(string username)
        {
            try
            {
                const string query = @"
                SELECT TOP (100) [Id]
                        ,[State]
                        ,[Icon]
                FROM [CestFurDelivery].[dbo].[DeliveryState]
                ORDER BY [State] DESC";
                using var connection = new SqlConnection(_connectionstring);
                var res = await connection.QueryAsync<DeliveryState>(query);
                if (res.Count() == 0)
                {
                    _logger.LogWarning($"{DateTime.Now} - DeliveryStateService - {username} - Table DeliveryState is empty?");
                    throw new Exception("404, Object not found");
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - DeliveryStateService - {username} - GetAll complete successfully");
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - DeliveryStateService - {username} - Error: {ex.Message}");
                throw;
            }
        }
        public async Task<DeliveryState> GetById(Guid id, string username)
        {
            try
            {
                const string query = @"
                SELECT [Id]
                        ,[State]
                        ,[Icon]
                FROM [CestFurDelivery].[dbo].[DeliveryState]
                WHERE [Id] = @id;";
                using var connection = new SqlConnection(_connectionstring);
                var res = await connection.QueryFirstOrDefaultAsync<DeliveryState>(query, new { id });
                if (res == null)
                {
                    _logger.LogWarning($"{DateTime.Now} - DeliveryStateService - {username} - DeliveryState not found");
                    //throw new Exception("404, Object not found");
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - DeliveryStateService - {username} - GetById complete successfully");
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - DeliveryStateService - {username} - Error: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> CheckInstance(Guid id, string username)
        {
            try
            {
                var instance = await GetById(id, username);
                return instance!=null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - DeliveryStateService - {username} - Error: {ex.Message}");
                throw;
            }
        }
    }
}