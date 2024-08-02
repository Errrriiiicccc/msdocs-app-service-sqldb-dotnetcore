using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DotNetCoreSqlDb.Models;

namespace DotNetCoreSqlDb.Data
{
    public class MyDatabaseContext : DbContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options, IHttpContextAccessor accessor)
    : base(options)
        {
            _contextAccessor = accessor; // Comment to trick the repo to republish & redeploy
            var conn = Database.GetDbConnection() as SqlConnection; 
            if (conn != null && accessor.HttpContext != null)
            {
                conn.AccessToken = accessor.HttpContext.Request.Headers["X-MS-TOKEN-AAD-ACCESS-TOKEN"];
            }
        }

        public DbSet<DotNetCoreSqlDb.Models.Todo> Todo { get; set; } = default!;
    }
}
