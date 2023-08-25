using System.Net;

namespace TCBApp.WebApi.Types;

public delegate Task ExecutorDelegate(HttpContext context, NextHandlerDelegate next);