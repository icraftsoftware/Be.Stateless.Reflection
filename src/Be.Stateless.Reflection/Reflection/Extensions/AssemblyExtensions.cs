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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Be.Stateless.Reflection.Extensions;

[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
[SuppressMessage("ReSharper", "UnusedType.Global", Justification = "Public API.")]
// @formatter:wrap_chained_method_calls chop_if_long
public static class AssemblyExtensions
{
	public static string GetCultureName(this Assembly assembly)
	{
		ArgumentNullException.ThrowIfNull(assembly);
		return assembly.GetName().GetCultureName();
	}

	public static string? GetPublicKeyTokenString(this Assembly assembly)
	{
		ArgumentNullException.ThrowIfNull(assembly);
		return assembly.GetName().GetPublicKeyTokenString();
	}
}
