using Elements;
using System;
using System.Linq;
using System.Collections.Generic;
using Elements.Geometry;
using Elements.Geometry.Solids;
using Newtonsoft.Json;
using PerParentOverrides;

namespace Elements
{
    public partial class ChildElement
    {

        [JsonProperty("Add Id")]
        public string AddId { get; set; }

        [JsonProperty("Parent Name")]
        public string? ParentName { get; set; }

        public Guid? ParentElement { get; set; }
        public ChildElement(Vector3 v)
        {
            Transform = new Transform(v);
            Representation = new Extrude(Polygon.Rectangle(1, 1), 1, Vector3.ZAxis, false);
            AddId = $"{v.X:0.00},{v.Y:0.00},{v.Z:0.00}";
        }

        public ChildElement(ChildrenOverrideAddition addition, List<ParentElement> parents)
        {
            Transform = addition.Value.Transform;
            Representation = new Extrude(Polygon.Rectangle(1, 1), 1, Vector3.ZAxis, false);
            var parent = parents.FirstOrDefault(p => p.Name == addition.Value.ParentElement?.Name);
            AddId = addition.Id;
            if (parent != null)
            {
                ParentName = parent.Name;
                ParentElement = parent.Id;
                parent.Children.Add(this);
            }
        }

        public ChildElement Update(ChildrenOverride edit, List<ParentElement> allParents)
        {
            Transform = edit.Value.Transform ?? Transform;
            var parent = allParents.FirstOrDefault(p => p.Name == edit.Value.ParentElement?.Name);
            if (parent != null)
            {
                ParentName = parent.Name;
                ParentElement = parent.Id;
                if (!parent.Children.Contains(this))
                {
                    parent.Children.Add(this);
                }
            }
            return this;
        }
    }
}