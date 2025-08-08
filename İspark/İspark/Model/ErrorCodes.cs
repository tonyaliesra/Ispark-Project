namespace İspark.Model
{
    public static class ErrorCodes
    {
        // AUTH
        public const int UserNameWrong = 101; 
        public const int UserNotFound = 102;
        public const int JwtCreationFailed = 103;
        public const int TokenInvalid = 104;
        public const int UserNameEmpty = 105;
        public const int PasswordEmpty = 106;
        public const int PasswordWrong = 107; 
        public const int TokenExpired = 108;
        public const int InvalidCredentials = 109; 

        // KAMPANYA
        public const int CampaignIdInvalid = 201;
        public const int CampaignNotFound = 202;
        public const int CampaignRequiredFieldMissing = 203;
        public const int CampaignUpdateFailed = 204;
        public const int CampaignDeleteFailed = 205;

        // HABER
        public const int NewsIdInvalid = 301;
        public const int NewsNotFound = 302;
        public const int NewsRequiredFieldMissing = 303;
        public const int NewsUpdateFailed = 304;
        public const int NewsDeleteFailed = 305;

        // VERİTABANI
        public const int DbConnectionFailed = 501;
        public const int SqlQueryFailed = 502;
        public const int TransactionFailed = 503;
        public const int NullReferenceData = 504;

        // HTTP TEMELLİ
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int Forbidden = 403;
        public const int NotFound = 404;
        public const int MethodNotAllowed = 405;
        public const int InternalServerError = 500;

        // REQUEST / VALIDATION
        public const int BodyEmpty = 601;
        public const int RequiredFieldMissing = 602;
        public const int DataFormatInvalid = 603;
        public const int IdFormatInvalid = 604;
        public const int ModelStateInvalid = 605;
    }
}