using System;
using System.Collections;
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
                byte[] arraySizeInt = new byte[4];
                Array.Copy(byteArray, arraySizeInt, 4);
                int arraySizeNumber = BitConverter.ToInt32(arraySizeInt, 0);
                byte[] returnData = new byte[arraySizeNumber];
                Array.Copy(byteArray, 4, returnData, 0, arraySizeNumber);
                return returnData;
            }
            else if (tagID.Equals(TagID.String))
            {
                byte[] arraySizeShort = new byte[2];
                Array.Copy(byteArray, arraySizeShort, 2);
                short arraySizeNumber = BitConverter.ToInt16(arraySizeShort, 0);
                byte[] returnCharData = new byte[arraySizeNumber];
                Array.Copy(byteArray, 2, returnCharData, 0, arraySizeNumber);
                string returnData = Encoding.UTF8.GetString(returnCharData);
                return returnData; //ej testad
            }
            else if (tagID.Equals(TagID.List))
            {
                byte listTagID = byteArray[0];
                byte[] listSizeInt = new byte[4];
                Array.Copy(byteArray, 1, listSizeInt, 0, 4);
                int listSizeNumber = BitConverter.ToInt32(listSizeInt, 0);

                IList returnList = null;

                if (listTagID.Equals(TagID.Byte))
                {
                    returnList = new List<byte>();
                    for (int i = 0; i < listSizeNumber; i++)
                    {
                        returnList.Add(byteArray[5 + i]);
                    }
                }
                return returnList; //ej testad
            }
            else if (tagID.Equals(TagID.Compound))
            {
                //no data (compound tag)
                return null;
            }
            else if (tagID.Equals(TagID.IntArray))
            {
                byte[] arraySizeInt = new byte[4];
                Array.Copy(byteArray, arraySizeInt, 4);
                int arraySizeNumber = BitConverter.ToInt32(arraySizeInt, 0);
                int[] returnData = new int[arraySizeNumber];
                Array.Copy(byteArray, 4, returnData, 0, arraySizeNumber * 4);
                return returnData; //ej testad
            }
            else
            {
                //no data (end tag)
                return null;
            }
        }


        public TagID getID()
        {
            return tagID;
        }

        public String getName()
        {
            return tagIdentifier;
        }

        //unchecked
        //        {
        //            sbyte derpa =  -127;
        //            byte b = (byte)derpa;
        //        }
        //till sen när vi måste få ut signed byte

    }
}
