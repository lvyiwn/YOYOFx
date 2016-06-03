﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOYO.Mvc.Route;
using YOYO.Owin;
using YOYO.Owin.Pipeline;

namespace YOYO.Mvc.Owin
{
    public class YOYOFxOwinMiddleware : PipelineComponent
    {

        public override async Task Invoke(IOwinContext context, AppFunc next)
        {
            IRouteBuilder builder = RouteBuilder.Builder;
            var route = builder.Resolve(context.Request);
            if (route != null)
            {
                IRouteHandler handler = RouteHandlerFactory.Default.CreateRouteHandler(context, route);

                try
                {
                    await handler.ProcessAsync(context, context.CancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {

                }
            }
            if(next!=null)
                await next(context.Environment);
        }

    }
}
