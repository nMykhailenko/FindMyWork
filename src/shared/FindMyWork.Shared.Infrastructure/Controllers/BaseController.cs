﻿using Microsoft.AspNetCore.Mvc;

namespace FindMyWork.Shared.Infrastructure.Controllers;

[ApiController]
[Route(BaseApiPath + "/[controller]")]
public abstract class BaseController : Controller
{
    protected const string BaseApiPath = "api/v{version:apiVersion}";
}