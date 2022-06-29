using cw5.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.Services
{
    public class DbService : IDbService
    {
        private IConfiguration _configuration;
        public DbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> IsCorrectValueAsync(NewProduct product)
        {
            decimal price = 0;
            int idOrder = 0;
            int idProductWarehouse = 0;
            string name = "";
            using var con = new SqlConnection("Data Source=db-mssql16.pjwstk.edu.pl; Initial Catalog=s20296; Integrated Security=True");
            using var com = new SqlCommand("SELECT * from Product WHERE IdProduct=@IdProduct", con);
            com.Parameters.AddWithValue("@IdProduct", product.IdProduct);
            await con.OpenAsync();
            DbTransaction tran = await con.BeginTransactionAsync();
            com.Transaction = (SqlTransaction)tran;
            try
            {
                using (var dr = await com.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        price = decimal.Parse(dr["Price"].ToString());
                    }
                }
                if (price == 0)
                    return -1;
                com.Parameters.Clear();
                com.CommandText = "SELECT * from Warehouse WHERE IdWarehouse=@IdWarehouse";
                com.Parameters.AddWithValue("@IdWarehouse", product.IdWarehouse);
                using (var dr = await com.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        name = dr["Name"].ToString();
                    }
                }
                if (name == "")
                    return -2;
                com.Parameters.Clear();
                com.CommandText = "SELECT *  FROM  \"Order\" o  WHERE o.IdProduct = @IdProduct AND o.Amount = @Amount AND o.CreatedAt < @CreatedAt ";
                com.Parameters.AddWithValue("@IdProduct", product.IdProduct);
                com.Parameters.AddWithValue("@Amount", product.Amount);
                com.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);
                using (var dr = await com.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        idOrder = int.Parse(dr["IdOrder"].ToString());
                    }
                }
                if (idOrder != 0)
                    return -3;

                com.Parameters.Clear();
                com.CommandText = "SELECT * from Product_Warehouse WHERE IdOrder = @IdOrder ";
                com.Parameters.AddWithValue("@IdOrder", idOrder);
                using (var dr = await com.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        idProductWarehouse = int.Parse(dr["IdProductWarehouse"].ToString());
                    }
                }
                if (idProductWarehouse > 0)
                    return -4;

                await tran.CommitAsync();
            }
            catch (SqlException exc)
            {
                await tran.RollbackAsync();
            }
            catch (Exception exc)
            {
                await tran.RollbackAsync();
            }
            await con.CloseAsync();
            return 0;
        }

        public async Task<int> PostProductAsync(NewProduct product)
        {
            decimal price = 0;
            int idOrder = 0;
            int result = 0;
            using var con = new SqlConnection("Data Source=db-mssql16.pjwstk.edu.pl; Initial Catalog=s20296; Integrated Security=True");
                using var com = new SqlCommand("SELECT * from Product WHERE IdProduct=@IdProduct", con);
                com.Parameters.AddWithValue("@IdProduct", product.IdProduct);
                await con.OpenAsync();
                DbTransaction tran = await con.BeginTransactionAsync();
                com.Transaction = (SqlTransaction)tran;
                try
                {
                    using (var dr = await com.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            price = decimal.Parse(dr["Price"].ToString());
                        }
                    }
              
               
                com.Parameters.Clear();
                com.CommandText = "SELECT *  FROM  \"Order\" o  WHERE o.IdProduct = @IdProduct AND o.Amount = @Amount AND o.CreatedAt < @CreatedAt ";
                com.Parameters.AddWithValue("@IdProduct", product.IdProduct);
                com.Parameters.AddWithValue("@Amount", product.Amount);
                com.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);
                using (var dr = await com.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        idOrder = int.Parse(dr["IdOrder"].ToString());
                    }
                }
              
                com.Parameters.Clear();
                com.CommandText = "UPDATE \"Order\" SET  FulfilledAt = @CreatedAt WHERE IdOrder = @IdOrder; ";
                com.Parameters.AddWithValue("@IdOrder", idOrder);
                com.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);
                await com.ExecuteNonQueryAsync();

                com.Parameters.Clear();
                com.CommandText = "INSERT INTO Product_Warehouse(IdWarehouse,   IdProduct, IdOrder, Amount, Price, CreatedAt)  VALUES(@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Amount * @Price, @CreatedAt); ";
                com.Parameters.AddWithValue("@IdWarehouse", product.IdWarehouse);
                com.Parameters.AddWithValue("@IdProduct", product.IdProduct);
                com.Parameters.AddWithValue("@IdOrder", idOrder);
                com.Parameters.AddWithValue("@Amount", product.Amount);
                com.Parameters.AddWithValue("@Price", price);
                com.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);
                await com.ExecuteNonQueryAsync();
                com.Parameters.Clear();
                com.CommandText = "SELECT * from Product_Warehouse WHERE IdOrder = @IdOrder ";
                com.Parameters.AddWithValue("@IdOrder", idOrder);

                using (var dr = await com.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        result = int.Parse(dr["IdProductWarehouse"].ToString());
                    }
                }
                await tran.CommitAsync();
                }
                catch (SqlException exc)
                {
                    await tran.RollbackAsync();
                }
                catch (Exception exc)
                {
                    await tran.RollbackAsync();
                }
            await con.CloseAsync();
            return result;
        }

        public async Task PostProductByStoredProcedureAsync(NewProduct product)
        {
            using var con = new SqlConnection("Data Source=db-mssql16;Initial Catalog=s20296;Integrated Security=True");
            using var com = new SqlCommand("AddProductToWarehouse", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@IdWarehouse", product.IdWarehouse);
            com.Parameters.AddWithValue("@IdProduct", product.IdProduct);
            com.Parameters.AddWithValue("@Amount", product.Amount);
            com.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);

            await con.OpenAsync();
            await com.ExecuteNonQueryAsync();
            return;
        }
    }
}
