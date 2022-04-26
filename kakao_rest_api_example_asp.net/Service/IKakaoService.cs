using Microsoft.AspNetCore.Http;
using System;

namespace kakao_rest_api_example_asp.net.Service
{
    public interface IKakaoService
    {
        public String login();
        public String login(String scope);
        public String loginCallback(String code);
        public String getProfile();
        public String getFriends();
        public String message();
    }
}