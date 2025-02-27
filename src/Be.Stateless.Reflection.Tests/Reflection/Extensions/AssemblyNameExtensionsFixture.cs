#region Copyright & License

// Copyright © 2012 - 2025 François Chabot
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

namespace Be.Stateless.Reflection.Extensions;

public class AssemblyNameExtensionsFixture
{
	[Fact]
	public void GetCultureNameReturnsCulture()
	{
		// @formatter:wrap_object_and_collection_initializer_style chop_if_long
		// @formatter:wrap_chained_method_calls chop_if_long
		new AssemblyName { CultureName = CultureInfo.GetCultureInfo("fr-BE").Name }.GetCultureName()
			// @formatter:wrap_chained_method_calls restore
			.Should()
			.Be("fr-BE");
		// @formatter:wrap_object_and_collection_initializer_style restore
	}

	[Fact]
	public void GetCultureNameReturnsNeutral()
	{
		Assembly.GetExecutingAssembly()
			.GetName()
			.GetCultureName()
			.Should()
			.Be("neutral");
	}

	[Fact]
	public void GetPublicKeyTokenStringReturnsNull()
	{
		new AssemblyName().GetPublicKeyTokenString()
			.Should()
			.BeNull();
	}

	[Fact]
	public void GetPublicKeyTokenStringReturnsValue()
	{
		Assembly.GetExecutingAssembly()
			.GetName()
			.GetPublicKeyTokenString()
			.Should()
			.Be("3707daa0b119fc14");
	}
}
