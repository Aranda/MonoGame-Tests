#region License
/*
Microsoft Public License (Ms-PL)
MonoGame - Copyright © 2009-2012 The MonoGame Team

All rights reserved.

This license governs use of the accompanying software. If you use the software,
you accept this license. If you do not accept the license, do not use the
software.

1. Definitions

The terms "reproduce," "reproduction," "derivative works," and "distribution"
have the same meaning here as under U.S. copyright law.

A "contribution" is the original software, or any additions or changes to the
software.

A "contributor" is any person that distributes its contribution under this
license.

"Licensed patents" are a contributor's patent claims that read directly on its
contribution.

2. Grant of Rights

(A) Copyright Grant- Subject to the terms of this license, including the
license conditions and limitations in section 3, each contributor grants you a
non-exclusive, worldwide, royalty-free copyright license to reproduce its
contribution, prepare derivative works of its contribution, and distribute its
contribution or any derivative works that you create.

(B) Patent Grant- Subject to the terms of this license, including the license
conditions and limitations in section 3, each contributor grants you a
non-exclusive, worldwide, royalty-free license under its licensed patents to
make, have made, use, sell, offer for sale, import, and/or otherwise dispose of
its contribution in the software or derivative works of the contribution in the
software.

3. Conditions and Limitations

(A) No Trademark License- This license does not grant you rights to use any
contributors' name, logo, or trademarks.

(B) If you bring a patent claim against any contributor over patents that you
claim are infringed by the software, your patent license from such contributor
to the software ends automatically.

(C) If you distribute any portion of the software, you must retain all
copyright, patent, trademark, and attribution notices that are present in the
software.

(D) If you distribute any portion of the software in source code form, you may
do so only under this license by including a complete copy of this license with
your distribution. If you distribute any portion of the software in compiled or
object code form, you may only do so under a license that complies with this
license.

(E) The software is licensed "as-is." You bear the risk of using it. The
contributors give no express warranties, guarantees or conditions. You may have
additional consumer rights under your local laws which this license cannot
change. To the extent permitted under your local laws, the contributors exclude
the implied warranties of merchantability, fitness for a particular purpose and
non-infringement.
*/
#endregion License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NUnit.Framework;

using MonoGame.Tests.Components;

namespace MonoGame.Tests.Visual {
	[TestFixture]
	class FontVisualTest : VisualTestFixtureBase {

		[Test, RequiresSTA]
		public void DrawString_plain ()
		{
			DrawStringTest("plain", "Default", (frameInfo, font, spriteBatch) => {
				spriteBatch.Begin ();
				spriteBatch.DrawString (font, "plain old text", new Vector2 (50, 50), Color.Violet);
				spriteBatch.End ();
			});
		}

