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

using FluentAssertions;
using Xunit;

namespace Be.Stateless.Extensions;

public class ArrayExtensionsFixture
{
	// @formatter:wrap_array_initializer_style chop_if_long
	[Theory]
	[InlineData(null, null)]
	[InlineData(new byte[] { }, null)]
	[InlineData(new byte[] { 20, 1, 5, 6, 72, 23 }, "140105064817")]
	public void ToHex(byte[]? actual, string? expected)
	{
		actual.ToHex()
			.Should()
			.Be(expected);
	}
}
