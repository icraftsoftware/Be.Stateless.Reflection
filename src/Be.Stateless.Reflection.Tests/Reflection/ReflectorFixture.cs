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
using Be.Stateless.Dummies.Reflection;
using FluentAssertions;
using Xunit;
using static FluentAssertions.FluentActions;

namespace Be.Stateless.Reflection;

public static class ReflectorFixture
{
	#region Nested Type: FieldReflectionFixture

	[Collection("Sequential")]
	public class FieldReflectionFixture
	{
		[Fact]
		public void GetInstanceField()
		{
			Reflector.TryGetField(ReflectedDummy.Instance, "_field", out _)
				.Should()
				.BeTrue();
			Reflector.GetField(ReflectedDummy.Instance, "_field")
				.Should()
				.Be(ReflectedDummy.Instance.FieldSpy);

			Reflector.TryGetField(ReflectedDummy.Instance, "_missingField", out _)
				.Should()
				.BeFalse();
			Invoking(static () => Reflector.GetField(ReflectedDummy.Instance, "_missingField"))
				.Should()
				.Throw<InvalidOperationException>();
		}

		[Fact]
		public void GetStaticFieldThroughGeneric()
		{
			Reflector.TryGetField<ReflectedDummy>("_staticField", out _)
				.Should()
				.BeTrue();
			Reflector.GetField<ReflectedDummy>("_staticField")
				.Should()
				.Be(ReflectedDummy.StaticFieldSpy);

			Reflector.TryGetField<ReflectedDummy>("_missingStaticField", out _)
				.Should()
				.BeFalse();
			Invoking(static () => Reflector.GetField<ReflectedDummy>("_missingStaticField"))
				.Should()
				.Throw<InvalidOperationException>();
		}

		[Fact]
		[SuppressMessage("Usage", "CA2263:Prefer generic overload when type is known")]
		public void GetStaticFieldThroughType()
		{
			Reflector.TryGetField(typeof(ReflectedDummy), "_staticField", out _)
				.Should()
				.BeTrue();
			Reflector.GetField(typeof(ReflectedDummy), "_staticField")
				.Should()
				.Be(ReflectedDummy.StaticFieldSpy);

			Reflector.TryGetField(typeof(ReflectedDummy), "_missingStaticField", out _)
				.Should()
				.BeFalse();
			Invoking(static () => Reflector.GetField(typeof(ReflectedDummy), "_missingStaticField"))
				.Should()
				.Throw<InvalidOperationException>();
		}

		[Fact]
		public void SetInstanceField()
		{
			Reflector.SetField(ReflectedDummy.Instance, "_field", nameof(SetInstanceField));
			ReflectedDummy.Instance.FieldSpy.Should()
				.Be(nameof(SetInstanceField));
		}

		[Fact]
		public void SetStaticFieldThroughGeneric()
		{
			Reflector.SetField<ReflectedDummy>("_staticField", nameof(SetStaticFieldThroughGeneric));
			ReflectedDummy.StaticFieldSpy.Should()
				.Be(nameof(SetStaticFieldThroughGeneric));
		}

		[Fact]
		[SuppressMessage("Usage", "CA2263:Prefer generic overload when type is known")]
		public void SetStaticFieldThroughType()
		{
			Reflector.SetField(typeof(ReflectedDummy), "_staticField", nameof(SetStaticFieldThroughType));
			ReflectedDummy.StaticFieldSpy.Should()
				.Be(nameof(SetStaticFieldThroughType));
		}
	}

	#endregion

	#region Nested Type: MethodReflectionFixture

	[Collection("Sequential")]
	public class MethodReflectionFixture
	{
		[Fact]
		public void InvokeInstanceMethod()
		{
			Reflector.TryInvokeMethod(ReflectedDummy.Instance, "Method", [nameof(InvokeInstanceMethod)], out _)
				.Should()
				.BeTrue();
			Reflector.InvokeMethod(ReflectedDummy.Instance, "Method", nameof(InvokeInstanceMethod))
				.Should()
				.BeNull();
			ReflectedDummy.Instance.FieldSpy.Should()
				.Be(nameof(InvokeInstanceMethod));

			Reflector.TryInvokeMethod(ReflectedDummy.Instance, "MissingMethod", [nameof(InvokeInstanceMethod)], out _)
				.Should()
				.BeFalse();
			Invoking(static () => Reflector.InvokeMethod(ReflectedDummy.Instance, "MissingMethod", nameof(InvokeInstanceMethod)))
				.Should()
				.Throw<InvalidOperationException>();
		}

		[Fact]
		public void InvokeStaticMethodThroughGeneric()
		{
			Reflector.TryInvokeMethod<ReflectedDummy>("StaticMethod", [nameof(InvokeStaticMethodThroughGeneric)], out _)
				.Should()
				.BeTrue();
			Reflector.InvokeMethod<ReflectedDummy>("StaticMethod", nameof(InvokeStaticMethodThroughGeneric))
				.Should()
				.BeNull();
			ReflectedDummy.StaticFieldSpy.Should()
				.Be(nameof(InvokeStaticMethodThroughGeneric));

			Reflector.TryInvokeMethod<ReflectedDummy>("MissingStaticMethod", [nameof(InvokeStaticMethodThroughGeneric)], out _)
				.Should()
				.BeFalse();
			Invoking(static () => Reflector.InvokeMethod<ReflectedDummy>("MissingStaticMethod", nameof(InvokeStaticMethodThroughGeneric)))
				.Should()
				.Throw<InvalidOperationException>();
		}

