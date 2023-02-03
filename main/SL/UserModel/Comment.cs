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
using System;

namespace NPOI.SL.UserModel
{
	public interface Comment
	{
		/**
     * Get the Author of this comment
     */
		string GetAuthor();

		/**
		 * Set the Author of this comment.
		 * if the author wasn't registered before, create a new entry
		 */
		void SetAuthor(string author);

		/**
		 * Get the Author's Initials of this comment
		 */
		string GetAuthorInitials();

		/**
		 * Set the Author's Initials of this comment.
		 * if the author wasn't registered before via {@link #setAuthor(String)}
		 * this has no effect
		 */
		void SetAuthorInitials(string initials);

		/**
		 * Get the text of this comment
		 */
		string GetText();

		/**
		 * Set the text of this comment
		 */
		void SetText(string text);

		/**
		 * Gets the date the comment was made.
		 * @return the comment date.
		 */
		DateTime GetDate();

		/**
		 * Sets the date the comment was made.
		 * @param date the comment date.
		 */
		void SetDate(DateTime date);

		/**
		 * Gets the offset of the comment on the page.
		 * @return the offset.
		 */
		Point2D GetOffset();

		/**
		 * Sets the offset of the comment on the page.
		 * @param offset the offset.
		 */
		void SetOffset(Point2D offset);
	}
}