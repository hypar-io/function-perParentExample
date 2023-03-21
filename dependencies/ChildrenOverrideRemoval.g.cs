using Elements;
using System.Collections.Generic;
using System;
using System.Linq;

namespace PerParentOverrides
{
	/// <summary>
	/// Override metadata for ChildrenOverrideRemoval
	/// </summary>
	public partial class ChildrenOverrideRemoval : IOverride
	{
        public static string Name = "Children Removal";
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