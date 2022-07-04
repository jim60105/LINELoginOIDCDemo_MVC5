# LINE Login OpenID Connect Demo Project (ASP.NET MVC5)

這是 LINE 的 OAuth 2.0 OpenID Connect 串接練習專案。

- C# ASP<area>.NET MVC5 (.NET Framework 4.8) 專案
- 使用 `Microsoft.Owin.Security.OpenIdConnect` 套件
- **OIDC實做的重點在 [Startup.Auth.cs](App_Start/Startup.Auth.cs)、[HomeController.cs](Controllers/HomeController.cs) !**

## Try it out!

- 在 [LINE Developer Console](https://developers.line.biz/console) 註冊新的 Channel，取得「Channel ID」、「Channel secret」
- 在 「Callback URL」填入 `https://localhost:44378/signin-oidc`
- Git clone

  ```bash
  git clone https://github.com/jim60105/LINELoginOIDCDemo_MVC5.git
  ```

- Visual Studio 啟動 `LINELoginOIDCDemo_MVC5.sln`
- 在 Web.config 填入 ClientId、ClientSecret

    ```xml
    <add key="OpenIDConnect:ClientID" value="" />
    <add key="OpenIDConnect:ClientSecret" value="" />
    ```

- 啟動但不偵錯 (Ctrl+F5)
- 訪問 `https://localhost:44378/` ，拿回來的登入資訊將顯示在 `https://localhost:44378/Home/Privacy`
