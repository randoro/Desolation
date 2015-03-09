using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Desolation
{
    static class TagTranslator
    {

        public static Chunk getUnloadedChunk(Region regionFile)
        {
            return null;

            //regionFile.fileStream.ReadByte();
        }


        public static void readTag(FileStream fileStream)
        {
            TagID tagID = (TagID)fileStream.ReadByte();

            if (tagID.Equals(TagID.Compound))
            {

            }

        }
    }
}
