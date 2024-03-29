﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Tools
{
    public static class ImageUploader
    {
        //Geriye string deger metodumuz resmin yolunu döndürecek veya resim yükleme ile ilgili bir sorun varsa onun kodunu döndürecek

        public static string UploadImage(string serverPath,HttpPostedFileBase file,string name)
        {
            if (file!=null)
            {
                Guid uniqueName = Guid.NewGuid();

                string[] fileArray = file.FileName.Split('.');

                string extension = fileArray[fileArray.Length - 1].ToLower(); //dosya uzantısını yakalayarak kücük harflere cevirdik

                string fileName = $"{uniqueName}.{name}.{extension}";

                if (extension=="jpg"||extension=="gif"|| extension=="png")
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath+fileName)))
                    {
                        return "1";
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                        file.SaveAs(filePath);
                        return serverPath + fileName;
                    }

                }
                else
                {
                    return "2";
                }

            }
            else
            {
                return "3";
            }
        }
    }
}