﻿using File.Abstraction;
using File.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace File.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DirectoryController : ControllerBase
{
    readonly IDirectoryAction _directoryAction;

    public DirectoryController(IDirectoryAction directoryAction)
    {
        _directoryAction = directoryAction;
    }

    [HttpPost("UpsertDirectory")]
    public async Task<IActionResult> UpsertDirectoryAsync(UpsertDirectory upsert)
    {
        UpsertDirectoryResponse? directory = await _directoryAction.UpsertAsync(upsert, HttpContext);
        return directory.Status switch
        {
            DirecttoryActionStatus.Success => Ok(Success("دایرکتوری با موفقیت ثبت شد", "", directory.Directory)),
            DirecttoryActionStatus.AccessDenied => Ok(Faild(403, "شما به این بخش دسترسی ندارید لطفا وارد حساب کاربری شوید", "")),
            DirecttoryActionStatus.Exception => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
            DirecttoryActionStatus.ObjectNotFound => Ok(Faild(404, "دایرکتوری مورد نظر یافت نشد", "")),
            _ => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
        };
    }
}
