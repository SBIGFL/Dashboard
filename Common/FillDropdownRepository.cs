using Context;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
namespace Common
{
    public class FillDropdownRepository
    {
        private readonly DapperContext _context;
        public FillDropdownRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetFillDropDown(FillDropdown input)
        {

            using (var connection = _context.CreateConnection())
            {
                //Guid? userIdValue = (Guid)user.UserId;
                try
                {

                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();

                    var queryResult = await connection.QueryAsync("ProcSearchTableData", SetParameter(input), commandType: CommandType.StoredProcedure);
                    var Model = queryResult.ToList();
                    return new ObjectResult(Model)
                    {
                        StatusCode = 200
                    };
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        public async Task<IActionResult> GetFillDropDown12(FillDropdown input)
        {

            using (var connection = _context.CreateConnection())
            {
                //Guid? userIdValue = (Guid)user.UserId;
                try
                {

                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();

                    var queryResult = await connection.QueryAsync("ProcSearchTableData3", SetParameter3(input), commandType: CommandType.StoredProcedure);
                    var Model = queryResult.ToList();
                    return new ObjectResult(Model)
                    {
                        StatusCode = 200
                    };
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        public async Task<IActionResult> GetFillDropDown1(FillDropdown input)
        {

            using (var connection = _context.CreateConnection())
            {
                //Guid? userIdValue = (Guid)user.UserId;
                try
                {

                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();

                    var queryResult = await connection.QueryAsync("ProcSearchTableData2", SetParameter2(input), commandType: CommandType.StoredProcedure);
                    var Model = queryResult.ToList();
                    return new ObjectResult(Model)
                    {
                        StatusCode = 200
                    };
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        public async Task<IActionResult> GetAutoFillDropDown(FillDropdown input)
        {
 
            using (var connection = _context.CreateConnection())
            {
                //Guid? userIdValue = (Guid)user.UserId;
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("ProcSearchTableData", SetParameter(input), commandType: CommandType.StoredProcedure);
                    var Model = queryResult.Read<Object>().ToList();
                    return new ObjectResult(Model)
                    {
                        StatusCode = 200
                    };
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public DynamicParameters SetParameter(FillDropdown user)
        {
            DynamicParameters parameters = new DynamicParameters();
           
            parameters.Add("@TableName", user.sTableName, DbType.String);
            parameters.Add("@ColumnIdName", user.Id, DbType.String);
            parameters.Add("@ColumnValueName", user.Value, DbType.String);
            parameters.Add("@ColumnName", user.sColumnName, DbType.String);
            parameters.Add("@ColumnValue", user.sColumnValue, DbType.String);
            parameters.Add("@IsActiveColumn", user.IsActiveColumn, DbType.String);



            //parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            return parameters;
        }
        public DynamicParameters SetParameter3(FillDropdown user)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@TableName", user.sTableName, DbType.String);
            parameters.Add("@ColumnIdName", user.Id, DbType.String);
            parameters.Add("@ColumnValueName", user.Value, DbType.String);
            parameters.Add("@ColumnName", user.sColumnName, DbType.String);
            parameters.Add("@ColumnName1", user.sColumnName1, DbType.String);
            parameters.Add("@ColumnValue1", user.sColumnValue1, DbType.String);
            parameters.Add("@ColumnValue", user.sColumnValue, DbType.String);
            parameters.Add("@IsActiveColumn", user.IsActiveColumn, DbType.String);



            //parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            return parameters;
        }
        public DynamicParameters SetParameter2(FillDropdown user)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@TableName", user.sTableName, DbType.String);
            parameters.Add("@ColumnIdName", user.Id, DbType.String);
            parameters.Add("@ColumnValueName", user.Value, DbType.String);
            parameters.Add("@ColumnName", user.sColumnName, DbType.String);
            parameters.Add("@ColumnName2", user.sColumnName2, DbType.String);
           // parameters.Add("@ColumnName3", user.sColumnName2, DbType.String);
            parameters.Add("@ColumnValue", user.sColumnValue, DbType.String);
            parameters.Add("@ColumnValue2", user.sColumnValue2, DbType.String);
            parameters.Add("@ColumnName1", user.sColumnName1, DbType.String);
            parameters.Add("@ColumnValue1", user.sColumnValue1, DbType.String);
            //parameters.Add("@ColumnValue3", user.sColumnValue2, DbType.String);
            parameters.Add("@IsActiveColumn", user.IsActiveColumn, DbType.String);



            //parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            return parameters;
        }
    }
}
