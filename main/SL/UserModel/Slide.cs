﻿using NPOI.SS.Formula.Eval;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI.SL.UserModel
{
	public interface Slide<S, P> : Sheet<S,P>
		where S : Shape<S,P> 
		where P : TextParagraph<S,P, TextRun>
	{
		/**
     * @return the comment(s) for this slide
     */
		//@SuppressWarnings("java:S1452")
		List<Comment> getComments();

		MasterSheet<S,P> MasterSheet { get; set; }

		/**
		 * @return the assigned slide layout
		 *
		 * @since POI 4.0.0
		 */
		MasterSheet<S,P> SlideLayout{ get; set; }

		/**
		 * @return the slide name, defaults to "Slide[slideNumber]"
		 *
		 * @since POI 4.0.0
		 */
		string GetSlideName();

		Notes<S, P> GetNotes();
		void SetNotes(Notes<S, P> notes);

		bool GetFollowMasterBackground();
		void SetFollowMasterBackground(bool follow);

		bool GetFollowMasterColourScheme();
		void SetFollowMasterColourScheme(bool follow);

		bool GetFollowMasterObjects();
		void SetFollowMasterObjects(bool follow);

		/**
     * @return the 1-based slide no.
     */
		int GetSlideNumber();

		/**
		 * @return title of this slide or null if title is not set
		 */
		string GetTitle();

		/**
		 * In XSLF, slidenumber and date shapes aren't marked as placeholders
		 * whereas in HSLF they are activated via a HeadersFooter configuration.
		 * This method is used to generalize that handling.
		 *
		 * @param placeholderRefShape the shape which references to the placeholder
		 * @return {@code true} if the placeholder should be displayed/rendered
		 * @since POI 5.2.0
		*/
		bool GetDisplayPlaceholder(SimpleShape<S, P> placeholderRefShape);

		/**
		 * Sets the slide visibility
		 *
		 * @param hidden slide visibility, if {@code true} the slide is hidden, {@code false} shows the slide
		 *
		 * @since POI 4.0.0
		 */
		void SetHidden(bool hidden);

		/**
		 * @return the slide visibility, the slide is hidden when {@code true} - or shown when {@code false}
		 *
		 * @since POI 4.0.0
		 */
		bool IsHidden();
	}
}
