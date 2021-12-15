using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IFileService
    {
        IDataResult<FileDetailDto> Add(IFormFile file);
        IDataResult<FileDetailDto> Update(string oldFilePath, IFormFile file);
        IResult Delete(string filePath);
    }
}
