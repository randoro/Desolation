using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Desolation
{
    public class FileLoader
    {
        public String currentWorldFolder = @"mainworld\";
        public String regionFolder = @"region\";
        public FileLoader()
        {
            checkAndCreateFolder(currentWorldFolder);
            checkAndCreateFolder(currentWorldFolder+regionFolder);
            
        }

        public Region loadRegionFile(int xPosRegion, int yPosRegion)
        {
            try
            {
                FileStream fileStream = new FileStream(Globals.gamePath + currentWorldFolder + regionFolder + "x" + xPosRegion + ".y" + yPosRegion + ".region", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Delete);
                //FileStream fileStream = File.Open(Globals.gamePath + regionFolder + "x"+xPosRegion+".y"+yPosRegion+".region", FileMode.OpenOrCreate);
                return new Region(fileStream, xPosRegion, yPosRegion);
            }
            catch(IOException) {
                Console.WriteLine("region: x:" + xPosRegion+" y:"+yPosRegion+ " already loaded");
            }
            return null;
        }


        public void checkAndCreateFolder(String pathAndFolder) 
        {
            if(!Directory.Exists(Globals.gamePath + pathAndFolder)) {
                Directory.CreateDirectory(Globals.gamePath + pathAndFolder);
            }
        }


    }
}
