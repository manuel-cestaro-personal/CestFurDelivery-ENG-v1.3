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
    public class VehicleService : IVehicleService
    {
        private readonly string _connectionstring;
        private readonly ILogger<VehicleService> _logger;

        public VehicleService(IConfiguration configuration, ILogger<VehicleService> logger)
        {
            _connectionstring = configuration.GetConnectionString("CestFurDeliveryDB");
            _logger = logger;
        }
        public async Task<IEnumerable<Vehicle>> GetAll(string username)
        {
            try
            {
                const string query = @"
                SELECT TOP (100) [Id]
                        ,[VehicleName]
                        ,[Note]
                FROM [CestFurDelivery].[dbo].[Vehicle]";
                using var connection = new SqlConnection(_connectionstring);
                var res = await connection.QueryAsync<Vehicle>(query);
                if (res.Count() == 0)
                {
                    _logger.LogWarning($"{DateTime.Now} - VehicleService - {username} - Table Vehicle is empty?");
                    throw new Exception("404, Object not found");
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - VehicleService - {username} - GetAll complete successfully");
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - VehicleService - {username} - Error: {ex.Message}");
                throw;
            }
        }
        public async Task<Vehicle> GetById(Guid id, string username)
        {
            try
            {
                const string query = @"
                SELECT [Id]
                        ,[VehicleName]
                        ,[Note]
                FROM [CestFurDelivery].[dbo].[Vehicle]
                WHERE [Id] = @id;";
                using var connection = new SqlConnection(_connectionstring);
                var res = await connection.QueryFirstOrDefaultAsync<Vehicle>(query, new { id });
                if (res == null)
                {
                    _logger.LogWarning($"{DateTime.Now} - VehicleService - {username} - Vehicle not found");
                    //throw new Exception("404, Object not found");
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - VehicleService - {username} - GetById complete successfully");
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - VehicleService - {username} - Error: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> CheckInstance(Guid id, string username)
        {
            try
            {
                var instance = await GetById(id, username);
                return instance != null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - DeliveryStateService - {username} - Error: {ex.Message}");
                throw;
            }
        }
    }
}