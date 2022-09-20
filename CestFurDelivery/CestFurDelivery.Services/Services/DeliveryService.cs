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
    public class DeliveryService : IDeliveryService
    {
        private readonly string _connectionstring;
        private readonly ILogger<DeliveryService> _logger;

        public DeliveryService(IConfiguration configuration, ILogger<DeliveryService> logger)
        {
            _connectionstring = configuration.GetConnectionString("CestFurDeliveryDB");
            _logger = logger;
        }
        public async Task Insert(Delivery model, string username)
        {
            try
            {
                const string query = @"
                INSERT INTO [dbo].[Delivery]
                       ([Id]
                       ,[Date]
                       ,[TimeStart]
                       ,[TimeEnd]
                       ,[ClientName]
                       ,[ClientSurname]
                       ,[City]
                       ,[Furniture]
                       ,[Note]
                       ,[IdDeliveryState]
                       ,[Team]
                       ,[Vehicle1]
                       ,[Vehicle2]
                       ,[Vehicle3])
                VALUES
                       (@Id
                       ,@Date
                       ,@TimeStart
                       ,@TimeEnd
                       ,@ClientName
                       ,@ClientSurname
                       ,@City
                       ,@Furniture
                       ,@Note
                       ,@IdDeliveryState
                       ,@Team
                       ,@Vehicle1
                       ,@Vehicle2
                       ,@Vehicle3);";
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

        public async Task Update(Delivery model, string username)
        {
            try
            {
                const string query = @"
                UPDATE [dbo].[Delivery]
                SET [Date] = @Date
                      ,[TimeStart] = @TimeStart
                      ,[TimeEnd] = @TimeEnd
                      ,[ClientName] = @ClientName
                      ,[ClientSurname] = @ClientSurname
                      ,[City] = @City
                      ,[Furniture] = @Furniture
                      ,[Note] = @Note
                      ,[IdDeliveryState] = @IdDeliveryState
                      ,[Team] = @Team
                      ,[Vehicle1] = @Vehicle1
                      ,[Vehicle2] = @Vehicle2
                      ,[Vehicle3] = @Vehicle3
                WHERE [Id] = @Id;";
                using var connection = new SqlConnection(_connectionstring);
                var res = await connection.ExecuteAsync(query, model);
                if (res == 0)
                {
                    _logger.LogError($"{DateTime.Now} - DeliveryService - {username} - No rows affected, bad request");
                    throw new Exception("400, Bad request");
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - DeliveryService - {username} - Update complete successfully");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - DeliveryService - {username} - Error: {ex.Message}");
                throw;
            }
        }

        public async Task Delete(Guid id, string username)
        {
            try
            {
                const string query = @"
                DELETE FROM [dbo].[Delivery]
                WHERE [Id] = @id;";
                using var connection = new SqlConnection(_connectionstring);
                var res = await connection.ExecuteAsync(query, new { id });
                if (res == 0)
                {
                    _logger.LogError($"{DateTime.Now} - DeliveryService - {username} - No rows affected, bad request");
                    throw new Exception("400, Bad request");
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - DeliveryService - {username} - Delete complete successfully");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - DeliveryService - {username} - Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Delivery> GetById(Guid id, string username)
        {
            try
            {
                const string query = @"
                SELECT [Id]
                      ,[Date]
                      ,[TimeStart]
                      ,[TimeEnd]
                      ,[ClientName]
                      ,[ClientSurname]
                      ,[City]
                      ,[Furniture]
                      ,[Note]
                      ,[IdDeliveryState]
                      ,[Team]
                      ,[Vehicle1]
                      ,[Vehicle2]
                      ,[Vehicle3]
                FROM [dbo].[Delivery]
                WHERE [Id] = @id;";
                using var connection = new SqlConnection(_connectionstring);
                var res = await connection.QueryFirstOrDefaultAsync<Delivery>(query, new { id });
                if (res == null)
                {
                    _logger.LogWarning($"{DateTime.Now} - DeliveryService - {username} - Delivery with the id {id} not found");
                    throw new Exception("404, Object not found");
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - DeliveryService - {username} - GetById complete successfully");
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - DeliveryService - {username} - Error: {ex.Message}");
                throw;
            }
        }
        public async Task<IEnumerable<Delivery>> GetByDate(DateTime date, string username)
        {
            try
            {
                const string query = @"
                SELECT [Id]
                      ,[Date]
                      ,[TimeStart]
                      ,[TimeEnd]
                      ,[ClientName]
                      ,[ClientSurname]
                      ,[City]
                      ,[Furniture]
                      ,[Note]
                      ,[IdDeliveryState]
                      ,[Team]
                      ,[Vehicle1]
                      ,[Vehicle2]
                      ,[Vehicle3]
                FROM [dbo].[Delivery]
                WHERE [Date] = (SELECT CAST(@date AS date) AS date)
                ORDER BY [TimeStart], [TimeEnd];";
                using var connection = new SqlConnection(_connectionstring);
                var res = await connection.QueryAsync<Delivery>(query, new { date });
                if (res.Count() == 0)
                {
                    _logger.LogInformation($"{DateTime.Now} - DeliveryService - {username} - Delivery for the date {date} not found");
                    //throw new Exception("404, Object not found");
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - DeliveryService - {username} - GetByDate complete successfully");
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - DeliveryService - {username} - Error: {ex.Message}");
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
