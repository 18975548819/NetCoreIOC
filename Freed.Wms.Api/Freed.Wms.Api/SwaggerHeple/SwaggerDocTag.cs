using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.Wms.Api.SwaggerHeple
{
    public class SwaggerDocTag : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            //添加对应的控制器描述 这个是我好不容易在issues里面翻到的
            //swaggerDoc.Tags = new List<OpenApiTag> { new OpenApiTag{ Name = "Account", Description = "登陆操作" }
            //};
            List<OpenApiTag> openApiTags = new List<OpenApiTag>();
            OpenApiTag apiTag1 = new OpenApiTag();
            apiTag1.Name = "Account";
            apiTag1.Description = "登陆操作";
            openApiTags.Add(apiTag1);

            swaggerDoc.Tags = openApiTags;

        }
    }
}
