using System.IO;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public class FileLogic
    {
        public void DeleteArchivos(DirectoryInfo directoryInfo)
        {
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                DeleteArchivos(directory);
                directory.Delete();
            }
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
            }
        }
    }
}