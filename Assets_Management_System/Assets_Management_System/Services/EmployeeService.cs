using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Assets_Management_System.Models;

namespace Assets_Management_System.Services
{
    public class EmployeeService
    {
        public List<Employee> GetAll()
        {
            var list = new List<Employee>();
            string sql = "SELECT * FROM Employees ORDER BY FullName";
            DataTable dt = DbHelper.GetData(sql);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }
            return list;
        }

        public Employee GetById(int id)
        {
            string sql = $"SELECT * FROM Employees WHERE Id = {id}";
            DataTable dt = DbHelper.GetData(sql);
            if (dt.Rows.Count > 0) return Map(dt.Rows[0]);
            return null;
        }

        public void Add(Employee emp)
        {
            string sql = @"INSERT INTO Employees (FullName, Department, Position, Email, PhoneNumber) 
                           VALUES (@FullName, @Department, @Position, @Email, @PhoneNumber)";
            
            DbHelper.Execute(sql, 
                new NpgsqlParameter("FullName", emp.FullName),
                new NpgsqlParameter("Department", emp.Department ?? ""),
                new NpgsqlParameter("Position", emp.Position ?? ""),
                new NpgsqlParameter("Email", emp.Email ?? ""),
                new NpgsqlParameter("PhoneNumber", emp.PhoneNumber ?? "")
            );
        }

        public void Update(Employee emp)
        {
            string sql = @"UPDATE Employees SET 
                           FullName=@FullName, Department=@Department, Position=@Position, 
                           Email=@Email, PhoneNumber=@PhoneNumber 
                           WHERE Id=@Id";
            
            DbHelper.Execute(sql, 
                new NpgsqlParameter("FullName", emp.FullName),
                new NpgsqlParameter("Department", emp.Department ?? ""),
                new NpgsqlParameter("Position", emp.Position ?? ""),
                new NpgsqlParameter("Email", emp.Email ?? ""),
                new NpgsqlParameter("PhoneNumber", emp.PhoneNumber ?? ""),
                new NpgsqlParameter("Id", emp.Id)
            );
        }

        public void Delete(int id)
        {
            string sql = $"DELETE FROM Employees WHERE Id = {id}";
            DbHelper.Execute(sql);
        }

        private Employee Map(DataRow row)
        {
            return new Employee
            {
                Id = Convert.ToInt32(row["Id"]),
                FullName = row["FullName"].ToString(),
                Department = row["Department"].ToString(),
                Position = row["Position"].ToString(),
                Email = row["Email"].ToString(),
                PhoneNumber = row["PhoneNumber"].ToString()
            };
        }

        public void EnsureTableExists()
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Employees (
                    Id SERIAL PRIMARY KEY,
                    FullName VARCHAR(100) NOT NULL,
                    Department VARCHAR(100),
                    Position VARCHAR(100),
                    Email VARCHAR(100),
                    PhoneNumber VARCHAR(20)
                );";
            DbHelper.Execute(sql);
        }
    }
}
