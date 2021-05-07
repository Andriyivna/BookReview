using API.Dtos;
using API.Entities;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class AvatarUrlResolver : IValueResolver<Avatar, AvatarDto, string>
    {
        private readonly IConfiguration _config;

        public AvatarUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Avatar source, AvatarDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Url))
            {
                return _config["ApiUrl"] + source.Url;
            }

            return null;
        }
    }
}
