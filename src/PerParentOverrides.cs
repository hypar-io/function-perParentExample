using Elements;
using Elements.Geometry;
using Elements.Geometry.Solids;
using System.Collections.Generic;

namespace PerParentOverrides
{
    public static class PerParentOverrides
    {
        /// <summary>
        /// The PerParentOverrides function.
        /// </summary>
        /// <param name="model">The input model.</param>
        /// <param name="input">The arguments to the execution.</param>
        /// <returns>A PerParentOverridesOutputs instance containing computed results and the model with any new elements.</returns>
        public static PerParentOverridesOutputs Execute(Dictionary<string, Model> inputModels, PerParentOverridesInputs input)
        {
            var output = new PerParentOverridesOutputs();

            var childrenA = new List<ChildElement> {
                new ChildElement((0, 0, 0)),
                new ChildElement((2, 0, 0)),
                new ChildElement((4, 0, 0))
            };
            var parentA = new ParentElement
            {
                Children = childrenA,
                Name = "Parent A",
                Color = Colors.Red
            };


            var childrenB = new List<ChildElement> {
                new ChildElement((0, 2, 0)),
                new ChildElement((2, 2, 0)),
                new ChildElement((4, 2, 0))
            };
            var parentB = new ParentElement
            {
                Children = childrenB,
                Name = "Parent B",
                Color = Colors.Blue
            };

            var allChildren = new List<ChildElement>();
            allChildren.AddRange(parentA.Children);
            allChildren.AddRange(parentB.Children);

            var allParents = new List<ParentElement> {
                parentA,
                parentB
            };

            allChildren = input.Overrides.Children.CreateElements(
              input.Overrides.Additions.Children,
              input.Overrides.Removals.Children,
              (add) => new ChildElement(add, allParents),
              (child, ident) => child.AddId == ident.AddId,
              (child, edit) => child.Update(edit, allParents),
              allChildren);

            foreach (var p in allParents)
            {
                var col = p.Color;
                var mat = new Material
                {
                    Name = p.Name,
                    Color = p.Color
                };
                foreach (var c in p.Children)
                {
                    c.Material = mat;
                }
                if (input.GeometricParent)
                {
                    var pts = p.Children.Select(c => c.Transform.Origin).ToList();
                    var averagePt = pts.Average();
                    var lines = pts.Where(p => p.DistanceTo(averagePt) > 0.01).Select(pt => new Line(averagePt, pt));
                    var offset = lines.ToList().Offset(2);
                    var laminae = offset.Select(pgon => new Lamina(pgon));
                    p.Material = mat;
                    p.Representation = new Representation(laminae.OfType<SolidOperation>().ToList());
                }
            }

            output.Model.AddElements(parentA, parentB);

            output.Model.AddElements(allChildren);

            return output;
        }
    }
}