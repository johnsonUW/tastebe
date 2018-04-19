namespace Framework.ExceptionHandling
{
    public enum ApiErrorCode
    {
        Unknown = 1,
        InternalServerError = 2,
        SessionExpired = 3,
        InvalidData = 4,
        WechatError = 5,
        WechatPaymentError = 6,
        SerializationError = 7,
        PermissionDenied = 8,
        CloverError = 9,

        UserDoesNotExist = 101,
        UserAlreadyRegistered = 102,
        UserNotVerified = 104,

        ImageDoesNotExist = 201,
        ImageFailUpload = 202,

        RestaurantDoesNotExist = 301,

        OrderDoesNotExist = 401,

        PaymentDoesNotExist = 901,
        PaymentNotMade = 902
    }
}