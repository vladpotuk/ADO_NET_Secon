using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class WarehouseItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public DateTime SupplyDate { get; set; }
    }

    public class DBManager
    {
        public string ConnectionString = "Data Source=(local);Initial Catalog=Склад;Integrated Security=True";

        public DBManager() { }

        

        public void InsertNewItem(WarehouseItem newItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Товари (Назва_товару, Тип_товару, Постачальник_товару, Кількість, Собівартість, Дата_постачання) VALUES (@Name, @Type, @Supplier, @Quantity, @Cost, @SupplyDate)", connection);
                    cmd.Parameters.AddWithValue("@Name", newItem.Name);
                    cmd.Parameters.AddWithValue("@Type", newItem.Type);
                    cmd.Parameters.AddWithValue("@Supplier", newItem.Supplier);
                    cmd.Parameters.AddWithValue("@Quantity", newItem.Quantity);
                    cmd.Parameters.AddWithValue("@Cost", newItem.Cost);
                    cmd.Parameters.AddWithValue("@SupplyDate", newItem.SupplyDate);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при вставці нового товару: " + ex.Message);
            }
        }

        public void InsertNewType(string newType)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Типи_товарів (Тип_товару) VALUES (@Type)", connection);
                    cmd.Parameters.AddWithValue("@Type", newType);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при вставці нового типу товару: " + ex.Message);
            }
        }

        public void InsertNewSupplier(string newSupplier)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Постачальники (Постачальник) VALUES (@Supplier)", connection);
                    cmd.Parameters.AddWithValue("@Supplier", newSupplier);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при вставці нового постачальника: " + ex.Message);
            }
        }
        
        public void UpdateItem(WarehouseItem updatedItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Товари SET Назва_товару = @Name, Тип_товару = @Type, Постачальник_товару = @Supplier, Кількість = @Quantity, Собівартість = @Cost, Дата_постачання = @SupplyDate WHERE ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@Name", updatedItem.Name);
                    cmd.Parameters.AddWithValue("@Type", updatedItem.Type);
                    cmd.Parameters.AddWithValue("@Supplier", updatedItem.Supplier);
                    cmd.Parameters.AddWithValue("@Quantity", updatedItem.Quantity);
                    cmd.Parameters.AddWithValue("@Cost", updatedItem.Cost);
                    cmd.Parameters.AddWithValue("@SupplyDate", updatedItem.SupplyDate);
                    cmd.Parameters.AddWithValue("@ID", updatedItem.ID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при оновленні інформації про товар: " + ex.Message);
            }
        }

        public void UpdateType(int typeId, string updatedType)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Типи_товарів SET Тип_товару = @Type WHERE ID = @TypeID", connection);
                    cmd.Parameters.AddWithValue("@Type", updatedType);
                    cmd.Parameters.AddWithValue("@TypeID", typeId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при оновленні інформації про тип товару: " + ex.Message);
            }
        }

        public void UpdateSupplier(int supplierId, string updatedSupplier)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Постачальники SET Постачальник = @Supplier WHERE ID = @SupplierID", connection);
                    cmd.Parameters.AddWithValue("@Supplier", updatedSupplier);
                    cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при оновленні інформації про постачальника: " + ex.Message);
            }
        }


        public void DeleteItem(int itemId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Товари WHERE ID = @ItemID", connection);
                    cmd.Parameters.AddWithValue("@ItemID", itemId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при видаленні товару: " + ex.Message);
            }
        }

        public void DeleteType(int typeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Типи_товарів WHERE ID = @TypeID", connection);
                    cmd.Parameters.AddWithValue("@TypeID", typeId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при видаленні типу товару: " + ex.Message);
            }
        }

        public void DeleteSupplier(int supplierId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Постачальники WHERE ID = @SupplierID", connection);
                    cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при видаленні постачальника: " + ex.Message);
            }
        }


        public string GetSupplierWithMaxItems()
        {
            try
            {
                string supplierName = "";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 Постачальники.Постачальник FROM Постачальники JOIN Товари ON Постачальники.ID = Товари.Постачальник_товару GROUP BY Постачальники.Постачальник ORDER BY COUNT(*) DESC", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        supplierName = reader.GetString(0);
                    }
                }
                return supplierName;
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при отриманні інформації про постачальника з найбільшою кількістю товарів: " + ex.Message);
            }
        }

        public string GetSupplierWithMinItems()
        {
            try
            {
                string supplierName = "";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 Постачальники.Постачальник FROM Постачальники JOIN Товари ON Постачальники.ID = Товари.Постачальник_товару GROUP BY Постачальники.Постачальник ORDER BY COUNT(*) ASC", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        supplierName = reader.GetString(0);
                    }
                }
                return supplierName;
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при отриманні інформації про постачальника з найменшою кількістю товарів: " + ex.Message);
            }
        }

        public string GetTypeWithMaxItems()
        {
            try
            {
                string typeName = "";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 Тип_товару FROM Товари GROUP BY Тип_товару ORDER BY COUNT(*) DESC", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        typeName = reader.GetString(0);
                    }
                }
                return typeName;
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при отриманні інформації про тип товару з найбільшою кількістю одиниць на складі: " + ex.Message);
            }
        }

        public string GetTypeWithMinItems()
        {
            try
            {
                string typeName = "";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 Тип_товару FROM Товари GROUP BY Тип_товару ORDER BY COUNT(*) ASC", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        typeName = reader.GetString(0);
                    }
                }
                return typeName;
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при отриманні інформації про тип товару з найменшою кількістю товарів на складі: " + ex.Message);
            }
        }

        public List<WarehouseItem> GetItemsBySupplyDate(DateTime fromDate)
        {
            try
            {
                List<WarehouseItem> items = new List<WarehouseItem>();
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Товари WHERE Дата_постачання < @FromDate", connection);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        WarehouseItem item = new WarehouseItem();
                        item.ID = reader.GetInt32(0);
                        item.Name = reader.GetString(1);
                        item.Type = reader.GetString(2);
                        item.Supplier = reader.GetString(3);
                        item.Quantity = reader.GetInt32(4);
                        item.Cost = reader.GetDecimal(5);
                        item.SupplyDate = reader.GetDateTime(6);
                        items.Add(item);
                    }
                }
                return items;
            }
            catch (SqlException ex)
            {
                throw new Exception("Помилка при отриманні товарів за вказаною датою постачання: " + ex.Message);
            }
        }


    }
}
