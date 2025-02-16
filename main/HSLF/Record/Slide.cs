/* ====================================================================
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
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NPOI.HSLF.Record
{

    /**
     * Master container for Slides. There is one of these for every slide,
     *  and they have certain specific children
     */

    public class Slide : SheetContainer
{
    private byte[] _header;
    private static long _type = 1006L;

    // Links to our more interesting children
    private SlideAtom slideAtom;
    private PPDrawing ppDrawing;
    private ColorSchemeAtom _colorScheme;

    /**
     * Returns the SlideAtom of this Slide
     */
    public SlideAtom getSlideAtom() { return slideAtom; }

    /**
     * Returns the PPDrawing of this Slide, which has all the
     *  interesting data in it
     */
    public PPDrawing getPPDrawing() { return ppDrawing; }


    /**
     * Set things up, and find our more interesting children
     */
    protected Slide(byte[] source, int start, int len) {
        // Grab the header
        _header = Arrays.copyOfRange(source, start, start+8);

        // Find our children
        _children = Record.findChildRecords(source,start+8,len-8);

        // Find the interesting ones in there
        for (Record child : _children) {
            if (child instanceof SlideAtom) {
                slideAtom = (SlideAtom) child;
            } else if (child instanceof PPDrawing) {
                ppDrawing = (PPDrawing) child;
            }

            if (ppDrawing != null && child instanceof ColorSchemeAtom) {
                _colorScheme = (ColorSchemeAtom) child;
            }
        }
    }

    /**
     * Create a new, empty, Slide, along with its required
     *  child records.
     */
    public Slide(){
        _header = new byte[8];
        LittleEndian.putUShort(_header, 0, 15);
        LittleEndian.putUShort(_header, 2, (int)_type);
        LittleEndian.putInt(_header, 4, 0);

        slideAtom = new SlideAtom();
        ppDrawing = new PPDrawing();

        ColorSchemeAtom colorAtom = new ColorSchemeAtom();

        _children = new org.apache.poi.hslf.record.Record[] {
            slideAtom,
            ppDrawing,
            colorAtom
        };
    }

    /**
     * We are of type 1006
     */
    public long getRecordType() { return _type; }

    /**
     * Write the contents of the record back, so it can be written
     *  to disk
     */
    public void writeOut(OutputStream out) throws IOException {
        writeOut(_header[0],_header[1],_type,_children,out);
    }

    public ColorSchemeAtom getColorScheme(){
        return _colorScheme;
    }
}