		[Fact]
		[SuppressMessage("Usage", "CA2263:Prefer generic overload when type is known")]
		public void InvokeStaticMethodThroughType()
		{
			Reflector.TryInvokeMethod(typeof(ReflectedDummy), "StaticMethod", [nameof(InvokeStaticMethodThroughType)], out _)
				.Should()
				.BeTrue();
			Reflector.InvokeMethod(typeof(ReflectedDummy), "StaticMethod", nameof(InvokeStaticMethodThroughType))
				.Should()
				.BeNull();
			ReflectedDummy.StaticFieldSpy.Should()
				.Be(nameof(InvokeStaticMethodThroughType));

			Reflector.TryInvokeMethod(typeof(ReflectedDummy), "MissingStaticMethod", [nameof(InvokeStaticMethodThroughType)], out _)
				.Should()
				.BeFalse();
			Invoking(static () => Reflector.InvokeMethod(typeof(ReflectedDummy), "MissingStaticMethod", nameof(InvokeStaticMethodThroughType)))
				.Should()
				.Throw<InvalidOperationException>();
		}
	}

	#endregion

	#region Nested Type: PropertyReflectionFixture

	[Collection("Sequential")]
	public class PropertyReflectionFixture
	{
		[Fact]
		[SuppressMessage("Usage", "CA2263:Prefer generic overload when type is known")]
		public void GetGenericStaticPropertyThruDerivedType()
		{
			Reflector.TryGetProperty(typeof(ReflectedDerivedGenericDummy), "Instance", out _)
				.Should()
				.BeTrue();
			Reflector.GetProperty(typeof(ReflectedDerivedGenericDummy), "Instance")
				.Should()
				.NotBeNull();

			Reflector.TryGetProperty(typeof(ReflectedDerivedGenericDummy), "Missing", out _)
				.Should()
				.BeFalse();
			Invoking(static () => Reflector.GetProperty(typeof(ReflectedDerivedGenericDummy), "Missing"))
				.Should()
				.Throw<InvalidOperationException>();
		}

		[Fact]
		public void GetInstanceProperty()
		{
			Reflector.TryGetProperty(ReflectedDummy.Instance, "Property", out _)
				.Should()
				.BeTrue();
			Reflector.GetProperty(ReflectedDummy.Instance, "Property")
				.Should()
				.Be(ReflectedDummy.Instance.PropertySpy);

			Reflector.TryGetProperty(ReflectedDummy.Instance, "Missing", out _)
				.Should()
				.BeFalse();
			Invoking(static () => Reflector.GetProperty(ReflectedDummy.Instance, "Missing"))
				.Should()
				.Throw<InvalidOperationException>();
		}

		[Fact]
		public void GetStaticPropertyThroughGeneric()
		{
			Reflector.TryGetProperty<ReflectedDummy>("StaticProperty", out _)
				.Should()
				.BeTrue();
			Reflector.GetProperty<ReflectedDummy>("StaticProperty")
				.Should()
				.Be(ReflectedDummy.StaticPropertySpy);

			Reflector.TryGetProperty<ReflectedDummy>("MissingStaticProperty", out _)
				.Should()
				.BeFalse();
			Invoking(static () => Reflector.GetProperty<ReflectedDummy>("MissingStaticProperty"))
				.Should()
				.Throw<InvalidOperationException>();
		}

		[Fact]
		[SuppressMessage("Usage", "CA2263:Prefer generic overload when type is known")]
		public void GetStaticPropertyThroughType()
		{
			Reflector.TryGetProperty(typeof(ReflectedDummy), "StaticProperty", out _)
				.Should()
				.BeTrue();
			Reflector.GetProperty(typeof(ReflectedDummy), "StaticProperty")
				.Should()
				.Be(ReflectedDummy.StaticPropertySpy);

			Reflector.TryGetProperty(typeof(ReflectedDummy), "MissingStaticProperty", out _)
				.Should()
				.BeFalse();
			Invoking(static () => Reflector.GetProperty(typeof(ReflectedDummy), "MissingStaticProperty"))
				.Should()
				.Throw<InvalidOperationException>();
		}

		[Fact]
		public void SetInstanceProperty()
		{
			Reflector.SetProperty(ReflectedDummy.Instance, "Property", nameof(SetInstanceProperty));
			ReflectedDummy.Instance.PropertySpy.Should()
				.Be(nameof(SetInstanceProperty));
		}

		[Fact]
		public void SetStaticPropertyThroughGeneric()
		{
			Reflector.SetProperty<ReflectedDummy>("StaticProperty", nameof(SetStaticPropertyThroughGeneric));
			ReflectedDummy.StaticPropertySpy.Should()
				.Be(nameof(SetStaticPropertyThroughGeneric));
		}

		[Fact]
		[SuppressMessage("Usage", "CA2263:Prefer generic overload when type is known")]
		public void SetStaticPropertyThroughType()
		{
			Reflector.SetProperty(typeof(ReflectedDummy), "StaticProperty", nameof(SetStaticPropertyThroughType));
			ReflectedDummy.StaticPropertySpy.Should()
				.Be(nameof(SetStaticPropertyThroughType));
		}
	}

	#endregion
}
