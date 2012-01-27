﻿#region License
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
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace MonoGame.Tests.Components {
	class PixelDeltaFrameComparer : IFrameComparer {
		public unsafe float Compare (BitmapData a, BitmapData b)
		{
			int minWidth, maxWidth, minHeight, maxHeight;

			MathUtility.MinMax (a.Width, b.Width, out minWidth, out maxWidth);
			MathUtility.MinMax (a.Height, b.Height, out minHeight, out maxHeight);

			var rect = new System.Drawing.Rectangle (0, 0, minWidth, minHeight);

			long error = 0;

			byte* pRowA = (byte*) a.Scan0;
			byte* pRowB = (byte*) b.Scan0;
			for (int y = 0; y < rect.Height; ++y) {
				PixelArgb* pPixelA = (PixelArgb*) pRowA;
				PixelArgb* pPixelB = (PixelArgb*) pRowB;
				for (int x = 0; x < rect.Width; ++x) {
					error += pPixelA->Delta (pPixelB);
					pPixelA++;
					pPixelB++;
				}

				pRowA += a.Stride;
				pRowB += b.Stride;
			}

			// Mark all out-of-bounds pixels as non-match.
			error += PixelArgb.MaxDelta * ((maxWidth * maxHeight) - (minWidth * minHeight));

			var dissimilarity = ((float) error / (float) (PixelArgb.MaxDelta * maxWidth * maxHeight));

			// Project dissimilarity to a logarithmic scale.  The
			// difference between having zero pixels wrong and one
			// pixel wrong is more significant than the difference
			// betweeen 10,000 wrong and 10,001.
			return 1.0f - (float)Math.Pow(dissimilarity, 0.5);
		}
	}
}