		[Test, RequiresSTA]
		public void DrawString_rotated ()
		{
			DrawStringTest ("rotated", "Default", (frameInfo, font, spriteBatch) => {
				spriteBatch.Begin ();
				spriteBatch.DrawString (
					font, "rotated", new Vector2 (50, 50), Color.Orange, MathHelper.PiOver4,
					Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
				spriteBatch.End ();
			});
		}

		[Test, RequiresSTA]
		public void DrawString_scaled ()
		{
			DrawStringTest ("scaled", "Default", (frameInfo, font, spriteBatch) => {
				spriteBatch.Begin ();
				spriteBatch.DrawString (
					font, "scaled", new Vector2 (50, 50), Color.Orange, 0,
					Vector2.Zero, new Vector2(3.0f, 1.5f), SpriteEffects.None, 0.0f);
				spriteBatch.End ();
			});
		}

		[Test, RequiresSTA]
		public void DrawString_flip_horizontal ()
		{
			DrawStringTest ("flip_horizontal", "Default", (frameInfo, font, spriteBatch) => {
				spriteBatch.Begin ();
				spriteBatch.DrawString (
					font, "flipped horizontally", new Vector2 (50, 50), Color.Orange, 0,
					Vector2.Zero, Vector2.One, SpriteEffects.FlipHorizontally, 0.0f);
				spriteBatch.End ();
			});
		}

		[Test, RequiresSTA]
		public void DrawString_flip_vertical ()
		{
			DrawStringTest ("flip_vertical", "Default", (frameInfo, font, spriteBatch) => {
				spriteBatch.Begin ();
				spriteBatch.DrawString (
					font, "flipped vertically", new Vector2 (50, 50), Color.Orange, 0,
					Vector2.Zero, Vector2.One, SpriteEffects.FlipVertically, 0.0f);
				spriteBatch.End ();
			});
		}

		[Test, RequiresSTA]
		public void DrawString_flip_both ()
		{
			DrawStringTest ("flip_both", "Default", (frameInfo, font, spriteBatch) => {
				spriteBatch.Begin ();
				spriteBatch.DrawString (
					font, "flipped both", new Vector2 (50, 50), Color.Orange, 0,
					Vector2.Zero, Vector2.One,
					SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0.0f);
				spriteBatch.End ();
			});
		}

		[Test, RequiresSTA]
		public void DrawString_origins ()
		{
			DrawStringTest ("origins", "Default", (frameInfo, font, spriteBatch) => {
				spriteBatch.Begin ();

				var position = new Vector2 (100, 100);
				var text = "origin";

				spriteBatch.DrawString (
					font, text, position, Color.Orange, MathHelper.PiOver4,
					new Vector2(0f, 0f), 1.0f, SpriteEffects.None, 0.0f);

				spriteBatch.DrawString (
					font, text, position, Color.Blue, MathHelper.PiOver4,
					new Vector2(40f, 0f), 1.0f, SpriteEffects.None, 0.0f);

				spriteBatch.DrawString (
					font, text, position, Color.HotPink, MathHelper.PiOver4,
					new Vector2(0f, 40f), 1.0f, SpriteEffects.None, 0.0f);

				spriteBatch.DrawString (
					font, text, position, Color.Violet, MathHelper.PiOver4,
					new Vector2(40f, 40f), 1.0f, SpriteEffects.None, 0.0f);

				spriteBatch.End ();
			});
		}

		[Test, RequiresSTA]
		public void DrawString_origins_scaled ()
		{
			DrawStringTest ("origins_scaled", "Default", (frameInfo, font, spriteBatch) => {
				spriteBatch.Begin ();

				var position = new Vector2 (100, 100);
				var text = "origin";

				spriteBatch.DrawString (
					font, text, position, Color.Orange, 0,
					new Vector2(0f, 0f), 0.5f, SpriteEffects.None, 0.0f);

				spriteBatch.DrawString (
					font, text, position, Color.Blue, 0,
					new Vector2(40f, 0f), 2.0f, SpriteEffects.None, 0.0f);

				spriteBatch.DrawString (
					font, text, position, Color.HotPink, 0,
					new Vector2(0f, 40f), 0.75f, SpriteEffects.None, 0.0f);

				spriteBatch.DrawString (
					font, text, position, Color.Violet, 0,
					new Vector2(40f, 40f), 1.0f, SpriteEffects.None, 0.0f);

				spriteBatch.End ();
			});
		}

		[Test, RequiresSTA]
		public void DrawString_hullabaloo ()
		{
			DrawStringTest ("hullabaloo", "Default", (frameInfo, font, spriteBatch) => {
				spriteBatch.Begin ();
				spriteBatch.DrawString (
					font, "hullabaloo", new Vector2 (100, 150), Color.HotPink,
					MathHelper.ToRadians(15), new Vector2(20f, 50f), new Vector2(0.8f, 1.1f),
					SpriteEffects.FlipHorizontally, 0.0f);
				spriteBatch.End ();
			});
		}

		[Test, RequiresSTA]
		public void DrawString_hullabaloo2 ()
		{
			DrawStringTest ("hullabaloo2", "Default", (frameInfo, font, spriteBatch) => {
				spriteBatch.Begin ();
				spriteBatch.DrawString (
					font, "hullabaloo2", new Vector2 (100, 150), Color.Yellow,
					MathHelper.ToRadians(130), new Vector2(40f, 60f), new Vector2(1.8f, 1.1f),
					SpriteEffects.FlipVertically, 0.0f);
				spriteBatch.End ();
			});
		}

		[Test, RequiresSTA]
		public void DrawString_multiline ()
		{
			DrawStringTest ("multiline", "Default", (frameInfo, font, spriteBatch) => {
				spriteBatch.Begin ();

				var text =
@"A programming genius called Hugh
Said 'I really must see if it's true.'
So he wrote a routine
To ask 'What's it all mean?'
But the answer was still '42'.
                R Humphries, Sutton Coldfield";

				spriteBatch.DrawString (
					font, text, new Vector2 (100, 150), Color.Yellow,
					MathHelper.ToRadians(20), new Vector2(40f, 60f), new Vector2(0.9f, 0.9f),
					SpriteEffects.None, 0.0f);
				spriteBatch.End ();
			});
		}

		const string DrawStringFolderBase = "DrawString";
		private void DrawStringTest (
			string name, string fontName, Action<FrameInfo, SpriteFont, SpriteBatch> drawAction)
		{
			const int FramesToDraw = 1;
			Game.Components.Add (new ClearComponent (Game) { ColorFunction = x => Color.CornflowerBlue });
			Game.Components.Add (new SpriteFontComponent (Game, Paths.Font (fontName)) {
				DrawAction = drawAction
			});

			var folder = DrawStringFolderBase + "_" + name;
			var frameComparer = new FrameCompareComponent(
				Game, x => true,
				"frame-{0:00}.png",
				Paths.ReferenceImage(folder),
				Paths.CapturedFrame(folder)) {
					{ new PixelDeltaFrameComparer(), 1 },
				};
			Game.Components.Add (frameComparer);

			Game.ExitCondition = x => x.DrawNumber > FramesToDraw;
			Game.Run ();

			WriteFrameComparisonDiffs(
				frameComparer.Results,
				Paths.CapturedFrameDiff(folder));
			AssertFrameComparisonResultsPassed (
				frameComparer.Results, Constants.StandardRequiredSimilarity, FramesToDraw);
		}
	}
}
