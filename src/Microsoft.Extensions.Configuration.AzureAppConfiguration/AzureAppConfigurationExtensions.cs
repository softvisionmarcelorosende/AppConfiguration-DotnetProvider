﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// This type provides helper methods to make common use cases for Azure App Configuration easy.
    /// </summary>
    public static class AzureAppConfigurationExtensions
    {
        /// <summary>
        /// Adds key-value data from an Azure App Configuration store to a configuration builder.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder to add key-values to.</param>
        /// <param name="connectionString">The connection string used to connect to the configuration store.</param>
        /// <param name="optional">If true, this method will not throw an exception if the configuration store cannot be accessed.</param>
        /// <returns>The provided configuration builder.</returns>
        public static IConfigurationBuilder AddAzureAppConfiguration(
            this IConfigurationBuilder configurationBuilder,
            string connectionString,
            bool optional = false)
        {
            return configurationBuilder.AddAzureAppConfiguration(options => options.Connect(connectionString), optional);
        }

        /// <summary>
        /// Adds key-value data from an Azure App Configuration store to a configuration builder.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder to add key-values to.</param>
        /// <param name="action">A callback used to configure Azure App Configuration options.</param>
        /// <param name="optional">If true, this method will not throw an exception if the configuration store cannot be accessed.</param>
        /// <returns>The provided configuration builder.</returns>
        public static IConfigurationBuilder AddAzureAppConfiguration(
            this IConfigurationBuilder configurationBuilder,
            Action<AzureAppConfigurationOptions> action,
            bool optional = false)
        {
            return configurationBuilder.Add(new AzureAppConfigurationSource(action, optional));
        }

        internal static IConfigurationBuilder AddAzureAppConfiguration(
            this IConfigurationBuilder configurationBuilder,
            Action<AzureAppConfigurationOptions> action,
            IConfigurationClientFactory configurationClientFactory)
        {
            return configurationBuilder.Add(new AzureAppConfigurationSource(action, optional: false, configurationClientFactory));
        }
    }
}
