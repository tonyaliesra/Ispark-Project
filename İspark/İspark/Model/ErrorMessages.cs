namespace İspark.Model
{
    public static class ErrorMessages
    {
        public static readonly Dictionary<int, string> Messages = new()
        {
            // AUTH
            { ErrorCodes.UserNameWrong, "Incorrect username" },
            { ErrorCodes.UserNotFound, "User not found in the system" },
            { ErrorCodes.JwtCreationFailed, "Failed to create JWT token" },
            { ErrorCodes.TokenInvalid, "Invalid token" }, 
            { ErrorCodes.UserNameEmpty, "Username field is empty" },
            { ErrorCodes.PasswordEmpty, "Password field is empty" },
            { ErrorCodes.PasswordWrong, "Incorrect password" },
            { ErrorCodes.TokenExpired, "Token has expired" },
            { ErrorCodes.InvalidCredentials, "Invalid username or password" }, 

            // CAMPAIGN
            { ErrorCodes.CampaignIdInvalid, "Invalid campaign ID" },
            { ErrorCodes.CampaignNotFound, "Campaign not found" },
            { ErrorCodes.CampaignRequiredFieldMissing, "Required campaign field is missing" },
            { ErrorCodes.CampaignUpdateFailed, "Failed to update campaign" },
            { ErrorCodes.CampaignDeleteFailed, "Failed to delete campaign" },

            // NEWS
            { ErrorCodes.NewsIdInvalid, "Invalid news ID" },
            { ErrorCodes.NewsNotFound, "News not found" },
            { ErrorCodes.NewsRequiredFieldMissing, "Required news field is missing" },
            { ErrorCodes.NewsUpdateFailed, "Failed to update news" },
            { ErrorCodes.NewsDeleteFailed, "Failed to delete news" },

            // DATABASE
            { ErrorCodes.DbConnectionFailed, "Failed to connect to the database" },
            { ErrorCodes.SqlQueryFailed, "SQL query failed" },
            { ErrorCodes.TransactionFailed, "Transaction failed" },
            { ErrorCodes.NullReferenceData, "Null reference: data could not be retrieved" },

            // HTTP BASED
            { ErrorCodes.BadRequest, "Invalid request (Bad Request)" },
            { ErrorCodes.Unauthorized, "Unauthorized access" },
            { ErrorCodes.Forbidden, "Access forbidden" },
            { ErrorCodes.NotFound, "Resource or endpoint not found" },
            { ErrorCodes.MethodNotAllowed, "HTTP method not supported" },
            { ErrorCodes.InternalServerError, "Internal server error" },

            // REQUEST / VALIDATION
            { ErrorCodes.BodyEmpty, "Request body is empty" },
            { ErrorCodes.RequiredFieldMissing, "Required field(s) missing" },
            { ErrorCodes.DataFormatInvalid, "Invalid data format" },
            { ErrorCodes.IdFormatInvalid, "Invalid ID format (e.g. string instead of int)" },
            { ErrorCodes.ModelStateInvalid, "ModelState validation failed" }
        };
    }
}