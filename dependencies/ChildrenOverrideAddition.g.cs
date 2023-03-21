using Elements;
using System.Collections.Generic;
using System;
using System.Linq;

namespace PerParentOverrides
{
	/// <summary>
	/// Override metadata for ChildrenOverrideAddition
	/// </summary>
	public partial class ChildrenOverrideAddition : IOverride
	{
        public static string Name = "Children Addition";
        public static string Dependency = null;
        public static string Context = "[*discriminator=Elements.ChildElement]";
		public static string Paradigm = "Edit";

        /// <summary>
        /// Get the override name for this override.
        /// </summary>
        public string GetName() {
			return Name;
		}

		public object GetIdentity() {

			return Identity;
		}

	}

}