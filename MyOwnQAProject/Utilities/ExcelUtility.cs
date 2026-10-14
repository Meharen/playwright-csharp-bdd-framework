using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ExcelDataReader;

namespace Enterprise.Automation.Framework.Utilities
{
    public class ExcelUtility
    {
        private readonly string _filePath;

        public ExcelUtility(string filePath)
        {
            _filePath = filePath;

            // Register encoding provider for ExcelDataReader (.NET standard requirement)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public List<T> ReadAsList<T>(string sheetName) where T : new()
        {
            var list = new List<T>();

            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException($"Excel file not found at path: {_filePath}");
            }

            using (var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    DataTable? table = result.Tables[sheetName];

                    if (table == null)
                    {
                        throw new Exception($"Worksheet '{sheetName}' was not found in the Excel file at path: {_filePath}");
                    }

                    var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    foreach (DataRow row in table.Rows)
                    {
                        // Skip completely blank rows in excel
                        if (row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field?.ToString())))
                        {
                            continue;
                        }

                        var item = new T();
                        bool hasAnyValue = false;

                        foreach (var prop in properties)
                        {
                            // Case-insensitive column matching
                            DataColumn? column = table.Columns
                                .Cast<DataColumn>()
                                .FirstOrDefault(c => c.ColumnName.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));

                            if (column != null && row[column] != DBNull.Value)
                            {
                                var cellValue = row[column];
                                var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                                try
                                {
                                    object? convertedValue = Convert.ChangeType(cellValue, targetType!);
                                    prop.SetValue(item, convertedValue);
                                    hasAnyValue = true;
                                }
                                catch (Exception ex)
                                {
                                    throw new InvalidCastException(
                                        $"Failed to map column '{column.ColumnName}' with value '{cellValue}' to property '{prop.Name}' of type '{prop.PropertyType.Name}'. Inner Exception: {ex.Message}");
                                }
                            }
                        }

                        if (hasAnyValue)
                        {
                            list.Add(item);
                        }
                    }
                }
            }

            return list;
        }
    }
}