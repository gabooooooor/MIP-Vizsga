using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Microsoft.SqlServer.Server;

namespace MIPVizsga
{
    public class SqliteDeliveryRepository : IDeliveryRepository
    {
        private readonly string _connectionString;
        private readonly string _dbPath;

        // Constructor - takes dbPath parameter (default "ponyexpress.db")
        public SqliteDeliveryRepository(string dbPath = "ponyexpress.db")
        {
            _dbPath = dbPath;
            _connectionString = $"Data Source={dbPath};Version=3;";
        }

        // Implement Initialize() - creates database file and table
        public void Initialize()
        {
           
            if (!File.Exists(_dbPath))
            {
                SQLiteConnection.CreateFile(_dbPath);
            }

            
            SQLiteConnection connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string sql = "CREATE TABLE IF NOT EXISTS Deliveries (Id INTEGER PRIMARY KEY AUTOINCREMENT, Recipient TEXT NOT NULL, Priority INTEGER NOT NULL, Status TEXT NOT NULL)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }

        // Implement Insert() - adds delivery to database
        public void Insert(Delivery delivery)
        {
            SQLiteConnection connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string sql = "INSERT INTO Deliveries (Recipient, Priority, Status) VALUES (@recipient, @priority, @status)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@recipient", delivery.Recipient);
            command.Parameters.AddWithValue("@priority", delivery.Priority);
            command.Parameters.AddWithValue("@status", delivery.Status);
            command.ExecuteNonQuery();

            connection.Close();
        }

        // Implement LoadAll() - gets all deliveries from database
        public List<Delivery> LoadAll()
        {
            List<Delivery> deliveries = new List<Delivery>();

            SQLiteConnection connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string sql = "SELECT Id, Recipient, Priority, Status FROM Deliveries";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)(long)reader["Id"];
                string recipient = (string)reader["Recipient"];
                int priority = (int)(long)reader["Priority"];
                string status = (string)reader["Status"];

                // Create the right type based on priority
                Delivery delivery;
                if (priority == 3)
                {
                    delivery = new ExpressDelivery();
                }
                else
                {
                    delivery = new RegularDelivery();
                }

                // Set the properties
                delivery.Id = id;
                delivery.Recipient = recipient;
                delivery.Priority = priority;
                delivery.Status = status;

                deliveries.Add(delivery);
            }

            reader.Close();
            connection.Close();

            return deliveries;
        }

        // Implement UpdateStatus() - updates a delivery's status
        public void UpdateStatus(int deliveryId, string newStatus)
        {
            SQLiteConnection connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string sql = "UPDATE Deliveries SET Status = @status WHERE Id = @id";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@status", newStatus);
            command.Parameters.AddWithValue("@id", deliveryId);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}