using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace TchatAgileNoSQL.Models.Utils
{
    public static class FileUpload
    {
        public static char DirSeparator = System.IO.Path.DirectorySeparatorChar;
        public static string FilesPath = HttpContext.Current.Server.MapPath("~\\Content" + DirSeparator + "Uploads" + DirSeparator);

        public static string UploadFile(HttpPostedFileBase file)
        {
            // Verif qu'on a un fichier
            if (null == file) return "";
            // Verif qu'il contient qqc
            if (!(file.ContentLength > 0)) return "";

            string fileName = DateTime.Now.Millisecond + file.FileName;
            string fileExt = Path.GetExtension(file.FileName);

            // Verif extension
            if (null == fileExt) return "";

            // Verif que le dossier destination existe
            if (!Directory.Exists(FilesPath))
            {
                // Si non, on le crée
                Directory.CreateDirectory(FilesPath);
            }

            // Chemin de sauvegarde
            string path = FilesPath + DirSeparator + fileName;

            // Stocke le contenu du fichier
            file.SaveAs(Path.GetFullPath(path));

            // sauvegarde l'image resizée
            ResizeImage(file, 70, 70);

            // renvoie le nom du fichier
            return fileName;
        }

        public static void DeleteFile(string fileName)
        {
            // On fait rien si le contenu est vide
            if (fileName.Length == 0) return;

            // On utilise le chemin complet pour supprimer
            string path = FilesPath + DirSeparator + fileName;
            string thumbPath = FilesPath + DirSeparator + "Images" + DirSeparator + fileName;

            RemoveFile(path);
            RemoveFile(thumbPath);
        }

        private static void RemoveFile(string path)
        {
            // Verif qu'on a un fichier
            if (File.Exists(Path.GetFullPath(path)))
            {
                // Supprime le fichier
                File.Delete(Path.GetFullPath(path));
            }
        }

        public static void ResizeImage(HttpPostedFileBase file, int width, int height)
        {
            string imagesDirectory = String.Format(@"{0}{1}{2}", FilesPath, DirSeparator, "Images");

            // Verif que le dossier de destination existe
            if (!Directory.Exists(imagesDirectory))
            {
                // Si non, on le crée
                Directory.CreateDirectory(imagesDirectory);
            }

            // Chemin dans lequel on stocke le fichier
            string imagePath = String.Format(@"{0}{1}{2}", imagesDirectory, DirSeparator, file.FileName);
            // Stream pour stocker le fichier quand il sera redimensionné
            FileStream stream = new FileStream(Path.GetFullPath(imagePath), FileMode.OpenOrCreate);

            // Convertit le fichier uploadé en image
            Image OrigImage = Image.FromStream(file.InputStream);
            // Bitmap avec la taille de l'image
            Bitmap TempBitmap = new Bitmap(width, height);

            // Nouvelle image en haute qualité
            Graphics NewImage = Graphics.FromImage(TempBitmap);
            NewImage.CompositingQuality = CompositingQuality.HighQuality;
            NewImage.SmoothingMode = SmoothingMode.HighQuality;
            NewImage.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Crée un rectangle et dessine l'image
            Rectangle imageRectangle = new Rectangle(0, 0, width, height);
            NewImage.DrawImage(OrigImage, imageRectangle);

            // Sauvegarde le fichier final
            TempBitmap.Save(stream, OrigImage.RawFormat);

            // Libère les ressources
            NewImage.Dispose();
            TempBitmap.Dispose();
            OrigImage.Dispose();
            stream.Close();
            stream.Dispose();
        }

    }
}