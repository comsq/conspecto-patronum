using ConspectoPatronum.Domain;
using System.Collections.Generic;
using System.IO.Compression;

namespace ConspectoPatronum.Core.Services
{
    public interface IImagesService : IService<Image>
    {
        IList<Image> GetBySubject(string title);

        Image GetByFileName(string fileName);

        // ZipFile GetZipFileBySubject(string title); or something like that
    }
}
