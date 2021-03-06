﻿using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Jelly.Database.Configuration;
using Jelly.Helpers;

namespace Jelly.Database
{
    public class DatabaseLog
    {
        private static DatabaseSection section = null;
        private static DatabaseInfoLogSettings InfoLogSettings = null;
        private static DatabaseErrorLogSettings ErrorLogSettings = null;

        static DatabaseLog() 
        {
            section = ConfigurationManager.GetSection("JellyDatabase") as DatabaseSection;

            if (section != null)
            {
                InfoLogSettings = section.InfoLog;
                ErrorLogSettings = section.ErrorLog;
            }
        }

        private static string GetFullLogPath(string path, string pattern)
        {
            if (!string.IsNullOrWhiteSpace(pattern))
            {
                path = Regex.Replace(path, "{pattern}", DateTime.Now.ToString(pattern), RegexOptions.IgnoreCase);
            }

            string fullPath = IOUtils.GetFullPath(path);
            if (!Directory.Exists(fullPath)) 
            {
                Directory.CreateDirectory(IOUtils.GetFullDirectoryName(fullPath));
            }
            
            return fullPath;
        }

        private static string LogCommandString(IDbCommand command) 
        {
            StringBuilder builder = new StringBuilder();

            switch (command.CommandType) 
            {
                case CommandType.Text:
                    builder.AppendLine("SQL: " + command.CommandText);
                    break;
                case CommandType.StoredProcedure:
                    builder.AppendLine("Stored Procedure: " + command.CommandText);
                    break;
                case CommandType.TableDirect:
                    builder.AppendLine("TableDirect: " + command.CommandText);
                    break;
            }

            if (command.Parameters != null && command.Parameters.Count > 0)
            {
                builder.AppendLine("/* Parameters Table */");
                builder.AppendLine(String.Concat(new string[] { 
                                "Name".PadRight(30),
                                "DbType".PadRight(15),
                                "Size".PadRight(10),
                                "IsNullable".PadRight(15),
                                "SourceColumn".PadRight(20),
                                "SourceColumnNullMapping".PadRight(30),
                                "SourceVersion".PadRight(15),
                                "Direction".PadRight(15), 
                                "Value"}));

                foreach (DbParameter parameter in command.Parameters)
                {
                    builder.AppendLine(String.Concat(new string[] { 
                                    parameter.ParameterName.PadRight(30),
                                    parameter.DbType.ToString().PadRight(15),
                                    parameter.Size.ToString().PadRight(10),
                                    parameter.IsNullable.ToString().PadRight(15),
                                    parameter.SourceColumn.PadRight(20),
                                    parameter.SourceColumnNullMapping.ToString().PadRight(30),
                                    parameter.SourceVersion.ToString().PadRight(15),
                                    parameter.Direction.ToString().PadRight(15),
                                    parameter.Value.ToString()}));
                }
            }

            return builder.ToString();
        }

        public static void WriteInfo(DbCommand command) 
        {
            if (InfoLogSettings != null && InfoLogSettings.Enabled) 
            {
                string FullLogPath = GetFullLogPath(InfoLogSettings.LogPath, InfoLogSettings.DateTimePattern);

                using (StreamWriter writer = new StreamWriter(FullLogPath, true))
                {
                    writer.WriteLine("DateTime: " + DateTime.Now.ToString());
                    writer.Write(LogCommandString(command));
                    writer.WriteLine();
                }
            }
        }

        public static void WriteError(string errmsg, DbCommand command) 
        {
            if (ErrorLogSettings != null && ErrorLogSettings.Enabled)
            {
                string FullLogPath = GetFullLogPath(ErrorLogSettings.LogPath, ErrorLogSettings.DateTimePattern);

                using (StreamWriter writer = new StreamWriter(FullLogPath, true))
                {
                    writer.WriteLine("DateTime: " + DateTime.Now.ToString());
                    writer.Write(LogCommandString(command));
                    writer.WriteLine("Error: " + errmsg);
                    writer.WriteLine();
                }
            }
        }
    }
}