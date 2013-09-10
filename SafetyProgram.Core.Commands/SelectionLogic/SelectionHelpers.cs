using System;
using System.Collections.Generic;
using System.Linq;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.KernelCommands
{
    public static class SelectionHelpers
    {
        /// <summary>
        /// Get the parent element of the coordinate. Returns null if the coordinate has no parent.
        /// </summary>
        /// <param name="selectionCoordinate">The coordinate to get the parent coordinates of.</param>
        /// <returns>The coordinates of the parent.</returns>
        public static IList<int> ParentCoordinates(this IList<int> selectionCoordinate)
        {
            return 
                selectionCoordinate.Count > 1 ?
                    selectionCoordinate
                    .Take(selectionCoordinate.Count - 1)
                    .ToList()
                :
                    null;
        }

        /// <summary>
        /// Get the model the selection coordinates refer to.
        /// </summary>
        /// <param name="selectionCoordinate">The coordinates of the selection.</param>
        /// <param name="model">The hierarchy to naviagate with the coordinates.</param>
        /// <returns>The model located at the coordinates.</returns>
        public static IApplicationModel GetSelection(IList<int> selectionCoordinate, IApplicationModel model)
        {
            if (selectionCoordinate.Count == 0)
            {
                return model;
            }
            else
            {
                if (model is IHasMany)
                {
                    var foundModel = ((IHasMany)model)
                        .Content
                        .ElementAt(
                            selectionCoordinate
                                .First());

                    // Recurse down the selection tree, heading toward the leaf of the selection.
                    return GetSelection(
                        selectionCoordinate
                            .Skip(1)
                            .ToArray(),
                            foundModel);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
