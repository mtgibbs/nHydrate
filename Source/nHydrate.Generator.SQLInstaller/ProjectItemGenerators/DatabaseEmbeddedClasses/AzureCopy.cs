//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 0168
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.IO;

namespace PROJECTNAMESPACE
{
	internal class AzureCopy
	{
		public void Run(InstallSettings settings)
		{
			//STEPS TO COPY DATABASE TO AZURE
			//1. Verify that target is a blank database
			//2. Execute only tables schemas with no defaults, relations, etc
			//3. Copy data with BCP one table at a time
			//4. Run full installer on the target database

			//1. Verify that target is a blank database
			if (!this.TargetIsBlank(settings))
				throw new Exception("The target database must be empty!");

			//2. Execute only tables schemas and PK with no defaults, relations, etc
			Assembly assem = Assembly.GetExecutingAssembly();
			string[] resourceNames = assem.GetManifestResourceNames();
			var resourceName = resourceNames.FirstOrDefault(x => x.EndsWith(".Create_Scripts.Generated.CreateSchema.sql"));
			if (string.IsNullOrEmpty(resourceName)) throw new Exception("Could not find the 'CreateSchema.sql' resource!");

			var scripts = SqlServers.ReadSQLFileSectionsFromResource(resourceName, new InstallSetup());

			SqlConnection connection = null;
			try
			{
				connection = new SqlConnection(settings.GetCloudConnectionString());
				connection.Open();

				////Create version table
				//var sb = new StringBuilder();
				//sb.AppendLine("if not exists(select * from sysobjects where name = '__nhydrateschema' and xtype = 'U')");
				//sb.AppendLine("BEGIN");
				//sb.AppendLine("CREATE TABLE [__nhydrateschema] (");
				//sb.AppendLine("[dbVersion] [varchar] (50) NOT NULL,");
				//sb.AppendLine("[LastUpdate] [datetime] NOT NULL,");
				//sb.AppendLine("[ModelKey] [uniqueidentifier] NOT NULL,");
				//sb.AppendLine("[History] [text] NOT NULL");
				//sb.AppendLine(")");
				//sb.AppendLine("--PRIMARY KEY FOR TABLE");
				//sb.AppendLine("if not exists(select * from sysobjects where name = '__pk__nhydrateschema' and xtype = 'PK')");
				//sb.AppendLine("ALTER TABLE [__nhydrateschema] WITH NOCHECK ADD CONSTRAINT [__pk__nhydrateschema] PRIMARY KEY CLUSTERED ([ModelKey])");
				//sb.AppendLine("END");
				//var command2 = new SqlCommand(sb.ToString(), connection);
				//command2.ExecuteNonQuery();

				foreach (string sql in scripts)
				{
					if (
						sql.Contains("--CREATE TABLE") ||
						sql.Contains("--CREATE AUDIT TABLE") ||
						sql.StartsWith("--APPEND AUDIT") ||
						sql.StartsWith("--PRIMARY KEY FOR TABLE"))
					{
						var command = new SqlCommand(sql, connection);
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (connection != null)
					connection.Close();
			}

			//3. Copy data with BCP one table at a time
			this.CopyData(settings);

			//4. Run full installer on the target database
			var setup = new InstallSetup()
			{
				ConnectionString = settings.GetCloudConnectionString(),
				InstallStatus = InstallStatusConstants.Upgrade,
			};
			UpgradeInstaller.UpgradeDatabase(setup);

		}

		private void CopyData(InstallSettings settings)
		{
			//Get source tables
			SqlConnection connection = null;
			var sourceTables = new Dictionary<string, string>();
			try
			{
				connection = new SqlConnection(settings.GetPrimaryConnectionString());
				connection.Open();
				var adpater = new SqlDataAdapter("select T.name as tablename, S.name as schemaname from sys.tables T inner join sys.schemas S on T.schema_id = S.schema_id where T.name <> 'dtproperties' AND T.name <> 'sysdiagrams' order by tablename", connection);
				DataSet ds = new DataSet();
				adpater.Fill(ds);
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					sourceTables.Add((string)row["tablename"], (string)row["schemaname"]);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (connection != null)
					connection.Close();
			}

			var tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
			Directory.CreateDirectory(tempFolder);

			//Create format file
			foreach (string tableName in sourceTables.Keys)
			{
				//Get the format FROM THE SERVER!!!
				string text = string.Empty;
				text = settings.CloudDatabase + "." + sourceTables[tableName] + "." + tableName + " format nul -f \"" + Path.Combine(tempFolder, tableName + ".xml") + "\" -x -n -q -S " + settings.CloudServer + " -U " + settings.CloudUserName + " -P " + settings.CloudPassword;

				System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("bcp", text);
				procStartInfo.RedirectStandardOutput = true;
				procStartInfo.UseShellExecute = false;
				procStartInfo.CreateNoWindow = true;

				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.StartInfo = procStartInfo;
				proc.Start();
				string result = proc.StandardOutput.ReadToEnd();

				if (result.Contains("Error ="))
					throw new Exception("There was an error while creating the format file for table '" + tableName + "'.", new Exception(result));
			}

			//Create data file
			foreach (string tableName in sourceTables.Keys)
			{
				var columnList = this.GetColumnList(settings.GetCloudConnectionString(), tableName);
				string text = "\"select " + columnList + " from " + settings.PrimaryDatabase + "." + sourceTables[tableName] + "." + tableName + "\" queryout \"" + Path.Combine(tempFolder, tableName + ".dat") + "\" -n -q ";

				if (settings.PrimaryUseIntegratedSecurity)
					text += "-T";
				else
					text += "-S " + settings.PrimaryServer + " -U " + settings.PrimaryUserName + " -P " + settings.PrimarySecurityPhrase;

				System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("bcp", text);
				procStartInfo.RedirectStandardOutput = true;
				procStartInfo.UseShellExecute = false;
				procStartInfo.CreateNoWindow = true;

				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.StartInfo = procStartInfo;
				proc.Start();
				string result = proc.StandardOutput.ReadToEnd();

				if (result.Contains("Error ="))
					throw new Exception("There was an error while creating the data file for table '" + tableName + "'.", new Exception(result));

			}

			//Move data
			foreach (string tableName in sourceTables.Keys)
			{
				string text = string.Empty;
				text = settings.CloudDatabase + "." + sourceTables[tableName] + "." + tableName + " in \"" + Path.Combine(tempFolder, tableName + ".dat") + "\" -n -q -E -S " + settings.CloudServer + " -U " + settings.CloudUserName + " -P " + settings.CloudPassword; // +" -f " + Path.Combine(tempFolder, tableName + ".xml");

				System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("bcp", text);
				procStartInfo.RedirectStandardOutput = true;
				procStartInfo.UseShellExecute = false;
				procStartInfo.CreateNoWindow = true;

				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.StartInfo = procStartInfo;
				proc.Start();
				string result = proc.StandardOutput.ReadToEnd();

				if (result.Contains("Error ="))
					throw new Exception("There was an error while moving data for table '" + tableName + "'.", new Exception(result));
			}

			//Remove temp folder
			Directory.Delete(tempFolder, true);

		}

		private bool TargetIsBlank(InstallSettings settings)
		{
			var connection = new SqlConnection(settings.GetCloudConnectionString());
			var adapter = new SqlDataAdapter("select * from sys.tables", connection);
			var ds = new DataSet();
			adapter.Fill(ds);
			return (ds.Tables[0].Rows.Count == 0);
		}

		private string GetColumnList(string connectionString, string tableName)
		{
			SqlConnection connection = null;
			try
			{
				connection = new SqlConnection(connectionString);
				connection.Open();
				var adpater = new SqlDataAdapter("select C.name as columnname from sys.columns C inner join sys.objects O ON C.object_id = O.object_id where O.name = '" + tableName + "' order by C.column_id", connection);
				DataSet ds = new DataSet();
				adpater.Fill(ds);

				var l = new List<string>();
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					l.Add((string)row[0]);
				}
				return string.Join(", ", l.ToArray());

			}
			catch (Exception ex)
			{
			}
			finally
			{
				if (connection != null)
					connection.Close();
			}
			return string.Empty;

		}

	}
}
#pragma warning restore 0168