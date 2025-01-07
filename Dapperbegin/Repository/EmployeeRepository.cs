using Dapper;
using Dapperbegin.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

namespace Dapperbegin.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionstring;
        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionstring = _configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() 
        {
            return new SqlConnection(_connectionstring);
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            using var connection= CreateConnection();
            var query = "select * from Employees";
            return await connection.QueryAsync<Employee>(query);    
        }
        public async Task<Employee>GetByIdAsync(int id)
        {
            using var connection= CreateConnection();
            var Query = "select * from employees where Id =@id";
            return await connection.QueryFirstOrDefaultAsync<Employee>(Query, new { Id = id });
        }
        public async Task<int>AddAsync(Employee employee)
        {
            using var connection = CreateConnection();
            var query = "insert into Employees (name,position,salary) values(@Name,@Position,@Salary)";
            return await connection.ExecuteAsync(query, employee);
        }
        public async Task<int>UpdateAsync(Employee employee)
        {
            using var connection = CreateConnection();
            var query = "update Employees set Name=@Name,Position=@Position,Salary=@Salary where Id=@Id";
            return await connection.ExecuteAsync(query, employee);
        }
        public async Task<int>DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var query = "delete employees where Id=@Id";
            return await connection.ExecuteAsync(query, new {Id= id});
        }


    }
}
