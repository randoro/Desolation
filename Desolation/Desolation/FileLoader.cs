using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Desolation
{
    class FileLoader
    {

        const String regionFolder = @"region\";
        public FileLoader()
        {
            checkAndCreateFolder(regionFolder);
            
        }

        public Region loadRegionFile(int xPosRegion, int yPosRegion)
        {
            try
            {
                FileStream fileStream = File.Open(Globals.gamePath + regionFolder + "x"+xPosRegion+".y"+yPosRegion+".region", FileMode.OpenOrCreate);
                return new Region(fileStream, xPosRegion, yPosRegion);
            }
            catch(OutOfMemoryException) {
                Console.WriteLine("region already loaded");
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
