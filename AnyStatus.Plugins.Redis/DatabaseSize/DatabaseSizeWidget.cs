﻿/*
Anystatus Redis plugin
Copyright 2019 Fatih Boy

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 */
using AnyStatus.API;
using AnyStatus.API.Common.Utils;
using AnyStatus.Plugins.Redis.Shared;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace AnyStatus.Plugins.Redis.DatabaseSize
{
    [DisplayName("Database Size")]
    [DisplayColumn("Redis")]
    [Description("Shows the given redis database size")]
    public class DatabaseSizeWidget : Sparkline, IRedisDatabaseConnection, ISchedulable
    {
        [Required]
        [PropertyOrder(10)]
        [Category("Database Size")]
        [Description("Redis endpoint in host:port format")]
        public string EndPoint { get; set; }

        [PropertyOrder(30)]
        [Category("Database Size")]
        [Description("Password for the redis server")]
        public string Password { get; set; }

        [Required]
        [PropertyOrder(20)]
        [Category("Database Size")]
        [Description("Redis database")]
        public int Database { get; set; }

        [Required]
        [PropertyOrder(40)]
        [Category("Database Size")]
        [DisplayName("Connection Timeout")]
        [Description("Timeout (ms) for connect operations")]
        public int ConnectionTimeout { get; set; }

        [Required]
        [PropertyOrder(50)]
        [Category("Blocked Clients")]
        [DisplayName("Connect Retry")]
        [Description("The number of times to repeat connect attempts during initial Connect")]
        public int ConnectRetry { get; set; }

        [Required]
        [PropertyOrder(60)]
        [Category("Database Size")]
        [DisplayName("Enable SSL")]
        [Description("Enable ssl connection")]
        public bool EnableSSL { get; set; }

        public DatabaseSizeWidget()
        {
            Name = "Database Size";
            Database = 0;
            ConnectionTimeout = 60 * 1000;
            EnableSSL = false;
            ConnectRetry = 3;
        }

        public override string ToString()
        {
            return BytesFormatter.Format(Convert.ToInt64(Value));
        }
    }
}
