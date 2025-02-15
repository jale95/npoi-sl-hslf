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

using NPOI.Common.UserModel;
using NPOI.HSLF.Record;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI.HSLF.Model
{
	/** 
 * Definition of the indent level of some text. Defines how many
 *  characters it applies to, and what indent level they share.
 * 
 * This is defined by the slightly confusingly named MasterTextPropRun 
 */
	public class IndentProp : GenericRecord
	{

	private int charactersCovered;
	private short indentLevel;

	/** 
     * Generate the definition of a given text indent
     */
	public IndentProp(int charactersCovered, short indentLevel)
	{
		this.charactersCovered = charactersCovered;
		this.indentLevel = indentLevel;
	}

	/** Fetch the number of characters this styling applies to */
	public int getCharactersCovered() { return charactersCovered; }

	public int getIndentLevel()
	{
		return indentLevel;
	}

	/**
     * Sets the indent level, which can be between 0 and 4
     */
	public void setIndentLevel(int indentLevel)
	{
		if (indentLevel >= TxMasterStyleAtom.MAX_INDENT ||
			indentLevel < 0)
		{
			throw new ArgumentException("Indent must be between 0 and 4");
		}
		this.indentLevel = (short)indentLevel;
	}

	/**
     * Update the size of the text that this set of properties
     *  applies to 
     */
	public void updateTextSize(int textSize)
	{
		charactersCovered = textSize;
	}

	
	public IDictionary<string, Func<T>> GetGenericProperties<T>()
	{
		return (IDictionary<string, Func<T>>)GenericRecordUtil.GetGenericProperties(
			"charactersCovered", ()=>getCharactersCovered(),
			"indentLevel", ()=>getIndentLevel()
		);
	}

		public RecordTypes GetGenericRecordType()
		{
			throw new NotImplementedException();
		}

		public IList<GenericRecord> GetGenericChildren()
		{
			throw new NotImplementedException();
		}
	}
}
