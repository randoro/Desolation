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
        object data;

        public Tag(TagID tagID, String tagIdentifier, object data)
        {
            this.tagID = tagID;
            this.tagIdentifier = tagIdentifier;
            this.data = data;

        }


        public object getData()
        {
            if (tagID.Equals(TagID.Byte))
            {
                unchecked
                {
                    sbyte[] signed = (sbyte[])(Array)data;
                    sbyte returnData = signed[0];
                    return returnData;
                }

            }
            else if (tagID.Equals(TagID.Short))
            {
                short returnData = BitConverter.ToInt16((byte[])data, 0);
                return returnData;
                
            }
            else if (tagID.Equals(TagID.Int))
            {
                int returnData = BitConverter.ToInt32((byte[])data, 0);
                return returnData;
                
            }
            else if (tagID.Equals(TagID.Long))
            {
                long returnData = BitConverter.ToInt64((byte[])data, 0);
                return returnData;
            }
            else if (tagID.Equals(TagID.Float))
            {
                float returnData = BitConverter.ToSingle((byte[])data, 0);
                return returnData;
            }
            else if (tagID.Equals(TagID.Double))
            {
                double returnData = BitConverter.ToDouble((byte[])data, 0);
                return returnData;
            }
            else if (tagID.Equals(TagID.ByteArray))
            {
                byte[] arraySizeInt = new byte[4];
                Array.Copy((byte[])data, arraySizeInt, 4);
                int arraySizeNumber = BitConverter.ToInt32(arraySizeInt, 0);
                byte[] returnData = new byte[arraySizeNumber];
                Array.Copy((byte[])data, 4, returnData, 0, arraySizeNumber);
                return returnData;
            }
            else if (tagID.Equals(TagID.String))
            {
                byte[] arraySizeShort = new byte[2];
                Array.Copy((byte[])data, arraySizeShort, 2);
                short arraySizeNumber = BitConverter.ToInt16(arraySizeShort, 0);
                byte[] returnCharData = new byte[arraySizeNumber];
                Array.Copy((byte[])data, 2, returnCharData, 0, arraySizeNumber);
                string returnData = Encoding.UTF8.GetString(returnCharData);
                return returnData; //ej testad
            }
            else if (tagID.Equals(TagID.List))
            {
                byte listTagID = ((byte[])data)[0];
                byte[] listSizeInt = new byte[4];
                Array.Copy((byte[])data, 1, listSizeInt, 0, 4); //ändra
                int listSizeNumber = BitConverter.ToInt32(listSizeInt, 0);

                IList returnList = null;

                if (listTagID.Equals(TagID.Byte))
                {
                    returnList = new List<byte>();
                    for (int i = 0; i < listSizeNumber; i++)
                    {
                        returnList.Add(((byte[])data)[5 + i]);
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
                Array.Copy((byte[])data, arraySizeInt, 4);
                int arraySizeNumber = BitConverter.ToInt32(arraySizeInt, 0);
                int[] returnData = new int[arraySizeNumber];
                Array.Copy((byte[])data, 4, returnData, 0, arraySizeNumber * 4);
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
