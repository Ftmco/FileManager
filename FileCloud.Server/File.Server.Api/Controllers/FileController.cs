using Microsoft.AspNetCore.Mvc;

namespace File.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    readonly IFileAction _fileAction;

    readonly IFileGet _fileGet;

    public FileController(IFileAction fileAction, IFileGet fileGet)
    {
        _fileAction = fileAction;
        _fileGet = fileGet;
    }

    [HttpPost("UploadFile")]
    public async Task<IActionResult> UploadFileAsync(CreateFile createFile)
    {
        CreateFileResponse? file = await _fileAction.CreateFileAsync(createFile);
        return file.Status switch
        {
            FileActionStatus.Success => Ok(Success("فایل با موفقیت ذخیره شد", "", file.File)),
            FileActionStatus.AccessDenied => Ok(Faild(401, "وارد حساب کاربری خود شوید", "")),
            FileActionStatus.Exception => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
            FileActionStatus.NotFound => Ok(Faild(404, "فایل مورد نظر یافت نشد", "")),
            FileActionStatus.DirectoryNotFound => Ok(Faild(404, "دایرکتوری مقصد یافت نشد", "")),
            _ => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", ""))
        };
    }

    [HttpGet("GetFileBase64")]
    public async Task<IActionResult> GetFileBase64Async(string token)
    {
        GetFileResponse? file = await _fileGet.GetFileBase64Async(token);
        return file.Status switch
        {
            FileActionStatus.Success => Ok(Success("", "", new { file.Base64, file.Mime, file.Extension })),
            FileActionStatus.AccessDenied => Ok(Faild(401, "وارد حساب کاربری خود شوید", "")),
            FileActionStatus.Exception => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
            FileActionStatus.NotFound => Ok(Faild(404, "فایل مورد نظر یافت نشد", "")),
            FileActionStatus.DirectoryNotFound => Ok(Faild(404, "دایرکتوری مقصد یافت نشد", "")),
            _ => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
        };
    }

    [HttpGet("Image")]
    public async Task<IActionResult> GetImageAsync(string token, int width, int heigth, string action)
    {
        GetFileResponse? image = await _fileGet.GetImageAsync(token, width, heigth, action);
        return image.Status switch
        {
            FileActionStatus.Success => File(image.ContentBytes, image.Mime),
            FileActionStatus.AccessDenied => throw new NotImplementedException(),
            FileActionStatus.Exception => Ok(ApiException()),
            FileActionStatus.NotFound => Ok(Faild(404, "فایل مورد نظر یافت نشد", "")),
            FileActionStatus.DirectoryNotFound => Ok(Faild(404, "دایرکتوری مقصد یافت نشد", "")),
            _ => Ok(ApiException())
        };
    }

    [HttpGet("Video")]
    public async Task<IActionResult> GetVideoAsync()
    {
        return Ok();
    }

    [HttpGet("StreamVideo")]
    public async Task<IActionResult> StreamVideoAsync()
    {
        return Ok();
    }
}
