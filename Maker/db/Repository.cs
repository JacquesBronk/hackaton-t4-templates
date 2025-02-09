
using Dapper;
using Microsoft.Data.SqlClient;
using  Rentalthingy;

public class Repository(){
			public List<Rental> GetAllRental(){
			using SqlConnection cn = new(ConnectionString());
			return cn.Query<Rental>("SELECT * FROM Rental").ToList();
		}
			public List<Customer> GetAllCustomer(){
			using SqlConnection cn = new(ConnectionString());
			return cn.Query<Customer>("SELECT * FROM Customer").ToList();
		}
			public List<Vehicle> GetAllVehicle(){
			using SqlConnection cn = new(ConnectionString());
			return cn.Query<Vehicle>("SELECT * FROM Vehicle").ToList();
		}
		public string ConnectionString(){
		SqlConnectionStringBuilder builder = new();
		builder.DataSource = "localhost,1433";
		builder.InitialCatalog = "Bruno";
		builder.UserID = "sa";
		builder.Password = "yourStrong(!)Password";
		builder.TrustServerCertificate = true;

		return builder.ToString();
	}
}

