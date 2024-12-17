using Microsoft.AspNetCore.Http;

namespace Common.Web.Authorization.Extensions;

public static class HttpContextExtensions
 {
     public static void AddSessionCookie(this HttpContext httpContext, Guid sessionId)
     {
         httpContext.Response.Cookies.Append(Const.SessionIdCookieKey, sessionId.ToString());
     }

     public static void AddUserId(this HttpContext httpContext, long userId)
     {
         httpContext.Items.Add(Const.UserIdKey, userId);
     }
     
     public static long GetUserId(this HttpContext httpContext)
     {
         Console.WriteLine("from common 1: " + httpContext.Items[Const.UserIdKey]);
         if (!httpContext.Items.TryGetValue(Const.UserIdKey, out var userIdObj))
         {
             throw new ArgumentException(Const.UserIdKey);
         }

         if (userIdObj is not long userId)
         {
             throw new ArgumentException(Const.UserIdKey);
         }
         Console.WriteLine("from common 2: " + userId);

         return userId;
     }
     
     public static void AddSessionId(this HttpContext httpContext, Guid sessionId)
     {
         httpContext.Items.Add(Const.SessionIdKey, sessionId);
     }
     
     public static Guid GetSessionId(this HttpContext httpContext)
     {
         if (!httpContext.Items.TryGetValue(Const.SessionIdCookieKey, out var sessionIdObj))
         {
             throw new ArgumentException(Const.SessionIdKey);
         }
         
         if (sessionIdObj is not Guid sessionId)
         {
             throw new ArgumentException(Const.UserIdKey);
         }

         return sessionId;
     }
 }