using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

public enum ResultType
{
    /// <summary>
    /// 请求正常处理完毕
    /// </summary>
    [Description("OK")]
    OK = 200,

    /// <summary>
    /// 请求成功处理，没有实体的主体返回
    /// </summary>
    [Description("No Content")]
    NoContent = 204,

    /// <summary>
    /// 请求报文语法错误或参数错误
    /// </summary>
    [Description("Bad Request")]
    BadRequest = 400,

    /// <summary>
    /// 需要通过HTTP认证，或认证失败
    /// </summary>
    [Description("Unauthorized")]
    Unauthorized = 401,

    /// <summary>
    /// 请求资源被拒绝
    /// </summary>
    [Description("Forbidden")]
    Forbidden = 403,

    /// <summary>
    /// 无法找到请求资源
    /// </summary>
    [Description("Not Found")]
    NotFound = 404,

    /// <summary>
    /// 先决条件失败
    /// </summary>
    [Description("Precondition Failed")]
    PreconditionFailed = 412,

    /// <summary>
    /// 服务器内部服务错误
    /// </summary>
    [Description("Internal Server Error")]
    InternalServerError = 500,

    /// <summary>
    /// 服务器超负载或停机维护
    /// </summary>
    [Description("Service Unavailable")]
    ServiceUnavailable = 503,
}

#region HttpStatueCodes

//[Description("Continue")] Continue=100,
//[Description("SwitchingProtocols")] SwitchingProtocols=101,
//[Description("OK")] OK=200,
//[Description("Created")] Created=201,
//[Description("Accepted")] Accepted=202,
//[Description("Non-AuthoritativeInformation")] Non-AuthoritativeInformation=203,
//[Description("NoContent")] NoContent=204,
//[Description("ResetContent")] ResetContent=205,
//[Description("PartialContent")] PartialContent=206,
//[Description("MultipleChoices")] MultipleChoices=300,
//[Description("MovedPermanently")] MovedPermanently=301,
//[Description("Found")] Found=302,
//[Description("SeeOther")] SeeOther=303,
//[Description("NotModified")] NotModified=304,
//[Description("UseProxy")] UseProxy=305,
//[Description("TemporaryRedirect")] TemporaryRedirect=307,
//[Description("BadRequest")] BadRequest=400,
//[Description("Unauthorized")] Unauthorized=401,
//[Description("PaymentRequired")] PaymentRequired=402,
//[Description("Forbidden")] Forbidden=403,
//[Description("NotFound")] NotFound=404,
//[Description("MethodNotAllowed")] MethodNotAllowed=405,
//[Description("NotAcceptable")] NotAcceptable=406,
//[Description("ProxyAuthenticationRequired")] ProxyAuthenticationRequired=407,
//[Description("RequestTimeout")] RequestTimeout=408,
//[Description("Conflict")] Conflict=409,
//[Description("Gone")] Gone=410,
//[Description("LengthRequired")] LengthRequired=411,
//[Description("PreconditionFailed")] PreconditionFailed=412,
//[Description("PayloadTooLarge")] PayloadTooLarge=413,
//[Description("URITooLong")] URITooLong=414,
//[Description("UnsupportedMediaType")] UnsupportedMediaType=415,
//[Description("RangeNotSatisfiable")] RangeNotSatisfiable=416,
//[Description("ExpectationFailed")] ExpectationFailed=417,
//[Description("UpgradeRequired")] UpgradeRequired=426,
//[Description("InternalServerError")] InternalServerError=500,
//[Description("NotImplemented")] NotImplemented=501,
//[Description("BadGateway")] BadGateway=502,
//[Description("ServiceUnavailable")] ServiceUnavailable=503,
//[Description("GatewayTimeout")] GatewayTimeout=504,
//[Description("HTTPVersionNotSupported")] HTTPVersionNotSupported=505,

#endregion HttpStatueCodes
