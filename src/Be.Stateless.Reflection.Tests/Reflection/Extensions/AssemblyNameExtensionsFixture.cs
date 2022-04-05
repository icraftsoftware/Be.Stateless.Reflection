#region Copyright & License

// Copyright © 2012 - 2022 François Chabot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.Globalization;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Be.Stateless.Reflection.Extensions
{
	public class AssemblyNameExtensionsFixture
	{
		[Fact]
		public void GetCultureNameReturnsCulture()
		{
			new AssemblyName { CultureName = CultureInfo.GetCultureInfo("fr-BE").Name }.GetCultureName().Should().Be("fr-BE");
		}

		[Fact]
		public void GetCultureNameReturnsNeutral()
		{
			Assembly.GetExecutingAssembly().GetName().GetCultureName().Should().Be("neutral");
		}

		[Fact]
		public void GetPublicKeyTokenStringReturnsNull()
		{
			new AssemblyName().GetPublicKeyTokenString().Should().BeNull();
		}

		[Fact]
		public void GetPublicKeyTokenStringReturnsValue()
		{
			Assembly.GetExecutingAssembly().GetName().GetPublicKeyTokenString().Should().Be("3707daa0b119fc14");
		}
	}
}
