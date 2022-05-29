using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomApiController : ControllerBase
    {
        public IMapper _mapper { get; set; }

        public CustomApiController()
        {

        }

        public IActionResult Json(object value)
        {
            JsonResult jsonResult = new JsonResult(value);
            return jsonResult;
        }

        public TDestination MapTo<TSource, TDestination>(TSource source)
        {
            _mapper = GetMapperConfiguration<TSource, TDestination>();
            return _mapper.Map<TSource, TDestination>(source);
        }

        public List<TDestination> MapListTo<TSource, TDestination>(List<TSource> source)
        {
            _mapper = GetMapperConfiguration<TSource, TDestination>();
            return _mapper.Map<List<TSource>, List<TDestination>>(source);
        }

        /// <summary>
        /// Get the Mapper was injected if we have one and it's has the mapping. If 
        /// don't have a Mapper or don't have the mapping, we create a new one with this 
        /// mapping and return it.
        /// </summary>
        private IMapper GetMapperConfiguration<TSource, TDestination>()
        {
            if (_mapper == null || _mapper.ConfigurationProvider.FindTypeMapFor<TSource, TDestination>() == null)
            {
                var configurationExpression = new MapperConfigurationExpression();

                configurationExpression.CreateMap<TSource, TDestination>();
                configurationExpression.AddMaps(GetType().Assembly);

                var config = new MapperConfiguration(configurationExpression);
                return config.CreateMapper();
            }

            return _mapper;
        }

        public string GetDeepException(Exception ex, string templateMessage)
        {
            var e = ex;
            var exceptionMessage = $"[{ex.Message}]";
            var i = 4;
            while (e.InnerException != null && i > 0)
            {
                e = e.InnerException;
                exceptionMessage = exceptionMessage + $"[{e.Message}]";
                i -= 1;
            }

            return $"{templateMessage} {exceptionMessage}";
        }
    }
}
