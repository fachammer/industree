using System;
using System.Linq;

namespace UnityTest
{
	public static class AssertionComponentExt
	{
		public static Type GetFirstArgumentType (this AssertionComponent assertion)
		{
			return assertion.Action.GetParameterType();
		}
	}
}
