using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class ApiController : Controller
    {
        [HttpPost]
        public JsonResult UploadImage()
        {
            try
            {
                if (Request.Files.Count == 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Solicitud incorrecta."
                    });
                }

                HttpPostedFileBase file = Request.Files[0];

                int size = file.ContentLength;
                string filename = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                string ext = System.IO.Path.GetExtension(file.FileName);

                string[] validExt = { ".jpg", ".png" };

                if (!Array.Exists(validExt, element => element == ext.ToLower().Trim()))
                {
                    return Json(new
                    {
                        status = false,
                        message = "La imagen no es válida."
                    });
                }

                if (size > 104857600)
                {
                    return Json(new
                    {
                        success = false,
                        message = "El tamaño excede el limite permitido."
                    });
                }

                var ruta = "~/Content/uploads/image/" + Guid.NewGuid().ToString() + "_" + file.FileName;
                string clientPathFile = ruta;
                string serverPathFile = Server.MapPath(ruta);

                if (System.IO.File.Exists(serverPathFile))
                {
                    return Json(new
                    {
                        success = false,
                        message = "El archivo ya existe. Puede probar renombrando el archivo."
                    });
                }

                file.SaveAs(serverPathFile);

                return Json(new
                {
                    status = true,
                    message = "Imagen cargada éxitosamente.",
                    content = Json(new
                    {
                        clientPathFile
                    })
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        public JsonResult UploadTask()
        {
            try
            {
                if (Request.Files.Count == 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Solicitud incorrecta."
                    });
                }

                HttpPostedFileBase file = Request.Files[0];

                int size = file.ContentLength;
                string filename = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                string ext = System.IO.Path.GetExtension(file.FileName);

                string[] validExt = { ".doc", ".docx", ".ppt", ".pptx", ".xls", ".xlsx", ".zip", ".rar", ".pdf" };

                if (!Array.Exists(validExt, element => element == ext.ToLower().Trim()))
                {
                    return Json(new
                    {
                        status = false,
                        message = "El archivo no es válida."
                    });
                }

                if (size > 104857600)
                {
                    return Json(new
                    {
                        success = false,
                        message = "El tamaño excede el limite permitido."
                    });
                }

                string ruta = "~/Content/uploads/task/" + Guid.NewGuid() + "_" + file.FileName;
                string clientPathFile = ruta;
                string serverPathFile = Server.MapPath(ruta);

                if (System.IO.File.Exists(serverPathFile))
                {
                    return Json(new
                    {
                        success = false,
                        message = "El archivo ya existe. Puede probar renombrando el archivo."
                    });
                }

                file.SaveAs(serverPathFile);

                return Json(new
                {
                    status = true,
                    message = "Archivo cargado éxitosamente.",
                    content = Json(new
                    {
                        clientPathFile
                    })
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        public JsonResult UploadImagePath()
        {
            try
            {
                if (Request.Files.Count == 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Solicitud incorrecta."
                    });
                }

                HttpPostedFileBase file = Request.Files[0];
                var path = Request.Form.Get("path");

                int size = file.ContentLength;
                string filename = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                string ext = System.IO.Path.GetExtension(file.FileName);

                string[] validExt = { ".jpg", ".png", ".svg" };

                if (!Array.Exists(validExt, element => element == ext.ToLower().Trim()))
                {
                    return Json(new
                    {
                        status = false,
                        message = "La imagen no es válida."
                    });
                }

                if (size > 104857600)
                {
                    return Json(new
                    {
                        success = false,
                        message = "El tamaño excede el limite permitido."
                    });
                }

                var ruta = "~/Content/" + path + "/" + Guid.NewGuid().ToString() + "_" + file.FileName;
                string clientPathFile = ruta;
                string serverPathFile = Server.MapPath(ruta);

                if (System.IO.File.Exists(serverPathFile))
                {
                    return Json(new
                    {
                        success = false,
                        message = "El archivo ya existe. Puede probar renombrando el archivo."
                    });
                }

                file.SaveAs(serverPathFile);

                return Json(new
                {
                    status = true,
                    message = "Imagen cargada éxitosamente.",
                    content = Json(new
                    {
                        clientPathFile
                    })
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        public JsonResult UploadFilePath()
        {
            try
            {
                if (Request.Files.Count == 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "Solicitud incorrecta."
                    });
                }

                HttpPostedFileBase file = Request.Files[0];
                var path = Request.Form.Get("path");

                int size = file.ContentLength;
                string filename = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                string ext = System.IO.Path.GetExtension(file.FileName);

                string[] validExt = { ".doc", ".docx", ".ppt", ".pptx", ".xls", ".xlsx", ".zip", ".rar", ".pdf" };

                if (!Array.Exists(validExt, element => element == ext.ToLower().Trim()))
                {
                    return Json(new
                    {
                        status = false,
                        message = "El archivo no es válida."
                    });
                }

                if (size > 104857600)
                {
                    return Json(new
                    {
                        success = false,
                        message = "El tamaño excede el limite permitido."
                    });
                }

                var ruta = "~/Content/" + path + "/" + Guid.NewGuid().ToString() + "_" + file.FileName;
                string clientPathFile = ruta;
                string serverPathFile = Server.MapPath(ruta);

                if (System.IO.File.Exists(serverPathFile))
                {
                    return Json(new
                    {
                        success = false,
                        message = "El archivo ya existe. Puede probar renombrando el archivo."
                    });
                }

                file.SaveAs(serverPathFile);

                return Json(new
                {
                    status = true,
                    message = "Archivo cargado éxitosamente.",
                    content = Json(new
                    {
                        clientPathFile
                    })
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }
    }
}