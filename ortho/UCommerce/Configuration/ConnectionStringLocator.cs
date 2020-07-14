﻿using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;

namespace UCommerce.Sitefinity.Configuration
{
    public class ConnectionStringLocator : UCommerce.Infrastructure.Configuration.ConnectionStringLocator
    {
        public override string LocateConnectionString()
        {
            string connectionString = LocateConnectionStringInternal("Sitefinity");
            if (!string.IsNullOrWhiteSpace(connectionString))
                return connectionString;

            connectionString = TryGetNamedConnectionStringFromDataConfig("uCommerce");
            if (!string.IsNullOrWhiteSpace(connectionString))
                return connectionString;

            connectionString = TryGetNamedConnectionStringFromDataConfig("Sitefinity");
            if (!string.IsNullOrWhiteSpace(connectionString))
                return connectionString;

            throw new NotSupportedException("Could not find a suitable connection string in either dataconfig configuration file nor a connection string named either: 'uCommerce' or 'sitefinity' in web.config connectionstrings element.");
        }

        protected virtual string TryGetNamedConnectionStringFromDataConfig(string connectionStringName)
        {
            ConfigElementDictionary<string, ConnStringSettings> connectionStrings = Config.Get<DataConfig>().ConnectionStrings;

            if (connectionStrings.ContainsKey(connectionStringName))
            {
                return connectionStrings[connectionStringName].ConnectionString;
            }

            return string.Empty;
        }
    }
}
