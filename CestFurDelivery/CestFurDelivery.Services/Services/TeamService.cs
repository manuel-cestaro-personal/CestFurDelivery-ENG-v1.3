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
    public class TeamService : ITeamService
    {
        private readonly string _connectionstring;
        private readonly ILogger<TeamService> _logger;

        public TeamService(IConfiguration configuration, ILogger<TeamService> logger)
        {
            _connectionstring = configuration.GetConnectionString("CestFurDeliveryDB");
            _logger = logger;
        }

        public async Task<IEnumerable<Team>> GetAll(string username)
        {
            try
            {
                const string query = @"
                SELECT TOP (100) [Id]
                        ,[Name]
                        ,[Description]
                FROM [CestFurDelivery].[dbo].[Team]
                ORDER BY [Name];";
                using var connection = new SqlConnection(_connectionstring);
                var res = await connection.QueryAsync<Team>(query);
                if (res.Count() == 0)
                {
                    _logger.LogWarning($"{DateTime.Now} - TeamService - {username} - Table Team is empty?");
                    throw new Exception("404, Object not found");
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - TeamService - {username} - GetAll complete successfully");
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - TeamService - {username} - Error: {ex.Message}");
                throw;
            }
        }
        public async Task<Team> GetById(Guid id, string username)
        {
            try
            {
                const string query = @"
                SELECT [Id]
                        ,[Name]
                        ,[Description]
                FROM [CestFurDelivery].[dbo].[Team]
                WHERE [Id] = @id;";
                using var connection = new SqlConnection(_connectionstring);
                var res = await connection.QueryFirstOrDefaultAsync<Team>(query, new { id });
                if (res == null)
                {
                    _logger.LogWarning($"{DateTime.Now} - TeamService - {username} - Team not found");
                    //throw new Exception("404, Object not found");
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - TeamService - {username} - GetById complete successfully");
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - TeamService - {username} - Error: {ex.Message}");
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
