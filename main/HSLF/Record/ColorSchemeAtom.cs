﻿/* ====================================================================
   Licensed to the Apache Software Foundation (ASF) under one or more
   contributor license agreements.  See the NOTICE file distributed with
   this work for additional information regarding copyright ownership.
   The ASF licenses this file to You under the Apache License, Version 2.0
   (the "License"); you may not use this file except in compliance with
   the License.  You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
==================================================================== */

using NPOI.HSLF.Exceptions;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace NPOI.HSLF.Record
{
	public class ColorSchemeAtom: RecordAtom
	{
		private static long _type = 2032L;

		private byte[] _header;
		private int backgroundColourRGB;
		private int textAndLinesColourRGB;
		private int shadowsColourRGB;
		private int titleTextColourRGB;
		private int fillsColourRGB;
		private int accentColourRGB;
		private int accentAndHyperlinkColourRGB;
		private int accentAndFollowingHyperlinkColourRGB;

		/** Fetch the RGB value for Background Colour */
		public int GetBackgroundColourRGB() { return backgroundColourRGB; }
		/** Set the RGB value for Background Colour */
		public void SetBackgroundColourRGB(int rgb) { backgroundColourRGB = rgb; }

		/** Fetch the RGB value for Text And Lines Colour */
		public int GetTextAndLinesColourRGB() { return textAndLinesColourRGB; }
		/** Set the RGB value for Text And Lines Colour */
		public void SetTextAndLinesColourRGB(int rgb) { textAndLinesColourRGB = rgb; }

		/** Fetch the RGB value for Shadows Colour */
		public int GetShadowsColourRGB() { return shadowsColourRGB; }
		/** Set the RGB value for Shadows Colour */
		public void SetShadowsColourRGB(int rgb) { shadowsColourRGB = rgb; }

		/** Fetch the RGB value for Title Text Colour */
		public int GetTitleTextColourRGB() { return titleTextColourRGB; }
		/** Set the RGB value for Title Text Colour */
		public void SetTitleTextColourRGB(int rgb) { titleTextColourRGB = rgb; }

		/** Fetch the RGB value for Fills Colour */
		public int GetFillsColourRGB() { return fillsColourRGB; }
		/** Set the RGB value for Fills Colour */
		public void SetFillsColourRGB(int rgb) { fillsColourRGB = rgb; }

		/** Fetch the RGB value for Accent Colour */
		public int GetAccentColourRGB() { return accentColourRGB; }
		/** Set the RGB value for Accent Colour */
		public void SetAccentColourRGB(int rgb) { accentColourRGB = rgb; }

		/** Fetch the RGB value for Accent And Hyperlink Colour */
		public int GetAccentAndHyperlinkColourRGB()
		{ return accentAndHyperlinkColourRGB; }
		/** Set the RGB value for Accent And Hyperlink Colour */
		public void SetAccentAndHyperlinkColourRGB(int rgb)
		{ accentAndHyperlinkColourRGB = rgb; }

		/** Fetch the RGB value for Accent And Following Hyperlink Colour */
		public int GetAccentAndFollowingHyperlinkColourRGB()
		{ return accentAndFollowingHyperlinkColourRGB; }
		/** Set the RGB value for Accent And Following Hyperlink Colour */
		public void SetAccentAndFollowingHyperlinkColourRGB(int rgb)
		{ accentAndFollowingHyperlinkColourRGB = rgb; }

		/* *************** record code follows ********************** */

		/**
		 * For the Colour Scheme (ColorSchem) Atom
		 */
		protected ColorSchemeAtom(byte[] source, int start, int len)
		{
			// Sanity Checking - we're always 40 bytes long
			if (len < 40)
			{
				if (source.Length - start < 40)
				{
					throw new HSLFException("Not enough data to form a ColorSchemeAtom (always 40 bytes long) - found " + (source.Length - start));
				}
			}

			// Get the header
			_header = Arrays.CopyOfRange(source, start, start + 8);

			// Grab the rgb values
			backgroundColourRGB = LittleEndian.GetInt(source, start + 8);
			textAndLinesColourRGB = LittleEndian.GetInt(source, start + 8 + 4);
			shadowsColourRGB = LittleEndian.GetInt(source, start + 8 + 8);
			titleTextColourRGB = LittleEndian.GetInt(source, start + 8 + 12);
			fillsColourRGB = LittleEndian.GetInt(source, start + 8 + 16);
			accentColourRGB = LittleEndian.GetInt(source, start + 8 + 20);
			accentAndHyperlinkColourRGB = LittleEndian.GetInt(source, start + 8 + 24);
			accentAndFollowingHyperlinkColourRGB = LittleEndian.GetInt(source, start + 8 + 28);
		}

		/**
		 * Create a new ColorSchemeAtom, to go with a new Slide
		 */
		public ColorSchemeAtom()
		{
			_header = new byte[8];
			LittleEndian.PutUShort(_header, 0, 16);
			LittleEndian.PutUShort(_header, 2, (int)_type);
			LittleEndian.PutInt(_header, 4, 32);

			// Setup the default rgb values
			backgroundColourRGB = 16777215;
			textAndLinesColourRGB = 0;
			shadowsColourRGB = 8421504;
			titleTextColourRGB = 0;
			fillsColourRGB = 10079232;
			accentColourRGB = 13382451;
			accentAndHyperlinkColourRGB = 16764108;
			accentAndFollowingHyperlinkColourRGB = 11711154;
		}


		/**
		 * We are of type 3999
		 */
		//@Override
	public override long GetRecordType() { return _type; }


		/**
		 * Convert from an integer RGB value to individual R, G, B 0-255 values
		 */
		public static byte[] SplitRGB(int rgb)
		{
			byte[] ret = new byte[3];

			// Serialise to bytes, then grab the right ones out
			MemoryStream baos = new MemoryStream();
			try
			{
				WriteLittleEndian(rgb, (OutputStream)baos);
			}
			catch (IOException ie)
			{
				// Should never happen
				throw new HSLFException(ie);
			}
			byte[] b = baos.ToArray();
			Array.Copy(b, 0, ret, 0, 3);

			return ret;
		}

		/**
		 * Convert from split R, G, B values to an integer RGB value
		 */
		public static int JoinRGB(byte r, byte g, byte b)
		{
			return JoinRGB(new byte[] { r, g, b });
		}
		/**
		 * Convert from split R, G, B values to an integer RGB value
		 */
		public static int JoinRGB(byte[] rgb)
		{
			if (rgb.Length != 3)
			{
				throw new HSLFException("joinRGB accepts a byte array of 3 values, but got one of " + rgb.Length + " values!");
			}
			byte[] with_zero = new byte[4];
			Array.Copy(rgb, 0, with_zero, 0, 3);
			with_zero[3] = 0;
			return LittleEndian.GetInt(with_zero, 0);
		}


		/**
		 * Write the contents of the record back, so it can be written
		 *  to disk
		 */
		//@Override
	public override void WriteOut(OutputStream _out)
		{
        // Header - size or type unchanged
        _out.Write(_header);

        // Write out the rgb values
        WriteLittleEndian(backgroundColourRGB, _out);
		WriteLittleEndian(textAndLinesColourRGB, _out);
		WriteLittleEndian(shadowsColourRGB, _out);
		WriteLittleEndian(titleTextColourRGB, _out);
		WriteLittleEndian(fillsColourRGB, _out);
		WriteLittleEndian(accentColourRGB, _out);
		WriteLittleEndian(accentAndHyperlinkColourRGB, _out);
		WriteLittleEndian(accentAndFollowingHyperlinkColourRGB, _out);
	}

	/**
     * Returns color by its index
     *
     * @param idx 0-based color index
     * @return color by its index
     */
	public int GetColor(int idx)
	{
		int[] clr = {backgroundColourRGB, textAndLinesColourRGB, shadowsColourRGB, titleTextColourRGB,
				fillsColourRGB, accentColourRGB, accentAndHyperlinkColourRGB, accentAndFollowingHyperlinkColourRGB};
		return clr[idx];
	}

	//@Override
	public override IDictionary<string, Func<T>> GetGenericProperties<T>()
	{
		return	(IDictionary<string, Func<T>>)GenericRecordUtil.GetGenericProperties(
			"backgroundColourRGB", () => GetBackgroundColourRGB(),
			"textAndLinesColourRGB", () => GetTextAndLinesColourRGB(),
			"shadowsColourRGB", () => GetShadowsColourRGB(),
			"titleTextColourRGB", () => GetTitleTextColourRGB(),
			"fillsColourRGB", () => GetFillsColourRGB(),
			"accentColourRGB", () => GetAccentColourRGB(),
			"accentAndHyperlinkColourRGB", ()=>GetAccentAndHyperlinkColourRGB(),
			"accentAndFollowingHyperlinkColourRGB", () => GetAccentAndFollowingHyperlinkColourRGB()
			);
	}
}
}