using Common;
using Context;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Web.Http.ExceptionHandling;
using Tokens;

namespace common
{
    public class ExceptionHandle 
    {
        private readonly DapperContext _context;
        public ExceptionHandle(DapperContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ExeptionHandle(ExHandle user)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameter = SetParameter(user);
                //Guid userIdValue = (Guid)user.UserId;
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();

                    var queryResult = await connection.QueryMultipleAsync("ProcTokenAndException", parameter, commandType: CommandType.StoredProcedure);
                    var outcome = queryResult.ReadSingleOrDefault<Outcome>();
                    var outcomeId = outcome?.OutcomeId ?? 0;
                    var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;

                    var Model = queryResult.ReadSingleOrDefault<object>();


                    var result = new Result
                    {
                        Outcome = outcome,
                        Data = Model,
                        //UserId = userIdValue
                    };

                    if (outcomeId == 1)
                    {
                        // Login successful
                        return new ObjectResult(result)
                        {
                            StatusCode = 200
                        };
                    }
                    else
                    {
                        // Login failed
                        return new ObjectResult(result)
                        {
                            StatusCode = 400
                        };
                    }


                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public DynamicParameters SetParameter(ExHandle user)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);

            parameters.Add("@Id", user.Id, DbType.Guid);
            parameters.Add("@UserId", user.UserId, DbType.Guid);
            parameters.Add("@Message", user.Message, DbType.String);
            parameters.Add("@API", user.API, DbType.String);
            parameters.Add("@Source", user.Source, DbType.String);
            parameters.Add("@InnerException", user.InnerException, DbType.String);
          
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);


            return parameters;

        }
    }
}
