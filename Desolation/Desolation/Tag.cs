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


        public object getData()
        {
            if (tagID.Equals(TagID.Byte))
            {
                unchecked
                {
                    sbyte[] signed = (sbyte[])(Array)byteArray;
                    sbyte returnData = signed[0];
                    return returnData;
                }

            }
            else if (tagID.Equals(TagID.Short))
            {
                short returnData = BitConverter.ToInt16(byteArray, 0);
                return returnData;
                
            }
            else if (tagID.Equals(TagID.Int))
            {
                int returnData = BitConverter.ToInt32(byteArray, 0);
                return returnData;
                
            }
            else if (tagID.Equals(TagID.Long))
            {
                long returnData = BitConverter.ToInt64(byteArray, 0);
                return returnData;
            }
            else if (tagID.Equals(TagID.Float))
            {
                float returnData = BitConverter.ToSingle(byteArray, 0);
                return returnData;
            }
            else if (tagID.Equals(TagID.Double))
            {
                double returnData = BitConverter.ToDouble(byteArray, 0);
                return returnData;
            }
            else if (tagID.Equals(TagID.ByteArray))
            {
                //not done yet
                return null;
            }
            else if (tagID.Equals(TagID.String))
            {
                //not done yet
                return null;
            }
            else if (tagID.Equals(TagID.List))
            {
                //not done yet
                return null;
            }
            else if (tagID.Equals(TagID.Compound))
            {
                //no data
                return null;
            }
            else if (tagID.Equals(TagID.IntArray))
            {
                //not done
                return null;
            }
            else
            {
                //no data (end tag)
                return null;
            }
        }

        //unchecked
        //        {
        //            sbyte derpa =  -127;
        //            byte b = (byte)derpa;
        //        }
        //till sen när vi måste få ut signed byte

    }
}
