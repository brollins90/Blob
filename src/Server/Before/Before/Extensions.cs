using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blob.Contracts.Security;
using Microsoft.AspNet.Identity.Owin;

namespace Before
{

    public static class SvcConvert
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static SignInStatus SignInStatusFromDto(SignInStatusDto dto)
        {
            return ParseEnum<SignInStatus>(dto.ToString());
        }
    }
}