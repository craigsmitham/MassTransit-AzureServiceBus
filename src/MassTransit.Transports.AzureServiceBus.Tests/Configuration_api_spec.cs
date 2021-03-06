﻿// Copyright 2012 Henrik Feldt
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using Magnum.TestFramework;
using MassTransit.Transports.AzureServiceBus.Configuration;
using NUnit.Framework;
using MassTransit.NLogIntegration;
using AccountDetails = MassTransit.Transports.AzureServiceBus.Tests.Framework.AccountDetails;

namespace MassTransit.Transports.AzureServiceBus.Tests
{
	[Scenario, Integration, Explicit("uses faulty data")]
	public class Configuration_api_spec
	{
		[Test]
		public void configuring_the_message_bus()
		{
			using (ServiceBusFactory.New(sbc =>
				{
					sbc.ReceiveFrom(string.Format("azure-sb://owner:{0}@mt-client/my-application", AccountDetails.Key));
					sbc.UseNLog();
					sbc.UseAzureServiceBusRouting();
				}))
			{
			}
		}

		[Test]
		public void alternative()
		{
			using (ServiceBusFactory.New(sbc =>
			{
				sbc.ReceiveFromComponents("owner", AccountDetails.Key, "mt-client", "my-application");
				sbc.UseNLog();
				sbc.UseAzureServiceBusRouting();
			}))
			{
			}
		}
	}
}