using API.Dtos;
using API.Entities;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class AvatarUserUrlResolver : IValueResolver<User, UserDto, string>
    {
        private readonly IConfiguration _config;

        public AvatarUserUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(User source, UserDto destination, string destMember, ResolutionContext context)
        {
            if (source.Avatar != null && !string.IsNullOrEmpty(source.Avatar.Url))
            {
                return _config["ApiUrl"] + source.Avatar.Url;
            }

            return null;
        }
    }
}
