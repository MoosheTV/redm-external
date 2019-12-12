﻿using System.Drawing;
using CitizenFX.Core;

namespace RedM.External
{
    public interface IElement
    {

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IElement"/> will be drawn.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the color of this <see cref="IElement"/>.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        Color Color { get; set; }

        /// <summary>
        /// Gets or sets the position of this <see cref="IElement"/>.
        /// </summary>
        /// <value>
        /// The position scaled on a 1280*720 pixel base.
        /// </value>
        /// <remarks>
        /// If ScaledDraw is called, the position will be scaled by the width returned in <see cref="Screen.ScaledWidth"/>.
        /// </remarks>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IElement"/> should be positioned based on its center or top left corner
        /// </summary>
        /// <value>
        ///   <c>true</c> if centered; otherwise, <c>false</c>.
        /// </value>
        bool Centered { get; set; }

        /// <summary>
        /// Draws this <see cref="IElement"/> this frame at the specified offset.
        /// </summary>
        /// <param name="offset">The offset to shift the draw position of this <see cref="IElement"/> using a 1280*720 pixel base.</param>
        void Draw(Vector2 offset);

        /// <summary>
        /// Draws this <see cref="IElement"/> this frame at the specified offset using the width returned in <see cref="Screen.ScaledWidth"/>.
        /// </summary>
        /// <param name="offset">The offset to shift the draw position of this <see cref="IElement"/> using a <see cref="Screen.ScaledWidth"/>*720 pixel base.</param>
        void ScaledDraw(Vector2 offset);
    }
}
