﻿using Org.BouncyCastle.Asn1.Pkcs;
using PriceSignageSystem.Models.Constants;
using PriceSignageSystem.Models.DatabaseContext;
using PriceSignageSystem.Models.Dto;
using PriceSignageSystem.Models.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PriceSignageSystem.Models.Repository
{
    public class STRPRCRepository : ISTRPRCRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly string connectionString;
        private readonly string connectionString151;
        private readonly int commandTimeoutInSeconds;

        public STRPRCRepository(ApplicationDbContext db)
        {
            _db = db;
            connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            connectionString151 = ConfigurationManager.ConnectionStrings["MyConnectionString151"].ConnectionString;
            commandTimeoutInSeconds = 180;

        }

        public List<STRPRC> GetAll()
        {
            var records = new List<STRPRC>();
            // Set up the connection and command
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_FetchAllSTRPRC", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Open the connection and execute the command
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Process the result set
                while (reader.Read())
                {
                    var record = new STRPRC
                    {
                        O3LOC = (decimal)reader["O3LOC"],
                        O3CLAS = (decimal)reader["O3CLAS"],
                        O3IDSC = reader["O3IDSC"].ToString(),
                        O3SKU = (decimal)reader["O3SKU"],
                        O3SCCD = reader["O3SCCD"].ToString(),
                        O3UPC = (decimal)reader["O3UPC"],
                        O3VNUM = (decimal)reader["O3VNUM"],
                        O3TYPE = reader["O3TYPE"].ToString(),
                        O3DEPT = (decimal)reader["O3DEPT"],
                        O3SDPT = (decimal)reader["O3SDPT"],
                        O3SCLS = (decimal)reader["O3SCLS"],
                        O3POS = (decimal)reader["O3POS"],
                        O3POSU = (decimal)reader["O3POSU"],
                        O3REG = (decimal)reader["O3REG"],
                        O3REGU = (decimal)reader["O3REGU"],
                        O3ORIG = (decimal)reader["O3ORIG"],
                        O3ORGU = (decimal)reader["O3ORGU"],
                        O3EVT = (decimal)reader["O3EVT"],
                        O3PMMX = (decimal)reader["O3PMMX"],
                        O3PMTH = (decimal)reader["O3PMTH"],
                        O3PDQT = (decimal)reader["O3PDQT"],
                        O3PDPR = (decimal)reader["O3PDPR"],
                        O3SDT = (decimal)reader["O3SDT"],
                        O3EDT = (decimal)reader["O3EDT"],
                        O3TRB3 = reader["O3TRB3"].ToString(),
                        O3FGR = (decimal)reader["O3FGR"],
                        O3FNAM = reader["O3FNAM"].ToString(),
                        O3SLUM = reader["O3SLUM"].ToString(),
                        O3DIV = reader["O3DIV"].ToString(),
                        O3TUOM = reader["O3TUOM"].ToString(),
                        O3DATE = (decimal)reader["O3DATE"],
                        O3CURD = (decimal)reader["O3CURD"],
                        O3USER = reader["O3USER"].ToString(),
                        DateUpdated = (DateTime)reader["DateUpdated"]
                    };

                    records.Add(record);
                }

                // Close the reader and connection
                reader.Close();
                connection.Close();
            }

            return records;
        }

        public List<STRPRCDto> GetStores()
        {
            var stores = new List<STRPRCDto>();
            // Set up the connection and command
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_FetchDistinctStores", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Open the connection and execute the command
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Process the result set
                while (reader.Read())
                {
                    var store = new STRPRCDto
                    {
                        O3LOC = (decimal)reader["O3LOC"]
                    };

                    stores.Add(store);
                }

                // Close the reader and connection
                reader.Close();
                connection.Close();
            }

            return stores;
        }

        public STRPRCDto SearchString(string query, string searchFilter)
        {
            var data = new STRPRCDto();

            if (searchFilter == "SKU")
            {
                data = (from a in _db.STRPRCs
                        where a.O3SKU.ToString() == query
                        select new STRPRCDto
                        {
                            O3LOC = a.O3LOC,
                            O3CLAS = a.O3CLAS,
                            O3IDSC = a.O3IDSC,
                            O3SKU = a.O3SKU,
                            O3SCCD = String.IsNullOrEmpty(a.O3SCCD) ? "-" : a.O3SCCD,
                            O3UPC = a.O3UPC,
                            O3VNUM = a.O3VNUM,
                            O3TYPE = a.O3TYPE,
                            O3DEPT = a.O3DEPT,
                            O3SDPT = a.O3SDPT,
                            O3SCLS = a.O3SCLS,
                            O3POS = a.O3POS,
                            O3POSU = a.O3POSU,
                            O3REG = a.O3REG,
                            O3REGU = a.O3REGU,
                            O3ORIG = a.O3ORIG,
                            O3ORGU = a.O3ORGU,
                            O3EVT = a.O3EVT,
                            O3PMMX = a.O3PMMX,
                            O3PMTH = a.O3PMTH,
                            O3PDQT = a.O3PDQT,
                            O3PDPR = a.O3PDPR,
                            O3SDT = a.O3SDT,
                            O3EDT = a.O3EDT,
                            O3TRB3 = String.IsNullOrEmpty(a.O3TRB3) ? "-" : a.O3TRB3,
                            O3FGR = a.O3FGR,
                            O3FNAM = a.O3FNAM,
                            O3MODL = String.IsNullOrEmpty(a.O3MODL) ? "-" : a.O3MODL,
                            O3LONG = String.IsNullOrEmpty(a.O3LONG) ? "-" : a.O3LONG,
                            O3SLUM = a.O3SLUM,
                            O3DIV = a.O3DIV,
                            O3TUOM = a.O3TUOM,
                            O3DATE = a.O3DATE,
                            O3CURD = a.O3CURD,
                            O3USER = a.O3USER,
                            DateUpdated = a.DateUpdated,
                            SelectedTypeId = a.O3REGU == a.O3POS && a.O3EDT == 999999 ? 1 : 2,
                            SelectedCategoryId = (a.O3DEPT == 150 && (a.O3SDPT == 10 || a.O3SDPT == 12 || a.O3SDPT == 13 || a.O3SDPT == 14)) ||
                            (a.O3DEPT == 401 || a.O3DEPT == 402 || a.O3DEPT == 403 || a.O3DEPT == 404) ? 1 : 2,
                            IsExemp = (a.O3REG == a.O3POS && a.O3EDT != 999999) || (a.O3REG < a.O3POS && a.TypeId == 2) ? "Y" : "N",
                        }).FirstOrDefault();
            }
            else
            {
                data = (from a in _db.STRPRCs
                        where a.O3UPC.ToString() == query
                        select new STRPRCDto
                        {
                            O3LOC = a.O3LOC,
                            O3CLAS = a.O3CLAS,
                            O3IDSC = a.O3IDSC,
                            O3SKU = a.O3SKU,
                            O3SCCD = String.IsNullOrEmpty(a.O3SCCD) ? "-" : a.O3SCCD,
                            O3UPC = a.O3UPC,
                            O3VNUM = a.O3VNUM,
                            O3TYPE = a.O3TYPE,
                            O3DEPT = a.O3DEPT,
                            O3SDPT = a.O3SDPT,
                            O3SCLS = a.O3SCLS,
                            O3POS = a.O3POS,
                            O3POSU = a.O3POSU,
                            O3REG = a.O3REG,
                            O3REGU = a.O3REGU,
                            O3ORIG = a.O3ORIG,
                            O3ORGU = a.O3ORGU,
                            O3EVT = a.O3EVT,
                            O3PMMX = a.O3PMMX,
                            O3PMTH = a.O3PMTH,
                            O3PDQT = a.O3PDQT,
                            O3PDPR = a.O3PDPR,
                            O3SDT = a.O3SDT,
                            O3EDT = a.O3EDT,
                            O3TRB3 = String.IsNullOrEmpty(a.O3TRB3) ? "-" : a.O3TRB3,
                            O3FGR = a.O3FGR,
                            O3FNAM = a.O3FNAM,
                            O3MODL = String.IsNullOrEmpty(a.O3MODL) ? "-" : a.O3MODL,
                            O3LONG = String.IsNullOrEmpty(a.O3LONG) ? "-" : a.O3LONG,
                            O3SLUM = a.O3SLUM,
                            O3DIV = a.O3DIV,
                            O3TUOM = a.O3TUOM,
                            O3DATE = a.O3DATE,
                            O3CURD = a.O3CURD,
                            O3USER = a.O3USER,
                            DateUpdated = a.DateUpdated,
                            SelectedTypeId = a.O3REGU == a.O3POS && a.O3EDT == 999999 ? 1 : 2,
                            SelectedCategoryId = (a.O3DEPT == 150 && (a.O3SDPT == 10 || a.O3SDPT == 12 || a.O3SDPT == 13 || a.O3SDPT == 14)) ||
                            (a.O3DEPT == 401 || a.O3DEPT == 402 || a.O3DEPT == 403 || a.O3DEPT == 404) ? 1 : 2,
                            IsExemp = (a.O3REG == a.O3POS && a.O3EDT != 999999) || (a.O3REG < a.O3POS && a.TypeId == 2) ? "Y" : "N",
                        }).FirstOrDefault();
            }


            return data;
        }

        public List<STRPRCDto> GetData(decimal O3SKU)
        {
            var data = new List<STRPRCDto>();

            // Set up the connection and command
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_GetDataBySKU", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters if required
                command.Parameters.AddWithValue("@O3SKU", O3SKU);

                // Open the connection and execute the command
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Process the result set
                while (reader.Read())
                {
                    var record = new STRPRCDto
                    {
                        O3LOC = (decimal)reader["O3LOC"],
                        O3CLAS = (decimal)reader["O3CLAS"],
                        O3IDSC = reader["O3IDSC"].ToString(),
                        O3SKU = (decimal)reader["O3SKU"],
                        O3SCCD = reader["O3SCCD"].ToString(),
                        O3UPC = (decimal)reader["O3UPC"],
                        O3VNUM = (decimal)reader["O3VNUM"],
                        O3TYPE = reader["O3TYPE"].ToString(),
                        O3DEPT = (decimal)reader["O3DEPT"],
                        O3SDPT = (decimal)reader["O3SDPT"],
                        O3SCLS = (decimal)reader["O3SCLS"],
                        O3POS = (decimal)reader["O3POS"],
                        O3POSU = (decimal)reader["O3POSU"],
                        O3REG = (decimal)reader["O3REG"],
                        O3REGU = (decimal)reader["O3REGU"],
                        O3ORIG = (decimal)reader["O3ORIG"],
                        O3ORGU = (decimal)reader["O3ORGU"],
                        O3EVT = (decimal)reader["O3EVT"],
                        O3PMMX = (decimal)reader["O3PMMX"],
                        O3PMTH = (decimal)reader["O3PMTH"],
                        O3PDQT = (decimal)reader["O3PDQT"],
                        O3PDPR = (decimal)reader["O3PDPR"],
                        O3SDT = (decimal)reader["O3SDT"],
                        O3EDT = (decimal)reader["O3EDT"],
                        O3TRB3 = reader["O3TRB3"].ToString(),
                        O3FGR = (decimal)reader["O3FGR"],
                        O3FNAM = reader["O3FNAM"].ToString(),
                        O3MODL = reader["O3MODL"].ToString(),
                        O3LONG = reader["O3LONG"].ToString(),
                        O3SLUM = reader["O3SLUM"].ToString(),
                        O3DIV = reader["O3DIV"].ToString(),
                        O3TUOM = reader["O3TUOM"].ToString(),
                        O3DATE = (decimal)reader["O3DATE"],
                        O3CURD = (decimal)reader["O3CURD"],
                        O3USER = reader["O3USER"].ToString(),
                        DateUpdated = (DateTime)reader["DateUpdated"]
                    };

                    data.Add(record);
                }

                // Close the reader and connection
                reader.Close();
                connection.Close();
            }

            return data;
        }

        public IEnumerable<STRPRC> GetAllData()
        {
            return _db.STRPRCs;
        }

        public async Task<List<STRPRCDto>> GetDataByStartDate(decimal startDate)
        {
            var sp = "sp_GetExemptions";
            var data = new List<STRPRCDto>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sp, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = commandTimeoutInSeconds;

                connection.Open();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    var record = new STRPRCDto
                    {
                        IsPrinted = "0",
                        O3SKU = (decimal)reader["O3SKU"],
                        O3UPC = (decimal)reader["O3UPC"],
                        O3IDSC = reader["O3IDSC"].ToString(),
                        O3REG = (decimal)reader["O3REG"],
                        O3POS = (decimal)reader["O3POS"],
                        O3SDT = (decimal)reader["O3SDT"],
                        O3EDT = (decimal)reader["O3EDT"],
                        O3RSDT = (decimal)reader["O3RSDT"],
                        O3REDT = (decimal)reader["O3REDT"],
                        TypeId = (int)reader["TypeId"],
                        SizeId = (int)reader["SizeId"],
                        CategoryId = (int)reader["CategoryId"],
                        DepartmentName = reader["DepartmentName"].ToString(),
                        IsReverted = reader["IsReverted"].ToString(),
                        HasInventory = reader["HasInventory"].ToString(),
                        IsExemp = reader["IsExemp"].ToString(),
                        ExempType = reader["ExempType"].ToString(),
                        O3TYPE = reader["O3TYPE"].ToString(),
                        IBHAND = (decimal)reader["IBHAND"],
                        SizeName = reader["SizeName"].ToString(),
                        TypeName = reader["TypeName"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        StoreIDs = reader["StoreIDs"].ToString().Split(',').Select(int.Parse).ToList().Count == 27 ? "All Clubs" : reader["StoreIDs"].ToString(),
                        DateExemption = reader["DateExemption"].ToString(),
                    };

                    //if ((decimal)reader["O3RSDT"] == startDate)
                    //{
                    //    if ((int)reader["TypeId"] == ReportConstants.Type.Regular)
                    //    {
                    //        record.O3SDT = (decimal)reader["O3RSDT"];
                    //        record.O3EDT = (decimal)reader["O3REDT"];
                    //    }
                    //    else continue;
                    //}

                    data.Add(record);
                }

                reader.Close();
                connection.Close();
            }

            return data;

        }

        public STRPRCDto GetSKUDetails(decimal O3SKU)
        {
            var sp = "sp_GetSKUDetails";
            var data = new STRPRCDto();
            // Set up the connection and command
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sp, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = commandTimeoutInSeconds;

                // Add parameters if required
                command.Parameters.AddWithValue("@O3SKU", O3SKU);

                // Open the connection and execute the command
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Process the result set
                if (reader.Read())
                {
                    var record = new STRPRCDto
                    {
                        O3SKU = (decimal)reader["O3SKU"],
                        O3FNAM = reader["O3FNAM"].ToString(),
                        O3MODL = reader["O3MODL"].ToString(),
                        O3DEPT = (decimal)reader["O3DEPT"],
                        O3SDPT = (decimal)reader["O3SDPT"],
                        O3CLAS = (decimal)reader["O3CLAS"],
                        O3SCLS = (decimal)reader["O3SCLS"],
                        O3SCCD = reader["O3SCCD"].ToString(),
                        O3TRB3 = reader["O3TRB3"].ToString(),
                        O3LONG = reader["O3LONG"].ToString()
                    };

                    data = record;
                }

                // Close the reader and connection
                reader.Close();
                connection.Close();
            }

            return data;

        }

        public List<STRPRCLogDto> GetUpdatedData(decimal o3sku = 0)
        {
            var records = new List<STRPRCLogDto>();
            // Set up the connection and command
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_GetUpdatedData", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = commandTimeoutInSeconds;

                command.Parameters.Add("@o3loc", SqlDbType.Decimal).Value = ConfigurationManager.AppSettings["StoreID"];
                if (o3sku > 0)
                    command.Parameters.Add("@o3sku", SqlDbType.Decimal).Value = o3sku;

                // Open the connection and execute the command
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Process the result set
                while (reader.Read())
                {
                    var record = new STRPRCLogDto
                    {
                        O3SKU = (decimal)reader["O3SKU"],
                        IsPrinted = (bool)reader["IsPrinted"] ? "Yes" : "No",
                        O3IDSC = reader["O3IDSC"].ToString(),
                        ColumnName = reader["ColumnName"].ToString(),
                        DateUpdated = (DateTime)reader["DateUpdated"],
                        O3FNAM = reader["O3FNAM"].ToString(),
                        O3DEPT = (decimal)reader["O3DEPT"],
                        DepartmentName = reader["DepartmentName"].ToString(),
                        O3CLAS = (decimal)reader["O3CLAS"],
                        O3SCCD = reader["O3SCCD"].ToString(),
                        O3LONG = reader["O3LONG"].ToString(),
                        O3MODL = reader["O3MODL"].ToString(),
                        O3SDPT = (decimal)reader["O3SDPT"],
                        O3SCLS = (decimal)reader["O3SCLS"],
                        O3TRB3 = reader["O3TRB3"].ToString(),
                        Inv = reader["INV2"].ToString(),
                        TypeName = reader["TypeName"].ToString(),
                        SizeName = reader["SizeName"].ToString(),
                        CategoryName = reader["CategoryName"].ToString()
                       
                    };

                    records.Add(record);
                }

                // Close the reader and connection
                reader.Close();
                connection.Close();
            }

            return records;
        }

        public STRPRCDto GetDataBySKU(decimal O3SKU)
        {
            var data = (from a in _db.STRPRCs
                        where a.O3SKU == O3SKU
                        select new STRPRCDto
                        {
                            O3LOC = a.O3LOC,
                            O3CLAS = a.O3CLAS,
                            O3IDSC = a.O3IDSC,
                            O3SKU = a.O3SKU,
                            O3SCCD = a.O3SCCD,
                            O3UPC = a.O3UPC,
                            O3VNUM = a.O3VNUM,
                            O3TYPE = a.O3TYPE,
                            O3DEPT = a.O3DEPT,
                            O3SDPT = a.O3SDPT,
                            O3SCLS = a.O3SCLS,
                            O3POS = a.O3POS,
                            O3POSU = a.O3POSU,
                            O3REG = a.O3REG,
                            O3REGU = a.O3REGU,
                            O3ORIG = a.O3ORIG,
                            O3ORGU = a.O3ORGU,
                            O3EVT = a.O3EVT,
                            O3PMMX = a.O3PMMX,
                            O3PMTH = a.O3PMTH,
                            O3PDQT = a.O3PDQT,
                            O3PDPR = a.O3PDPR,
                            O3SDT = a.O3SDT,
                            O3EDT = a.O3EDT,
                            O3TRB3 = a.O3TRB3,
                            O3FGR = a.O3FGR,
                            O3FNAM = a.O3FNAM,
                            O3MODL = a.O3MODL,
                            O3LONG = a.O3LONG,
                            O3SLUM = a.O3SLUM,
                            O3DIV = a.O3DIV,
                            O3TUOM = a.O3TUOM,
                            O3DATE = a.O3DATE,
                            O3CURD = a.O3CURD,
                            O3USER = a.O3USER,
                            DateUpdated = a.DateUpdated,
                            TypeId = a.TypeId,
                            SizeId = a.SizeId,
                            CategoryId = a.CategoryId
                        }).FirstOrDefault();

            return data;
        }

        public STRPRCDto GetLatestUpdate()
        {
            var data = (from a in _db.STRPRCs
                        orderby a.DateUpdated descending
                        select new STRPRCDto
                        {
                            DateUpdated = a.DateUpdated,
                            LatestDate = a.O3DATE
                        }).FirstOrDefault();

            return data;
        }

        public bool GetLatestInventory(string storeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetLatestInventory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = commandTimeoutInSeconds;
                        // Add any required parameters to the command if needed
                        command.Parameters.AddWithValue("@Store", storeId);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing stored procedure: " + ex.Message);
                return false;
            }
        }

        public decimal CheckSTRPRCUpdates(int storeId)
        {
            decimal date = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString151))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetLatestUpdate", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = commandTimeoutInSeconds;
                        // Add any required parameters to the command if needed
                        command.Parameters.AddWithValue("@Store", storeId);

                        connection.Open();

                        // Execute the command and retrieve the result count
                        date = (decimal)command.ExecuteScalar();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing stored procedure: " + ex.Message);
            }
            return date;
        }

        public async Task UpdateSTRPRC151(int storeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString151))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetSTRPRC", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = commandTimeoutInSeconds;
                        // Add any required parameters to the command if needed
                        command.Parameters.AddWithValue("@Store", storeId);

                        connection.Open();

                        // Execute the command and retrieve the result count
                        await command.ExecuteNonQueryAsync();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing stored procedure: " + ex.Message);
            }
        }

        public decimal UpdateSTRPRCTable(int storeId)
        {
            decimal date = 0;
            try
            {
                //using (SqlConnection connection = new SqlConnection(connectionString151))
                //{
                //    using (SqlCommand command = new SqlCommand("sp_GetSTRPRC", connection))
                //    {
                //        command.CommandType = CommandType.StoredProcedure;
                //        command.CommandTimeout = commandTimeoutInSeconds;
                //        // Add any required parameters to the command if needed
                //        command.Parameters.AddWithValue("@Store", storeId);

                //        connection.Open();

                //        // Execute the command and retrieve the result count
                //        command.ExecuteNonQuery();
                //        connection.Close();
                //    }
                //}

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetLatestSTRPRCTable", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = commandTimeoutInSeconds;
                        // Add any required parameters to the command if needed
                        command.Parameters.AddWithValue("@O3LOC", storeId);

                        connection.Open();

                        // Execute the command and retrieve the result count
                        date = (decimal)command.ExecuteScalar();

                        connection.Close();

                        // Use the result count as needed
                        //Console.WriteLine("Result Count: " + resultCount);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing stored procedure: " + ex.Message);
            }
            return date;
        }

        public CountryDto GetCountryImg(string country)
        {
            var data = (from a in _db.Countries
                        where a.iatrb3 == country
                        select new CountryDto
                        {
                            iatrb3 = a.iatrb3,
                            country_img = a.country_img
                        }).FirstOrDefault();

            return data;
        }

        public ReportDto GetReportData(decimal O3SKU)
        {
            var data = (from a in _db.STRPRCs
                        join b in _db.Countries on a.O3TRB3 equals b.iatrb3 into ab
                        from c in ab.DefaultIfEmpty()
                        where a.O3SKU == O3SKU
                        select new ReportDto
                        {
                            O3LOC = a.O3LOC,
                            O3CLAS = a.O3CLAS,
                            O3IDSC = a.O3IDSC,
                            O3SKU = a.O3SKU,
                            O3SCCD = a.O3SCCD,
                            O3UPC = a.O3UPC,
                            O3VNUM = a.O3VNUM,
                            O3TYPE = a.O3TYPE,
                            O3DEPT = a.O3DEPT,
                            O3SDPT = a.O3SDPT,
                            O3SCLS = a.O3SCLS,
                            O3POS = a.O3POS,
                            O3POSU = a.O3POSU,
                            O3REG = a.O3REG,
                            O3REGU = a.O3REGU,
                            O3ORIG = a.O3ORIG,
                            O3ORGU = a.O3ORGU,
                            O3EVT = a.O3EVT,
                            O3PMMX = a.O3PMMX,
                            O3PMTH = a.O3PMTH,
                            O3PDQT = a.O3PDQT,
                            O3PDPR = a.O3PDPR,
                            O3SDT = a.O3SDT,
                            O3EDT = a.O3EDT,
                            O3TRB3 = a.O3TRB3,
                            O3FGR = a.O3FGR,
                            O3FNAM = a.O3FNAM,
                            O3MODL = a.O3MODL,
                            O3LONG = a.O3LONG,
                            O3SLUM = a.O3SLUM,
                            O3DIV = a.O3DIV,
                            O3TUOM = a.O3TUOM,
                            O3DATE = a.O3DATE,
                            O3CURD = a.O3CURD,
                            O3USER = a.O3USER,
                            DateUpdated = a.DateUpdated,
                            TypeId = a.TypeId,
                            SizeId = a.SizeId,
                            CategoryId = a.CategoryId,
                            country_img = c.country_img
                        }).FirstOrDefault();

            return data;
        }

        public List<ReportDto> GetReportDataList(List<decimal> O3SKUs)
        {
            var data = (from a in _db.STRPRCs
                        join b in _db.Countries on a.O3TRB3 equals b.iatrb3 into ab
                        from c in ab.DefaultIfEmpty()
                        where O3SKUs.Contains(a.O3SKU)
                        select new ReportDto
                        {
                            O3LOC = a.O3LOC,
                            O3CLAS = a.O3CLAS,
                            O3IDSC = a.O3IDSC,
                            O3SKU = a.O3SKU,
                            O3SCCD = a.O3SCCD,
                            O3UPC = a.O3UPC,
                            O3VNUM = a.O3VNUM,
                            O3TYPE = a.O3TYPE,
                            O3DEPT = a.O3DEPT,
                            O3SDPT = a.O3SDPT,
                            O3SCLS = a.O3SCLS,
                            O3POS = a.O3POS,
                            O3POSU = a.O3POSU,
                            O3REG = a.O3REG,
                            O3REGU = a.O3REGU,
                            O3ORIG = a.O3ORIG,
                            O3ORGU = a.O3ORGU,
                            O3EVT = a.O3EVT,
                            O3PMMX = a.O3PMMX,
                            O3PMTH = a.O3PMTH,
                            O3PDQT = a.O3PDQT,
                            O3PDPR = a.O3PDPR,
                            O3SDT = a.O3SDT,
                            O3EDT = a.O3EDT,
                            O3TRB3 = a.O3TRB3,
                            O3FGR = a.O3FGR,
                            O3FNAM = a.O3FNAM,
                            O3MODL = a.O3MODL,
                            O3LONG = a.O3LONG,
                            O3SLUM = a.O3SLUM,
                            O3DIV = a.O3DIV,
                            O3TUOM = a.O3TUOM,
                            O3DATE = a.O3DATE,
                            O3CURD = a.O3CURD,
                            O3USER = a.O3USER,
                            DateUpdated = a.DateUpdated,
                            TypeId = a.TypeId,
                            SizeId = a.SizeId,
                            CategoryId = a.CategoryId,
                            country_img = c.country_img
                        }).ToList();

            return data;
        }

        public void UpdateSelection(decimal startDate, decimal endDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string storedProcedureName = "sp_UpdateSTRPRCSelection";
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@O3SDT", startDate);
                    command.Parameters.AddWithValue("@O3EDT", endDate);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateMultipleStatus(List<decimal> o3skus)
        {
            foreach (var item in o3skus)
            {
                var data = _db.STRPRCs.Where(a => a.O3SKU == item).FirstOrDefault();
                data.IsPrinted = true;

                var logs = _db.STRPRCLogs.Where(a => a.O3SKU == item).ToList();

                foreach (var log in logs)
                {
                    log.IsPrinted = true;
                }
            }

            _db.SaveChanges();

        }

        public void AddMultipleInventoryPrintingLog(List<decimal> o3skus, string user)
        {
            foreach (var item in o3skus)
            {
                _db.InventoryPrintingLogs.Add(new InventoryPrintingLog()
                {
                    O3SKU = item,
                    PrintedBy = user,
                    DateCreated = DateTime.Now
                });
            }
            _db.SaveChanges();
        }

        public void UpdateSingleStatus(decimal O3SKU)
        {
            var data = _db.STRPRCs.Where(a => a.O3SKU == O3SKU).FirstOrDefault();
            data.IsPrinted = true;

            var logs = _db.STRPRCLogs.Where(a => a.O3SKU == O3SKU).ToList();

            foreach (var log in logs)
            {
                log.IsPrinted = true;
            }

            _db.SaveChanges();
        }

        public void AddInventoryPrintingLog(ReportDto model, string user)
        {

            var data = new InventoryPrintingLog()
            {
                O3SKU = model.O3SKU,
                PrintedBy = user,
                DateCreated = DateTime.Now,
                RegularPrice = model.O3REGU,
                CurrentPrice = model.O3POS,
                Remarks = model.qRemarks,
                ItemDesc = model.O3IDSC,
                Brand = model.O3FNAM,
                Model = model.O3MODL,
                Divisor = model.O3DIV
            };
            _db.InventoryPrintingLogs.Add(data);
            _db.SaveChanges();
        }

        public List<STRPRCDto> GetLatestPCAData()
        {
            var sp = "sp_GetLatestPCAData";
            var data = new List<STRPRCDto>();
            // Set up the connection and command
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sp, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = commandTimeoutInSeconds;

                // Add parameters if required
                //command.Parameters.AddWithValue("@Filter", filter);

                // Open the connection and execute the command
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Process the result set
                while (reader.Read())
                {
                    var record = new STRPRCDto
                    {
                        O3LOC = (decimal)reader["O3LOC"],
                        O3CLAS = (decimal)reader["O3CLAS"],
                        O3IDSC = reader["O3IDSC"].ToString(),
                        O3SKU = (decimal)reader["O3SKU"],
                        O3SCCD = reader["O3SCCD"].ToString(),
                        O3UPC = (decimal)reader["O3UPC"],
                        O3VNUM = (decimal)reader["O3VNUM"],
                        O3TYPE = reader["O3TYPE"].ToString(),
                        O3DEPT = (decimal)reader["O3DEPT"],
                        O3SDPT = (decimal)reader["O3SDPT"],
                        O3SCLS = (decimal)reader["O3SCLS"],
                        O3POS = (decimal)reader["O3POS"],
                        O3POSU = (decimal)reader["O3POSU"],
                        O3REG = (decimal)reader["O3REG"],
                        O3REGU = (decimal)reader["O3REGU"],
                        O3ORIG = (decimal)reader["O3ORIG"],
                        O3ORGU = (decimal)reader["O3ORGU"],
                        O3EVT = (decimal)reader["O3EVT"],
                        O3PMMX = (decimal)reader["O3PMMX"],
                        O3PMTH = (decimal)reader["O3PMTH"],
                        O3PDQT = (decimal)reader["O3PDQT"],
                        O3PDPR = (decimal)reader["O3PDPR"],
                        O3SDT = (decimal)reader["O3SDT"],
                        O3EDT = (decimal)reader["O3EDT"],
                        O3TRB3 = reader["O3TRB3"].ToString(),
                        O3FGR = (decimal)reader["O3FGR"],
                        O3FNAM = reader["O3FNAM"].ToString(),
                        O3MODL = reader["O3MODL"].ToString(),
                        O3LONG = reader["O3LONG"].ToString(),
                        O3SLUM = reader["O3SLUM"].ToString(),
                        O3DIV = reader["O3DIV"].ToString(),
                        O3TUOM = reader["O3TUOM"].ToString(),
                        O3DATE = (decimal)reader["O3DATE"],
                        O3CURD = (decimal)reader["O3CURD"],
                        O3USER = reader["O3USER"].ToString(),
                        DateUpdated = (DateTime)reader["DateUpdated"],
                        TypeId = (int)reader["Type_STRPRC"],
                        SizeId = (int)reader["Size_STRPRC"],
                        CategoryId = (int)reader["Category_STRPRC"],
                        DepartmentName = reader["DPTNAM"].ToString(),
                        IsReverted = reader["O3FLAG1"].ToString(),
                        IsPrinted = reader["IsPrinted_STRPRC"].ToString(),
                        LatestDate = (decimal)reader["LatestDate"],
                        HasInventory = reader["INV2"].ToString(),
                        IsExemp = reader["IsExemp"].ToString()

                    };

                    data.Add(record);
                }

                // Close the reader and connection
                reader.Close();
                connection.Close();
            }

            return data;

        }

        public List<ExportPCAExemptionDto> PCAToExportExemption(string filter, string deptFilter)
        {
            var sp = "sp_GetExemptions";

            var data = new List<ExportPCAExemptionDto>();
            // Set up the connection and command
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sp, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = commandTimeoutInSeconds;

                // Add parameters if required
                //command.Parameters.AddWithValue("@SearchDate", date);

                // Open the connection and execute the command
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Process the result set
                while (reader.Read())
                {
                    var record = new ExportPCAExemptionDto
                    {
                        SKU = (decimal)reader["O3SKU"],
                        UPC = (decimal)reader["O3UPC"],
                        CurrentPrice = (decimal)reader["O3POS"],
                        RegularPrice = (decimal)reader["O3REG"],
                        StartDate = (decimal)reader["O3SDT"],
                        EndDate = (decimal)reader["O3EDT"],
                        //Brand = reader["O3FNAM"].ToString(),
                        //Model = reader["O3MODL"].ToString(),
                        //LongDesc = reader["O3LONG"].ToString(),
                        ItemDesc = reader["O3IDSC"].ToString(),
                        SignType = (int)reader["TypeId"] == 1 ? "Regular" : "Save",
                        //Size = reader["Size"].ToString(),
                        DepartmentName = reader["DPTNAM"].ToString(),
                        ExemptionType = reader["ExempType"].ToString(),
                        ExemptionDate = (decimal)reader["DateExemption"],
                        StoreCodes = reader["StoreIDs"].ToString(),
                    };

                    data.Add(record);
                }

                // Close the reader and connection
                reader.Close();
                connection.Close();
            }

            if (filter == "all")
                data = data.ToList();
            else if (filter == "negativeSave")
                data = data.Where(a => a.ExemptionType.Contains("Negative Save")).ToList();
            else if (filter == "saveZero")
                data = data.Where(a => a.ExemptionType.Contains("Zero Save")).ToList();
            else if (filter == "noCC")
                data = data.Where(a => a.ExemptionType.Contains("No CO Contract")).ToList();

            if (!String.IsNullOrWhiteSpace(deptFilter))
            {
                var deptFilters = deptFilter.Split(new[] { ','}, StringSplitOptions.RemoveEmptyEntries).Select(f => f.Trim().ToUpper()).ToList();
                data = data.Where(a => deptFilters.Any(f => a.DepartmentName.ToUpper().Contains(f))).ToList();
            }

            return data;
        }

        public List<ExportPCADto> PCAToExport()
        {
            var sp = "sp_NewExportPCAData";

            var data = new List<ExportPCADto>();
            // Set up the connection and command
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sp, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = commandTimeoutInSeconds;

                // Add parameters if required
                //command.Parameters.AddWithValue("@SearchDate", date);

                // Open the connection and execute the command
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Process the result set
                while (reader.Read())
                {
                    var record = new ExportPCADto
                    {
                        SKU = (decimal)reader["O3SKU"],
                        UPC = (decimal)reader["O3UPC"],
                        CurrentPrice = (decimal)reader["O3POS"],
                        RegularPrice = (decimal)reader["O3REGU"],
                        StartDate = (decimal)reader["O3SDT"],
                        EndDate = (decimal)reader["O3EDT"],
                        Brand = reader["O3FNAM"].ToString(),
                        Model = reader["O3MODL"].ToString(),
                        LongDesc = reader["O3LONG"].ToString(),
                        ItemDesc = reader["O3IDSC"].ToString(),
                        Type = reader["Type"].ToString(),
                        Size = reader["Size"].ToString(),
                        Category = reader["Category"].ToString(),
                        DepartmentName = reader["DPTNAM"].ToString(),
                        IsPrinted = reader["IsPrintedYN"].ToString(),
                        WithInventory = reader["INVYN"].ToString(),
                        IsExemption = reader["IsExemp"].ToString(),

                    };

                    data.Add(record);
                }

                // Close the reader and connection
                reader.Close();
                connection.Close();
            }

            return data;
        }



        public string GetSubClassDescription(decimal O3SKU)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT TOP 1 O3SDSC FROM STRPRCs WHERE O3SKU = {O3SKU}", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return reader.GetString(0);
                        else
                            return null;
                    }
                }
            }
        }
    }
}