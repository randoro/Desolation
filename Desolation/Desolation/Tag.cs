using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desolation
{

    class Tag
    {
        TagID tagID;
        String tagIdentifier;
        byte[] byteArray;

        public Tag(TagID tagID, String tagIdentifier, byte[] byteArray)
        {
            this.tagID = tagID;
            this.tagIdentifier = tagIdentifier;
            this.byteArray = byteArray;

        }

    }
}
