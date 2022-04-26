using kakao_rest_api_example_asp.net.Common;
using Microsoft.AspNetCore.Http;
using System;

namespace kakao_rest_api_example_asp.net.Service
{
    public class KakaoService : IKakaoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        private readonly IHttpCallService _httpCallService;
        public KakaoService(IHttpCallService httpCallService, IHttpContextAccessor httpContextAccessor)
        {
            _httpCallService = httpCallService;
            _httpContextAccessor = httpContextAccessor;
        }

        private String REST_API_KEY = "여기에 REST_API_KEY를 입력하세요";

        private String REDIRECT_URI = "http://localhost:8888/Kakao/login-callback";

        private String AUTHORIZE_URI = "https://kauth.kakao.com/oauth/authorize";

        public String TOKEN_URI = "https://kauth.kakao.com/oauth/token";

        private String CLIENT_SECRET = "";

        private String KAKAO_API_HOST = "https://kapi.kakao.com";

        
        public String login()
        {
            return login("");
        }

        public String login(String scope)
        {
            String uri = AUTHORIZE_URI + "?redirect_uri=" + REDIRECT_URI + "&response_type=code&client_id=" + REST_API_KEY;
            if (!String.IsNullOrEmpty(scope)) uri += "&scope=" + scope;
            return uri;
        }

        public String loginCallback(String code)
        {
            String param = "grant_type=authorization_code&client_id=" + REST_API_KEY + "&redirect_uri=" + Uri.EscapeDataString(REDIRECT_URI) + "&client_secret=" + CLIENT_SECRET + "&code=" + code;
            String rtn = _httpCallService.Call(Const.POST, TOKEN_URI, Const.EMPTY, param);

            _session.SetString("token", Trans.token(rtn));
            return "/";
        }

        public String getProfile()
        {
            String uri = KAKAO_API_HOST + "/v2/user/me";
            return _httpCallService.CallwithToken(Const.GET, uri, _session.GetString("token"));
        }

        public String getFriends()
        {
            String uri = KAKAO_API_HOST + "/v1/api/talk/friends";
            return _httpCallService.CallwithToken(Const.GET, uri, _session.GetString("token"));
        }

        public String message()
        {
            String uri = KAKAO_API_HOST + "/v2/api/talk/memo/default/send";
            return _httpCallService.CallwithToken(Const.POST, uri, _session.GetString("token"), Trans.default_msg_param);
        }
    }

}
