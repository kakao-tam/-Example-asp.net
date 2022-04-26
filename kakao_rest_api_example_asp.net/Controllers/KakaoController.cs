using System;
using kakao_rest_api_example_asp.net.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kakao_rest_api_example_asp.net.Controllers
{
    [Route("[controller]/[action]")]
    public class KakaoController : Controller
    {
        private readonly IKakaoService _kakaoService;

        public KakaoController(IKakaoService kakaoService)
        {
            _kakaoService = kakaoService;
        }

        [Route("~/Kakao/login")]
        public RedirectResult login()
        {
            return Redirect(_kakaoService.login());
        }

        [Route("~/Kakao/login-callback")]
        public RedirectResult loginCallback(String code)
        {
            return Redirect(_kakaoService.loginCallback(code));
        }

        [Route("~/Kakao/profile")]
        public String getProfile()
        {
            return _kakaoService.getProfile();
        }

        [Route("~/Kakao/authorize")]
        public RedirectResult authorize(String scope)
        {
            return Redirect(_kakaoService.login(scope));
        }

        [Route("~/Kakao/friends")]
        public String getFriends()
        {
            return _kakaoService.getFriends();
        }

        [Route("~/Kakao/message")]
        public String message()
        {
            return _kakaoService.message();
        }
    }
}
