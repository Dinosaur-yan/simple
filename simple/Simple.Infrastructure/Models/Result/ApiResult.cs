using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

public class ApiResult : ApiResult<object>
{
    public ApiResult()
    {
    }

    public ApiResult(ResultType status) : base(status)
    {
    }

    public ApiResult(object data) : base(data)
    {
    }

    public ApiResult(ResultType status, object data) : base(status, data)
    {
    }

    public ApiResult(ResultType status, string message) : base(status, message)
    {
    }

    public ApiResult(ResultType status, string message, object data) : base(status, message, data)
    {
    }
}

public class ApiResult<TData>
    where TData : class, new()
{
    public ApiResult() : this(ResultType.OK, "", null)
    {
    }

    public ApiResult(ResultType status) : this(status, "", null)
    {
    }

    public ApiResult(TData data) : this(ResultType.OK, "", data)
    {
    }

    public ApiResult(ResultType status, TData data) : this(status, "", data)
    {
    }

    public ApiResult(ResultType status, string message) : this(status, message, null)
    {
    }

    public ApiResult(ResultType status, string message, TData data)
    {
        Status = status;
        Message = message;
        Data = data;
    }

    public ResultType Status { get; set; }

    private string _message;

    public string Message
    {
        get
        {
            if (string.IsNullOrEmpty(_message))
            {
                Type type = Status.GetType();
                MemberInfo member = type.GetMember(Status.ToString()).FirstOrDefault();
                return member != null ? member.GetCustomAttribute<DescriptionAttribute>()?.Description : Status.ToString();
            }
            else
            {
                return _message;
            }
        }
        set { _message = value; }
    }

    public TData Data { get; set; }
}
