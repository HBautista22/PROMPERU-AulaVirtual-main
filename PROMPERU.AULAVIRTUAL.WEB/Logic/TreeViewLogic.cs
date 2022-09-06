using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI.WebControls;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public class Nodo
    {
        public Boolean isFolder { get; set; }
        public List<Nodo> children { get; set; }
        public String title { get; set; }
        public Boolean select { get; set; }
        public String Path { get; set; }
     }
    public class TreeViewLogic
    {
        public TreeViewLogic()
        {
        }

        public Nodo CreateTreeView(DirectoryInfo directoryInfo, Nodo nodo, String selectedFile, String path, Int32 nivel)
        {
            if (nodo == null)
            {
                nodo = new Nodo();
                nodo.title = nivel == 0 ? "Raíz" : directoryInfo.Name;
                nodo.isFolder = true;
                nodo.children = new List<Nodo>();
            }

            //var directoryNode = new TreeNode(directoryInfo.Name);

            foreach (var directory in directoryInfo.GetDirectories())
            {
                nodo.children.Add(CreateTreeView(directory, null, selectedFile, nivel == 0 ? "" : path + "/" + directoryInfo.Name, nivel + 1));                
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                
                Nodo hijo = new Nodo();
                hijo.title = file.Name;
                hijo.isFolder = false;
                hijo.Path = path + "/" + directoryInfo.Name + "/" + file.Name;
                if (selectedFile.Contains(file.Name) && !String.IsNullOrEmpty(selectedFile))
                    hijo.select = true;
                nodo.children.Add(hijo);
                
            }

            return nodo;
        }

        
    }
}