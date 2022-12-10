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
                        ,[IsActive]
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
                        ,[IsActive]
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
                _logger.LogError($"{DateTime.Now} - TeamService - {username} - Error: {ex.Message}");
                throw;
            }
        }
        public async Task Update(Team model, string username)
        {
			try
			{
				const string query = @"
                UPDATE [dbo].[Team]
                SET [Name] = @Name
                   ,[Description] = @Description
                WHERE [Id] = @Id;";
				using var connection = new SqlConnection(_connectionstring);
				var res = await connection.ExecuteAsync(query, model);
				if (res == 0)
				{
					_logger.LogError($"{DateTime.Now} - TeamService - {username} - No rows affected, bad request");
					throw new Exception("400, Bad request");
				}
				else
				{
					_logger.LogInformation($"{DateTime.Now} - TeamService - {username} - Update complete successfully");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{DateTime.Now} - TeamService - {username} - Error: {ex.Message}");
				throw;
			}
		}
		public async Task ChangeSet(Guid id, string username)
		{
			try
			{
                Team model = await GetById(id, username);
                if (model == null)
                {
					_logger.LogError($"{DateTime.Now} - TeamService - {username} - the Team is null!");
					throw new Exception("400, Bad request");
				}
				model.IsActive = !model.IsActive;
				const string query = @"
                UPDATE [dbo].[Team]
                SET [IsActive] = @IsActive
                WHERE [Id] = @Id;";
				using var connection = new SqlConnection(_connectionstring);
				var res = await connection.ExecuteAsync(query, model);
				if (res == 0)
				{
					_logger.LogError($"{DateTime.Now} - TeamService - {username} - No rows affected, bad request");
					throw new Exception("400, Bad request");
				}
				else
				{
					_logger.LogInformation($"{DateTime.Now} - TeamService - {username} - Update complete successfully");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{DateTime.Now} - TeamService - {username} - Error: {ex.Message}");
				throw;
			}
		}

		public async Task Insert(Team model, string username)
		{
            try
            { 
			    const string query = @"
                INSERT INTO [dbo].[Team]
                    ([Id]
                    ,[Name]
                    ,[Description]
                    ,[IsActive])
                VALUES
                    (@Id
                    ,@Name
                    ,@Description
                    ,@IsActive);";
			    using var connection = new SqlConnection(_connectionstring);
			    var res = await connection.ExecuteAsync(query, model);
			    if (res == 0)
			    {
				    _logger.LogError($"{DateTime.Now} - DeliveryService - {username} - No rows affected, bad request");
				    throw new Exception("400, Bad request");
			    }
			    else
			    {
				    _logger.LogInformation($"{DateTime.Now} - DeliveryService - {username} - Insert complete successfully");
			    }
			}
			catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - DeliveryService - {username} - Error: {ex.Message}");
                throw;
            }
        }
	}
}
